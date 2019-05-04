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
    [Route("api/restaurant")]
    public class RestaurantController : Controller
    {

        private readonly RestauranteContext _context;

        public RestaurantController(RestauranteContext context)
        {
            _context = context;
        }

        /// <summary>
        /// api/restaurant - Metodo GET
        /// Busca todos os restaurantes
        /// </summary>
        /// <returns>Retorna a lista de todos os restaurantes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantsList()
        {
            if (await _context.Restaurants.CountAsync() > 0)
            {
                return await _context.Restaurants.ToListAsync();
            }

            return NotFound();
        }

        /// <summary>
        /// api/restaurant/{ID} - Metodo GET
        /// Busca um restaurante de determinado ID
        /// </summary>
        /// <returns>Retorna um restaurante</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(long id)
        {
            var restaurantItem = await _context.Restaurants.FindAsync(id);

            if (restaurantItem == null)
            {
                return NotFound();
            }

            return restaurantItem;
        }

        /// <summary>
        /// api/restaurante/search/{STRING DE BUSCA} - Método POST
        /// Busca todos os restaurantes que atendem a string de busca
        /// </summary>
        /// <returns>Retorna a lista de todos os restaurantes encontrados</returns>
        [HttpGet("search/{value}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantByName(string value)
        {
            var restaurantItems = await _context.Restaurants.Where(x => x.Name.Contains(value)).ToListAsync();

            if (restaurantItems == null)
            {
                return NotFound();
            }

            return restaurantItems;
        }

        /// <summary>
        /// api/restaurant/add - Metodo POST
        /// Cadastra um restaurante
        /// </summary>
        /// <param name="restaurant">Restaurante</param>
        /// <returns>restaurante cadastrado</returns>
        [HttpPost("add")]
        public async Task<ActionResult<Restaurant>> AddRestaurant([FromBody]Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }

        /// <summary>
        /// api/restaurate/edit/{ID} - Metodo POST
        /// edita o registro de um restaurante pelo id
        /// </summary>
        /// <param name="restaurate">Restaurante</param>
        /// <returns>Restaurate editado</returns>
        [HttpPost("edit/{id}")]
        public async Task<ActionResult<Restaurant>> EditRestaurant(long id, [FromBody]Restaurant restaurant)
        {

            if (id != restaurant.Id)
            {
                BadRequest();
            }

            //restaurantItem = restaurant;

            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }

        /// <summary>
        /// api/restaurant/remove/{ID} - Metodo POST
        /// Remove um restaurante pelo Id
        /// </summary>
        /// <returns></returns>
        [HttpPost("remove/{id}")]
        public async Task<ActionResult<Restaurant>> RemoveRestaurant(long id)
        {
            var restaurantItem = await _context.Restaurants.FindAsync(id);

            if (restaurantItem == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurantItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
