using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Windows.System.UserProfile;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        string path;
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OpenBtn_Clicked(object sender, EventArgs e)
        {
            FileResult fileResult = await OpenFileDialog();
            if (fileResult != null)
            {
                path = fileResult.FullPath;
                OpenedFile.Text = $"File: {fileResult.FileName}";
                Read();
            }
        }
        private void Read()
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string text = sr.ReadToEnd().ToLower();
                    string[] words = text.Split(new[] { ' ', '\n', '\r', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Length > 0)
                        {
                            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                        }
                    }
                    string updatedText = string.Join("\n", words);
                    updatedText = updatedText.Replace(";", ";\n");
                    EOut.Text = updatedText;
                }
            }
        }
        private async Task<FileResult> OpenFileDialog()
        {
            FilePickerFileType fileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<String>>
                {
                {DevicePlatform.WinUI, new[]{".txt", ".csv", ".bat"} }
                });
            PickOptions pickOptions = new PickOptions()
            {
                PickerTitle = "Vyber textový soubor",
                FileTypes = fileType
            };
            return await FilePicker.Default.PickAsync(pickOptions);
        }
        private void EIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveBtn.BackgroundColor = Colors.Red;
            SaveBtn.TextColor = Colors.White;
        }
        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw2 = new StreamWriter(path, true))
                {
                    sw2.WriteLine(EIn.Text);
                }
                Read();
                EIn.Text = string.Empty;
                SaveBtn.BackgroundColor = Colors.Green;
            }
            catch
            {
                await DisplayAlert("Chyba!", "Vyberte prosím prvně soubor!", "OK");
                OpenBtn_Clicked(sender, e);
            }
        }
    }
}