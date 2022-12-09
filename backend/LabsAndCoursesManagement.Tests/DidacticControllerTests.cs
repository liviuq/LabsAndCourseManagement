using System.Runtime.InteropServices.JavaScript;
using System.Text;
using LabsAndCoursesManagement.Domain;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    [TestClass]
    public class DidacticControllerTests : IntegrationTest
    {
        [TestMethod]
        public async Task DidacticController_CheckIfReturnsPostedCourse()
        {
            var course = new Course("Test Course", 4, 5);
            var teacher = new Teacher("Alice", "Smith", "teacher@college.com", "Math");
            
            
            var didactic = new Didactic();
            
            didactic.AttachDidacticToCourse(course);
            didactic.AttachDidacticToTeacher(teacher);
            
            _db.Courses.Add(course);
            _db.Teachers.Add(teacher);
            _db.Didactics.Add(didactic);
            _db.SaveChanges();
            
            var response = await _httpClient.GetAsync("api/didactic");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            // print content
            Console.WriteLine(content);
            
            var jsonresponse = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
            
            
            Assert.IsTrue(jsonresponse.Any(j => j["courseId"].ToString() == course.Id.ToString()));
            Assert.IsTrue(jsonresponse.Any(j => j["teacherId"].ToString() == teacher.Id.ToString()));
            
            // delete test data from db
            _db.Courses.Remove(course);
            _db.Teachers.Remove(teacher);
            _db.Didactics.Remove(didactic);
            _db.SaveChanges();
        }

        [TestMethod]
        public async Task DidacticController_CheckIfReturnsPostedDidactic()
        {
            var course = new Course("Test Course", 4, 5);
            var teacher = new Teacher("Alice", "Smith", "teacher@college.com", "Math");

            var didactic = new Didactic();

            didactic.AttachDidacticToCourse(course);
            didactic.AttachDidacticToTeacher(teacher);

            _db.Courses.Add(course);
            _db.Teachers.Add(teacher);
            _db.Didactics.Add(didactic);
            _db.SaveChanges();

            // post didactic together with its data
            var didacticJson = JsonConvert.SerializeObject(didactic);
            var didacticContent = new StringContent(didacticJson, Encoding.UTF8, "application/json");
            

            // template is [HttpPost("teacher/{teacherId:guid}/course/{courseId:guid}")]
            var response = await _httpClient.PostAsync($"api/didactic/teacher/{teacher.Id}/course/{course.Id}", didacticContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            // print content
            Console.WriteLine(content);

            var jsonresponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            Assert.IsTrue(jsonresponse["courseId"].ToString() == course.Id.ToString());
            Assert.IsTrue(jsonresponse["teacherId"].ToString() == teacher.Id.ToString());
            
            // delete test data from db
            _db.Courses.Remove(course);
            _db.Teachers.Remove(teacher);
            _db.Didactics.Remove(didactic);
            _db.SaveChanges();
        }
    }
}