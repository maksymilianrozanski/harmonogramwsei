using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using CalendarGenerator;
using CalendarGenerator.PdfParse;

namespace WseiScheduleWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".pdf", Filter = "Pdf Files (.pdf)|*.pdf"
            };

            var result = openFileDlg.ShowDialog();

            if (result == null || result != true) return;

            var savingResult = await SaveICal(openFileDlg.FileName);
            StatusTextBlock.Text += savingResult + "\n";
        }

        private async Task<string> SaveICal(string source)
        {
            try
            {
                await using FileStream fileStream = File.Open(source, FileMode.Open, FileAccess.Read);
                var destinationDir = System.IO.Path.GetDirectoryName(source);
                var formattedTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                    .Replace(".", "_").Replace(":", "_")
                    .Replace(" ", "_").Replace("/", "_");
                var generator = new CalGeneratorImpl();
                var iCal = generator.GenerateICalCalendar(fileStream);
                var destinationFileName = System.IO.Path.Combine(destinationDir, "calendar" + formattedTime + ".ical");
                File.WriteAllText(destinationFileName, iCal);
                return "Calendar saved: " + destinationFileName;
            }
            catch (ParsingException e)
            {
                return e.Message;
            }
        }
    }
}