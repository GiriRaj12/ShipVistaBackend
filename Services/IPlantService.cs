namespace ShipVista.Services;
using ShipVista.Models;

public interface IPlantService{
    APIResponse GetPlants();

    Task<APIResponse> AddPlant(PlantDO plant);

    Task<APIResponse> UpdatePlant(Plant plant);

    Task<APIResponse> DeletePlant(String plant);

    Task<APIResponse> WaterPlant(WaterPlantDO waterPlant);
}