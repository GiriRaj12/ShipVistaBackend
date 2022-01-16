namespace ShipVista.DAO;
using ShipVista.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class PlantDataAcces : IPlantDataAccess { 

    private OfficeContext dbContext; 

    public PlantDataAcces(OfficeContext dbContex){
        this.dbContext = dbContex;
    }
    
    public List<Plant> getPlants(){
        try{
            List<Plant> list = this.dbContext.plants.ToList();
            Console.WriteLine("Got response" + list.Count);
            return list;
        } catch(Exception e){
            Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public Plant getPlantById(string id){
        return dbContext.plants.Find(id);
    }

    public async Task<Plant> updatePlant(Plant plant){
        var local = dbContext.Set<Plant>()
                    .Local
                    .FirstOrDefault(entry => entry.plant_id == plant.plant_id);

        if(local != null){
            dbContext.Entry(local).State = EntityState.Detached;
        }

        dbContext.Entry(plant).State = EntityState.Modified;

        dbContext.plants.Update(plant);

        dbContext.SaveChanges();

        return plant;

    }

    public async Task<Boolean> deletePlant(string plantId){
        var task = true;
        try{
            Plant plant = this.getPlantById(plantId);
            if(plant != null){
                dbContext.plants.Remove(plant);
                await dbContext.SaveChangesAsync();
                task = true;
            } else{
                task = false;
            } 
        } catch (Exception e){
            Console.WriteLine(e.StackTrace);
            task = false;
        }
        return task;
    }

    public async Task<Plant> createPlant(Plant plant){
        await dbContext.plants.AddAsync(plant);
        dbContext.SaveChanges();
        return plant;
    }

}