using Catalog.Domain.Entities;
using static Catalog.Tests.LoadTests.Categories;

namespace Catalog.Tests.LoadTests.Seed;

public static class ProductNameTagsPerCategory
{
    public static readonly Dictionary<CategoryEntity, List<string>> Dictionary = new()
    {
        { ComputersAndLaptops, new() { "Computer", "Laptop", "Ultrabook", "Transformer", "Desktop", "Gaming Laptop", "Notebook" } },
        { SmartphonesAndTablets, new() { "Smartphone", "Tablet" } },
        { PhoneAccessories, new() { "Case", "Screen protector", "Charger", "Wireless Charger", "Cable", "Power Bank" } },
        { TVsAndAudioVideoEquipment, new() { "Smart", "LED", "OLED", "4K", "Soundbar", "Blu-ray Player", "Projector", "Home Theater System" } },
        { CamerasAndPhotography, new() { "Camera", "Tripod", "Memory Card", "Lens", "Flash", "Studio Lighting", "Camera Bag", "Camera Strap" } },
        { GamingConsolesAndGames, new() { "GamingConsole", "Disc", "Game Controller", "VR Headset" } },
        { PrintersAndScanners, new() { "Inkjet", "Laser", "All-in-One", "Portable Printer", "Document Scanner", "Photo Scanner" } },
        { DataStorage, new() { "Hard Drive", "SSD", "USB Drive", "SD Card", "External Hard Drive", "Network Attached Storage" } },
        { PCComponents, new() { "Motherboard", "CPU", "GPU", "RAM", "Power Supply", "Cooling System", "Storage Drive" } },
        { EBooksAndPeripherals, new() { "E-reader", "Keyboard", "Mouse", "Stylus Pen", "Digital Writing Pad" } },
        { HeadphonesAndSpeakers, new() { "Wireless", "Noise-Cancelling", "In-Ear", "Over-Ear", "Bluetooth Speaker", "Gaming Headset", "Earbuds" } },
        { SecurityAndMonitoringSystems, new() { "Security Camera", "Doorbell Camera", "Smart Lock", "Motion Sensor", "Alarm System" } },
        { ComputerGamesAndSoftware, new() { "Operating System", "Office Suite", "Antivirus", "Photo Editing Software", "Video Editing Software", "Music Production Software" } },
        { KitchenAppliancesAndElectricals, new() { "Microwave", "Toaster", "Coffee Maker", "Blender", "Food Processor", "Air Fryer" } },
        { ClimateControlEquipment, new() { "Air Conditioner", "Heater", "Humidifier", "Dehumidifier", "Thermostat", "Air Purifier" } },
        { CarElectronics, new() { "Dash Cam", "GPS Navigator", "Car Audio System", "Backup Camera", "Car Alarm System", "Car Diagnostic Tool" } },
        { ElectronicTools, new() { "Multimeter", "Oscilloscope", "Soldering Iron", "Digital Caliper", "Heat Gun", "Crimping Tool" } },
        { HomeAppliances, new() { "Vacuum Cleaner", "Washer", "Dryer", "Dishwasher", "Refrigerator", "Oven" } },
        { SportsElectronics, new() { "Fitness Tracker", "GPS Watch", "Cycling Computer", "Smart Bike Trainer", "Action Camera", "Heart Rate Monitor" } }
    };
}