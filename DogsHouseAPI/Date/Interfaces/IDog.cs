using DogsHouseAPI.Models;

namespace DogsHouseAPI.Date.Interfaces
{
    public interface IDog
    {
        //Add new Dog
        public Task<string> AddDog(Dog dog);
        //Get Dogs 
        public Task<List<Dog>> GetDogs(string? attribute = null,
            string? order = null,
            int? pageNumber = null,
            int? pageSize = null);
      
    }
}
