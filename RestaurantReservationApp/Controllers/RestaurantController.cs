using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationApp.Dto;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;
using System.Security.Claims;

namespace RestaurantReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantRepository restaurantRepository, IUserRepository userRepository,IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // Create a new restaurant
        /// <summary>
        /// Create a new restaurant.
        /// </summary>
        /// <param name="restaurantCreateDto">The restaurant details.</param>
        /// <returns>The created restaurant.</returns>
        /// <response code="201">Returns the newly created restaurant.</response>
        /// <response code="400">If the restaurant is null or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(RestaurantDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RestaurantDto>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto)
        {
            if (restaurantCreateDto == null)
            {
                return BadRequest("Restaurant data is null.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in the token.");
            }

            var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);
            restaurant.OwnerId = userId;
            restaurant.Reviews = new List<Review>();
            restaurant.Owner = await _userRepository.GetByIdAsync(userId);
            restaurant.Reservations = new List<Reservation>();
            restaurant.Id = Guid.NewGuid().ToString();

            try
            {
                await _restaurantRepository.AddAsync(restaurant);
                var createdRestaurantDto = _mapper.Map<RestaurantDto>(restaurant);
                return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, createdRestaurantDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create restaurant. " + ex.Message);
            }
        }

        /// <summary>
        /// Get a restaurant by ID.
        /// </summary>
        /// <param name="id">The restaurant ID.</param>
        /// <returns>The restaurant details.</returns>
        /// <response code="200">Returns the restaurant details.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RestaurantDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(string id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantDto);
        }

        /// <summary>
        /// Get all owned Restaurants 
        /// </summary>
        /// <returns>The restaurant details.</returns>
        /// <response code="200">Returns the restaurant details.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(RestaurantDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RestaurantDto>> GetRestaurants()
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId == null)
            {
                return NotFound("User not found!");
            }

            var restaurants = await _restaurantRepository.GetAllByOwnerId(ownerId);
            var restaurantDtos = _mapper.Map<IEnumerable<RestaurantCreateDto>>(restaurants);

            if (restaurants.Any())
            {
                return Ok(restaurantDtos);
            }

            return NoContent();
        }

        // Update a restaurant
        /// <summary>
        /// Update a restaurant.
        /// </summary>
        /// <param name="id">The restaurant ID.</param>
        /// <param name="restaurantDto">The updated restaurant details.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the update is successful.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRestaurant(string id, RestaurantCreateDto restaurantCreateDto)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                return NotFound();
            }

            _mapper.Map(restaurantCreateDto, existingRestaurant);

            try
            {
                await _restaurantRepository.UpdateAsync(existingRestaurant);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update restaurant. " + ex.Message);
            }
        }

        // Delete a restaurant
        /// <summary>
        /// Delete a restaurant.
        /// </summary>
        /// <param name="id">The restaurant ID.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the delete is successful.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRestaurant(string id)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                return NotFound();
            }

            try
            {
                await _restaurantRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete restaurant. " + ex.Message);
            }
        }
    }
}
