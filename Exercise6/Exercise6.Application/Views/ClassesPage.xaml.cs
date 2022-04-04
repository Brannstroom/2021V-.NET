using System;

using Exercise6.Application.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Exercise6.Application.Views
{
    public sealed partial class ClassesPage : Page
    {
        public ClassesViewModel ViewModel { get; } = new ClassesViewModel();

        public ClassesPage()
        {
            InitializeComponent();
        }
    }
}
