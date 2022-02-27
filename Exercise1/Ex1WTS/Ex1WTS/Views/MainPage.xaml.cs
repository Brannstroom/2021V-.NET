using System;

using Ex1WTS.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Ex1WTS.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }

        private void mainPageText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            mainPageText.Text = "Teksten har nå endret seg til dette :)";
        }
    }
}
