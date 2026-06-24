using System.Diagnostics;
using System.Text;
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
    private static readonly Color AppBackground = Color.FromArgb(244, 247, 251);
    private static readonly Color CardBackground = Color.White;
    private static readonly Color Primary = Color.FromArgb(37, 99, 235);
    private static readonly Color PrimaryDark = Color.FromArgb(30, 64, 175);
    private static readonly Color TextDark = Color.FromArgb(15, 23, 42);
    private static readonly Color TextMuted = Color.FromArgb(100, 116, 139);
    private static readonly Color EditorBackground = Color.FromArgb(15, 23, 42);
    private static readonly Color EditorForeground = Color.FromArgb(226, 232, 240);

    private readonly string _root;
    private readonly List<Problem> _problems;

    private readonly ListBox _listProblems = new();
    private readonly TextBox _txtSearch = new();
    private readonly ComboBox _cboLevel = new();
    private readonly TextBox _txtProblem = new();
    private readonly TextBox _txtCode = new();
    private readonly TextBox _txtOutput = new();
    private readonly Label _lblTitle = new();
    private readonly Label _lblSubtitle = new();
    private readonly Label _lblPath = new();
    private readonly Label _lblCount = new();

    private Problem? CurrentProblem => _listProblems.SelectedItem as Problem;

    public MainForm()
    {
        _root = FindRoot();
        _problems = LoadProblems(_root);

        Text = "C# Practice 200";
        Width = 1420;
        Height = 900;
        MinimumSize = new Size(1100, 720);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10F);
        BackColor = AppBackground;

        BuildUi();
        LoadFilters();
        RefreshProblemList();

        if (_listProblems.Items.Count > 0)
            _listProblems.SelectedIndex = 0;
    }

    private void BuildUi()
    {
        var rootLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            Padding = new Padding(14),
            BackColor = AppBackground
        };
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(rootLayout);

        rootLayout.Controls.Add(CreateHeader(), 0, 0);

        // Không dùng SplitContainer ở layout chính nữa vì một số màn hình/DPI nhỏ sẽ lỗi SplitterDistance.
        // Dùng TableLayoutPanel cố định bên trái + phần làm bài bên phải để app mở ổn định hơn.
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 1,
            ColumnCount = 2,
            BackColor = AppBackground
        };
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330));
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        rootLayout.Controls.Add(mainLayout, 0, 1);

        Control leftPanel = CreateLeftPanel();
        leftPanel.Margin = new Padding(0, 0, 12, 0);
        mainLayout.Controls.Add(leftPanel, 0, 0);

        Control workspace = CreateWorkspace();
        workspace.Margin = new Padding(0);
        mainLayout.Controls.Add(workspace, 1, 0);
    }

    private Control CreateHeader()
    {
        var header = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            BackColor = AppBackground,
            Padding = new Padding(2, 0, 2, 6)
        };
        header.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        header.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));

        var title = new Label
        {
            Text = "C# Practice 200",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = TextDark,
            TextAlign = ContentAlignment.MiddleLeft
        };
        header.Controls.Add(title, 0, 0);

        var sub = new Label
        {
            Text = "200 bài luyện C# · WinForms Practice App · Offline Judge",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F),
            ForeColor = TextMuted,
            TextAlign = ContentAlignment.MiddleLeft
        };
        header.Controls.Add(sub, 0, 1);

        return header;
    }

    private Control CreateLeftPanel()
    {
        Panel card = CreateCard();

        var left = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 5,
            ColumnCount = 1,
            Padding = new Padding(12),
            BackColor = CardBackground
        };
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        card.Controls.Add(left);

        var navTitle = new Label
        {
            Text = "DANH SÁCH BÀI TẬP",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        };
        left.Controls.Add(navTitle, 0, 0);

        _txtSearch.PlaceholderText = "Tìm P001, mảng, chuỗi...";
        _txtSearch.Dock = DockStyle.Fill;
        _txtSearch.BorderStyle = BorderStyle.FixedSingle;
        _txtSearch.TextChanged += (_, _) => RefreshProblemList();
        left.Controls.Add(_txtSearch, 0, 1);

        _cboLevel.Dock = DockStyle.Fill;
        _cboLevel.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboLevel.SelectedIndexChanged += (_, _) => RefreshProblemList();
        left.Controls.Add(_cboLevel, 0, 2);

        _lblCount.Dock = DockStyle.Fill;
        _lblCount.ForeColor = TextMuted;
        _lblCount.Font = new Font("Segoe UI", 9F);
        _lblCount.TextAlign = ContentAlignment.MiddleLeft;
        left.Controls.Add(_lblCount, 0, 3);

        _listProblems.Dock = DockStyle.Fill;
        _listProblems.BorderStyle = BorderStyle.None;
        _listProblems.Font = new Font("Segoe UI", 10F);
        _listProblems.ItemHeight = 24;
        _listProblems.SelectedIndexChanged += (_, _) => LoadSelectedProblem();
        left.Controls.Add(_listProblems, 0, 4);

        return card;
    }

    private Control CreateWorkspace()
    {
        var workspace = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1,
            BackColor = AppBackground
        };
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));
        workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titlePanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            BackColor = AppBackground,
            Padding = new Padding(0, 4, 0, 0)
        };
        titlePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        titlePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));

        _lblTitle.Dock = DockStyle.Fill;
        _lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        _lblTitle.ForeColor = TextDark;
        _lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        titlePanel.Controls.Add(_lblTitle, 0, 0);

        _lblSubtitle.Dock = DockStyle.Fill;
        _lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        _lblSubtitle.ForeColor = TextMuted;
        _lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
        titlePanel.Controls.Add(_lblSubtitle, 0, 1);

        workspace.Controls.Add(titlePanel, 0, 0);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            BackColor = AppBackground,
            Padding = new Padding(0, 3, 0, 3)
        };
        workspace.Controls.Add(buttons, 0, 1);

        AddButton(buttons, "← Bài trước", false, (_, _) => MoveProblem(-1));
        AddButton(buttons, "Bài tiếp →", false, (_, _) => MoveProblem(1));
        AddButton(buttons, "Lưu code", false, (_, _) => SaveCode());
        AddButton(buttons, "Format code", false, (_, _) => FormatCode());
        AddButton(buttons, "Chạy ví dụ", true, async (_, _) => await RunJudgeAsync(sampleOnly: true));
        AddButton(buttons, "Chấm bài", true, async (_, _) => await RunJudgeAsync(sampleOnly: false));
        AddButton(buttons, "Thư mục", false, (_, _) => OpenCurrentFolder());
        AddButton(buttons, "Reset", false, (_, _) => ResetCode());

        Control problemPanel = CreateProblemPanel();
        problemPanel.Margin = new Padding(0, 0, 0, 12);
        workspace.Controls.Add(problemPanel, 0, 2);

        var bottomLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 1,
            ColumnCount = 2,
            BackColor = AppBackground
        };
        bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76));
        bottomLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24));
        workspace.Controls.Add(bottomLayout, 0, 3);

        Control codePanel = CreateCodePanel();
        codePanel.Margin = new Padding(0, 0, 12, 0);
        bottomLayout.Controls.Add(codePanel, 0, 0);

        Control outputPanel = CreateOutputPanel();
        outputPanel.Margin = new Padding(0);
        bottomLayout.Controls.Add(outputPanel, 1, 0);

        return workspace;
    }

    private Control CreateProblemPanel()
    {
        Panel card = CreateCard();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            Padding = new Padding(12),
            BackColor = CardBackground
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        card.Controls.Add(layout);

        layout.Controls.Add(new Label
        {
            Text = "ĐỀ BÀI",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);

        _txtProblem.Dock = DockStyle.Fill;
        _txtProblem.Multiline = true;
        _txtProblem.ReadOnly = true;
        _txtProblem.ScrollBars = ScrollBars.Vertical;
        _txtProblem.WordWrap = true;
        _txtProblem.BorderStyle = BorderStyle.None;
        _txtProblem.Font = new Font("Segoe UI", 10.5F);
        _txtProblem.BackColor = CardBackground;
        _txtProblem.ForeColor = TextDark;
        layout.Controls.Add(_txtProblem, 0, 1);

        return card;
    }

    private Control CreateCodePanel()
    {
        Panel card = CreateCard();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1,
            Padding = new Padding(12),
            BackColor = CardBackground
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        card.Controls.Add(layout);

        layout.Controls.Add(new Label
        {
            Text = "NHẬP CODE C# CỦA BẠN Ở ĐÂY",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);

        _lblPath.Dock = DockStyle.Fill;
        _lblPath.Font = new Font("Segoe UI", 9F);
        _lblPath.ForeColor = TextMuted;
        _lblPath.TextAlign = ContentAlignment.MiddleLeft;
        layout.Controls.Add(_lblPath, 0, 1);

        _txtCode.Dock = DockStyle.Fill;
        _txtCode.Multiline = true;
        _txtCode.AcceptsReturn = true;
        _txtCode.AcceptsTab = true;
        _txtCode.ScrollBars = ScrollBars.Both;
        _txtCode.WordWrap = false;
        _txtCode.BorderStyle = BorderStyle.None;
        _txtCode.Font = new Font("Consolas", 12F);
        _txtCode.BackColor = EditorBackground;
        _txtCode.ForeColor = EditorForeground;
        layout.Controls.Add(_txtCode, 0, 2);

        return card;
    }

    private Control CreateOutputPanel()
    {
        Panel card = CreateCard();

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1,
            Padding = new Padding(12),
            BackColor = CardBackground
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        card.Controls.Add(layout);

        layout.Controls.Add(new Label
        {
            Text = "KẾT QUẢ CHẤM",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);

        _txtOutput.Dock = DockStyle.Fill;
        _txtOutput.Multiline = true;
        _txtOutput.ReadOnly = true;
        _txtOutput.ScrollBars = ScrollBars.Both;
        _txtOutput.WordWrap = false;
        _txtOutput.BorderStyle = BorderStyle.None;
        _txtOutput.Font = new Font("Consolas", 10.5F);
        _txtOutput.BackColor = EditorBackground;
        _txtOutput.ForeColor = EditorForeground;
        layout.Controls.Add(_txtOutput, 0, 1);

        return card;
    }

    private static Panel CreateCard()
    {
        return new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = CardBackground,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(1),
            Margin = new Padding(0)
        };
    }

    private static void AddButton(FlowLayoutPanel panel, string text, bool primary, EventHandler click)
    {
        int width = text switch
        {
            "← Bài trước" => 112,
            "Bài tiếp →" => 104,
            "Lưu code" => 98,
            "Format code" => 118,
            "Chạy ví dụ" => 112,
            "Chấm bài" => 104,
            "Thư mục" => 92,
            "Reset" => 82,
            _ => Math.Max(96, text.Length * 10)
        };

        var btn = new Button
        {
            Text = text,
            Width = width,
            Height = 34,
            Margin = new Padding(0, 0, 8, 0),
            FlatStyle = FlatStyle.Flat,
            BackColor = primary ? Primary : Color.White,
            ForeColor = primary ? Color.White : TextDark,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            TextAlign = ContentAlignment.MiddleCenter
        };
        btn.FlatAppearance.BorderColor = primary ? Primary : Color.FromArgb(203, 213, 225);
        btn.FlatAppearance.MouseOverBackColor = primary ? PrimaryDark : Color.FromArgb(241, 245, 249);
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

        _lblCount.Text = $"Hiển thị {filtered.Count}/200 bài";
    }

    private void LoadSelectedProblem()
    {
        Problem? p = CurrentProblem;
        if (p == null) return;

        _lblTitle.Text = $"{p.Id} - {p.Title}";
        _lblSubtitle.Text = $"Mức độ: {p.Level} · Chủ đề: {p.Topic}";

        TestCase? sample = p.Tests.FirstOrDefault(t => t.IsSample);
        _txtProblem.Text = ToTextBoxNewLines($"Đề bài:\n{p.Statement}\n\n" +
                           $"Input:\n{p.Input}\n\n" +
                           $"Output:\n{p.Output}\n\n" +
                           $"Ví dụ Input:\n{sample?.Input}\n\n" +
                           $"Ví dụ Output:\n{sample?.Output}");

        string codePath = GetCodePath(p.Id);
        _lblPath.Text = $"File code: solutions/{p.Id}/Program.cs";
        string code = File.Exists(codePath) ? File.ReadAllText(codePath) : CreateStarterCode(p);
        _txtCode.Text = NormalizeCodeForEditor(code);
        _txtOutput.Text = "Viết code ở khung bên trái, bấm 'Chạy ví dụ' hoặc 'Chấm bài'. App sẽ tự lưu code trước khi chấm.";
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

        string args = $"run --project \"{Path.Combine(_root, "src", "CSharpPractice.Judge")}\" -- {p.Id}";
        if (sampleOnly) args += " --sample";

        string output = await Task.Run(() => RunProcess("dotnet", args, _root));
        _txtOutput.Text = ToTextBoxNewLines(output);
    }

    private string RunProcess(string fileName, string arguments, string workingDirectory)
    {
        using Process process = new();
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WorkingDirectory = workingDirectory;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
        process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        try
        {
            process.Start();
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return stdout + (string.IsNullOrWhiteSpace(stderr) ? "" : "\n" + stderr);
        }
        catch (Exception ex)
        {
            return "Không chạy được dotnet. Kiểm tra đã cài .NET SDK 8 chưa.\r\n" + ex.Message;
        }
    }


    private static string ToTextBoxNewLines(string text)
    {
        return text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    }

    private void FormatCode()
    {
        _txtCode.Text = FormatCSharpCode(_txtCode.Text);
    }

    private static string NormalizeCodeForEditor(string code)
    {
        code = code.Replace("\r\n", "\n").Replace("\r", "\n");
        int lineCount = code.Count(c => c == '\n') + 1;

        if (lineCount <= 2 && code.Contains("class Program", StringComparison.Ordinal))
            return FormatCSharpCode(code);

        return code.Replace("\n", Environment.NewLine);
    }

    private static string FormatCSharpCode(string code)
    {
        code = code.Replace("\r\n", "\n").Replace("\r", "\n");

        if (!code.Contains('\n') || code.Count(c => c == '\n') <= 2)
        {
            code = code.Replace(";using", ";\nusing")
                       .Replace(";class", ";\n\nclass")
                       .Replace("{", "\n{\n")
                       .Replace("}", "\n}\n")
                       .Replace(";", ";\n");
        }

        string[] rawLines = code.Split('\n');
        var lines = rawLines
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .ToList();

        var sb = new StringBuilder();
        int indent = 0;

        foreach (string line in lines)
        {
            if (line.StartsWith("}", StringComparison.Ordinal))
                indent = Math.Max(0, indent - 1);

            sb.Append(new string(' ', indent * 4));
            sb.AppendLine(line);

            if (line.EndsWith("{", StringComparison.Ordinal))
                indent++;
        }

        return sb.ToString().TrimEnd() + Environment.NewLine;
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
        // Đọc đề trong giao diện rồi viết lời giải của bạn ở đây.
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
