namespace ShipVista.DAO;

using ShipVista.Models;
public class UserDataAccess : IUserDataAccess{

    private OfficeContext dbContex;

    public UserDataAccess(OfficeContext dbContex){
        this.dbContex = dbContex;
    }

    public Boolean addUser(User user){
        try{
            this.dbContex.users.Add(user);
            this.dbContex.SaveChanges();
            return true;
        } catch(Exception e){
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    public User getUserById(string id){
        try{
            return dbContex.users.Where(user => user.email == (id)).FirstOrDefault();
        } catch(Exception e){
            return null;
        }
    }

    public Boolean deleteUser(String user_id){
        try{
            User user = this.getUserById(user_id);

            if(user != null){
                dbContex.Remove(user);
                dbContex.SaveChanges();
                return true;
            } else {
                return false;
            }
        } catch(Exception e){
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

}