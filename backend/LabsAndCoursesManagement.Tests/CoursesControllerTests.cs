using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class CoursesControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task Get_CheckIfReturnsPostedCourse()
        {
            _db.Courses.Add(new Course("TestCourse", 1, 5));
            _db.SaveChanges();


            var response = await _httpClient.GetAsync("api/courses");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var courses = JsonConvert.DeserializeObject<List<Course>>(content);
            Assert.IsTrue(courses.Any(c => c.Title == "TestCourse"));

            // ensure deleted
            _db.Courses.RemoveRange(_db.Courses);
        }
    }
}
