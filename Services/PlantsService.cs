using ShipVista.Models;
using ShipVista.DAO;

namespace ShipVista.Services;

public class PlantService : IPlantService {

    public  IPlantDataAccess _plantDataAcces;
    public PlantService(IPlantDataAccess plantDataAcces) : base(){
        this._plantDataAcces = plantDataAcces;
    }

    public APIResponse GetPlants(){
        List<Plant> list = _plantDataAcces.getPlants();

        if(list == null){
            return getAPIResponse(false, null);
        } else {
            return getAPIResponse(true, list);
        }
    }

    public async Task<APIResponse> AddPlant(PlantDO plantDo){

        Console.WriteLine("Into create plant ! ");
        
        if( !(plantDo != null && plantDo.plant_image_url != null && plantDo.plant_name != null)){
            return getAPIResponse(false, "Please Add plant image url and name");
        }

        Plant plant = convertDOToPlant(plantDo);
        Console.WriteLine("Into create " + plant.plant_id);
        
        plant.plant_id = Guid.NewGuid().ToString("P");
        plant.created_time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        plant.watered_time = 0;
        plant.last_watered_by = Constants.Constants.NO_LAST_VALUE;

        Plant result = await _plantDataAcces.createPlant(plant);
        return getAPIResponse(true, result);
    }

    public async Task<APIResponse> UpdatePlant(Plant plant){
        if(!(plant != null && plant.plant_image_url != null && plant.plant_name != null)){
            return getAPIResponse(false, "Please Create plant to update plant or something went wrong !");
        }

        Plant result = await _plantDataAcces.updatePlant(plant);
        return getAPIResponse(true, result);
        
    }

    public async Task<APIResponse> DeletePlant(String plant){

        if(plant == null){
            return getAPIResponse(false, "Please provide corrent plant ID");
        }

        Console.WriteLine("Plant ID" + plant);

        bool isDeleted = await _plantDataAcces.deletePlant(plant);
        return getAPIResponse(isDeleted, isDeleted ? "Done" : "Something Went Wrong");
    }

    public async Task<APIResponse> WaterPlant(WaterPlantDO waterPlantDO){
        long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        
        if(!(waterPlantDO != null && waterPlantDO.plant_id != null && waterPlantDO.watered_user != null)){
            return getAPIResponse(false, "BAD_REQUEST");
        }

        Plant plant = this._plantDataAcces.getPlantById(waterPlantDO.plant_id);

        
        if(plant.watered_time != 0 && !(this.getTargetTime(plant.watered_time) <= currentTime)){
            return getAPIResponse(false, "WATER_RESTING", plant.last_watered_by);
        }

        plant.watered_time = currentTime;

        plant.last_watered_by = Constants.Constants.toJSON(waterPlantDO.watered_user);

        Plant updatedPlant = await this._plantDataAcces.updatePlant(plant);

        if(updatedPlant != null)
            return getAPIResponse(true, updatedPlant);
        else 
            return getAPIResponse(false, "SOMETHING_WENT_WRONG");

    }

    private long getTargetTime(long time){
        return time + 40000;
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

    private Plant convertDOToPlant(PlantDO plantdo){
        Plant plant = new Plant();
        plant.plant_image_url = plantdo.plant_image_url;
        plant.plant_name = plantdo.plant_name;
        return plant;
    }

}