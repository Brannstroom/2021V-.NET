using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Applikasjon.ViewModels;
using Exercise6.API.Models;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;

namespace Applikasjon.Views
{
    public sealed partial class StudentsPage : Page
    {
        public StudentsViewModel ViewModel { get; } = new StudentsViewModel();

        public StudentsPage()
        {
            InitializeComponent();
        }

        private async void StudentsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string studentsString = "";
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/Student?includeCourses=true");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        List<Student> students = JsonConvert.DeserializeObject<List<Student>>(await response.Content.ReadAsStringAsync());
                        if (students != null)
                        {
                            students.ForEach(student =>
                            {
                                studentsString += student.GetString() + "\n";
                            });
                        }
                        else
                        {
                            studentsString = "There are no students.";
                        }

                    }
                    else
                    {

                        studentsString = "An error occured.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                StudentsList.Text = studentsString;

            }
        }
    }
}
