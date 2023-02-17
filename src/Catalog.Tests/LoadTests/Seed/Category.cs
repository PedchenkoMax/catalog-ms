// ReSharper disable All

#pragma warning disable CS8604
#pragma warning disable CA2211

using static System.Guid;

namespace Catalog.Tests.LoadTests;

// 19 entries
public static class Categories
{
    static Categories() => All = new List<CategoryEntity> { ComputersAndLaptops, SmartphonesAndTablets, PhoneAccessories, TVsAndAudioVideoEquipment, CamerasAndPhotography, GamingConsolesAndGames, PrintersAndScanners, DataStorage, PCComponents, EBooksAndPeripherals, HeadphonesAndSpeakers, SecurityAndMonitoringSystems, ComputerGamesAndSoftware, KitchenAppliancesAndElectricals, ClimateControlEquipment, CarElectronics, ElectronicTools, HomeAppliances, SportsElectronics };
    
    public static readonly List<CategoryEntity> All;
    private const string blob = "blob.com/gl-survivors/categories";

    public static readonly CategoryEntity ComputersAndLaptops = new(NewGuid(), "Computers and Laptops", $"{blob}/ComputersAndLaptops.png");
    public static readonly CategoryEntity SmartphonesAndTablets = new(NewGuid(), "Smartphones and Tablets", $"{blob}/SmartphonesAndTablets.png");
    public static readonly CategoryEntity PhoneAccessories = new(NewGuid(), "Phone Accessories", $"{blob}/PhoneAccessories.png");
    public static readonly CategoryEntity TVsAndAudioVideoEquipment = new(NewGuid(), "TVs and Audio-Video Equipment", $"{blob}/TVsAndAudioVideoEquipment.png");
    public static readonly CategoryEntity CamerasAndPhotography = new(NewGuid(), "Cameras and Photography", $"{blob}/CamerasAndPhotography.png");
    public static readonly CategoryEntity GamingConsolesAndGames = new(NewGuid(), "Gaming Consoles and Games", $"{blob}/GamingConsolesAndGames.png");
    public static readonly CategoryEntity PrintersAndScanners = new(NewGuid(), "Printers and Scanners", $"{blob}/PrintersAndScanners.png");
    public static readonly CategoryEntity DataStorage = new(NewGuid(), "Data Storage", $"{blob}/DataStorage.png");
    public static readonly CategoryEntity PCComponents = new(NewGuid(), "PC Components", $"{blob}/PCComponents.png");
    public static readonly CategoryEntity EBooksAndPeripherals = new(NewGuid(), "EBooks and Peripherals", $"{blob}/EBooksAndPeripherals.png");
    public static readonly CategoryEntity HeadphonesAndSpeakers = new(NewGuid(), "Headphones and Speakers", $"{blob}/HeadphonesAndSpeakers.png");
    public static readonly CategoryEntity SecurityAndMonitoringSystems = new(NewGuid(), "Security and Monitoring Systems", $"{blob}/SecurityAndMonitoringSystems.png");
    public static readonly CategoryEntity ComputerGamesAndSoftware = new(NewGuid(), "Computer Games and Software", $"{blob}/ComputerGamesAndSoftware.png");
    public static readonly CategoryEntity KitchenAppliancesAndElectricals = new(NewGuid(), "Kitchen Appliances and Electricals", $"{blob}/KitchenAppliancesAndElectricals.png");
    public static readonly CategoryEntity ClimateControlEquipment = new(NewGuid(), "Climate Control Equipment", $"{blob}/ClimateControlEquipment.png");
    public static readonly CategoryEntity CarElectronics = new(NewGuid(), "Car Electronics", $"{blob}/CarElectronics.png");
    public static readonly CategoryEntity ElectronicTools = new(NewGuid(), "Electronic Tools", $"{blob}/ElectronicTools.png");
    public static readonly CategoryEntity HomeAppliances = new(NewGuid(), "Home Appliances", $"{blob}/HomeAppliances.png");
    public static readonly CategoryEntity SportsElectronics = new(NewGuid(), "Sports Electronics", $"{blob}/SportsElectronics.png");
}