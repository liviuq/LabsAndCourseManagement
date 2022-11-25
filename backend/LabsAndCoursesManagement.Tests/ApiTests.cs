using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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

            var response = await _httpClient.GetAsync("/api/Courses/396c3715-d4e9-4e6f-ade0-be9f61d4b058");
            var stringResult = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(response.StatusCode);
            // check if the contents of the json matches the ones we defined
            Assert.AreEqual(response.StatusCode, 200);
            //Assert.AreEqual(stringResult, " {\r\n  \"id\": \"396c3715-d4e9-4e6f-ade0-be9f61d4b058\",\r\n  \"title\": \"curs\",\r\n  \"semester\": 5,\r\n  \"credits\": 5\r\n}");
        }
    }
}