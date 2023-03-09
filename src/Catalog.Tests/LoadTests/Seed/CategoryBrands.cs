using Catalog.Domain.Entities;
using static Catalog.Tests.LoadTests.Seed.Brands;
using static Catalog.Tests.LoadTests.Categories;

namespace Catalog.Tests.LoadTests.Seed;

public static class CategoryBrands
{
    public static readonly Dictionary<CategoryEntity, List<BrandEntity>> Dictionary = new()
    {
        { ComputersAndLaptops, new() { Dell, HP, Lenovo, Apple, Acer, ASUS, MSI, Samsung, LG, Sony, Toshiba, Fujitsu, Brands.Microsoft, Razer, Huawei, Xiaomi, Gigabyte, LG, Panasonic, NEC } },
        { SmartphonesAndTablets, new() { Apple, Samsung, Huawei, Xiaomi, Oppo, OnePlus, Motorola, Nokia, LG, Sony, HTC, ZTE, Lenovo, ASUS, Blackberry, Google, Meizu, Panasonic, Vivo, Alcatel } },
        { PhoneAccessories, new() { Apple, Samsung, Huawei, Xiaomi, Anker, Belkin, Spigen, Otterbox, UAG, Caseology, Mophie, Incipio, ESR, Aukey, Satechi, Nomad, Moment, Razer, Bose } },
        { TVsAndAudioVideoEquipment, new() { Samsung, LG, Sony, Panasonic, Philips, Sharp, TCL, Hisense, Toshiba, Vizio, JVC, Onkyo, Pioneer, Denon, Marantz, Bose, Sonos, HarmanKardon, Yamaha, PolkAudio } },
        { CamerasAndPhotography, new() { Canon, Nikon, Sony, Fujifilm, Panasonic, Olympus, Leica, Hasselblad, GoPro, DJI, Sigma, Tamron, Zeiss, Manfrotto, Benro, Gitzo, Lowepro, ThinkTank, MindShift, Joby } },
        { GamingConsolesAndGames, new() { SonyPlayStation, MicrosoftXbox, Nintendo, Sega, Atari, SNK, EA, Activision, Ubisoft, Bethesda, RockstarGames, SquareEnix, BlizzardEntertainment, ValveCorporation, GOG, Steam, HumbleBundle, EpicGames } },
        { PrintersAndScanners, new() { HP, Canon, Epson, Brother, Samsung, Xerox, Ricoh, Kyocera, Lexmark, Dell, KonicaMinolta, Fujitsu, Panasonic, ZebraTechnologies, Honeywell, Intermec, DatamaxONeil, MotorolaSolutions, Datalogic, CipherLab } },
        { DataStorage, new() { WesternDigital, Seagate, Toshiba, Kingston, SanDisk, Samsung, Crucial, Adata, Plextor, Corsair, Transcend, Lexar, LaCie, GTechnology, BuffaloTechnology, Synology, QNAP, Netgear, DLink, TPLink } },
        { PCComponents, new() { Intel, AMD, NVIDIA, ASUS, MSI, Gigabyte, ASRock, Biostar, Corsair, Kingston, Crucial, GSkill, WesternDigital, Seagate, Samsung, Toshiba, SilverStone, CoolerMaster, Thermaltake, Noctua } },
        { EBooksAndPeripherals, new() { AmazonKindle, AmazonKindle, Kobo, PocketBook, OnyxBoox, Likebook, Wacom, Logitech, Brands.Microsoft, HP, Canon, Epson, Brother, Samsung, LG, Asus, Acer } },
        { HeadphonesAndSpeakers, new() { Bose, Sennheiser, Sony, JBL, AKG, Beyerdynamic, AudioTechnica, Shure, HarmanKardon, Klipsch, PolkAudio, KEF, Focal, Sonos, Marshall, RHA, Pioneer, Skullcandy } },
        { SecurityAndMonitoringSystems, new() { Hikvision, Dahua, Honeywell, Bosch, AxisCommunications, FLIRSystems, Panasonic, Sony, Pelco, Vivotek, Samsung, Canon, Zmodo, Lorex, Swann, Amcrest, Annke, Reolink, Nest, Arlo } },
        { ComputerGamesAndSoftware, new() { Brands.Microsoft, Adobe, Autodesk, Corel, Symantec, Kaspersky, McAfee, Avast, ESET, Bitdefender, Malwarebytes, IObit, Acronis, Nero, CyberLink, WinZip, Steam, Origin, GOG } },
        { KitchenAppliancesAndElectricals, new() { Bosch, Electrolux, Whirlpool, Samsung, LG, Panasonic, Philips, Kenwood, Braun, KitchenAid, Cuisinart, HamiltonBeach, DeLonghi, Tefal, Moulinex, RussellHobbs, Breville, Sunbeam, Oster } },
        { ClimateControlEquipment, new() { Daikin, MitsubishiElectric, Panasonic, LG, Samsung, Carrier, Gree, Fujitsu, Hitachi, Sharp, Toshiba, Haier, Electrolux, Honeywell, Dyson, Vornado, Bionaire, DeLonghi, Philips, StadlerForm } },
        { CarElectronics, new() { Pioneer, Sony, Kenwood, JVC, Alpine, Clarion, Blaupunkt, Garmin, TomTom, Magellan, Cobra, Escort, Beltronics, Escort, K40, Uniden, Escort, BlackVue, Thinkware, Vantrue, Rexing } },
        { ElectronicTools, new() { DeWalt, Makita, Bosch, Milwaukee, Stanley, Ryobi, Hitachi, Hilti, Festool, PorterCable, Metabo, SKIL, Craftsman, Ridgid, Husqvarna, Dremel, IngersollRand, Snapon, Matco } },
        { HomeAppliances, new() { LG, Samsung, Bosch, Electrolux, Whirlpool, Panasonic, Philips, Sharp, Toshiba, Kenwood, Braun, Dyson, Miele, Bissell, Hoover, Shark, iRobot, Eureka, DirtDevil } },
        { SportsElectronics, new() { Garmin, Polar, Suunto, Fitbit, Apple, Samsung, Huawei, TomTom, Adidas, Nike, UnderArmour, GoPro, Sony, DJI, Yuneec, Parrot, Airwheel, Segway, Razor, Ninebot } }
    };
}