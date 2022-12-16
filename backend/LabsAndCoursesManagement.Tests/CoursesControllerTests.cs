using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;
using System.Text;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class CoursesControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task Get_CheckIfReturnsPostedCourse()
        {
            _db.Courses.Add(new Course("TestCourseGet", 1, 5));
            _db.SaveChanges();


            var response = await _httpClient.GetAsync("api/courses");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var courses = JsonConvert.DeserializeObject<List<Course>>(content);

            Assert.IsTrue(courses != null);
            Assert.IsTrue(courses.Any(c => c.Title == "TestCourseGet"));

            // ensure deleted
            _db.Courses.RemoveRange(_db.Courses);
        }

        [TestMethod]
        public async Task Post_CheckIfReturnsPostedCourse()
        {
            var course = new Course("TestCoursePost", 1, 5);
            var content = JsonConvert.SerializeObject(course);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/courses", stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var postedCourse = JsonConvert.DeserializeObject<Course>(responseContent);

            Assert.IsTrue(postedCourse != null);
            Assert.AreEqual(course.Title, postedCourse.Title);

            // ensure deleted
            _db.Courses.RemoveRange(_db.Courses);
        }

        [TestMethod]
        public async Task GetById_CheckIfReturnsPostedCourse()
        {
            _db.Courses.Add(new Course("TestCourseGetById", 1, 5));
            _db.SaveChanges();

            var course = _db.Courses.FirstOrDefault(c => c.Title == "TestCourseGetById");

            var response = await _httpClient.GetAsync($"api/courses/{course.Id}");
            var content = await response.Content.ReadAsStringAsync();

            var courseFromResponse = JsonConvert.DeserializeObject<Course>(content);

            Assert.IsTrue(courseFromResponse != null);
            Assert.IsTrue(courseFromResponse.Title == "TestCourseGetById");

            // ensure deleted
            _db.Courses.RemoveRange(_db.Courses);
        }
    }
}
