using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class CoursesControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task Get_CheckIfReturnsPostedCourse()
        {

            // after switching from GUID's to NR MATRICOL,
            // we need to find a naming convention for 
            // these tests, like ApiCoursesId_ReturnsCourseWithId310910401RSL...
            // or something easier to read and to understand

            // this tests the request for this specific course below
            // "396c3715-d4e9-4e6f-ade0-be9f61d4b058"


            _db.Courses.Add(new Course("TestCourse", 1, 5));
            _db.SaveChanges();


            var response = await _httpClient.GetAsync("api/courses");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var courses = JsonConvert.DeserializeObject<List<Course>>(content);
            Assert.IsTrue(courses.Any(c => c.Title == "TestCourse"));
        }
    }
}
