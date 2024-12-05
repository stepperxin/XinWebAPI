
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using global::XinWebAPI.Services.XinIdentity;
    using global::XinWebAPI.Models.XinIdentity;

    namespace XinWebAPI.Controllers.XinIdentity
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly JwtService _jwtService;
            private readonly ApiKeyService _apiKeyService;

            public UsersController(RoleManager<IdentityRole> roleManager,
                                    UserManager<IdentityUser> userManager,
                                    JwtService jwtService,
                                    ApiKeyService apiKeyService
            )
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _jwtService = jwtService;
                _apiKeyService = apiKeyService;
            }

            // POST: api/Users
            [HttpPost]
            public async Task<ActionResult<User>> PostUser(User user)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _userManager.CreateAsync(
                    new IdentityUser() { UserName = user.UserName, Email = user.Email },
                    user.Password
                );

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                user.Password = null;
                return CreatedAtAction("GetUser", new { username = user.UserName }, user);
            }

            // GET: api/Users/username
            [HttpGet("{username}")]
            public async Task<ActionResult<User>> GetUser(string username)
            {
                IdentityUser user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return NotFound();
                }

                return new User
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
            }

            // POST: api/Users/BearerToken
            [HttpPost("BearerToken")]
            public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Bad credentials");
                }

                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    return BadRequest("Bad credentials");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!isPasswordValid)
                {
                    return BadRequest("Bad credentials");
                }

                var token = _jwtService.CreateToken(user);

                return Ok(token);
            }

            // POST: api/Users/ApiKey
            [HttpPost("ApiKey")]
            public async Task<ActionResult> CreateApiKey(AuthenticationRequest request)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    return BadRequest("Bad credentials");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!isPasswordValid)
                {
                    return BadRequest("Bad credentials");
                }

                var token = _apiKeyService.CreateApiKey(user);

                return Ok(token);
            }
        }
    }


