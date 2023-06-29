using api2.Commons;
using api2.Services;
using api2.Models.DB;
using api2.Models.General;
using api2.Models.INPUT;
using Library.Extension;
using Library.Functions;
using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using Microsoft.Win32;
using System;
using static api2.Models.General.LogHelper;

namespace api2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Dictionary<string, string> user = new Dictionary<string, string>
        {
            { "admin", "admin" },
            { "office_admin", "office" },
            { "sambar_admin", "sambar" },
            { "sambar_farm_admin", "farm" }
        };

        private readonly Dictionary<string, string> userRoles = new Dictionary<string, string>
        {
            { "admin", "Admin" },
            { "office_admin", "Office Administrator" },
            { "sambar_admin", "Sambar Administrator" },
            { "sambar_farm_admin", "Farm Administrator" }
        };

        [HttpPost("logic")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (user.TryGetValue(request.Username, out var password) && password == request.Password)
                {
                    var role = GetUserRole(request.Username);
                    if(!string.IsNullOrEmpty(role))
                    {
                        return Ok(new { Role = role });
                    }
                    else
                    {
                        return BadRequest("Invalid role.");
                    }
                }
                else
                {
                    return BadRequest("Invalid username or password. ");
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    [HttpPost("create-account")]
    public ActionResult CreateAccount([FromBody] CreateAccountRequest request)
    {
        try
        {
            if (!users.ContainsKey(request.Username))
            {
                users.Add(request.Username, request.Password);
                userRoles.Add(request.Username, request.Role);
                return Ok();
            }
            else
            {
                return BadRequest("Username already exists.");
            }
        }
        catch (Exception ex)
        {
          return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    [HttpPost("set-account-role")]
    public ActionResult SetAccountRole([FromBody] SetAccountRoleRequest request)
    {
        try
        {
            if (users.ContainsKey(request.Username))
            {
                userRoles[request.Username] = request.Role;
                return Ok();
            }
            else
            {
                return BadRequest("Username not found.");
            }
        }
        catch (Exception ex)
        {
          return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}