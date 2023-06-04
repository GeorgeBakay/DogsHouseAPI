using DogsHouseAPI;

using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DogsHouseUTest
{
    [TestFixture]
    public class TestDogsController
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task AddDogTest()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/ping");

            // Assert
            Assert.IsNotNull(response);
           
        }

    }
}