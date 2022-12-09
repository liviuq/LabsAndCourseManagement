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
        
        [TestMethod]
        public async Task GradeController_CheckIfPutWorks()
        {
            var grade = new Grade(3, DateTime.Now, false, true);
            var student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            var course = new Course("Test Course", 4, 5);
            
            _db.Students.Add(student);
            _db.Courses.Add(course);

            grade.AttachGradeToCourse(course);
            grade.AttachGradeToStudent(student);

            _db.Grades.Add(grade);
            _db.SaveChanges();
             
            var modifiedGrade = new Grade(4, DateTime.Now, false, true);

            var response = await _httpClient.PutAsync($"api/Grades/{grade.Id}", new StringContent(JsonConvert.SerializeObject(modifiedGrade), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonresponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
            
            Assert.IsTrue(jsonresponse["value"].ToString() == modifiedGrade.Value.ToString());
            
            _db.Grades.Remove(grade);
            _db.SaveChanges();
        }
        
        [TestMethod]
        public async Task GradeController_CheckIfDeleteWorks()
        {
            var grade = new Grade(3, DateTime.Now, false, true);
            var student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            var course = new Course("Test Course", 4, 5);
            
            _db.Students.Add(student);
            _db.Courses.Add(course);
            
            grade.AttachGradeToCourse(course);
            grade.AttachGradeToStudent(student);
            
            _db.Grades.Add(grade);
            _db.SaveChanges();
            
            var response = await _httpClient.DeleteAsync($"api/Grades/{grade.Id}");
            response.EnsureSuccessStatusCode();

            _db.SaveChanges();

            // Check if the grade has been deleted from the database
            Assert.IsFalse(_db.Grades.Any(g => g.Id == grade.Id));
            
        }
        
        [TestMethod]
        public async Task GradeController_CheckIfGetByIdWorks()
        {
            var grade = new Grade(3, DateTime.Now, false, true);
            var student = new Student("mockemail", "mockName", "mockLastName", 3, "2B4", 500);
            var course = new Course("Test Course", 4, 5);
            
            _db.Students.Add(student);
            _db.Courses.Add(course);
            
            grade.AttachGradeToCourse(course);
            grade.AttachGradeToStudent(student);
            
            _db.Grades.Add(grade);
            _db.SaveChanges();
            
            var response = await _httpClient.GetAsync($"api/Grades/{grade.Id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonresponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);
            
            Assert.IsTrue(jsonresponse["id"].ToString() == grade.Id.ToString());
            
            _db.Grades.Remove(grade);
            _db.SaveChanges();
        }

    }
}