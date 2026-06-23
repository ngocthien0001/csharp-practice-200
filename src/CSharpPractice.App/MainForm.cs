using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpPractice.App;

public sealed class ProblemBank
{
    [JsonPropertyName("problems")]
    public List<Problem> Problems { get; set; } = new();
}

public sealed class Problem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("level")]
    public string Level { get; set; } = "";

    [JsonPropertyName("topic")]
    public string Topic { get; set; } = "";

    [JsonPropertyName("statement")]
    public string Statement { get; set; } = "";

    [JsonPropertyName("input")]
    public string Input { get; set; } = "";

    [JsonPropertyName("output")]
    public string Output { get; set; } = "";

    [JsonPropertyName("tests")]
    public List<TestCase> Tests { get; set; } = new();

    public override string ToString() => $"{Id} - {Title}";
}

public sealed class TestCase
{
    [JsonPropertyName("input")]
    public string Input { get; set; } = "";

    [JsonPropertyName("output")]
    public string Output { get; set; } = "";

    [JsonPropertyName("isSample")]
    public bool IsSample { get; set; }
}

public sealed class MainForm : Form
{
    private readonly string _root;
    private readonly List<Problem> _problems;

    private readonly ListBox _listProblems = new();
    private readonly TextBox _txtSearch = new();
    private readonly ComboBox _cboLevel = new();
    private readonly TextBox _txtProblem = new();
    private readonly TextBox _txtCode = new();
    private readonly TextBox _txtOutput = new();
    private readonly Label _lblTitle = new();
    private readonly Label _lblPath = new();

    private Problem? CurrentProblem => _listProblems.SelectedItem as Problem;

    public MainForm()
    {
        _root = FindRoot();
        _problems = LoadProblems(_root);

        Text = "CSharp Practice 200 - Offline Judge";
        Width = 1280;
        Height = 820;
        MinimumSize = new Size(1100, 720);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10);

        BuildUi();
        LoadFilters();
        RefreshProblemList();

