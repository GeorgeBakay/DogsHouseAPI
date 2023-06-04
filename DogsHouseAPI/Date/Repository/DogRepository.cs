using DogsHouseAPI.Date.Interfaces;
using DogsHouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsHouseAPI.Date.Repository
{
    public class DogRepository : IDog
    {
        private readonly DataContext db;
        public DogRepository(DataContext db)
        {
            this.db = db;
        }
        public async Task<string> AddDog(Dog dog)
        {
            try
            {
                Dog? sameDogName = await db.dogs.Where(u => u.Name == dog.Name).FirstOrDefaultAsync();
                if (sameDogName == null)
                {
                    if (dog.Tail_length < 0)
                    {
                        return "The Tail length less then zero";
                    }
                    else if (dog.Weight < 0)
                    {
                        return "The waight less then zero, rotate the dog";
                    }
                    else if (dog.Name == "")
                    {
                        return "Please , Enter the name";
                    }
                    else if (dog.Color == "")
                    {
                        return "Please , Enter the color";
                    }
                    await db.dogs.AddAsync(new Dog()
                    {
                        Name = dog.Name,
                        Color = dog.Color,
                        Tail_length = dog.Tail_length,
                        Weight = dog.Weight,
                    });
                    await db.SaveChangesAsync();
                    return "Succes";
                }
                return "dog with this name already exist in data base, please enter other name for the dog";
            }
            catch(Exception ex)
            {
            
                return ex.Message;
            }
        }

        public async Task<List<Dog>> GetDogs(string? attribute = null, string? order = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Dog> result = db.dogs;
                if (attribute != null && order != null)
                {
                    switch (attribute.ToLower())
                    {
                        case "weight":
                            result = order.ToLower() == "desc" ? result.OrderByDescending(d => d.Weight)
                        : result.OrderBy(d => d.Weight);
                            break;
                        case "name":
                            result = order.ToLower() == "desc" ? result.OrderByDescending(d => d.Name)
                        : result.OrderBy(d => d.Name);
                            break;
                        case "color":
                            result = order.ToLower() == "desc" ? result.OrderByDescending(d => d.Color)
                        : result.OrderBy(d => d.Color);
                            break;
                        case "tail_length":
                            result = order.ToLower() == "desc" ? result.OrderByDescending(d => d.Tail_length)
                        : result.OrderBy(d => d.Tail_length);
                            break;
                    }
                }
                if (pageNumber != null && pageSize != null && pageNumber > 0 && pageSize > 0)
                {
                    var splitDogs = result.Skip(((int)pageNumber - 1) * (int)pageSize).Take((int)pageSize);
                    return await splitDogs.ToListAsync();
                }

                return await result.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Dog>();
            }
        }
    }
}
