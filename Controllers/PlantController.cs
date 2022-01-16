using Microsoft.AspNetCore.Mvc;
using ShipVista.Models;
using ShipVista.Services;
using ShipVista.Filters;

namespace ShipVista.Controllers;

[ApiController]
[Route("api/plants")]
[TypeFilter(typeof(UserActionsFilter))]
public class PlantController : ControllerBase {

    private readonly IPlantService _plantService;
    public PlantController(IPlantService plantService){
        this._plantService = plantService;
    }

    [HttpGet(Name = "GetPlants")]
    public APIResponse GetPlants(){
        return _plantService.GetPlants();
    }

    [HttpPost(Name ="Add Plants")]
    public APIResponse AddPlant(PlantDO plant){
        return _plantService.AddPlant(plant).Result;
    }

    [HttpPut(Name ="Update Plant")]
    public APIResponse UpdatePlant(Plant plant){
        return _plantService.UpdatePlant(plant).Result;
    }

    [HttpDelete(Name ="Delete Plant")]
    public APIResponse DeletePlant([FromQuery(Name = "plant_id")] string plantId){
        return _plantService.DeletePlant(plantId).Result;
    }

}