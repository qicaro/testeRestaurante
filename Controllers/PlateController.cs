using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestaurant.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestRestaurant.Controllers
{
    [Route("api/plate")]
    public class PlateController : Controller
    {
        private readonly RestauranteContext _context;

        public PlateController(RestauranteContext context)
        {
            _context = context;
        }

        /// <summary>
        /// api/plate - Metodo GET
        /// Busca todos os pratos
        /// </summary>
        /// <returns>Retorna a lista de todos os pratos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plate>>> GetPlatesList()
        {
            if (await _context.Plates.CountAsync() > 0)
            {
                return await _context.Plates.Include("Restaurant").ToListAsync();
            }

            return NotFound();
        }

        /// <summary>
        /// api/plate/{ID} - Metodo GET
        /// Busca um prato de determinado ID
        /// </summary>
        /// <returns>Retorna um prato</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Plate>> GetPlate(long id)
        {
            var plateItem = await _context.Plates.FindAsync(id);

            if (plateItem == null)
            {
                return NotFound();
            }

            return plateItem;
        }

        /// <summary>
        /// api/plate/search/{STRING DE BUSCA} - Método POST
        /// Busca todos os pratos que atendem a string de busca
        /// </summary>
        /// <returns>Retorna a lista de todos os pratos encontrados</returns>
        [HttpGet("search/{value}")]
        public async Task<ActionResult<IEnumerable<Plate>>> GetPlateByName(string value)
        {
            var plateItems = await _context.Plates.Where(x => x.Name.Contains(value)).ToListAsync();

            if (plateItems == null)
            {
                return NotFound();
            }

            return plateItems;
        }

        /// <summary>
        /// api/plate/add - Metodo POST
        /// Cadastra um prato
        /// </summary>
        /// <param name="plate">Prato</param>
        /// <returns>Prato cadastrado</returns>
        [HttpPost("add")]
        public async Task<ActionResult<Plate>> AddPlate([FromBody]Plate plate)
        {
            _context.Plates.Add(plate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlate), new { id = plate.Id }, plate);
        }

        /// <summary>
        /// api/plate/edit/{ID} - Metodo POST
        /// edita o registro de um prato pelo id
        /// </summary>
        /// <param name="plate">Prato</param>
        /// <returns>Prato editado</returns>
        [HttpPost("edit/{id}")]
        public async Task<ActionResult<Plate>> EditPlate(long id, [FromBody]Plate plate)
        {

            if (id != plate.Id)
            {
                BadRequest();
            }

            //restaurantItem = restaurant;

            _context.Entry(plate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlate), new { id = plate.Id }, plate);
        }

        /// <summary>
        /// api/plate/remove/{ID} - Metodo POST
        /// Remove um prato pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("remove/{id}")]
        public async Task<ActionResult<Plate>> RemovePlate(long id)
        {
            var plateItem = await _context.Plates.FindAsync(id);

            if (plateItem == null)
            {
                return NotFound();
            }

            _context.Plates.Remove(plateItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

