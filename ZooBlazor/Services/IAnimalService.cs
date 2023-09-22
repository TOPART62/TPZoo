using ZooCore.Models;

namespace ZooBlazor.Services
{
    public interface IAnimalService
    {
        public Task<Animal?> Get(int id);
        public Task<List<Animal>> GetAll();
        public Task<bool> Post(Animal animal);
        public Task<bool> Put(Animal animal);
        public Task<bool> Delete(int id);
    }
}
