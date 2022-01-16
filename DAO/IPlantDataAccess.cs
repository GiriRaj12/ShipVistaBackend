
namespace ShipVista.DAO;

using ShipVista.Models;

public interface IPlantDataAccess{

    List<Plant> getPlants();

    Plant getPlantById(string id);

    Task<Plant> updatePlant(Plant plant);

    Task<Boolean> deletePlant(string id);

    Task<Plant> createPlant(Plant plant);
    

}