using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class ApiTest
    {
        public HttpClient _httpClient;

        public ApiTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task ApiCoursesId_ReturnsCourse()
        {

            // after switching from GUID's to NR MATRICOL,
            // we need to find a naming convention for 
            // these tests, like ApiCoursesId_ReturnsCourseWithId310910401RSL...
            // or something easier to read and to understand

            // this tests the request for this specific course below
            // "396c3715-d4e9-4e6f-ade0-be9f61d4b058"

            // create db instance
            var db = new DatabaseContext();
            // enusre created and migration
            db.Database.EnsureCreated();
            db.Courses.Add(new Course("TestCourse", 1, 5));
            db.SaveChanges();


            var response = await _httpClient.GetAsync("api/courses");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var courses = JsonConvert.DeserializeObject<List<Course>>(content);
            Assert.IsTrue(courses.Any(c => c.Title == "TestCourse"));
        }

        [TestMethod]
        public async Task StudentController_Get_ReturnsEmptyResponse()
        {

            // create db instance
            var db = new DatabaseContext();

            // enusre created and migration
            db.Database.EnsureCreated();
            db.Students.Add(new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500));
            db.SaveChanges();


            var response = await _httpClient.GetAsync("api/Students");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var students = JsonConvert.DeserializeObject<List<Student>>(content);
            Assert.IsTrue(students.Any(s => s.Email == "mockemail"));
        }
    }
}