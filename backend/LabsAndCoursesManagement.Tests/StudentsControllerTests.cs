using System.Text;
using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class StudentsControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task StudentController_Get_ReturnsEmptyResponse()
        {
            _db.Students.Add(new Student("mockemail@email.com", "mockName", "mockLastName", 3, "2B4", 500));
            _db.SaveChanges();


            var response = await _httpClient.GetAsync("api/Students");
            var content = await response.Content.ReadAsStringAsync();

            var students = JsonConvert.DeserializeObject<List<Student>>(content);

            Assert.IsTrue(students != null);
            Assert.IsTrue(students.Any(s => s.Email == "mockemail@email.com"));

            // ensure deleted
            _db.Students.RemoveRange(_db.Students);
        }

        [TestMethod]
        public async Task StudentController_GetById_ReturnsEmptyResponse()
        {
            _db.Students.Add(new Student("mockemail@email.com", "mockName", "mockLastName", 3, "2B4", 500));
            _db.SaveChanges();

            var student = _db.Students.FirstOrDefault(s => s.Email == "mockemail@email.com");

            Assert.IsTrue(student != null);
            var response = await _httpClient.GetAsync($"api/Students/{student.Id}");
            var content = await response.Content.ReadAsStringAsync();

            var studentFromResponse = JsonConvert.DeserializeObject<Student>(content);

            Assert.IsTrue(studentFromResponse != null);
            Assert.IsTrue(studentFromResponse.Email == "mockemail@email.com");

            // ensure deleted
            _db.Students.RemoveRange(_db.Students);
        }

        [TestMethod]
        public async Task StudentController_Create_ReturnsEmptyResponse()
        {
            var student = new Student("mockemail@email.com", "mockName", "mockLastName", 3, "2B4", 500);

            var response = await _httpClient.PostAsync("api/Students", new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            var studentFromResponse = JsonConvert.DeserializeObject<Student>(content);

            Assert.IsTrue(studentFromResponse != null);
            Assert.IsTrue(studentFromResponse.Email == "mockemail@email.com");

            // ensure deleted
            _db.Students.RemoveRange(_db.Students);
        }
    }
}
