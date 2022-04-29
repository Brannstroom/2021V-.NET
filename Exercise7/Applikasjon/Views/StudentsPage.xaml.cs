using Exercise6.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Applikasjon.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentsPage : Page
    {

        readonly string[] randomFirstNames = { "Peter", "Ola", "Hauk", "Chris", "Sander", "Fredrik", "Kasper", "Karsten", "Kurt", "Jonathan", "Hans", "Lars", "Jonas" };
        readonly string[] randomLastNames = { "Petersen", "Olsen", "Christoffersen", "Sandersen", "Fredriksen", "Kaspersen", "Knudsen", "Larsen" };


        public StudentsPage()
        {
            this.InitializeComponent();
        }

        private void StudentsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateList();
        }

        private async void StudentsAddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await PostRandom();
        }

        private async void StudentsEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await EditStudent();
        }

        private async Task EditStudent()
        {
            StudentsError.Text = "";

            Random random = new Random();
            int i = random.Next(randomFirstNames.Length);
            int j = random.Next(randomLastNames.Length);

            string input = StudentsIdTextBox.Text;
            try
            {
                int id = Int32.Parse(input);
                using (HttpClient client = new HttpClient())
                {
                    try
                    {

                        string json = "{\"studentId\":" + id + ",\"firstName\":\"" + randomFirstNames[i] + "\",\"lastName\":\"" + randomLastNames[j] + "\",\"courses\":null}";
                        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        HttpResponseMessage response = await client.PutAsync("https://localhost:5001/api/Student/" + id, httpContent);

                        if (response.IsSuccessStatusCode)
                        {
                            StudentsError.Text = "Seems like a success";
                        }
                        else
                        {
                            StudentsError.Text = response.StatusCode.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        StudentsError.Text = ex.ToString();
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                StudentsError.Text = "Looks like the ID could not be parsed. Check console.";
                Console.WriteLine(ex.ToString());
            }

            UpdateList();
        }

        private async void StudentsDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StudentsError.Text = "";

            string input = StudentsIdTextBox.Text;
            try
            {
                int id = Int32.Parse(input);
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        HttpResponseMessage response = await client.DeleteAsync("https://localhost:5001/api/Student?id=" + id);
                        response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            UpdateList();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        StudentsError.Text = "Something wrong happened with the Request - Maybe there are no student with that id? Check console.";
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                StudentsError.Text = "Looks like the ID could not be parsed. Check console.";
                Console.WriteLine(ex.ToString());
            }
        }

        private async void UpdateList()
        {
            StudentsList.Text = "loading...";

            using (HttpClient client = new HttpClient())
            {
                string studentsString = "";
                try
                {
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/Student?includeCourses=false");
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        List<Student> students = JsonConvert.DeserializeObject<List<Student>>(await response.Content.ReadAsStringAsync());
                        if (students != null)
                        {
                            if (students.Count > 0)
                            {
                                students.ForEach(student =>
                                {
                                    studentsString += student.GetString(true) + "\n";
                                });
                            }
                            else
                            {
                                StudentsError.Text = "There are no students.";
                            }
                        }
                        else
                        {
                            StudentsError.Text = "There are no students.";
                        }

                    }
                    else
                    {

                        StudentsError.Text = "An error occured.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                StudentsList.Text = studentsString;
            }
        }

        private async Task PostRandom()
        {
            StudentsError.Text = "";
            Random random = new Random();
            int i = random.Next(randomFirstNames.Length);
            int j = random.Next(randomLastNames.Length);

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    Student student = new Student
                    {
                        FirstName = randomFirstNames[i],
                        LastName = randomLastNames[j]
                    };

                    string json = JsonConvert.SerializeObject(student);
                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.PostAsync("https://localhost:5001/api/Student", httpContent);
                }
                catch (HttpRequestException ex)
                {
                    StudentsError.Text = ex.ToString();
                    Console.WriteLine(ex.ToString());
                }
            }

            UpdateList();
        }
    }
}
