using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace argue
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void RunPing_Click(object sender, RoutedEventArgs e)
        {
            string args = ArgTextBox.Text;

            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C ping {args}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = Process.Start(psi);
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                OutputText.Text = string.IsNullOrWhiteSpace(error) ? output : $"Error: {error}";
            }
            catch (System.Exception ex)
            {
                OutputText.Text = $"Exception: {ex.Message}";
            }
        }
    }
}
