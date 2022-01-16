namespace ShipVista.Constants;
using System.Text.Json;

public class Constants{
    public static string PLANT_TABLE = "Plants";
    public static string USERS_TABLE = "Users";

    public static string DB_PASSWORD = "d4psCUp94oeYvgONoODk6XTezeaBTBsz";

    public static string NO_LAST_VALUE = "NO_LAST_VALUE";

    public static string toJSON(Object json){
        return  JsonSerializer.Serialize(json);
    }

    public static T fromJson<T>(String json){
        return JsonSerializer.Deserialize<T>(json);
    }

}