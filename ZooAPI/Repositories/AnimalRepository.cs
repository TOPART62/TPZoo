using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZooAPI.Datas;
using ZooCore.Models;

namespace ZooAPI.Repositories
{
    public class AnimalRepository: IRepository<Animal>
    {
        private ApplicationDbContext _dbContext;

        public AnimalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(Animal animal)
        {
            var added = await _dbContext.Animals.AddAsync(animal);
            await _dbContext.SaveChangesAsync();
            return added.Entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var animal = await GetById(id);
            if (animal == null)
                return false;
            _dbContext.Animals.Remove(animal);
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<Animal?> Get(Expression<Func<Animal, bool>> predicate)
        {
            return await _dbContext.Animals.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Animal>> GetAll()
        {
            return await _dbContext.Animals.ToListAsync();
        }

        public async Task<List<Animal>> GetAll(Expression<Func<Animal, bool>> predicate)
        {
            return await _dbContext.Animals.Where(predicate).ToListAsync();
        }

        public async Task<Animal?> GetById(int id)
        {
            return await Get(a => a.Id == id);
        }

        public async Task<bool> Update(Animal animal)
        {
            var animalToUpdate = await GetById(animal.Id);

            if (animalToUpdate == null)
                return false;

            if (animalToUpdate.Name != animal.Name)
                animalToUpdate.Name = animal.Name;
            if (animalToUpdate.Espece != animal.Espece)
                animalToUpdate.Espece = animal.Espece;
            if (animalToUpdate.Age != animal.Age)
                animalToUpdate.Age = animal.Age;
            if (animalToUpdate.Description != animal.Description)
                animalToUpdate.Description = animal.Description;
            if (animalToUpdate.ImageURL != animal.ImageURL)
                animalToUpdate.ImageURL = animal.ImageURL;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
