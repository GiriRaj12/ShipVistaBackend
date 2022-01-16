namespace ShipVista.Models;
using System.ComponentModel.DataAnnotations;
public class User{

    [Key]
    public string user_id {get; set;}
    public string user_name {get ; set;}

    public string email {get; set;}

    public string user_password {get; set;}

}