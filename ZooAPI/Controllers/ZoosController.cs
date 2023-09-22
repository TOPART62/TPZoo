using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooAPI.Datas;
using ZooAPI.Helpers;
using ZooAPI.Repositories;
using ZooCore.Models;

namespace ZooAPI.Controllers
{
    [Route("api/animals")]
    [ApiController]
    //[Authorize(Roles = Constants.RoleAdmin)]
    public class ZoosController : ControllerBase
    {
        private readonly IRepository<Animal> _repository;

        public ZoosController(IRepository<Animal> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //[Authorize(Roles = Constants.RoleUser+","+Constants.RoleAdmin)]
        public async Task<IActionResult> Menu()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var animal = await _repository.GetById(id);

            if (animal == null)
                return NotFound(new{Message = "There is no Pokemon with this Id."});
            return Ok(animal);
        }
        [HttpGet("nom/{nom}")]
        public async Task<IActionResult> GetNom(string nom)
        {
            return Ok(await _repository.GetAll(a => a.Name.Contains(nom)));
        }

        [HttpGet("espece/{espece}")]
        public async Task<IActionResult> GetEspece(string espece)
        {
            try {
                Espece especeTmp = (Espece)Enum.Parse(typeof(Espece), espece);
                return Ok(await _repository.GetAll(a => a.Espece == especeTmp));
            }
            catch 
            {
                return BadRequest("This type is wrong !!!");
            }
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> AddAnimal([FromBody] Animal animal)
        {
            var animalId = await _repository.Add(animal);

            if (animalId > 1)
                return CreatedAtAction(nameof(Menu), "Animal added");

            return BadRequest("Something went wrong");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animal)
        {
            var aniTemp = await _repository.GetById(id);
            if (aniTemp == null)
                return BadRequest("Animal not found");

            animal.Id = id;
            if (await _repository.Update(animal)) 
                return Ok("Animal Updated !");

            return BadRequest("Something went wrong...");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> RemoveAnimal(int id)
        {
            var aniTemp = await _repository.GetById(id);
            if (aniTemp == null)
                return BadRequest("Animal not found");

            if (await _repository.Delete(id))
                return Ok("Animal Deleted !");

            return BadRequest("Something went wrong...");
        }
    }
}
