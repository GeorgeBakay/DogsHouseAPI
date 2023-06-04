using DogsHouseAPI;
using DogsHouseAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace DogsHouseTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestPing()
        {
            await using var application = new
            WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var response = await client.GetAsync("/ping");
            var data = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogs1()
        {
            //Arrange
            await using var application = new
            WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogsSortedByWaight1()
        {
            //Arrange
            await using var application = new
            WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs?attribute=weight&order=desc");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedResult = result.OrderByDescending(u => u.Weight).ToList();
            Assert.Equal(result,expextedResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogsSortedByWaight2()
        {
            await using var application = new
             WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs?attribute=weight&order=order");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedResult = result.OrderBy(u => u.Weight).ToList();
            Assert.Equal(result, expextedResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogsSortedByTail1()
        {
            await using var application = new
             WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs?attribute=tail_length&order=order");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedResult = result.OrderBy(u => u.Tail_length).ToList();
            Assert.Equal(result, expextedResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task TestDogsSortedByTail2()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs?attribute=tail_length&order=desc");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedResult = result.OrderByDescending(u => u.Tail_length).ToList();
            Assert.Equal(result, expextedResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogsSortedByName1()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs?attribute=name&order=desc");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedResult = result.OrderByDescending(u => u.Name).ToList();
            Assert.Equal(result, expextedResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogsSortedByName2()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var response = await client.GetAsync("/dogs?attribute=name&order=order");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedResult = result.OrderBy(u => u.Name).ToList();
            Assert.Equal(result, expextedResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogsSortedByWeightPage()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var allResponse = await client.GetAsync($"/dogs?attribute=wight&order=order");
            var allData = await allResponse.Content.ReadAsStringAsync();
            var allResult = JsonConvert.DeserializeObject<List<Dog>>(allData);
            int pageSize = 2;
            int pageNum = 1;
            //Act
            var response = await client.GetAsync($"/dogs?attribute=weight&order=order&pageNumber={pageNum}&pageSize={pageSize}");
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Dog>>(data);
            var expextedAllResult = allResult.OrderBy(u => u.Weight).ToList();
            var expextedSplitResult = expextedAllResult.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            Assert.True(result.SequenceEqual(expextedSplitResult));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogAdd()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            Dog dogToAdd = new Dog()
            {
                Name = "Lola",
                Color = "Orange",
                Tail_length = 10,
                Weight = 5
            };
            var json = JsonContent.Create(dogToAdd);
            //Act
            var response = await client.PostAsync($"/dog",json);
            //Assert
            var data = await response.Content.ReadAsStringAsync();
           

            Assert.True(data == "Succes" || data == "dog with this name already exist in data base, please enter other name for the dog");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogAddWithNull()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            Dog dogToAdd = new Dog()
            {
                Name = null,
                Color = "Orange",
                Tail_length = 10,
                Weight = 5
            };
            var json = JsonContent.Create(dogToAdd);
            //Act
            var response = await client.PostAsync($"/dog", json);
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task TestDogAddWithNegativTail()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            Dog dogToAdd = new Dog()
            {
                Name = "Miki",
                Color = "Orange",
                Tail_length = -10,
                Weight = 5
            };
            var json = JsonContent.Create(dogToAdd);
            //Act
            var response = await client.PostAsync($"/dog", json);
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            Assert.True(data == "The Tail length less then zero");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogAddWithNegativWeight()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            Dog dogToAdd = new Dog()
            {
                Name = "Miki",
                Color = "Orange",
                Tail_length = 10,
                Weight = -5
            };
            var json = JsonContent.Create(dogToAdd);
            //Act
            var response = await client.PostAsync($"/dog", json);
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            Assert.True(data == "The waight less then zero, rotate the dog");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogAddEmptyName()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            Dog dogToAdd = new Dog()
            {
                
                Color = "Orange",
                Tail_length = 10,
                Weight = 5
            };
            var json = JsonContent.Create(dogToAdd);
            //Act
            var response = await client.PostAsync($"/dog", json);
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            Assert.True(data == "Please , Enter the name");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestDogAddEmptyColor()
        {
            await using var application = new
              WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            Dog dogToAdd = new Dog()
            {
                Name = "Vilam",
                Color = "",
                Tail_length = 10,
                Weight = 5
            };
            var json = JsonContent.Create(dogToAdd);
            //Act
            var response = await client.PostAsync($"/dog", json);
            //Assert
            var data = await response.Content.ReadAsStringAsync();
            Assert.True(data == "Please , Enter the color");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}