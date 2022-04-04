using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Applikasjon.Helpers;
using Applikasjon.ViewModels;
using Exercise6.API.Models;
using Nancy.Json;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;


namespace Applikasjon.Views
{
    public sealed partial class ClassesPage : Page
    {
        public ClassesViewModel ViewModel { get; } = new ClassesViewModel();

        public ClassesPage()
        {
            InitializeComponent();
        }

        private async void ClassesButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string coursesString = "";
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/Course?includeStudents=true");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(await response.Content.ReadAsStringAsync());
                        if (courses != null)
                        {
                            courses.ForEach(course =>
                            {
                                coursesString += course.GetString() + "\n";
                            });
                        }
                        else
                        {
                            coursesString = "There are no courses.";
                        }

                    }
                    else
                    {

                        coursesString = "An error occured.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                ClassesList.Text = coursesString;

            }
        }
    }
}
