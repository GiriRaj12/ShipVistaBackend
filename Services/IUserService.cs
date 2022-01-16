using ShipVista.Models;

public interface IUserService{

    APIResponse addUser(UserDO user);

    APIResponse deleteUser(string userId);

    APIResponse checkUserDetails(string email, string password);

    APIResponse LoginUser(LoginDO loginDO);

}