using LabsAndCoursesManagement.Domain;
using LabsAndCoursesManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace LabsAndCoursesManagement.Tests
{
    public class IntegrationTest
    {
        public readonly HttpClient _httpClient;
        public readonly DatabaseContext _db;
        public IntegrationTest()
        {
            // create the WebApp factory
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();

            // create db instance
            _db = new DatabaseContext();
            // ensure created and migration
            _db.Database.EnsureCreated();
        }
    }
}