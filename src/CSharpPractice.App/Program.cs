using System;
using System.Windows.Forms;

namespace CSharpPractice.App;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        try
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
            try
            {
                string logPath = Path.Combine(AppContext.BaseDirectory, "startup_error.log");
                File.WriteAllText(logPath, ex.ToString());
            }
            catch
            {
                // Ignore logging errors.
            }

            MessageBox.Show(
                "App bị lỗi khi mở:\n\n" + ex.Message + "\n\nHãy chụp lỗi này gửi mình để sửa tiếp.",
                "CSharp Practice 200 - Lỗi mở app",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(1);
        }
    }
}
