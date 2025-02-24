﻿using Microsoft.AspNetCore.Mvc;
using PM.Application.Interfaces;
using PM.Domain.Models.auths;

namespace AuthAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthLogic _authLogic;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthLogic authLogic, ILogger<AuthController> logger)
        {
            _authLogic = authLogic;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            try
            {
                var response = await _authLogic.Login(request);
                if (response == null)
                {
                    return BadRequest("Invalid email or password");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Login");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Login");
            }
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel request)
        {
            try
            {
                var response = await _authLogic.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Register");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Register");
            }
        }
        [HttpPost("log-out")]
        public async Task<IActionResult> LogOut(string token)
        {
            try
            {
                var response = await _authLogic.LogOut(token);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to log out: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                var response = await _authLogic.ForgotPassword(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to log out: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
 