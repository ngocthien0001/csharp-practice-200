using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class ProblemBank
{
    [JsonPropertyName("problems")]
    public List<Problem> Problems { get; set; } = new();
}

public sealed class Problem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("level")]
    public string Level { get; set; } = "";

    [JsonPropertyName("topic")]
    public string Topic { get; set; } = "";

    [JsonPropertyName("tests")]
    public List<TestCase> Tests { get; set; } = new();
}

public sealed class TestCase
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("input")]
    public string Input { get; set; } = "";

    [JsonPropertyName("output")]
    public string Output { get; set; } = "";

    [JsonPropertyName("isSample")]
    public bool IsSample { get; set; }
}

public static class Program
{
    private const int TimeoutMs = 3000;

    public static int Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        string root = FindRoot();
        ProblemBank bank = LoadProblems(root);

        if (args.Length == 0 || args[0].Equals("help", StringComparison.OrdinalIgnoreCase))
        {
            PrintHelp();
            return 0;
        }

        if (args[0].Equals("list", StringComparison.OrdinalIgnoreCase))
        {
            PrintList(bank);
            return 0;
        }

        if (args[0].Equals("--all", StringComparison.OrdinalIgnoreCase) || args[0].Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            bool sampleOnlyAll = args.Any(a => a.Equals("--sample", StringComparison.OrdinalIgnoreCase));
            int passed = 0;
            foreach (Problem problem in bank.Problems)
            {
                bool ok = JudgeProblem(root, problem, sampleOnlyAll, quiet: true);
                if (ok) passed++;
                Console.WriteLine($"{problem.Id}: {(ok ? "PASS" : "FAIL")}");
            }
            Console.WriteLine($"\nSummary: {passed}/{bank.Problems.Count} problems passed.");
            return passed == bank.Problems.Count ? 0 : 1;
        }

        string problemId = NormalizeProblemId(args[0]);
        bool sampleOnly = args.Any(a => a.Equals("--sample", StringComparison.OrdinalIgnoreCase));
        Problem? selected = bank.Problems.FirstOrDefault(p => p.Id.Equals(problemId, StringComparison.OrdinalIgnoreCase));
        if (selected == null)
        {
            Console.WriteLine($"Không tìm thấy bài {problemId}.");
            return 1;
        }

