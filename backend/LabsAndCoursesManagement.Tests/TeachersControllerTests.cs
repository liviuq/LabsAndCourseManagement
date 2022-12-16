using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class TeachersControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task TeacherController_Get_ReturnsEmptyResponse()
        {
            _db.Teachers.Add(new Teacher("John", "Doe", "mockemail", "PhD"));
            _db.SaveChanges();


            var response = await _httpClient.GetAsync("api/Teachers");
            var content = await response.Content.ReadAsStringAsync();

            var teachers = JsonConvert.DeserializeObject<List<Teacher>>(content);

            Assert.IsTrue(teachers != null);
            Assert.IsTrue(teachers.Any(t => t.Email == "mockemail"));

            // ensure deleted
            _db.Teachers.RemoveRange(_db.Teachers);
        }

        [TestMethod]
        public async Task TeacherController_GetById_ReturnsEmptyResponse()
        {
            _db.Teachers.Add(new Teacher("John", "Doe", "mockemail@email.com", "PhD"));
            _db.SaveChanges();

            var teacher = _db.Teachers.FirstOrDefault(t => t.Email == "mockemail@email.com");

            Assert.IsTrue(teacher != null);
            var response = await _httpClient.GetAsync($"api/Teachers/{teacher.Id}");
            var content = await response.Content.ReadAsStringAsync();

            var teacherFromResponse = JsonConvert.DeserializeObject<Teacher>(content);

            Assert.IsTrue(teacherFromResponse != null);
            Assert.IsTrue(teacherFromResponse.Email == "mockemail@email.com");

            // ensure deleted
            _db.Teachers.RemoveRange(_db.Teachers);
        }

        [TestMethod]
        public async Task TeacherController_Create_ReturnsEmptyResponse()
        {
            var teacher = new Teacher("John", "Doe", "mockemail@email.com", "PhD");

            var response = await _httpClient.PostAsync("api/Teachers", new StringContent(JsonConvert.SerializeObject(teacher), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            var teacherFromResponse = JsonConvert.DeserializeObject<Teacher>(content);

            Assert.IsTrue(teacherFromResponse != null);
            Assert.IsTrue(teacherFromResponse.Email == "mockemail@email.com");

            // ensure deleted
            _db.Teachers.RemoveRange(_db.Teachers);
        }
    }
}
