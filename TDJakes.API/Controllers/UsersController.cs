using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TDJakes.Business;
using TDJakes.Models.ViewModel;

namespace TDJakes.API.Controllers;

//[TDJakesAuthorize]
[Authorize(Policy = "TDJakesGroup:admin")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepo _userRepo;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserRepo userRepo, ILogger<UsersController> logger)
    {
        _userRepo = userRepo;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("Test message");
        return Ok(await _userRepo.GetUsers());
    }

    [HttpGet]
    [Route("{userEmail}")]
    public async Task<IActionResult> GetUser(string userEmail)
    {
        IEnumerable<UserProfile> users = await _userRepo.GetUserByEmail(userEmail);
        if (users.Any())
            return Ok(users);
        else
            return NoContent();
    }
}