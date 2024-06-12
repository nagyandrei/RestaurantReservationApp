using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationApp.Dto;
using RestaurantReservationApp.Interface;
using RestaurantReservationApp.Models;

namespace RestaurantReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="400">If there is an error.</response>
        /// <response code="204">If there are no users</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            if (userDtos.Any())
            {
                return Ok(userDtos);
            }

            return NoContent();
        }

        /// <summary>
        /// Gets a specific user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user with the specified username.</returns>
        /// <response code="200">Returns the user.</response>
        /// <response code="404">If the user is not found.</response>
        /// <response code="400">If there is an error.</response>
        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            var user = await _userRepository.GetByIdAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        /// <summary>
        /// Updates a specific user by ID.
        /// </summary>
        /// <param name="username">The ID of the user.</param>
        /// <param name="userDto">The user details to update.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the user was updated successfully.</response>
        /// <response code="400">If the ID does not match or there is an error.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> PutUser(string username, UserDto userDto)
        {
            //if (username != userDto.Id)
            //{
            //    return BadRequest();
            //}

            var user = await _userRepository.GetByIdAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userDto, user);
            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The user details to create.</param>
        /// <returns>The created user.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="400">If there is an error.</response>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            // Check if a user with the same username already exists
            if (await _userRepository.ExistsByUsernameAsync(userDto.Name))
            {
                return BadRequest("Username already taken!");
            }

            if (await _userRepository.ExistsByEmailAddressAsync(userDto.Email))
            {
                return BadRequest("Email address already taken!");
            }

            var userRole = await _roleRepository.GetRoleByNameAsync("User");

            if (userRole == null)
            {
                return BadRequest("Default role 'User' not found in the database.");
            }

            var user = _mapper.Map<User>(userDto);

            user.RoleId = userRole.Id;

            user.Id = Guid.NewGuid().ToString();

            try
            {
                await _userRepository.AddAsync(user);

                var createdUserDto = _mapper.Map<UserDto>(user);

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, createdUserDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create user. "+ex.Message);
            }
        }

        /// <summary>
        /// Deletes a specific user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the user was deleted successfully.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