        if (_listProblems.Items.Count > 0)
            _listProblems.SelectedIndex = 0;
    }

    private void BuildUi()
    {
        var main = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            SplitterDistance = 300
        };
        Controls.Add(main);

        var left = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1,
            Padding = new Padding(8)
        };
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        main.Panel1.Controls.Add(left);

        left.Controls.Add(new Label
        {
            Text = "DANH SÁCH 200 BÀI",
            Dock = DockStyle.Fill,
            Font = new Font(Font, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 80, 150)
        }, 0, 0);

        _txtSearch.PlaceholderText = "Tìm bài, ví dụ: P001, mảng, chuỗi...";
        _txtSearch.Dock = DockStyle.Fill;
        _txtSearch.TextChanged += (_, _) => RefreshProblemList();
        left.Controls.Add(_txtSearch, 0, 1);

        _cboLevel.Dock = DockStyle.Fill;
        _cboLevel.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboLevel.SelectedIndexChanged += (_, _) => RefreshProblemList();
        left.Controls.Add(_cboLevel, 0, 2);

        _listProblems.Dock = DockStyle.Fill;
        _listProblems.SelectedIndexChanged += (_, _) => LoadSelectedProblem();
        left.Controls.Add(_listProblems, 0, 3);

        var right = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1,
            Padding = new Padding(8)
        };
        right.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        right.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        right.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
        right.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
        main.Panel2.Controls.Add(right);

        _lblTitle.Dock = DockStyle.Fill;
        _lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
        _lblTitle.ForeColor = Color.FromArgb(0, 60, 120);
        right.Controls.Add(_lblTitle, 0, 0);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight
        };
        right.Controls.Add(buttons, 0, 1);

        AddButton(buttons, "← Bài trước", (_, _) => MoveProblem(-1));
        AddButton(buttons, "Bài tiếp →", (_, _) => MoveProblem(1));
        AddButton(buttons, "Lưu code", (_, _) => SaveCode());
        AddButton(buttons, "Format code", (_, _) => FormatCode());
        AddButton(buttons, "Chạy ví dụ", async (_, _) => await RunJudgeAsync(sampleOnly: true));
        AddButton(buttons, "Chấm bài", async (_, _) => await RunJudgeAsync(sampleOnly: false));
        AddButton(buttons, "Mở thư mục", (_, _) => OpenCurrentFolder());
        AddButton(buttons, "Reset code", (_, _) => ResetCode());

        var problemGroup = new GroupBox
        {
            Text = "ĐỀ BÀI",
            Dock = DockStyle.Fill,
            Font = new Font(Font, FontStyle.Bold)
        };
        right.Controls.Add(problemGroup, 0, 2);

        _txtProblem.Dock = DockStyle.Fill;
        _txtProblem.Multiline = true;
        _txtProblem.ReadOnly = true;
        _txtProblem.ScrollBars = ScrollBars.Both;
        _txtProblem.WordWrap = false;
        _txtProblem.Font = new Font("Consolas", 10);
        problemGroup.Controls.Add(_txtProblem);

        var bottom = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            SplitterDistance = 620
        };
        right.Controls.Add(bottom, 0, 3);

        var codeGroup = new GroupBox
        {
            Text = "NHẬP CODE C# CỦA BẠN Ở ĐÂY",
            Dock = DockStyle.Fill,
            Font = new Font(Font, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 80, 150)
        };
        bottom.Panel1.Controls.Add(codeGroup);

        var codeLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            Padding = new Padding(6)
        };
        codeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        codeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        codeGroup.Controls.Add(codeLayout);

        _lblPath.Dock = DockStyle.Fill;
        _lblPath.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        codeLayout.Controls.Add(_lblPath, 0, 0);

        _txtCode.Dock = DockStyle.Fill;
        _txtCode.Multiline = true;
        _txtCode.AcceptsReturn = true;
        _txtCode.AcceptsTab = true;
        _txtCode.ScrollBars = ScrollBars.Both;
        _txtCode.WordWrap = false;
        _txtCode.Font = new Font("Consolas", 11);
        _txtCode.BackColor = Color.FromArgb(18, 24, 32);
        _txtCode.ForeColor = Color.White;
        codeLayout.Controls.Add(_txtCode, 0, 1);

        var outputGroup = new GroupBox
        {
            Text = "KẾT QUẢ CHẤM",
            Dock = DockStyle.Fill,
            Font = new Font(Font, FontStyle.Bold)
        };
        bottom.Panel2.Controls.Add(outputGroup);

        _txtOutput.Dock = DockStyle.Fill;
        _txtOutput.Multiline = true;
        _txtOutput.ReadOnly = true;
        _txtOutput.ScrollBars = ScrollBars.Both;
        _txtOutput.WordWrap = false;
        _txtOutput.Font = new Font("Consolas", 10);
        _txtOutput.BackColor = Color.FromArgb(18, 24, 32);
        _txtOutput.ForeColor = Color.White;
        outputGroup.Controls.Add(_txtOutput);
    }

    private static void AddButton(FlowLayoutPanel panel, string text, EventHandler click)
    {
        var btn = new Button
        {
            Text = text,
            Width = 110,
            Height = 32,
            Margin = new Padding(3)
        };
        btn.Click += click;
        panel.Controls.Add(btn);
    }

    private void LoadFilters()
    {
        _cboLevel.Items.Clear();
        _cboLevel.Items.Add("Tất cả mức độ");
        foreach (string level in _problems.Select(p => p.Level).Distinct().OrderBy(x => x))
            _cboLevel.Items.Add(level);
        _cboLevel.SelectedIndex = 0;
    }

    private void RefreshProblemList()
    {
        string keyword = _txtSearch.Text.Trim().ToLowerInvariant();
        string? level = _cboLevel.SelectedIndex > 0 ? _cboLevel.Text : null;

        var filtered = _problems.Where(p =>
            (level == null || p.Level == level) &&
            (string.IsNullOrWhiteSpace(keyword) ||
             p.Id.ToLowerInvariant().Contains(keyword) ||
             p.Title.ToLowerInvariant().Contains(keyword) ||
             p.Topic.ToLowerInvariant().Contains(keyword))
        ).ToList();

        _listProblems.BeginUpdate();
        _listProblems.Items.Clear();
        foreach (Problem p in filtered) _listProblems.Items.Add(p);
        _listProblems.EndUpdate();
    }

    private void LoadSelectedProblem()
    {
        Problem? p = CurrentProblem;
        if (p == null) return;

        _lblTitle.Text = $"{p.Id} - {p.Title}";
        _txtProblem.Text = $"{p.Id} - {p.Title}\r\n" +
                           $"Mức độ: {p.Level}\r\n" +
                           $"Chủ đề: {p.Topic}\r\n" +
                           new string('-', 70) + "\r\n\r\n" +
                           $"Đề bài:\r\n{p.Statement}\r\n\r\n" +
                           $"Input:\r\n{p.Input}\r\n\r\n" +
                           $"Output:\r\n{p.Output}\r\n\r\n" +
                           $"Ví dụ Input:\r\n{p.Tests.FirstOrDefault(t => t.IsSample)?.Input}\r\n\r\n" +
                           $"Ví dụ Output:\r\n{p.Tests.FirstOrDefault(t => t.IsSample)?.Output}";

        string codePath = GetCodePath(p.Id);
        _lblPath.Text = $"File code: solutions/{p.Id}/Program.cs";
        _txtCode.Text = File.Exists(codePath) ? File.ReadAllText(codePath) : CreateStarterCode(p);
        _txtOutput.Text = "Viết code ở khung bên trái, bấm 'Lưu code', rồi bấm 'Chạy ví dụ' hoặc 'Chấm bài'.";
    }

    private void SaveCode()
    {
        Problem? p = CurrentProblem;
        if (p == null) return;

        string path = GetCodePath(p.Id);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, _txtCode.Text);
        _txtOutput.Text = $"Đã lưu code vào solutions/{p.Id}/Program.cs";
    }

    private async Task RunJudgeAsync(bool sampleOnly)
    {
        Problem? p = CurrentProblem;
        if (p == null) return;

        SaveCode();
        _txtOutput.Text = "Đang chạy bộ chấm...";

        string args = $"run --project \"{Path.Combine(_root, "src", "CSharpPractice.Judge", "CSharpPractice.Judge.csproj")}\" -- {p.Id}";
        if (sampleOnly) args += " --sample";

        string output = await Task.Run(() => RunProcess("dotnet", args, _root));
        _txtOutput.Text = output;
    }

    private string RunProcess(string fileName, string arguments, string workingDirectory)
    {
        using Process process = new();
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WorkingDirectory = workingDirectory;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        try
        {
            process.Start();
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return stdout + (string.IsNullOrWhiteSpace(stderr) ? "" : "\r\n" + stderr);
        }
        catch (Exception ex)
        {
            return "Không chạy được dotnet. Kiểm tra đã cài .NET SDK 8 chưa.\r\n" + ex.Message;
        }
    }

    private void FormatCode()
    {
        string code = _txtCode.Text;
        code = code.Replace(";using", ";\r\nusing")
                   .Replace(";class", ";\r\n\r\nclass")
                   .Replace("{", "{\r\n")
                   .Replace("}", "\r\n}\r\n")
                   .Replace(";", ";\r\n");
        code = string.Join("\r\n", code.Replace("\r\n", "\n").Split('\n').Select(line => line.TrimEnd()));
        _txtCode.Text = code;
    }

    private void ResetCode()
    {
        Problem? p = CurrentProblem;
        if (p == null) return;

        DialogResult result = MessageBox.Show("Reset code bài này về template ban đầu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        _txtCode.Text = CreateStarterCode(p);
        SaveCode();
    }

    private void OpenCurrentFolder()
    {
        Problem? p = CurrentProblem;
        if (p == null) return;

        string folder = Path.Combine(_root, "solutions", p.Id);
        Directory.CreateDirectory(folder);
        Process.Start(new ProcessStartInfo
        {
            FileName = folder,
            UseShellExecute = true
        });
    }

    private void MoveProblem(int delta)
    {
        int index = _listProblems.SelectedIndex;
        int next = index + delta;
        if (next >= 0 && next < _listProblems.Items.Count)
            _listProblems.SelectedIndex = next;
    }

    private string GetCodePath(string problemId) => Path.Combine(_root, "solutions", problemId, "Program.cs");

    private static string CreateStarterCode(Problem p)
    {
        return $$"""
using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // {{p.Id}} - {{p.Title}}
        // Viết lời giải của bạn ở đây.
    }
}
""";
    }

    private static string FindRoot()
    {
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "exercises", "problems.json")))
                return dir.FullName;
            dir = dir.Parent;
        }
        throw new DirectoryNotFoundException("Không tìm thấy exercises/problems.json");
    }

    private static List<Problem> LoadProblems(string root)
    {
        string json = File.ReadAllText(Path.Combine(root, "exercises", "problems.json"));
        return JsonSerializer.Deserialize<ProblemBank>(json)?.Problems ?? new List<Problem>();
    }
}
