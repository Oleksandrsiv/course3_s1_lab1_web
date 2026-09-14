using course3_s1_lab1_web.Models;

namespace course3_s1_lab1_web.Data;

public static class DataStore
{
    public static List<Factory> Factories { get; set; } = new List<Factory>
    {
        new Factory { Id = 1, Name = "Subaru" },
        new Factory { Id = 2, Name = "Hyundai"},
        new Factory { Id = 3, Name = "Volkswagen" }
    };

    public static List<Car> Cars { get; set; } = new List<Car>
    {
        new Car { Id = 1, ModelName = "Impreza", FactoryId = 1 },
        new Car { Id = 2, ModelName = "Tucson", FactoryId = 2 },
        new Car { Id = 3, ModelName = "Golf", FactoryId = 3 }
    };
}