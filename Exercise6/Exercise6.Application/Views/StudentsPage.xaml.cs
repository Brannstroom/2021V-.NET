using System;

using Exercise6.Application.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Exercise6.Application.Views
{
    public sealed partial class StudentsPage : Page
    {
        public StudentsViewModel ViewModel { get; } = new StudentsViewModel();

        public StudentsPage()
        {
            InitializeComponent();
        }
    }
}
