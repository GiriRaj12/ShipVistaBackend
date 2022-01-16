using ShipVista.Services;
using ShipVista.Models;
using ShipVista.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ShipVista.Controllers;


[ApiController]
[Route("/user/login")]
public class LoginController {
    private IUserService _iuserService;

    public LoginController(IUserService _iuserService){
        this._iuserService = _iuserService;
    }
    [HttpPost(Name ="Login User")]
    public APIResponse LoginUser(LoginDO loginDO){
        return _iuserService.LoginUser(loginDO);
    }


}