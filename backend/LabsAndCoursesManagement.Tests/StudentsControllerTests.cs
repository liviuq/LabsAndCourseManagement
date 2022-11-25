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
            _db.Students.Add(new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500));
            _db.SaveChanges();


            var response = await _httpClient.GetAsync("api/Students");
            var content = await response.Content.ReadAsStringAsync();

            var students = JsonConvert.DeserializeObject<List<Student>>(content);
            Assert.IsTrue(students.Any(s => s.Email == "mockemail"));

            // ensure deleted
            _db.Students.RemoveRange(_db.Students);
        }
    }
}
