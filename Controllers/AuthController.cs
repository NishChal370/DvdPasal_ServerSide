using DvD_Api.Data;
using DvD_Api.DTO;
using DvD_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RopeyUserDto = DvD_Api.DTO.RopeyUserDto;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Models.RopeyUserDto> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(
            UserManager<Models.RopeyUserDto> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    userName = user.UserName,
                    userId = user.Id,
                    expiration = token.ValidTo
                });
            }

            return Unauthorized("Username or password incorrect.");
        }

        [HttpGet("getCurrentUser")]
        [Authorize]
        public async Task<object> GetCurrentUser(){
            var userName = User.Identity.Name;
            var currentUser = await _userManager.FindByNameAsync(userName);

            if(currentUser == null){
                return NotFound("User not found!");
            }

            return new {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Email = currentUser.Email,
                Gender = currentUser.Gender,
                DateOfBirth = currentUser.DateOfBirth.ToString("d")
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null) return Conflict("Username already exists!");

            var userByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userByEmail != null) return Conflict("Email already exists!");

            Models.RopeyUserDto newUser = new Models.RopeyUserDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),          // Changes when password changes. 
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed!");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.ASSISTANT))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.ASSISTANT));
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.ASSISTANT);

            return Ok("User created successfully.");
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.ADMIN)]
        [Route("changePasswordAdmin")]
        public async Task<IActionResult> ChangePasswordAdmin(PasswordChangeDto passwordChange)
        {
            var user = await _userManager.FindByIdAsync(passwordChange.UserId);
            if (user == null)
            {
                return NotFound("User not found!");
            }

            var passwordHash = _userManager.PasswordHasher.HashPassword(user, passwordChange.NewPassword);
            user.PasswordHash = passwordHash;

            await _userManager.UpdateAsync(user);

            return Ok("Password changed successfully");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<IEnumerable<object>> GetAllUsers()
        {

            var mUsers = await _userManager.GetUsersInRoleAsync(UserRoles.ASSISTANT);


             return mUsers.Select(u => new
             {
                 FirstName = u.FirstName,
                 LastName = u.LastName,
                 Email = u.Email,
                 UserId = u.Id,
                 DateOfBirth = u.DateOfBirth,
                 Gender = u.Gender,
             });
        }

        [HttpPost]
        [Authorize]
        [Route("changeUserInfo")]
        public async Task<IActionResult> ChangeUserInfo(UserInfoDto user)
        {
            var currentUserName = User.Identity.Name;
            var currentUser = await _userManager.FindByNameAsync(currentUserName);

            try
            {
                var getByEmail = await _userManager.FindByEmailAsync(user.Email);
                if (getByEmail != null && getByEmail.Id != currentUser.Id)
                {
                    return BadRequest("User with the given email already exists!");
                }

                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Email = user.Email;
                currentUser.Gender = user.Gender;

                currentUser.DateOfBirth = user.DateOfBirth;

                await _userManager.UpdateAsync(currentUser);

                return Ok();

            }
            catch (Exception)
            {

                return BadRequest("User Creation failed!");
            }

        }


        [HttpPost]
        [Route("changeUserInfoAdmin")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public async Task<IActionResult> ChangeUserInfoAdmin(RopeyUserDto user)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);

            if (currentUser == null)
            {
                return NotFound("User not found");
            }

            try
            {
                var getByEmail = await _userManager.FindByEmailAsync(user.Email);
                if (getByEmail != null && getByEmail.Id != currentUser.Id)
                {
                    return BadRequest("User with the given email already exists!");
                }

                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Email = user.Email;
                currentUser.Gender = user.Gender;

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    var passwordHash = _userManager.PasswordHasher.HashPassword(currentUser, user.Password);
                    currentUser.PasswordHash = passwordHash;
                }


                currentUser.DateOfBirth = user.DateOfBirth;

                await _userManager.UpdateAsync(currentUser);

                return Ok();

            }
            catch (Exception)
            {

                return BadRequest("User Creation failed!");
            }

        }

        [HttpPost]
        [Authorize]
        [Route("changeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(UserPasswordDto passwordChange)
        {

            var currentUserName = User.Identity.Name;

            var currentUser = _userManager.Users.FirstOrDefault(x => x.UserName == currentUserName);
            if (currentUser == null)
            {
                return NotFound("User not found!");
            }

            var oldPasswordHash = _userManager.PasswordHasher.HashPassword(currentUser, passwordChange.OldPassword);

            var passwordHash = _userManager.PasswordHasher.HashPassword(currentUser, passwordChange.NewPassword);

            var doesPasswordMatch = _userManager.PasswordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, passwordChange.OldPassword);

            if (doesPasswordMatch == PasswordVerificationResult.Failed)
            {
                return BadRequest("Old password do not match!");
            }

            currentUser.PasswordHash = passwordHash;

            await _userManager.UpdateAsync(currentUser);

            return Ok("Password changed successfully");
        }

        [HttpPost]
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT: ValidAudiance"],
                    claims: authClaims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }

    }
}
