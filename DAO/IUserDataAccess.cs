
namespace ShipVista.DAO;

using ShipVista.Models;

public interface IUserDataAccess{

   Boolean addUser(User user);

   Boolean deleteUser(String user);

    User getUserById(string email);

}