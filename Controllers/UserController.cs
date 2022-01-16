using Microsoft.AspNetCore.Mvc;
using ShipVista.Models;
using ShipVista.Filters;

namespace ShipVista.Controllers;

[ApiController]
[Route("api/user")]
[BasicAuthentication("admin", "admin@123", BasicRealm = "users_auth")]
public class UserController : ControllerBase {

    private IUserService _userService;

    public UserController(IUserService _userService){
        this._userService = _userService;
    }
    
    [HttpPost(Name = "Add User")]
    public APIResponse RegisterUsers(UserDO userDo){
        return _userService.addUser(userDo);
    }

    [HttpDelete(Name = "Delete User")]
    public APIResponse DeleteUser([FromQuery(Name = "user_email")] string user_id ) {
        return _userService.deleteUser(user_id);
    }

}