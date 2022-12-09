using System.Runtime.InteropServices.JavaScript;
using System.Text;
using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class GradeControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task GradeController_CheckIfGetWorks()
        {
            var grade = new Grade(3, DateTime.Now, false, true);
            var student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            var course = new Course("Test Course", 4, 5);
            
            grade.AttachGradeToCourse(course);
            grade.AttachGradeToStudent(student);
            
            _db.Students.Add(student);
            _db.Courses.Add(course);
            _db.Grades.Add(grade);
            _db.SaveChanges();
            
            var response = await _httpClient.GetAsync("/api/grades");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonresponse = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseString);

            Assert.IsTrue(jsonresponse.Any(j => j["id"].ToString() == grade.Id.ToString()));

            _db.Grades.Remove(grade);
            _db.Courses.Remove(course);
            _db.Students.Remove(student);
            _db.SaveChanges();
        }

        [TestMethod]
        public async Task GradeController_CheckIfPostWorks()
        {
            var grade = new Grade(3, DateTime.Now, false, true);
            var student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            var course = new Course("Test Course", 4, 5);
            
            _db.Students.Add(student);
            _db.Courses.Add(course);
            _db.SaveChanges();
            
            var response = await _httpClient.PostAsync($"api/Grades/student/{student.Id}/course/{course.Id}", new StringContent(JsonConvert.SerializeObject(grade), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonresponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
            
            Assert.IsTrue(jsonresponse["studentId"].ToString() == student.Id.ToString());
            Assert.IsTrue(jsonresponse["courseId"].ToString() == course.Id.ToString());
            
            _db.Courses.Remove(course);
            _db.Students.Remove(student);
            _db.SaveChanges();

        }

    }
}