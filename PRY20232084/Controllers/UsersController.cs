﻿using Microsoft.AspNetCore.Cors;
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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                    return Ok(new { message = "Usuario registrado exitosamente." });
                }

                return BadRequest(new { message = "Error al registrar el usuario." });
            }

            return BadRequest(new { message = "Datos de registro inválidos." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //return user name
                var user = _userManager.FindByNameAsync(model.Email).Result;

                var userModel = new UserModel();

                userModel.Id = user.Id;
                userModel.Email = user.Email;
                userModel.Name = user.Name;
                userModel.Phone = user.Phone;
                userModel.UserType = user.UserType;
                userModel.Enterprise = user.Enterprise;
                userModel.RegisterDate = user.RegisterDate;

                return Ok(userModel);

                //return Ok("Inicio de sesión exitoso.");
            }

            return BadRequest("Inicio de sesión fallido.");
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
