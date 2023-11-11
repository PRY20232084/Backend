using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PRY20232084.Models;
using PRY20232084.Models.DTOs;

namespace PRY20232084.Controllers
{
    [EnableCors("InventoryManagement")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Phone = model.Phone,
                    UserType = model.UserType,
                    Enterprise = model.Enterprise, // Asigna el valor de Enterprise
                    RegisterDate = model.RegisterDate
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok("Usuario registrado exitosamente.");
                }

                return BadRequest("Error al registrar el usuario.");
            }

            return BadRequest("Datos de registro inválidos.");
        }

        [HttpGet("list")]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email
            }).ToList();

            return Ok(users);
        }
    }
}
