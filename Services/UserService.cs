namespace ShipVista.Services;

using ShipVista.Models;
using ShipVista.DAO;

public class UserService : IUserService{

    private IUserDataAccess _iuserDataAccess;
    private ILogger<UserService> _ilogger;
    public UserService(IUserDataAccess _iuserDataAccess, ILogger<UserService> _ilogger){
        this._iuserDataAccess = _iuserDataAccess;
        this._ilogger = _ilogger;
    }

    public APIResponse addUser(UserDO userDO){
        try{
            if(!(userDO != null && userDO.email != null && userDO.user_password != null && userDO.user_name != null)){
                return getAPIResponse(false, "BAD_REQUEST");
            }

            User user = new User();
            user.user_id = Guid.NewGuid().ToString();
            user.email = userDO.email;
            user.user_name = userDO.user_name;
            user.user_password = encode(userDO.user_password);

            if(this.hasUser(user.email))
                return getAPIResponse(false, "USER_ALDREADY_EXISTS");

            Boolean addedUser = this._iuserDataAccess.addUser(user);
            if(addedUser)
                return getAPIResponse(true, "DONE", user);
            else 
                return getAPIResponse(false, "SOMETHING_WENT_WRONG");
        } catch(Exception e){
            Console.WriteLine(e.StackTrace);
            return getAPIResponse(false, "SOMETHING_WENT_WRONG");
        }

    }

    public Boolean hasUser(string userEmail){
        return this._iuserDataAccess.getUserById(userEmail) != null;
    }

    public APIResponse checkUserDetails(string email, string password){
        try{
            Console.WriteLine(email+":"+password);
            User user = _iuserDataAccess.getUserById(email);
            if(user != null){
                string decoded = decode(user.user_password);
                return this.getAPIResponse(decoded == password, "DONE");
            } else {
                return this.getAPIResponse(false, "Please Register !");
            }
        } catch(Exception e){
            Console.WriteLine(e.StackTrace);
            return  this.getAPIResponse(false, "Somthing went wrong!");;
        }
    }

    public APIResponse deleteUser(string userId){
        try{
            var response = _iuserDataAccess.deleteUser(userId);
            return getAPIResponse(response, response ? "Done" : "Something Went Wrong");
        } catch(Exception e){
            Console.WriteLine(e.StackTrace);
            return getAPIResponse(false, "Something Went Wrong !");
        }
    }

    public APIResponse LoginUser(LoginDO loginDO){
        return this.checkUserDetails(loginDO.email, loginDO.password);
    }

     private APIResponse getAPIResponse(bool isResponse, Object response, Object suggest = null){
        APIResponse aPIResponse = new APIResponse();
        aPIResponse.isResponse = isResponse;
        var responses = new Dictionary<String, Object>();
            responses["message"] = response;
            aPIResponse.response = responses;
            aPIResponse.suggest = suggest;
        return aPIResponse;
    }

    public string encode(string pass){
        return System.Convert.ToBase64String ( System.Text.Encoding.UTF8.GetBytes(pass));
    }

    public string decode(string pass){
        var decoded = System.Convert.FromBase64String(pass);
        string password =  System.Text.Encoding.UTF8.GetString(decoded);
        _ilogger.LogInformation(password);
        return password;
    }

}