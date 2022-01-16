namespace ShipVista.Models;
using System.ComponentModel.DataAnnotations;

public class Plant{

    [Key]
    public string plant_id {get ; set;}

    public string plant_name {get; set;}

    public string plant_image_url {get; set;}

    public long created_time {get; set;}

    public long watered_time {get; set;}
    public string last_watered_by {get; set;} = null!;
        
}
