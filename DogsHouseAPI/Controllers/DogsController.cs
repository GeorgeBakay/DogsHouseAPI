using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using System.Text.Json;
using DogsHouseAPI.Models;
using Microsoft.EntityFrameworkCore;
using DogsHouseAPI.Date.Interfaces;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace DogsHouseAPI.Controllers
{
    
    [ApiController]
    [Route("/[action]")]
    public class DogsController : ControllerBase
    {
        private readonly IDog dogsRep;
        public DogsController(IDog dogsRep)
        {
    
            this.dogsRep = dogsRep;
        }
        

        [HttpGet]
        [ActionName("ping")]
        public ActionResult<string> Ping()
        {
            try
            {
                string name = "";
                string version = "";
                string json = System.IO.File.ReadAllText("appsettings.json");
                JObject appSettings= JObject.Parse(json);
                JToken? applicationVersionJSON = appSettings["ApplicationVersion"]; 
                if(applicationVersionJSON != null){
                    name = applicationVersionJSON["Name"] != null ? applicationVersionJSON["Name"].ToString() : "Error Name" ;
                    version = applicationVersionJSON["Version"] != null ? applicationVersionJSON["Version"].ToString() : "Error Version";
                    return Ok($"{name}, Versoin {version}");
                }
                return BadRequest("Cannot convert application version setting");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("dogs")]
        public async Task<ActionResult<List<Dog>>> Dogs(string? attribute = null, 
            string? order = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            try
            {
                List<Dog> dogs = await dogsRep.GetDogs(attribute, order, pageNumber, pageSize);
                return Ok(dogs);
            }
            catch
            {
                return BadRequest(new List<Dog>());
            }
        }
        [HttpPost]
        [ActionName("dog")]
        public async Task<ActionResult> AddDog([FromBody] Dog dog)
        {
            try
            {
                string result = await dogsRep.AddDog(dog);
                if(result == "succes")
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
       
    }
}
