using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        readonly string[] randomCourseNames = { "Databasesystemer", ".NET", "JS", "Java", "AI", "Datasikkerhet", "BS", "BI" };

        public ClassesPage()
        {
            InitializeComponent();
        }

        private void ClassesButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateList();
        }

        private async void ClassesAddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await PostRandom();
        }

        private async void ClassesEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await EditCourse();
        }

        private async Task EditCourse()
        {
            ClassesError.Text = "";

            Random random = new Random();
            int i = random.Next(randomCourseNames.Length);

            string input = ClassesIdTextBox.Text;
            try
            {
                int id = Int32.Parse(input);
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        Course course = new Course
                        {
                            CourseName = randomCourseNames[i]
                        };

                        string json = "{\"courseId\": " + id + ", \"courseName\": \"" + randomCourseNames[i] + "\",\"students\": null }";
                        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        HttpResponseMessage response = await client.PutAsync("https://localhost:5001/api/Course/" + id, httpContent);

                        if(response.IsSuccessStatusCode)
                        {
                            ClassesError.Text = "Seems like a success";
                        }
                        else
                        {
                            ClassesError.Text = response.StatusCode.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ClassesError.Text = ex.ToString();
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ClassesError.Text = "Looks like the ID could not be parsed. Check console.";
                Console.WriteLine(ex.ToString());
            }

            UpdateList();
        }

        private async void ClassesDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ClassesError.Text = "";

            string input = ClassesIdTextBox.Text;
            try
            {
                int id = Int32.Parse(input);
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        HttpResponseMessage response = await client.DeleteAsync("https://localhost:5001/api/Course?id=" + id);
                        response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            UpdateList();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        ClassesError.Text = "Something wrong happened with the Request - Maybe there are no course with that id? Check console.";
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ClassesError.Text = "Looks like the ID could not be parsed. Check console.";
                Console.WriteLine(ex.ToString());
            }
        }

        private async void UpdateList()
        {
            ClassesList.Text = "loading...";

            using (HttpClient client = new HttpClient())
            {
                string coursesString = "";
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/Course?includeStudents=false");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(await response.Content.ReadAsStringAsync());
                        if (courses != null)
                        {
                            if(courses.Count > 0)
                            {
                                courses.ForEach(course =>
                                {
                                    coursesString += course.GetString(true) + "\n";
                                });
                            }
                            else
                            {
                                ClassesError.Text = "There are no courses.";
                            }
                        }
                        else
                        {
                            ClassesError.Text = "There are no courses.";
                        }

                    }
                    else
                    {

                        ClassesError.Text = "An error occured.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                ClassesList.Text = coursesString;
            }
        }

        private async Task PostRandom()
        {
            ClassesError.Text = "";
            Random random = new Random();
            int i = random.Next(randomCourseNames.Length);

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    Course course = new Course
                    {
                        CourseName = randomCourseNames[i]
                    };

                    string json = JsonConvert.SerializeObject(course);
                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.PostAsync("https://localhost:5001/api/Course", httpContent);
                }
                catch (HttpRequestException ex)
                {
                    ClassesError.Text = ex.ToString();
                    Console.WriteLine(ex.ToString());
                }
            }

            UpdateList();
        }
    }
}