        bool result = JudgeProblem(root, selected, sampleOnly, quiet: false);
        return result ? 0 : 1;
    }

    private static void PrintHelp()
    {
        Console.WriteLine("CSharp Practice 200 - Offline Judge");
        Console.WriteLine();
        Console.WriteLine("Lệnh dùng:");
        Console.WriteLine("  dotnet run --project src/CSharpPractice.Judge -- list");
        Console.WriteLine("  dotnet run --project src/CSharpPractice.Judge -- P001");
        Console.WriteLine("  dotnet run --project src/CSharpPractice.Judge -- P001 --sample");
        Console.WriteLine("  dotnet run --project src/CSharpPractice.Judge -- --all");
    }

    private static void PrintList(ProblemBank bank)
    {
        foreach (Problem p in bank.Problems)
        {
            Console.WriteLine($"{p.Id} | {p.Level,-10} | {p.Topic,-22} | {p.Title}");
        }
    }

    private static bool JudgeProblem(string root, Problem problem, bool sampleOnly, bool quiet)
    {
        string sourcePath = Path.Combine(root, "solutions", problem.Id, "Program.cs");
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine($"Không tìm thấy file code: {sourcePath}");
            return false;
        }

        string tempRoot = Path.Combine(root, ".practice", "tmp", problem.Id);
        if (Directory.Exists(tempRoot)) Directory.Delete(tempRoot, recursive: true);
        Directory.CreateDirectory(tempRoot);

        string tempCode = Path.Combine(tempRoot, "Program.cs");
        string tempProject = Path.Combine(tempRoot, "PracticeSubmission.csproj");
        File.Copy(sourcePath, tempCode, overwrite: true);
        File.WriteAllText(tempProject, """
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>disable</Nullable>
  </PropertyGroup>
</Project>
""");

        if (!quiet)
        {
            Console.WriteLine($"Đang chấm {problem.Id} - {problem.Title}");
            Console.WriteLine(new string('-', 60));
        }

        ProcessResult build = RunProcess("dotnet", $"build \"{tempProject}\" -c Release -v q", tempRoot, input: null, timeoutMs: 15000);
        if (build.ExitCode != 0)
        {
            Console.WriteLine("Compile code: FAIL");
            Console.WriteLine(build.StdOut);
            Console.WriteLine(build.StdErr);
            return false;
        }

        if (!quiet) Console.WriteLine("Compile code: PASS");

        string dllPath = Path.Combine(tempRoot, "bin", "Release", "net8.0", "PracticeSubmission.dll");
        List<TestCase> tests = sampleOnly ? problem.Tests.Where(t => t.IsSample).ToList() : problem.Tests;
        int pass = 0;

        for (int i = 0; i < tests.Count; i++)
        {
            TestCase tc = tests[i];
            ProcessResult run = RunProcess("dotnet", $"\"{dllPath}\"", tempRoot, tc.Input + Environment.NewLine, TimeoutMs);
            string actual = NormalizeOutput(run.StdOut);
            string expected = NormalizeOutput(tc.Output);

            bool ok = run.ExitCode == 0 && actual == expected;
            if (ok) pass++;

            if (!quiet)
            {
                Console.WriteLine($"Test {i + 1} ({tc.Name}): {(ok ? "PASS" : "FAIL")}");
                if (!ok)
                {
                    if (run.TimedOut) Console.WriteLine("Lỗi: chương trình chạy quá thời gian.");
                    if (!string.IsNullOrWhiteSpace(run.StdErr))
                    {
                        Console.WriteLine("Runtime error:");
                        Console.WriteLine(run.StdErr.Trim());
                    }
                    PrintBlock("Input", tc.Input);
                    PrintBlock("Output đúng", expected);
                    PrintBlock("Output của bạn", actual);
                }
            }
        }

        bool allPassed = pass == tests.Count;
        if (!quiet)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Tổng kết: {pass}/{tests.Count} test pass.");
            Console.WriteLine(allPassed ? "Kết luận: ĐÚNG" : "Kết luận: CHƯA ĐÚNG");
        }

        if (allPassed && !sampleOnly)
        {
            MarkProgress(root, problem.Id);
        }

        return allPassed;
    }


    private static void PrintBlock(string title, string value)
    {
        Console.WriteLine(title + ":");
        Console.WriteLine("<<<");
        if (string.IsNullOrEmpty(value))
        {
            Console.WriteLine("[không có output]");
        }
        else
        {
            Console.WriteLine(value);
        }
        Console.WriteLine(">>>");
    }

    private static string NormalizeProblemId(string raw)
    {
        raw = raw.Trim().ToUpperInvariant();
        if (raw.StartsWith("P")) return raw;
        if (int.TryParse(raw, out int n)) return $"P{n:000}";
        return raw;
    }

    private static ProblemBank LoadProblems(string root)
    {
        string jsonPath = Path.Combine(root, "exercises", "problems.json");
        string json = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<ProblemBank>(json) ?? new ProblemBank();
    }

    private static string FindRoot()
    {
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir != null)
        {
            string jsonPath = Path.Combine(dir.FullName, "exercises", "problems.json");
            if (File.Exists(jsonPath)) return dir.FullName;
            dir = dir.Parent;
        }
        throw new DirectoryNotFoundException("Không tìm thấy thư mục gốc có exercises/problems.json.");
    }

    private static string NormalizeOutput(string value)
    {
        string normalized = value.Replace("\r\n", "\n").Replace("\r", "\n");
        string[] lines = normalized.Split('\n').Select(line => line.TrimEnd()).ToArray();
        return string.Join("\n", lines).Trim();
    }

    private static void MarkProgress(string root, string problemId)
    {
        try
        {
            string progressDir = Path.Combine(root, ".practice");
            Directory.CreateDirectory(progressDir);
            string path = Path.Combine(progressDir, "progress.json");
            SortedSet<string> solved = new();
            if (File.Exists(path))
            {
                using JsonDocument doc = JsonDocument.Parse(File.ReadAllText(path));
                if (doc.RootElement.TryGetProperty("solved", out JsonElement arr))
                {
                    foreach (JsonElement item in arr.EnumerateArray())
                    {
                        string? id = item.GetString();
                        if (!string.IsNullOrWhiteSpace(id)) solved.Add(id);
                    }
                }
            }
            solved.Add(problemId);
            string json = JsonSerializer.Serialize(new { solved = solved.ToArray(), updatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        catch
        {
            // Không làm fail bài chỉ vì lỗi ghi progress.
        }
    }

    private static ProcessResult RunProcess(string fileName, string arguments, string workingDirectory, string? input, int timeoutMs)
    {
        using Process process = new();
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WorkingDirectory = workingDirectory;
        process.StartInfo.RedirectStandardInput = input != null;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        if (input != null)
        {
            process.StandardInput.Write(input);
            process.StandardInput.Close();
        }

        bool exited = process.WaitForExit(timeoutMs);
        if (!exited)
        {
            try { process.Kill(entireProcessTree: true); } catch { }
            return new ProcessResult(-1, process.StandardOutput.ReadToEnd(), process.StandardError.ReadToEnd(), TimedOut: true);
        }

        return new ProcessResult(process.ExitCode, process.StandardOutput.ReadToEnd(), process.StandardError.ReadToEnd(), TimedOut: false);
    }

    private sealed record ProcessResult(int ExitCode, string StdOut, string StdErr, bool TimedOut);
}
