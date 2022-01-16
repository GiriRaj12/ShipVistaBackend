using ShipVista.Services;
using ShipVista.Models;
using ShipVista.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ShipVista.Controllers;


[ApiController]
[TypeFilter(typeof(UserActionsFilter))]
[Route("api/plant/water")]
public class WaterController {
    private IPlantService _iplantService;

    public WaterController(IPlantService _iplantService){
        this._iplantService = _iplantService;
    }
    [HttpPost(Name ="Water Plant")]
    public APIResponse WaterPlant(WaterPlantDO plant){
        return _iplantService.WaterPlant(plant).Result;
    }


}