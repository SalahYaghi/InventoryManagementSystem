using Contract.Common.Interfaces;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Contacts.ContactInfo;
using Domain.Customer;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using Domain.Invoices;
using Domain.Orders;
using Domain.People;
using Domain.Products;
using Domain.Products.Category;
using Domain.Products.Enums;
using Domain.Suppliers;
using Domain.Suppliers.SupplierProducts;
using Domain.Warehouses;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.Intrinsics.X86;

namespace Infrastructure.Data;

public sealed class ApplicationDbContextInitialiser(
    AppDbContext context,
    ILogger<ApplicationDbContextInitialiser> logger,
    IHashingHelper hashingHelper)
{
    private const string SeedAdminUsername = "admin_salah";
    private const string SeedPassword = "Admin@12345";

    public async Task InitialiseAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (await context.Users.AnyAsync(
                    x => x.Username == SeedAdminUsername,
                    cancellationToken))
            {
                return;
            }

            await using var transaction =
                await context.Database.BeginTransactionAsync(cancellationToken);

            await SeedCountriesAsync(cancellationToken);
            await SeedCatalogAsync(cancellationToken);
            await SeedBusinessPartnersAsync(cancellationToken);
            await SeedInventoryAsync(cancellationToken);
            await SeedEmployeesAndUsersAsync(cancellationToken);
            await SeedOrdersAndInvoicesAsync(cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation("Development seed data was created successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedCountriesAsync(CancellationToken cancellationToken)
    {
        var palestine = Country.Create("Palestine").Value;
        var jordan = Country.Create("Jordan").Value;
        var egypt = Country.Create("Egypt").Value;

        var nablus = City.Create(
            Guid.NewGuid(),
            palestine.Id,
            "Nablus").Value;

        var ramallah = City.Create(
            Guid.NewGuid(),
            palestine.Id,
            "Ramallah").Value;

        var gaza = City.Create(
            Guid.NewGuid(),
            palestine.Id,
            "Gaza").Value;

        var amman = City.Create(
            Guid.NewGuid(),
            jordan.Id,
            "Amman").Value;

        var cairo = City.Create(
            Guid.NewGuid(),
            egypt.Id,
            "Cairo").Value;

        context.Countries.AddRange(
            palestine,
            jordan,
            egypt);

        context.Cities.AddRange(
            nablus,
            ramallah,
            gaza,
            amman,
            cairo);

        _locations = new SeedLocations(
            palestine,
            jordan,
            egypt,
            nablus,
            ramallah,
            gaza,
            amman,
            cairo);

        await Task.CompletedTask;
    }

    private async Task SeedCatalogAsync(CancellationToken cancellationToken)
    {
        var electronics = Category.Create(
            Guid.NewGuid(),
            "Electronics").Value;

        var tools = Category.Create(
            Guid.NewGuid(),
            "Tools").Value;

        var automotive = Category.Create(
            Guid.NewGuid(),
            "Automotive").Value;

        var safety = Category.Create(
            Guid.NewGuid(),
            "Safety").Value;

        var office = Category.Create(
            Guid.NewGuid(),
            "Office").Value;

        var products = new List<Product>
    {
        Product.Create(
            Guid.NewGuid(),
            "ELEC-001",
            "62810001",
            "USB-C Cable",
            "Braided USB-C charging and data cable",
            electronics.Id,
            8.50m,
            true,
            Unit.Piece).Value,

        Product.Create(
            Guid.NewGuid(),
            "ELEC-002",
            "62810002",
            "Power Adapter",
            "65W universal USB-C power adapter",
            electronics.Id,
            29.00m,
            true,
            Unit.Piece).Value
    };

        var electronicsProducts = new[]
        {
        ("ELEC-003", "62810003", "USB-C to USB-C Cable", "High-speed USB-C to USB-C cable", 9.50m),
        ("ELEC-004", "62810004", "USB-C to Lightning Cable", "USB-C to Lightning charging cable", 12.00m),
        ("ELEC-005", "62810005", "HDMI Cable 2m", "High-speed HDMI cable 2 meter", 7.50m),
        ("ELEC-006", "62810006", "HDMI Cable 5m", "High-speed HDMI cable 5 meter", 14.00m),
        ("ELEC-007", "62810007", "DisplayPort Cable", "DisplayPort video cable", 11.50m),
        ("ELEC-008", "62810008", "VGA Cable", "Standard VGA monitor cable", 6.00m),
        ("ELEC-009", "62810009", "DVI Cable", "Digital DVI display cable", 8.00m),
        ("ELEC-010", "62810010", "USB-A to USB-C Cable", "USB-A to USB-C charging cable", 6.50m),
        ("ELEC-011", "62810011", "USB Extension Cable", "USB 3.0 extension cable", 7.00m),
        ("ELEC-012", "62810012", "Ethernet Cable Cat6", "Cat6 network cable 3 meter", 5.50m),
        ("ELEC-013", "62810013", "Ethernet Cable Cat6 10m", "Cat6 network cable 10 meter", 12.50m),
        ("ELEC-014", "62810014", "Ethernet Cable Cat7", "Shielded Cat7 network cable", 10.50m),
        ("ELEC-015", "62810015", "USB Hub 4-Port", "Four-port USB 3.0 hub", 16.00m),
        ("ELEC-016", "62810016", "USB Hub 7-Port", "Seven-port powered USB hub", 27.00m),
        ("ELEC-017", "62810017", "USB-C Hub", "USB-C multiport adapter hub", 35.00m),
        ("ELEC-018", "62810018", "USB-C Docking Station", "Universal USB-C docking station", 89.00m),
        ("ELEC-019", "62810019", "Wireless Charger", "15W wireless charging pad", 18.00m),
        ("ELEC-020", "62810020", "Fast Wireless Charger", "25W fast wireless charging stand", 29.00m),

        ("ELEC-021", "62810021", "Power Bank 10000mAh", "Portable 10000mAh power bank", 24.00m),
        ("ELEC-022", "62810022", "Power Bank 20000mAh", "Portable 20000mAh power bank", 38.00m),
        ("ELEC-023", "62810023", "Power Bank 30000mAh", "High-capacity 30000mAh power bank", 52.00m),
        ("ELEC-024", "62810024", "USB Car Charger", "Dual-port USB car charger", 11.00m),
        ("ELEC-025", "62810025", "USB-C Car Charger", "45W USB-C car charger", 19.00m),
        ("ELEC-026", "62810026", "Universal Travel Adapter", "Universal international travel adapter", 22.00m),
        ("ELEC-027", "62810027", "Surge Protector 6-Port", "Six-outlet surge protector", 21.00m),
        ("ELEC-028", "62810028", "Surge Protector 10-Port", "Ten-outlet surge protector", 34.00m),
        ("ELEC-029", "62810029", "Extension Cord 5m", "Five meter electrical extension cord", 13.00m),
        ("ELEC-030", "62810030", "Extension Cord 10m", "Ten meter electrical extension cord", 22.00m),

        ("ELEC-031", "62810031", "Wireless Mouse", "2.4GHz wireless optical mouse", 15.00m),
        ("ELEC-032", "62810032", "Bluetooth Mouse", "Bluetooth ergonomic wireless mouse", 19.00m),
        ("ELEC-033", "62810033", "Gaming Mouse", "RGB gaming mouse with programmable buttons", 32.00m),
        ("ELEC-034", "62810034", "Mechanical Keyboard", "Mechanical USB keyboard", 48.00m),
        ("ELEC-035", "62810035", "Wireless Keyboard", "Wireless full-size keyboard", 25.00m),
        ("ELEC-036", "62810036", "Gaming Keyboard", "RGB mechanical gaming keyboard", 65.00m),
        ("ELEC-037", "62810037", "Keyboard and Mouse Combo", "Wireless keyboard and mouse combo", 34.00m),
        ("ELEC-038", "62810038", "USB Numeric Keypad", "External USB numeric keypad", 12.00m),
        ("ELEC-039", "62810039", "Webcam 1080p", "Full HD USB webcam", 42.00m),
        ("ELEC-040", "62810040", "Webcam 4K", "4K USB conference webcam", 89.00m),

        ("ELEC-041", "62810041", "Headset", "Stereo USB computer headset", 22.00m),
        ("ELEC-042", "62810042", "Gaming Headset", "Gaming headset with microphone", 45.00m),
        ("ELEC-043", "62810043", "Bluetooth Headphones", "Wireless Bluetooth over-ear headphones", 55.00m),
        ("ELEC-044", "62810044", "Wireless Earbuds", "True wireless Bluetooth earbuds", 39.00m),
        ("ELEC-045", "62810045", "Premium Earbuds", "Noise-isolating wireless earbuds", 69.00m),
        ("ELEC-046", "62810046", "USB Microphone", "USB condenser microphone", 58.00m),
        ("ELEC-047", "62810047", "Studio Microphone", "Professional condenser studio microphone", 125.00m),
        ("ELEC-048", "62810048", "Bluetooth Speaker", "Portable Bluetooth speaker", 35.00m),
        ("ELEC-049", "62810049", "Portable Speaker", "Compact waterproof portable speaker", 49.00m),
        ("ELEC-050", "62810050", "Soundbar", "Compact Bluetooth soundbar", 79.00m),

        ("ELEC-051", "62810051", "24-inch Monitor", "24-inch Full HD LED monitor", 125.00m),
        ("ELEC-052", "62810052", "27-inch Monitor", "27-inch Full HD LED monitor", 165.00m),
        ("ELEC-053", "62810053", "27-inch QHD Monitor", "27-inch QHD IPS monitor", 245.00m),
        ("ELEC-054", "62810054", "32-inch Monitor", "32-inch 4K UHD monitor", 345.00m),
        ("ELEC-055", "62810055", "Gaming Monitor 24", "24-inch 144Hz gaming monitor", 185.00m),
        ("ELEC-056", "62810056", "Gaming Monitor 27", "27-inch 165Hz gaming monitor", 265.00m),
        ("ELEC-057", "62810057", "Ultrawide Monitor", "34-inch ultrawide monitor", 395.00m),
        ("ELEC-058", "62810058", "Monitor Stand", "Adjustable desktop monitor stand", 32.00m),
        ("ELEC-059", "62810059", "Dual Monitor Arm", "Dual monitor adjustable arm", 65.00m),
        ("ELEC-060", "62810060", "HDMI Splitter", "1x2 HDMI splitter", 18.00m),

        ("ELEC-061", "62810061", "Laptop 14-inch", "14-inch business laptop", 620.00m),
        ("ELEC-062", "62810062", "Laptop 15-inch", "15.6-inch productivity laptop", 680.00m),
        ("ELEC-063", "62810063", "Laptop 16-inch", "16-inch performance laptop", 890.00m),
        ("ELEC-064", "62810064", "Gaming Laptop", "15.6-inch dedicated GPU gaming laptop", 1050.00m),
        ("ELEC-065", "62810065", "Business Laptop Pro", "Professional business laptop", 920.00m),
        ("ELEC-066", "62810066", "Ultrabook", "Lightweight premium ultrabook", 980.00m),
        ("ELEC-067", "62810067", "Chromebook", "14-inch Chromebook", 340.00m),
        ("ELEC-068", "62810068", "Laptop Stand", "Adjustable aluminum laptop stand", 28.00m),
        ("ELEC-069", "62810069", "Laptop Cooling Pad", "USB laptop cooling pad", 24.00m),
        ("ELEC-070", "62810070", "Laptop Sleeve 15", "Protective 15-inch laptop sleeve", 18.00m),

        ("ELEC-071", "62810071", "SSD 256GB", "2.5-inch 256GB SATA SSD", 28.00m),
        ("ELEC-072", "62810072", "SSD 512GB", "2.5-inch 512GB SATA SSD", 42.00m),
        ("ELEC-073", "62810073", "SSD 1TB", "1TB SATA SSD", 68.00m),
        ("ELEC-074", "62810074", "SSD 2TB", "2TB SATA SSD", 125.00m),
        ("ELEC-075", "62810075", "NVMe SSD 500GB", "500GB PCIe NVMe SSD", 45.00m),
        ("ELEC-076", "62810076", "NVMe SSD 1TB", "1TB PCIe NVMe SSD", 72.00m),
        ("ELEC-077", "62810077", "NVMe SSD 2TB", "2TB PCIe NVMe SSD", 135.00m),
        ("ELEC-078", "62810078", "External SSD 1TB", "Portable 1TB USB-C external SSD", 95.00m),
        ("ELEC-079", "62810079", "External SSD 2TB", "Portable 2TB USB-C external SSD", 165.00m),
        ("ELEC-080", "62810080", "External HDD 1TB", "Portable 1TB external hard drive", 52.00m),

        ("ELEC-081", "62810081", "External HDD 2TB", "Portable 2TB external hard drive", 65.00m),
        ("ELEC-082", "62810082", "External HDD 4TB", "Portable 4TB external hard drive", 105.00m),
        ("ELEC-083", "62810083", "USB Flash Drive 32GB", "32GB USB 3.0 flash drive", 7.50m),
        ("ELEC-084", "62810084", "USB Flash Drive 64GB", "64GB USB 3.0 flash drive", 10.00m),
        ("ELEC-085", "62810085", "USB Flash Drive 128GB", "128GB USB 3.0 flash drive", 16.00m),
        ("ELEC-086", "62810086", "USB Flash Drive 256GB", "256GB USB 3.0 flash drive", 28.00m),
        ("ELEC-087", "62810087", "Memory Card 64GB", "64GB microSD memory card", 9.00m),
        ("ELEC-088", "62810088", "Memory Card 128GB", "128GB microSD memory card", 15.00m),
        ("ELEC-089", "62810089", "Memory Card 256GB", "256GB microSD memory card", 27.00m),
        ("ELEC-090", "62810090", "Memory Card 512GB", "512GB microSD memory card", 55.00m),

        ("ELEC-091", "62810091", "RAM 8GB DDR4", "8GB DDR4 desktop memory module", 22.00m),
        ("ELEC-092", "62810092", "RAM 16GB DDR4", "16GB DDR4 desktop memory module", 38.00m),
        ("ELEC-093", "62810093", "RAM 32GB DDR4", "32GB DDR4 desktop memory module", 72.00m),
        ("ELEC-094", "62810094", "RAM 8GB DDR5", "8GB DDR5 memory module", 29.00m),
        ("ELEC-095", "62810095", "RAM 16GB DDR5", "16GB DDR5 memory module", 45.00m),
        ("ELEC-096", "62810096", "RAM 32GB DDR5", "32GB DDR5 memory module", 82.00m),
        ("ELEC-097", "62810097", "RAM 64GB DDR5 Kit", "64GB DDR5 desktop memory kit", 155.00m),
        ("ELEC-098", "62810098", "Laptop RAM 8GB", "8GB DDR4 laptop memory", 24.00m),
        ("ELEC-099", "62810099", "Laptop RAM 16GB", "16GB DDR4 laptop memory", 39.00m),
        ("ELEC-100", "62810100", "Laptop RAM 32GB", "32GB DDR5 laptop memory", 78.00m),

        ("ELEC-101", "62810101", "Wi-Fi Router AC1200", "Dual-band AC1200 wireless router", 38.00m),
        ("ELEC-102", "62810102", "Wi-Fi Router AX1800", "Wi-Fi 6 AX1800 wireless router", 59.00m),
        ("ELEC-103", "62810103", "Wi-Fi Router AX3000", "Wi-Fi 6 AX3000 wireless router", 89.00m),
        ("ELEC-104", "62810104", "Wi-Fi Mesh 2-Pack", "Dual-node Wi-Fi mesh system", 125.00m),
        ("ELEC-105", "62810105", "Wi-Fi Mesh 3-Pack", "Three-node Wi-Fi mesh system", 175.00m),
        ("ELEC-106", "62810106", "Wireless Access Point", "Dual-band wireless access point", 65.00m),
        ("ELEC-107", "62810107", "Wi-Fi USB Adapter", "USB wireless network adapter", 14.00m),
        ("ELEC-108", "62810108", "Wi-Fi 6 USB Adapter", "USB Wi-Fi 6 adapter", 27.00m),
        ("ELEC-109", "62810109", "Bluetooth USB Adapter", "USB Bluetooth adapter", 9.00m),
        ("ELEC-110", "62810110", "Gigabit Ethernet Adapter", "USB to Gigabit Ethernet adapter", 18.00m),

        ("ELEC-111", "62810111", "Network Switch 5-Port", "Five-port Gigabit Ethernet switch", 22.00m),
        ("ELEC-112", "62810112", "Network Switch 8-Port", "Eight-port Gigabit Ethernet switch", 29.00m),
        ("ELEC-113", "62810113", "Network Switch 16-Port", "Sixteen-port Gigabit Ethernet switch", 65.00m),
        ("ELEC-114", "62810114", "PoE Switch 8-Port", "Eight-port PoE network switch", 85.00m),
        ("ELEC-115", "62810115", "Network Patch Panel", "24-port Cat6 patch panel", 48.00m),
        ("ELEC-116", "62810116", "Wireless Repeater", "Dual-band Wi-Fi range extender", 32.00m),
        ("ELEC-117", "62810117", "4G LTE Router", "4G LTE wireless router", 95.00m),
        ("ELEC-118", "62810118", "5G Router", "5G cellular wireless router", 245.00m),
        ("ELEC-119", "62810119", "Network Card", "Gigabit PCIe network card", 25.00m),
        ("ELEC-120", "62810120", "USB Ethernet Adapter", "USB 3.0 Ethernet adapter", 15.00m),

        ("ELEC-121", "62810121", "Laser Printer", "Monochrome laser printer", 145.00m),
        ("ELEC-122", "62810122", "Color Laser Printer", "Color laser printer", 285.00m),
        ("ELEC-123", "62810123", "Inkjet Printer", "Wireless inkjet printer", 95.00m),
        ("ELEC-124", "62810124", "All-in-One Printer", "Wireless multifunction printer", 165.00m),
        ("ELEC-125", "62810125", "Photo Printer", "Compact photo printer", 125.00m),
        ("ELEC-126", "62810126", "Barcode Scanner", "USB barcode scanner", 48.00m),
        ("ELEC-127", "62810127", "Wireless Barcode Scanner", "Wireless barcode scanner", 75.00m),
        ("ELEC-128", "62810128", "Thermal Printer", "80mm thermal receipt printer", 85.00m),
        ("ELEC-129", "62810129", "Label Printer", "Desktop thermal label printer", 95.00m),
        ("ELEC-130", "62810130", "Document Scanner", "Automatic document scanner", 145.00m),

        ("ELEC-131", "62810131", "Smartphone Entry", "Budget Android smartphone", 145.00m),
        ("ELEC-132", "62810132", "Smartphone Midrange", "Midrange Android smartphone", 285.00m),
        ("ELEC-133", "62810133", "Smartphone Pro", "Premium Android smartphone", 625.00m),
        ("ELEC-134", "62810134", "Smartphone 5G", "5G Android smartphone", 425.00m),
        ("ELEC-135", "62810135", "Tablet 10-inch", "10-inch Android tablet", 195.00m),
        ("ELEC-136", "62810136", "Tablet 11-inch", "11-inch Android tablet", 285.00m),
        ("ELEC-137", "62810137", "Tablet Pro", "Professional 12-inch tablet", 495.00m),
        ("ELEC-138", "62810138", "E-Reader", "6-inch electronic reader", 115.00m),
        ("ELEC-139", "62810139", "Smartphone Tripod", "Adjustable smartphone tripod", 22.00m),
        ("ELEC-140", "62810140", "Phone Holder", "Adjustable desktop phone holder", 12.00m),

        ("ELEC-141", "62810141", "Smart Watch", "Bluetooth fitness smartwatch", 75.00m),
        ("ELEC-142", "62810142", "Smart Watch Pro", "Advanced GPS smartwatch", 165.00m),
        ("ELEC-143", "62810143", "Fitness Band", "Activity tracking fitness band", 39.00m),
        ("ELEC-144", "62810144", "Smart Plug", "Wi-Fi enabled smart plug", 14.00m),
        ("ELEC-145", "62810145", "Smart Bulb", "Wi-Fi RGB smart LED bulb", 12.00m),
        ("ELEC-146", "62810146", "Smart Light Strip", "RGB Wi-Fi LED light strip", 25.00m),
        ("ELEC-147", "62810147", "Smart Doorbell", "Wi-Fi video doorbell", 89.00m),
        ("ELEC-148", "62810148", "Smart Camera", "Indoor Wi-Fi security camera", 55.00m),
        ("ELEC-149", "62810149", "Outdoor Camera", "Weather-resistant Wi-Fi camera", 85.00m),
        ("ELEC-150", "62810150", "Smart Display", "Smart home display", 95.00m),

        ("ELEC-151", "62810151", "CPU Cooler", "Tower CPU air cooler", 38.00m),
        ("ELEC-152", "62810152", "CPU Cooler RGB", "RGB tower CPU cooler", 55.00m),
        ("ELEC-153", "62810153", "Case Fan 120mm", "120mm computer case fan", 9.00m),
        ("ELEC-154", "62810154", "Case Fan RGB", "120mm RGB computer case fan", 15.00m),
        ("ELEC-155", "62810155", "Computer Power Supply 500W", "500W ATX power supply", 48.00m),
        ("ELEC-156", "62810156", "Computer Power Supply 650W", "650W 80 Plus power supply", 69.00m),
        ("ELEC-157", "62810157", "Computer Power Supply 750W", "750W 80 Plus power supply", 89.00m),
        ("ELEC-158", "62810158", "Computer Case", "ATX mid-tower computer case", 65.00m),
        ("ELEC-159", "62810159", "Gaming Computer Case", "RGB gaming mid-tower case", 95.00m),
        ("ELEC-160", "62810160", "Motherboard AM4", "AM4 desktop motherboard", 95.00m),

        ("ELEC-161", "62810161", "Motherboard AM5", "AM5 DDR5 desktop motherboard", 165.00m),
        ("ELEC-162", "62810162", "Motherboard Intel", "Intel desktop motherboard", 145.00m),
        ("ELEC-163", "62810163", "Graphics Card 4GB", "4GB dedicated graphics card", 185.00m),
        ("ELEC-164", "62810164", "Graphics Card 8GB", "8GB dedicated graphics card", 315.00m),
        ("ELEC-165", "62810165", "Graphics Card 12GB", "12GB dedicated graphics card", 485.00m),
        ("ELEC-166", "62810166", "Graphics Card 16GB", "16GB dedicated graphics card", 625.00m),
        ("ELEC-167", "62810167", "Desktop CPU Entry", "Entry-level desktop processor", 95.00m),
        ("ELEC-168", "62810168", "Desktop CPU Midrange", "Midrange desktop processor", 185.00m),
        ("ELEC-169", "62810169", "Desktop CPU Performance", "High-performance desktop processor", 325.00m),
        ("ELEC-170", "62810170", "Laptop Dock", "Universal laptop docking station", 115.00m),

        ("ELEC-171", "62810171", "Digital Camera", "Compact digital camera", 285.00m),
        ("ELEC-172", "62810172", "Mirrorless Camera", "Entry-level mirrorless camera", 625.00m),
        ("ELEC-173", "62810173", "Camera Lens 35mm", "35mm camera lens", 285.00m),
        ("ELEC-174", "62810174", "Camera Lens 50mm", "50mm prime camera lens", 325.00m),
        ("ELEC-175", "62810175", "Camera Tripod", "Professional camera tripod", 65.00m),
        ("ELEC-176", "62810176", "Camera Memory Card", "128GB high-speed camera memory card", 22.00m),
        ("ELEC-177", "62810177", "Action Camera", "4K waterproof action camera", 165.00m),
        ("ELEC-178", "62810178", "Action Camera Mount", "Universal action camera mount kit", 25.00m),
        ("ELEC-179", "62810179", "Ring Light", "18-inch LED ring light", 45.00m),
        ("ELEC-180", "62810180", "LED Video Light", "Portable LED video light", 55.00m),

        ("ELEC-181", "62810181", "Projector", "Full HD multimedia projector", 385.00m),
        ("ELEC-182", "62810182", "Mini Projector", "Portable mini LED projector", 145.00m),
        ("ELEC-183", "62810183", "Projector Screen", "100-inch projection screen", 95.00m),
        ("ELEC-184", "62810184", "HDMI Wireless Adapter", "Wireless HDMI display adapter", 42.00m),
        ("ELEC-185", "62810185", "TV Box", "4K Android TV streaming box", 55.00m),
        ("ELEC-186", "62810186", "Streaming Stick", "4K streaming media stick", 45.00m),
        ("ELEC-187", "62810187", "Smart TV 43-inch", "43-inch 4K smart television", 385.00m),
        ("ELEC-188", "62810188", "Smart TV 55-inch", "55-inch 4K smart television", 575.00m),
        ("ELEC-189", "62810189", "Smart TV 65-inch", "65-inch 4K smart television", 795.00m),
        ("ELEC-190", "62810190", "TV Wall Mount", "Universal adjustable TV wall mount", 38.00m),

        ("ELEC-191", "62810191", "Digital Alarm Clock", "LED digital alarm clock", 18.00m),
        ("ELEC-192", "62810192", "Bluetooth FM Transmitter", "Bluetooth FM car transmitter", 15.00m),
        ("ELEC-193", "62810193", "Dash Camera", "Full HD vehicle dash camera", 65.00m),
        ("ELEC-194", "62810194", "GPS Tracker", "Compact GPS tracking device", 49.00m),
        ("ELEC-195", "62810195", "Portable SSD Case", "Protective external SSD case", 12.00m),
        ("ELEC-196", "62810196", "Hard Drive Enclosure", "USB 3.0 SATA drive enclosure", 15.00m),
        ("ELEC-197", "62810197", "M.2 SSD Enclosure", "USB-C NVMe SSD enclosure", 25.00m),
        ("ELEC-198", "62810198", "USB Sound Card", "External USB audio adapter", 14.00m),
        ("ELEC-199", "62810199", "USB Bluetooth Speaker", "Compact USB desktop speaker", 19.00m),
        ("ELEC-200", "62810200", "Desktop Speakers", "2.0 channel desktop speakers", 35.00m),

        ("ELEC-201", "62810201", "UPS 600VA", "600VA uninterruptible power supply", 75.00m),
        ("ELEC-202", "62810202", "UPS 1000VA", "1000VA uninterruptible power supply", 125.00m),
        ("ELEC-203", "62810203", "UPS 1500VA", "1500VA uninterruptible power supply", 185.00m),
        ("ELEC-204", "62810204", "Voltage Stabilizer", "Automatic electronic voltage stabilizer", 65.00m),
        ("ELEC-205", "62810205", "Electronic Multimeter", "Digital electronic multimeter", 35.00m)
    };

        products.AddRange(
            electronicsProducts.Select(product =>
                Product.Create(
                    Guid.NewGuid(),
                    product.Item1,
                    product.Item2,
                    product.Item3,
                    product.Item4,
                    electronics.Id,
                    product.Item5,
                    true,
                    Unit.Piece).Value));

        var automotiveProducts = new[]
{
    ("AUTO-001", "62830001", "Engine Oil 5W-30", "Full synthetic 5W-30 engine oil", 24.00m),
    ("AUTO-002", "62830002", "Brake Pads", "Front ceramic brake pad set", 55.00m),
    ("AUTO-003", "62830003", "Engine Oil 5W-40", "Full synthetic 5W-40 engine oil", 26.00m),
    ("AUTO-004", "62830004", "Engine Oil 10W-40", "Semi-synthetic 10W-40 engine oil", 21.00m),
    ("AUTO-005", "62830005", "Engine Oil 15W-40", "Heavy-duty 15W-40 engine oil", 19.00m),
    ("AUTO-006", "62830006", "Automatic Transmission Fluid", "ATF automatic transmission fluid", 18.00m),
    ("AUTO-007", "62830007", "Gear Oil 75W-90", "Synthetic 75W-90 gear oil", 22.00m),
    ("AUTO-008", "62830008", "Brake Fluid DOT3", "DOT3 hydraulic brake fluid", 8.00m),
    ("AUTO-009", "62830009", "Brake Fluid DOT4", "DOT4 hydraulic brake fluid", 10.00m),
    ("AUTO-010", "62830010", "Coolant 1L", "Ready-to-use engine coolant", 6.50m),

    ("AUTO-011", "62830011", "Coolant 5L", "Five-liter engine coolant", 21.00m),
    ("AUTO-012", "62830012", "Radiator Flush", "Cooling system radiator flush", 9.50m),
    ("AUTO-013", "62830013", "Windshield Washer Fluid", "Ready-to-use washer fluid", 5.00m),
    ("AUTO-014", "62830014", "Power Steering Fluid", "Hydraulic power steering fluid", 9.00m),
    ("AUTO-015", "62830015", "Engine Oil Filter", "Spin-on engine oil filter", 6.50m),
    ("AUTO-016", "62830016", "Air Filter", "Replacement engine air filter", 12.00m),
    ("AUTO-017", "62830017", "Cabin Air Filter", "Vehicle cabin air filter", 13.00m),
    ("AUTO-018", "62830018", "Fuel Filter", "Inline automotive fuel filter", 14.00m),
    ("AUTO-019", "62830019", "Transmission Filter", "Automatic transmission filter", 24.00m),
    ("AUTO-020", "62830020", "Performance Air Filter", "High-flow reusable air filter", 35.00m),

    ("AUTO-021", "62830021", "Front Brake Pads", "Front disc brake pad set", 55.00m),
    ("AUTO-022", "62830022", "Rear Brake Pads", "Rear disc brake pad set", 48.00m),
    ("AUTO-023", "62830023", "Brake Shoes", "Rear drum brake shoe set", 42.00m),
    ("AUTO-024", "62830024", "Brake Disc Front", "Front ventilated brake disc", 65.00m),
    ("AUTO-025", "62830025", "Brake Disc Rear", "Rear solid brake disc", 52.00m),
    ("AUTO-026", "62830026", "Brake Drum", "Rear brake drum", 48.00m),
    ("AUTO-027", "62830027", "Brake Caliper", "Front brake caliper assembly", 95.00m),
    ("AUTO-028", "62830028", "Brake Caliper Repair Kit", "Brake caliper seal repair kit", 18.00m),
    ("AUTO-029", "62830029", "Brake Hose", "Flexible hydraulic brake hose", 15.00m),
    ("AUTO-030", "62830030", "Brake Pad Wear Sensor", "Electronic brake pad wear sensor", 12.00m),

    ("AUTO-031", "62830031", "Car Battery 45Ah", "12V 45Ah automotive battery", 85.00m),
    ("AUTO-032", "62830032", "Car Battery 60Ah", "12V 60Ah automotive battery", 105.00m),
    ("AUTO-033", "62830033", "Car Battery 70Ah", "12V 70Ah automotive battery", 125.00m),
    ("AUTO-034", "62830034", "Car Battery 90Ah", "12V 90Ah automotive battery", 155.00m),
    ("AUTO-035", "62830035", "Battery Terminal", "Universal battery terminal pair", 7.00m),
    ("AUTO-036", "62830036", "Battery Cable", "Heavy-duty battery cable", 14.00m),
    ("AUTO-037", "62830037", "Battery Charger", "12V intelligent battery charger", 45.00m),
    ("AUTO-038", "62830038", "Jump Starter", "Portable automotive jump starter", 85.00m),
    ("AUTO-039", "62830039", "Alternator", "Replacement automotive alternator", 185.00m),
    ("AUTO-040", "62830040", "Starter Motor", "Replacement engine starter motor", 165.00m),

    ("AUTO-041", "62830041", "Spark Plug", "Standard automotive spark plug", 5.50m),
    ("AUTO-042", "62830042", "Iridium Spark Plug", "Long-life iridium spark plug", 14.00m),
    ("AUTO-043", "62830043", "Glow Plug", "Diesel engine glow plug", 12.00m),
    ("AUTO-044", "62830044", "Ignition Coil", "Automotive ignition coil", 38.00m),
    ("AUTO-045", "62830045", "Spark Plug Wire Set", "Engine ignition wire set", 32.00m),
    ("AUTO-046", "62830046", "Distributor Cap", "Ignition distributor cap", 18.00m),
    ("AUTO-047", "62830047", "Crankshaft Sensor", "Engine crankshaft position sensor", 28.00m),
    ("AUTO-048", "62830048", "Camshaft Sensor", "Engine camshaft position sensor", 26.00m),
    ("AUTO-049", "62830049", "Oxygen Sensor", "Universal oxygen sensor", 45.00m),
    ("AUTO-050", "62830050", "Mass Air Flow Sensor", "Mass airflow engine sensor", 65.00m),

    ("AUTO-051", "62830051", "Radiator", "Aluminum automotive radiator", 125.00m),
    ("AUTO-052", "62830052", "Radiator Fan", "Electric radiator cooling fan", 75.00m),
    ("AUTO-053", "62830053", "Radiator Hose Upper", "Upper radiator coolant hose", 14.00m),
    ("AUTO-054", "62830054", "Radiator Hose Lower", "Lower radiator coolant hose", 15.00m),
    ("AUTO-055", "62830055", "Thermostat", "Engine cooling thermostat", 18.00m),
    ("AUTO-056", "62830056", "Water Pump", "Engine water pump assembly", 55.00m),
    ("AUTO-057", "62830057", "Coolant Expansion Tank", "Engine coolant expansion reservoir", 28.00m),
    ("AUTO-058", "62830058", "Radiator Cap", "Pressurized radiator cap", 7.50m),
    ("AUTO-059", "62830059", "Cooling Fan Relay", "Cooling system fan relay", 11.00m),
    ("AUTO-060", "62830060", "Temperature Sensor", "Engine coolant temperature sensor", 16.00m),

    ("AUTO-061", "62830061", "Shock Absorber Front", "Front suspension shock absorber", 65.00m),
    ("AUTO-062", "62830062", "Shock Absorber Rear", "Rear suspension shock absorber", 58.00m),
    ("AUTO-063", "62830063", "Coil Spring Front", "Front suspension coil spring", 45.00m),
    ("AUTO-064", "62830064", "Coil Spring Rear", "Rear suspension coil spring", 42.00m),
    ("AUTO-065", "62830065", "Control Arm", "Front suspension control arm", 55.00m),
    ("AUTO-066", "62830066", "Ball Joint", "Lower suspension ball joint", 22.00m),
    ("AUTO-067", "62830067", "Tie Rod End", "Steering tie rod end", 18.00m),
    ("AUTO-068", "62830068", "Stabilizer Link", "Front stabilizer link", 15.00m),
    ("AUTO-069", "62830069", "Wheel Bearing", "Automotive wheel bearing", 32.00m),
    ("AUTO-070", "62830070", "CV Joint", "Constant velocity joint assembly", 65.00m),

    ("AUTO-071", "62830071", "Serpentine Belt", "Engine auxiliary drive belt", 18.00m),
    ("AUTO-072", "62830072", "Timing Belt", "Engine timing belt", 32.00m),
    ("AUTO-073", "62830073", "Timing Belt Kit", "Complete timing belt service kit", 85.00m),
    ("AUTO-074", "62830074", "Tensioner Pulley", "Drive belt tensioner pulley", 35.00m),
    ("AUTO-075", "62830075", "Idler Pulley", "Engine belt idler pulley", 28.00m),
    ("AUTO-076", "62830076", "Clutch Kit", "Complete manual transmission clutch kit", 185.00m),
    ("AUTO-077", "62830077", "Clutch Disc", "Manual transmission clutch disc", 75.00m),
    ("AUTO-078", "62830078", "Clutch Pressure Plate", "Clutch pressure plate assembly", 95.00m),
    ("AUTO-079", "62830079", "Clutch Release Bearing", "Clutch release bearing", 28.00m),
    ("AUTO-080", "62830080", "CV Axle", "Complete front CV axle assembly", 85.00m),

    ("AUTO-081", "62830081", "Headlight Bulb H4", "Halogen H4 headlight bulb", 8.00m),
    ("AUTO-082", "62830082", "Headlight Bulb H7", "Halogen H7 headlight bulb", 8.50m),
    ("AUTO-083", "62830083", "LED Headlight Bulb", "LED automotive headlight bulb pair", 32.00m),
    ("AUTO-084", "62830084", "Fog Light Bulb", "Automotive fog light bulb", 9.00m),
    ("AUTO-085", "62830085", "Tail Light Bulb", "Automotive tail light bulb", 5.00m),
    ("AUTO-086", "62830086", "Turn Signal Bulb", "Automotive indicator bulb", 4.50m),
    ("AUTO-087", "62830087", "LED Interior Light", "LED vehicle interior light kit", 15.00m),
    ("AUTO-088", "62830088", "Headlight Assembly", "Complete front headlight assembly", 145.00m),
    ("AUTO-089", "62830089", "Tail Light Assembly", "Complete rear tail light assembly", 95.00m),
    ("AUTO-090", "62830090", "LED Light Bar", "12V automotive LED light bar", 75.00m),

    ("AUTO-091", "62830091", "Windshield Wiper 16", "16-inch replacement wiper blade", 8.00m),
    ("AUTO-092", "62830092", "Windshield Wiper 18", "18-inch replacement wiper blade", 9.00m),
    ("AUTO-093", "62830093", "Windshield Wiper 20", "20-inch replacement wiper blade", 10.00m),
    ("AUTO-094", "62830094", "Windshield Wiper 22", "22-inch replacement wiper blade", 11.00m),
    ("AUTO-095", "62830095", "Wiper Blade Set", "Universal front wiper blade pair", 22.00m),
    ("AUTO-096", "62830096", "Windshield Washer Pump", "Electric washer fluid pump", 18.00m),
    ("AUTO-097", "62830097", "Wiper Motor", "Automotive windshield wiper motor", 65.00m),
    ("AUTO-098", "62830098", "Wiper Arm", "Replacement windshield wiper arm", 22.00m),
    ("AUTO-099", "62830099", "Windshield Repair Kit", "DIY windshield chip repair kit", 18.00m),
    ("AUTO-100", "62830100", "Glass Cleaner", "Automotive windshield glass cleaner", 7.00m)
};

        var automotiveCollection = automotiveProducts
            .Select(product =>
                Product.Create(
                    Guid.NewGuid(),
                    product.Item1,
                    product.Item2,
                    product.Item3,
                    product.Item4,
                    automotive.Id,
                    product.Item5,
                    true,
                    Unit.Piece).Value)
            .ToArray();

         
var toolsProducts = new[]
{
    ("TOOL-001", "62820001", "Drill Set", "Cordless professional drill set", 145.00m),
    ("TOOL-002", "62820002", "Socket Set", "Metric socket and ratchet set", 72.50m),
    ("TOOL-003", "62820003", "Cordless Drill", "18V cordless power drill", 95.00m),
    ("TOOL-004", "62820004", "Impact Driver", "18V cordless impact driver", 115.00m),
    ("TOOL-005", "62820005", "Impact Wrench", "Heavy-duty cordless impact wrench", 165.00m),
    ("TOOL-006", "62820006", "Hammer Drill", "Corded hammer drill", 85.00m),
    ("TOOL-007", "62820007", "Rotary Hammer", "Professional rotary hammer drill", 145.00m),
    ("TOOL-008", "62820008", "Angle Grinder", "115mm electric angle grinder", 65.00m),
    ("TOOL-009", "62820009", "Bench Grinder", "Double-wheel bench grinder", 95.00m),
    ("TOOL-010", "62820010", "Circular Saw", "Professional electric circular saw", 125.00m),

    ("TOOL-011", "62820011", "Jigsaw", "Variable-speed electric jigsaw", 75.00m),
    ("TOOL-012", "62820012", "Reciprocating Saw", "Cordless reciprocating saw", 135.00m),
    ("TOOL-013", "62820013", "Cut-Off Saw", "Metal cutting electric saw", 155.00m),
    ("TOOL-014", "62820014", "Heat Gun", "Two-speed electric heat gun", 35.00m),
    ("TOOL-015", "62820015", "Electric Sander", "Orbital electric sander", 58.00m),
    ("TOOL-016", "62820016", "Belt Sander", "Heavy-duty belt sander", 95.00m),
    ("TOOL-017", "62820017", "Polisher", "Variable-speed electric polisher", 85.00m),
    ("TOOL-018", "62820018", "Router", "Variable-speed wood router", 125.00m),
    ("TOOL-019", "62820019", "Planer", "Electric hand planer", 105.00m),
    ("TOOL-020", "62820020", "Electric Stapler", "Heavy-duty electric stapler", 45.00m),

    ("TOOL-021", "62820021", "Combination Wrench Set", "Metric combination wrench set", 48.00m),
    ("TOOL-022", "62820022", "Adjustable Wrench", "Chrome adjustable wrench", 12.00m),
    ("TOOL-023", "62820023", "Pipe Wrench", "Heavy-duty pipe wrench", 18.00m),
    ("TOOL-024", "62820024", "Torque Wrench", "Professional adjustable torque wrench", 65.00m),
    ("TOOL-025", "62820025", "Allen Key Set", "Metric hex key set", 15.00m),
    ("TOOL-026", "62820026", "Torx Key Set", "Torx wrench key set", 17.00m),
    ("TOOL-027", "62820027", "Screwdriver Set", "Professional screwdriver set", 28.00m),
    ("TOOL-028", "62820028", "Precision Screwdriver Set", "Precision electronics screwdriver set", 22.00m),
    ("TOOL-029", "62820029", "Ratchet Handle", "1/2-inch professional ratchet", 25.00m),
    ("TOOL-030", "62820030", "Socket Extension Set", "Metric socket extension set", 32.00m),

    ("TOOL-031", "62820031", "Combination Pliers", "Professional combination pliers", 16.00m),
    ("TOOL-032", "62820032", "Long Nose Pliers", "Long nose precision pliers", 14.00m),
    ("TOOL-033", "62820033", "Side Cutter", "Heavy-duty diagonal cutters", 18.00m),
    ("TOOL-034", "62820034", "Locking Pliers", "Adjustable locking pliers", 19.00m),
    ("TOOL-035", "62820035", "Water Pump Pliers", "Adjustable groove joint pliers", 17.00m),
    ("TOOL-036", "62820036", "Wire Stripper", "Automatic wire stripping tool", 21.00m),
    ("TOOL-037", "62820037", "Crimping Tool", "Electrical terminal crimping tool", 24.00m),
    ("TOOL-038", "62820038", "Cable Cutter", "Heavy-duty cable cutter", 29.00m),
    ("TOOL-039", "62820039", "Bolt Cutter", "Large heavy-duty bolt cutter", 35.00m),
    ("TOOL-040", "62820040", "Tin Snips", "Professional metal cutting snips", 18.00m),

    ("TOOL-041", "62820041", "Claw Hammer", "Professional claw hammer", 18.00m),
    ("TOOL-042", "62820042", "Sledge Hammer", "Heavy-duty sledge hammer", 35.00m),
    ("TOOL-043", "62820043", "Rubber Mallet", "Non-marking rubber mallet", 14.00m),
    ("TOOL-044", "62820044", "Dead Blow Hammer", "Professional dead blow hammer", 25.00m),
    ("TOOL-045", "62820045", "Hand Saw", "General-purpose hand saw", 16.00m),
    ("TOOL-046", "62820046", "Hacksaw", "Adjustable metal hacksaw", 12.00m),
    ("TOOL-047", "62820047", "Hacksaw Blade Pack", "Replacement hacksaw blades", 9.00m),
    ("TOOL-048", "62820048", "Wood Chisel Set", "Professional wood chisel set", 32.00m),
    ("TOOL-049", "62820049", "Cold Chisel Set", "Metalworking cold chisel set", 24.00m),
    ("TOOL-050", "62820050", "Punch Set", "Steel center punch set", 22.00m),

    ("TOOL-051", "62820051", "Tape Measure 5m", "5 meter professional tape measure", 10.00m),
    ("TOOL-052", "62820052", "Tape Measure 10m", "10 meter heavy-duty tape measure", 18.00m),
    ("TOOL-053", "62820053", "Spirit Level 40cm", "40cm aluminum spirit level", 14.00m),
    ("TOOL-054", "62820054", "Spirit Level 80cm", "80cm professional spirit level", 24.00m),
    ("TOOL-055", "62820055", "Laser Level", "Self-leveling laser level", 75.00m),
    ("TOOL-056", "62820056", "Digital Caliper", "150mm digital caliper", 32.00m),
    ("TOOL-057", "62820057", "Vernier Caliper", "150mm stainless steel caliper", 28.00m),
    ("TOOL-058", "62820058", "Measuring Wheel", "Professional distance measuring wheel", 35.00m),
    ("TOOL-059", "62820059", "Combination Square", "Adjustable combination square", 15.00m),
    ("TOOL-060", "62820060", "Plumb Bob", "Professional plumb bob", 8.00m),

    ("TOOL-061", "62820061", "Tool Box", "Medium professional toolbox", 45.00m),
    ("TOOL-062", "62820062", "Tool Chest", "Multi-drawer metal tool chest", 185.00m),
    ("TOOL-063", "62820063", "Tool Bag", "Heavy-duty technician tool bag", 38.00m),
    ("TOOL-064", "62820064", "Tool Organizer", "Portable tool organizer case", 28.00m),
    ("TOOL-065", "62820065", "Parts Organizer", "Multi-compartment parts organizer", 25.00m),
    ("TOOL-066", "62820066", "Workbench", "Heavy-duty workshop workbench", 285.00m),
    ("TOOL-067", "62820067", "Bench Vise", "Heavy-duty 4-inch bench vise", 85.00m),
    ("TOOL-068", "62820068", "Pipe Vise", "Workshop pipe vise", 75.00m),
    ("TOOL-069", "62820069", "Clamp Set", "Assorted woodworking clamp set", 35.00m),
    ("TOOL-070", "62820070", "Quick Grip Clamp", "Quick-release grip clamp", 15.00m),

    ("TOOL-071", "62820071", "Drill Bit Set", "High-speed steel drill bit set", 28.00m),
    ("TOOL-072", "62820072", "Masonry Drill Set", "Concrete and masonry drill set", 25.00m),
    ("TOOL-073", "62820073", "Wood Drill Set", "Wood drilling bit set", 22.00m),
    ("TOOL-074", "62820074", "Hole Saw Set", "Bi-metal hole saw set", 42.00m),
    ("TOOL-075", "62820075", "Screwdriver Bit Set", "Impact screwdriver bit set", 25.00m),
    ("TOOL-076", "62820076", "Impact Socket Set", "Heavy-duty impact socket set", 65.00m),
    ("TOOL-077", "62820077", "Spark Plug Socket Set", "Automotive spark plug socket set", 32.00m),
    ("TOOL-078", "62820078", "Hex Bit Set", "Professional hex bit set", 28.00m),
    ("TOOL-079", "62820079", "Grinding Disc", "115mm metal grinding discs", 12.00m),
    ("TOOL-080", "62820080", "Cutting Disc", "115mm metal cutting discs", 10.00m),

    ("TOOL-081", "62820081", "Jack Stand", "Heavy-duty vehicle jack stand", 35.00m),
    ("TOOL-082", "62820082", "Hydraulic Jack", "2-ton hydraulic floor jack", 95.00m),
    ("TOOL-083", "62820083", "Bottle Jack", "5-ton hydraulic bottle jack", 48.00m),
    ("TOOL-084", "62820084", "Workshop Creeper", "Low-profile mechanic creeper", 55.00m),
    ("TOOL-085", "62820085", "Mechanic Stool", "Adjustable workshop stool", 45.00m),
    ("TOOL-086", "62820086", "Inspection Mirror", "Telescopic inspection mirror", 12.00m),
    ("TOOL-087", "62820087", "Magnetic Pickup Tool", "Telescopic magnetic pickup tool", 9.00m),
    ("TOOL-088", "62820088", "LED Work Light", "Rechargeable LED work light", 28.00m),
    ("TOOL-089", "62820089", "Work Light Tripod", "Adjustable work light stand", 42.00m),
    ("TOOL-090", "62820090", "Extension Reel", "20-meter workshop extension reel", 55.00m),

    ("TOOL-091", "62820091", "Air Compressor", "50-liter workshop air compressor", 325.00m),
    ("TOOL-092", "62820092", "Air Hose", "10-meter compressed air hose", 25.00m),
    ("TOOL-093", "62820093", "Air Blow Gun", "Compressed air blow gun", 12.00m),
    ("TOOL-094", "62820094", "Air Impact Wrench", "Pneumatic impact wrench", 125.00m),
    ("TOOL-095", "62820095", "Air Ratchet", "Pneumatic ratchet wrench", 85.00m),
    ("TOOL-096", "62820096", "Grease Gun", "Manual workshop grease gun", 35.00m),
    ("TOOL-097", "62820097", "Caulking Gun", "Heavy-duty caulking gun", 14.00m),
    ("TOOL-098", "62820098", "Utility Knife", "Professional retractable utility knife", 8.00m),
    ("TOOL-099", "62820099", "Utility Blade Pack", "Replacement utility knife blades", 6.00m),
    ("TOOL-100", "62820100", "Tool Sharpening Stone", "Professional sharpening stone", 15.00m)
};
         
        var safetyProducts = new[]
        {
    ("SAFE-001", "62840001", "Safety Gloves", "Industrial protective gloves", 6.75m),
    ("SAFE-002", "62840002", "Cut Resistant Gloves", "Level 5 cut resistant work gloves", 12.00m),
    ("SAFE-003", "62840003", "Nitrile Gloves", "Disposable nitrile protective gloves", 9.00m),
    ("SAFE-004", "62840004", "Latex Gloves", "Disposable latex protective gloves", 7.50m),
    ("SAFE-005", "62840005", "Chemical Resistant Gloves", "Chemical-resistant protective gloves", 15.00m),
    ("SAFE-006", "62840006", "Welding Gloves", "Heat-resistant welding gloves", 18.00m),
    ("SAFE-007", "62840007", "Heat Resistant Gloves", "High-temperature work gloves", 16.00m),
    ("SAFE-008", "62840008", "Electrical Gloves", "Insulated electrical safety gloves", 28.00m),
    ("SAFE-009", "62840009", "Safety Helmet", "Industrial protective safety helmet", 14.00m),
    ("SAFE-010", "62840010", "Ventilated Safety Helmet", "Ventilated industrial safety helmet", 18.00m),

    ("SAFE-011", "62840011", "Safety Goggles", "Clear industrial safety goggles", 8.00m),
    ("SAFE-012", "62840012", "Chemical Goggles", "Chemical splash protective goggles", 12.00m),
    ("SAFE-013", "62840013", "Welding Goggles", "Welding protection goggles", 15.00m),
    ("SAFE-014", "62840014", "Face Shield", "Full-face protective shield", 18.00m),
    ("SAFE-015", "62840015", "Welding Helmet", "Auto-darkening welding helmet", 65.00m),
    ("SAFE-016", "62840016", "Dust Mask", "Disposable dust protection mask", 5.00m),
    ("SAFE-017", "62840017", "Respirator Mask", "Reusable industrial respirator", 28.00m),
    ("SAFE-018", "62840018", "Respirator Filter", "Replacement respirator filter", 9.00m),
    ("SAFE-019", "62840019", "Ear Plugs", "Disposable hearing protection ear plugs", 4.00m),
    ("SAFE-020", "62840020", "Ear Muffs", "Industrial hearing protection earmuffs", 18.00m),

    ("SAFE-021", "62840021", "Safety Vest", "High-visibility reflective safety vest", 9.00m),
    ("SAFE-022", "62840022", "Reflective Jacket", "High-visibility reflective jacket", 25.00m),
    ("SAFE-023", "62840023", "Work Jacket", "Industrial protective work jacket", 35.00m),
    ("SAFE-024", "62840024", "Work Coveralls", "Industrial protective coveralls", 42.00m),
    ("SAFE-025", "62840025", "Disposable Coveralls", "Disposable protective coveralls", 18.00m),
    ("SAFE-026", "62840026", "Chemical Suit", "Chemical-resistant protective suit", 55.00m),
    ("SAFE-027", "62840027", "Rain Safety Suit", "Industrial waterproof safety suit", 38.00m),
    ("SAFE-028", "62840028", "Work Apron", "Heavy-duty protective work apron", 16.00m),
    ("SAFE-029", "62840029", "Welding Apron", "Leather welding apron", 28.00m),
    ("SAFE-030", "62840030", "Heat Resistant Sleeve", "Protective heat-resistant arm sleeve", 15.00m),

    ("SAFE-031", "62840031", "Safety Boots", "Steel toe industrial safety boots", 55.00m),
    ("SAFE-032", "62840032", "Steel Toe Shoes", "Steel toe protective work shoes", 48.00m),
    ("SAFE-033", "62840033", "Slip Resistant Shoes", "Industrial slip-resistant shoes", 52.00m),
    ("SAFE-034", "62840034", "Chemical Resistant Boots", "Chemical-resistant safety boots", 65.00m),
    ("SAFE-035", "62840035", "Rubber Safety Boots", "Waterproof industrial rubber boots", 35.00m),
    ("SAFE-036", "62840036", "Knee Pads", "Heavy-duty industrial knee pads", 18.00m),
    ("SAFE-037", "62840037", "Elbow Pads", "Industrial protective elbow pads", 15.00m),
    ("SAFE-038", "62840038", "Back Support Belt", "Adjustable industrial back support belt", 22.00m),
    ("SAFE-039", "62840039", "Wrist Support", "Industrial wrist support brace", 12.00m),
    ("SAFE-040", "62840040", "Safety Harness", "Full-body fall protection harness", 75.00m),

    ("SAFE-041", "62840041", "Fall Arrest Lanyard", "Double-leg fall arrest lanyard", 45.00m),
    ("SAFE-042", "62840042", "Shock Absorbing Lanyard", "Energy absorbing safety lanyard", 55.00m),
    ("SAFE-043", "62840043", "Rope Lifeline", "Industrial safety rope lifeline", 65.00m),
    ("SAFE-044", "62840044", "Anchor Point", "Temporary fall protection anchor", 35.00m),
    ("SAFE-045", "62840045", "Safety Cone", "Heavy-duty traffic safety cone", 8.00m),
    ("SAFE-046", "62840046", "Warning Tape", "High-visibility hazard warning tape", 6.00m),
    ("SAFE-047", "62840047", "Barrier Tape", "Red and white safety barrier tape", 7.00m),
    ("SAFE-048", "62840048", "Warning Sign", "Industrial warning sign", 9.00m),
    ("SAFE-049", "62840049", "Caution Sign", "Industrial caution sign", 9.00m),
    ("SAFE-050", "62840050", "Floor Warning Sign", "Wet floor warning sign", 12.00m),

    ("SAFE-051", "62840051", "Fire Extinguisher 2kg", "Dry chemical fire extinguisher", 35.00m),
    ("SAFE-052", "62840052", "Fire Extinguisher 5kg", "Heavy-duty dry chemical extinguisher", 55.00m),
    ("SAFE-053", "62840053", "Fire Blanket", "Fire-resistant emergency blanket", 18.00m),
    ("SAFE-054", "62840054", "Fire Hose", "Industrial fire hose", 65.00m),
    ("SAFE-055", "62840055", "Fire Hose Reel", "Wall-mounted fire hose reel", 125.00m),
    ("SAFE-056", "62840056", "Emergency Exit Sign", "LED emergency exit sign", 28.00m),
    ("SAFE-057", "62840057", "Emergency Light", "Rechargeable emergency lighting unit", 35.00m),
    ("SAFE-058", "62840058", "First Aid Kit", "Industrial first aid kit", 32.00m),
    ("SAFE-059", "62840059", "Eye Wash Station", "Portable emergency eyewash station", 45.00m),
    ("SAFE-060", "62840060", "Emergency Shower", "Industrial emergency safety shower", 185.00m),

    ("SAFE-061", "62840061", "Spill Absorbent Pads", "Industrial oil absorbent pads", 25.00m),
    ("SAFE-062", "62840062", "Spill Absorbent Granules", "Industrial liquid absorbent granules", 18.00m),
    ("SAFE-063", "62840063", "Chemical Spill Kit", "Complete chemical spill response kit", 85.00m),
    ("SAFE-064", "62840064", "Oil Spill Kit", "Oil and fuel spill response kit", 65.00m),
    ("SAFE-065", "62840065", "Safety Barrier", "Portable industrial safety barrier", 55.00m),
    ("SAFE-066", "62840066", "Safety Chain", "Industrial plastic safety chain", 15.00m),
    ("SAFE-067", "62840067", "Reflective Traffic Cone", "Reflective road safety cone", 12.00m),
    ("SAFE-068", "62840068", "Portable Warning Light", "Battery-powered warning beacon", 25.00m),
    ("SAFE-069", "62840069", "Safety Lockout Kit", "Industrial lockout tagout kit", 45.00m),
    ("SAFE-070", "62840070", "Lockout Padlock", "Safety lockout padlock", 8.00m),
    ("SAFE-071", "62840071", "Danger Tag", "Industrial danger identification tag", 4.00m),
    ("SAFE-072", "62840072", "Safety Inspection Mirror", "Large industrial inspection mirror", 38.00m),
    ("SAFE-073", "62840073", "Safety Torch", "Industrial rechargeable safety flashlight", 28.00m),
    ("SAFE-074", "62840074", "Emergency Radio", "Battery-powered emergency radio", 45.00m),
    ("SAFE-075", "62840075", "Emergency Whistle", "High-volume emergency safety whistle", 5.00m)
        };
         
        var officeProducts = new[]
        {
    ("OFF-001", "62850001", "Printer Paper", "A4 80gsm copy paper", 5.25m),
    ("OFF-002", "62850002", "Printer Paper A4 Premium", "A4 premium 90gsm copy paper", 7.50m),
    ("OFF-003", "62850003", "Printer Paper A3", "A3 80gsm copy paper", 9.50m),
    ("OFF-004", "62850004", "Colored Paper", "Assorted colored office paper", 8.00m),
    ("OFF-005", "62850005", "Cardstock", "A4 heavyweight cardstock", 12.00m),
    ("OFF-006", "62850006", "Photo Paper", "Glossy A4 photo paper", 14.00m),
    ("OFF-007", "62850007", "Sticky Notes Small", "Small adhesive sticky notes", 3.50m),
    ("OFF-008", "62850008", "Sticky Notes Large", "Large adhesive sticky notes", 5.00m),
    ("OFF-009", "62850009", "Index Cards", "Ruled office index cards", 4.50m),
    ("OFF-010", "62850010", "Notepad A5", "A5 lined office notepad", 4.00m),

    ("OFF-011", "62850011", "Notebook A4", "A4 ruled office notebook", 6.50m),
    ("OFF-012", "62850012", "Notebook A5", "A5 hardcover notebook", 8.00m),
    ("OFF-013", "62850013", "Spiral Notebook", "Spiral-bound office notebook", 7.00m),
    ("OFF-014", "62850014", "Meeting Notebook", "Professional meeting notebook", 12.00m),
    ("OFF-015", "62850015", "Planner", "Annual office planner", 15.00m),
    ("OFF-016", "62850016", "Desk Calendar", "Desktop office calendar", 8.00m),
    ("OFF-017", "62850017", "Wall Calendar", "Large wall calendar", 10.00m),
    ("OFF-018", "62850018", "Clipboard", "A4 plastic clipboard", 5.00m),
    ("OFF-019", "62850019", "Clipboard Metal", "Heavy-duty metal clipboard", 9.00m),
    ("OFF-020", "62850020", "Document Folder", "A4 document folder", 2.50m),

    ("OFF-021", "62850021", "Lever Arch File", "A4 lever arch file", 5.50m),
    ("OFF-022", "62850022", "Ring Binder", "A4 two-ring binder", 4.50m),
    ("OFF-023", "62850023", "Expanding File", "Multi-section expanding document file", 9.00m),
    ("OFF-024", "62850024", "Document Wallet", "A4 plastic document wallet", 3.50m),
    ("OFF-025", "62850025", "Envelope Pack", "A4 office envelopes", 5.00m),
    ("OFF-026", "62850026", "Bubble Envelope", "Protective bubble envelopes", 8.00m),
    ("OFF-027", "62850027", "Shipping Labels", "Self-adhesive shipping labels", 7.50m),
    ("OFF-028", "62850028", "Address Labels", "Self-adhesive address labels", 5.50m),
    ("OFF-029", "62850029", "File Dividers", "A4 colored file dividers", 4.00m),
    ("OFF-030", "62850030", "Plastic Sleeves", "A4 transparent document sleeves", 6.00m),

    ("OFF-031", "62850031", "Ballpoint Pens", "Blue ballpoint pen pack", 3.00m),
    ("OFF-032", "62850032", "Gel Pens", "Assorted gel pen pack", 5.50m),
    ("OFF-033", "62850033", "Rollerball Pens", "Premium rollerball pen set", 8.00m),
    ("OFF-034", "62850034", "Permanent Markers", "Permanent marker pack", 5.00m),
    ("OFF-035", "62850035", "Whiteboard Markers", "Assorted whiteboard markers", 6.00m),
    ("OFF-036", "62850036", "Highlighter Set", "Assorted office highlighters", 5.00m),
    ("OFF-037", "62850037", "Pencils", "HB graphite pencil pack", 2.50m),
    ("OFF-038", "62850038", "Mechanical Pencils", "Mechanical pencil set", 5.00m),
    ("OFF-039", "62850039", "Pencil Leads", "Replacement mechanical pencil leads", 2.50m),
    ("OFF-040", "62850040", "Erasers", "White eraser pack", 2.00m),

    ("OFF-041", "62850041", "Stapler", "Desktop office stapler", 6.00m),
    ("OFF-042", "62850042", "Heavy Duty Stapler", "Heavy-duty office stapler", 18.00m),
    ("OFF-043", "62850043", "Staples Standard", "Standard office staples", 2.50m),
    ("OFF-044", "62850044", "Staples Heavy Duty", "Heavy-duty stapler staples", 4.50m),
    ("OFF-045", "62850045", "Paper Clips", "Standard metal paper clips", 2.00m),
    ("OFF-046", "62850046", "Binder Clips Small", "Small black binder clips", 3.00m),
    ("OFF-047", "62850047", "Binder Clips Large", "Large black binder clips", 4.50m),
    ("OFF-048", "62850048", "Push Pins", "Colored office push pins", 2.50m),
    ("OFF-049", "62850049", "Rubber Bands", "Assorted office rubber bands", 3.00m),
    ("OFF-050", "62850050", "Paper Punch", "Two-hole office paper punch", 7.00m),

    ("OFF-051", "62850051", "Scissors", "Office scissors", 5.00m),
    ("OFF-052", "62850052", "Heavy Duty Scissors", "Heavy-duty office scissors", 9.00m),
    ("OFF-053", "62850053", "Utility Cutter", "Office utility paper cutter", 8.00m),
    ("OFF-054", "62850054", "Paper Trimmer", "A4 office paper trimmer", 25.00m),
    ("OFF-055", "62850055", "Ruler 30cm", "30cm transparent ruler", 2.00m),
    ("OFF-056", "62850056", "Ruler 50cm", "50cm office ruler", 4.00m),
    ("OFF-057", "62850057", "Glue Stick", "Office glue stick pack", 3.50m),
    ("OFF-058", "62850058", "Liquid Glue", "General-purpose office adhesive", 3.00m),
    ("OFF-059", "62850059", "Correction Tape", "Office correction tape", 4.00m),
    ("OFF-060", "62850060", "Correction Fluid", "White correction fluid", 2.50m),

    ("OFF-061", "62850061", "Desk Organizer", "Multi-compartment desk organizer", 12.00m),
    ("OFF-062", "62850062", "Pen Holder", "Desktop pen holder", 5.00m),
    ("OFF-063", "62850063", "Letter Tray", "Stackable document letter tray", 8.00m),
    ("OFF-064", "62850064", "Desk File Organizer", "Desktop vertical file organizer", 14.00m),
    ("OFF-065", "62850065", "Cable Organizer", "Desktop cable management kit", 7.00m),
    ("OFF-066", "62850066", "Desk Mat", "Large office desk mat", 15.00m),
    ("OFF-067", "62850067", "Monitor Stand Office", "Adjustable office monitor stand", 25.00m),
    ("OFF-068", "62850068", "Keyboard Wrist Rest", "Ergonomic keyboard wrist rest", 12.00m),
    ("OFF-069", "62850069", "Mouse Pad", "Large office mouse pad", 7.00m),
    ("OFF-070", "62850070", "Ergonomic Footrest", "Adjustable under-desk footrest", 25.00m),

    ("OFF-071", "62850071", "Whiteboard", "Medium magnetic office whiteboard", 45.00m),
    ("OFF-072", "62850072", "Whiteboard Eraser", "Magnetic whiteboard eraser", 4.00m),
    ("OFF-073", "62850073", "Flip Chart Stand", "Adjustable office flip chart stand", 65.00m),
    ("OFF-074", "62850074", "Flip Chart Pad", "Large flip chart paper pad", 12.00m),
    ("OFF-075", "62850075", "Presentation Pointer", "Wireless presentation pointer", 25.00m)
        };

        
var toolsCollection = toolsProducts
    .Select(product =>
        Product.Create(
            Guid.NewGuid(),
            product.Item1,
            product.Item2,
            product.Item3,
            product.Item4,
            tools.Id,
            product.Item5,
            true,
            Unit.Piece).Value)
    .ToArray();

        var safetyCollection = safetyProducts
            .Select(product =>
                Product.Create(
                    Guid.NewGuid(),
                    product.Item1,
                    product.Item2,
                    product.Item3,
                    product.Item4,
                    safety.Id,
                    product.Item5,
                    true,
                    Unit.Piece).Value)
            .ToArray();

        var officeCollection = officeProducts
            .Select(product =>
                Product.Create(
                    Guid.NewGuid(),
                    product.Item1,
                    product.Item2,
                    product.Item3,
                    product.Item4,
                    office.Id,
                    product.Item5,
                    true,
                    Unit.Piece).Value)
            .ToArray();

        context.Categories.AddRange(
            electronics,
            tools,
            automotive,
            safety,
            office);

        context.Products.AddRange(products);
        context.Products.AddRange(automotiveCollection);
        context.Products.AddRange(officeCollection);
        context.Products.AddRange(safetyCollection);
        context.Products.AddRange(toolsCollection);

        _catalog = new SeedCatalog(
            electronics,
            tools,
            automotive,
            safety,
            office,
            products.ToArray());

        await Task.CompletedTask;
    }
    private async Task SeedBusinessPartnersAsync(
        CancellationToken cancellationToken)
    {
        var nablus = _locations.Nablus;
        var ramallah = _locations.Ramallah;
        var amman = _locations.Amman;
        var cairo = _locations.Cairo;

        var supplier1 = CreateSupplier(
            "Al Quds Tech",
            "SUP-001",
            "sup.tech@example.test",
            "+970599100001",
            _locations.Palestine.Id,
            nablus.Id,
            "Industrial Zone",
            "14",
            "Najah St",
            true,
            "Electronics and computer accessories supplier.");

        var supplier2 = CreateSupplier(
            "Jordan Tools",
            "SUP-002",
            "sup.tools@example.test",
            "+962790100002",
            _locations.Jordan.Id,
            amman.Id,
            "11931",
            "22",
            "King Hussein St",
            true,
            "Professional tools and workshop equipment.");

        var supplier3 = CreateSupplier(
            "Nile Automotive",
            "SUP-003",
            "sup.auto@example.test",
            "+201001000003",
            _locations.Egypt.Id,
            cairo.Id,
            "11511",
            "8",
            "Nasr St",
            true,
            "Automotive consumables and spare parts.");

        var supplier4 = CreateSupplier(
            "Palestine Safety",
            "SUP-004",
            "sup.safety@example.test",
            "+970599100004",
            _locations.Palestine.Id,
            ramallah.Id,
            "440",
            "31",
            "Al Irsal St",
            true,
            "Safety and industrial protection products.");

        var customer1 = CreateCustomer(
            "Nablus Auto Care",
            "CUS-001",
            "customer.auto@example.test",
            "+970599200001",
            _locations.Palestine.Id,
            nablus.Id,
            "12",
            "Al Makhfia St",
            "Fleet maintenance customer.");

        var customer2 = CreateCustomer(
            "Ramallah Office Hub",
            "CUS-002",
            "customer.office@example.test",
            "+970599200002",
            _locations.Palestine.Id,
            ramallah.Id,
            "7",
            "Rukab St",
            "Office supplies customer.");

        var customer3 = CreateCustomer(
            "Gaza Workshop",
            "CUS-003",
            "customer.workshop@example.test",
            "+970599200003",
            _locations.Palestine.Id,
            _locations.Gaza.Id,
            "18",
            "Omar Al Mukhtar St",
            "Independent repair workshop.");

        var customer4 = CreateCustomer(
            "Jordan Service Center",
            "CUS-004",
            "customer.jordan@example.test",
            "+962790200004",
            _locations.Jordan.Id,
            amman.Id,
            "25",
            "Al Madina St",
            "Regional service center.");

        var customer5 = CreateCustomer(
            "Cairo Parts Retail",
            "CUS-005",
            "customer.cairo@example.test",
            "+201002000005",
            _locations.Egypt.Id,
            cairo.Id,
            "11",
            "Tahrir St",
            "Automotive parts retailer.");

        context.Suppliers.AddRange(
            supplier1,
            supplier2,
            supplier3,
            supplier4);

        context.Customers.AddRange(
            customer1,
            customer2,
            customer3,
            customer4,
            customer5);

        var supplierProducts = new[]
        {
            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier1.Id,
                _catalog.Products[0].Id,
                4.25m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier1.Id,
                _catalog.Products[1].Id,
                17.50m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier2.Id,
                _catalog.Products[2].Id,
                105.00m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier2.Id,
                _catalog.Products[3].Id,
                51.00m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier3.Id,
                _catalog.Products[4].Id,
                15.50m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier3.Id,
                _catalog.Products[5].Id,
                36.00m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier4.Id,
                _catalog.Products[6].Id,
                3.25m).Value,

            SupplierProduct.Create(
                Guid.NewGuid(),
                supplier4.Id,
                _catalog.Products[7].Id,
                3.10m).Value
        };

        context.SupplierProducts.AddRange(supplierProducts);

        _partners = new SeedPartners(
            new[]
            {
                supplier1,
                supplier2,
                supplier3,
                supplier4
            },
            new[]
            {
                customer1,
                customer2,
                customer3,
                customer4,
                customer5
            });

        await Task.CompletedTask;
    }

    private async Task SeedEmployeesAndUsersAsync(
        CancellationToken cancellationToken)
    {
        var employee1 = CreateEmployee(
            "General Manager",
            "Salah",
            "Mohd",
            "Ali",
            "Ahmad",
            "EMP-EMAIL-1",
            "+970599300001",
            _warehouses.Nablus.Id,
            new DateOnly(2022, 3, 10));

        var employee2 = CreateEmployee(
            "Sales Officer",
            "Omar",
            "Khaled",
            null,
            "Saleh",
            "EMP-EMAIL-2",
            "+970599300002",
            _warehouses.Ramallah.Id,
            new DateOnly(2023, 1, 15));

        var employee3 = CreateEmployee(
            "Purchasing Officer",
            "Rami",
            "Tareq",
            null,
            "Hassan",
            "EMP-EMAIL-3",
            "+970599300003",
            _warehouses.Nablus.Id,
            new DateOnly(2023, 5, 20));

        var employee4 = CreateEmployee(
            "Warehouse Supervisor",
            "Yousef",
            "Adel",
            null,
            "Nasser",
            "EMP-EMAIL-4",
            "+970599300004",
            _warehouses.Gaza.Id,
            new DateOnly(2021, 9, 1));

        var employee5 = CreateEmployee(
            "Sales Officer",
            "Kareem",
            "Sami",
            null,
            "Odeh",
            "EMP-EMAIL-5",
            "+970599300005",
            _warehouses.Ramallah.Id,
            new DateOnly(2024, 2, 12));

        var employee6 = CreateEmployee(
            "Inventory Clerk",
            "Fadi",
            "Nabil",
            null,
            "Kamal",
            "EMP-EMAIL-6",
            "+970599300006",
            _warehouses.Nablus.Id,
            new DateOnly(2024, 7, 5));

        context.Employees.AddRange(
            employee1,
            employee2,
            employee3,
            employee4,
            employee5,
            employee6);

        var passwordHash = hashingHelper.Hash<User>(SeedPassword);

        var users = new[]
        {
            CreateUser(
                SeedAdminUsername,
                passwordHash,
                "admin@example.test",
                Role.Admin,
                employee1.Id,
                -1),

            CreateUser(
                "omar_sales",
                passwordHash,
                "omar_sales@example.test",
                Role.SalesUser,
                employee2.Id,
                -2),

            CreateUser(
                "rami_buy",
                passwordHash,
                "rami_buy@example.test",
                Role.PurchasesUser,
                employee3.Id,
                -3),

            CreateUser(
                "yousef_wh",
                passwordHash,
                "yousef_wh@example.test",
                Role.WarehouseUser,
                employee4.Id,
                -4),

            CreateUser(
                "kareem_sale",
                passwordHash,
                "kareem_sale@example.test",
                Role.SalesUser,
                employee5.Id,
                -5),

            CreateUser(
                "fadi_stock",
                passwordHash,
                "fadi_stock@example.test",
                Role.Viewer,
                employee6.Id,
                -6)
        };

        context.Users.AddRange(users);

        await Task.CompletedTask;
    }

    private async Task SeedInventoryAsync(
        CancellationToken cancellationToken)
    {
        var warehouse1 = CreateWarehouse(
            "Nablus Central",
            "WH-NBL",
            _locations.Palestine.Id,
            _locations.Nablus.Id,
            "10",
            "Najah St");

        var warehouse2 = CreateWarehouse(
            "Ramallah Branch",
            "WH-RAM",
            _locations.Palestine.Id,
            _locations.Ramallah.Id,
            "5",
            "Al Irsal St");

        var warehouse3 = CreateWarehouse(
            "Gaza Branch",
            "WH-GZA",
            _locations.Palestine.Id,
            _locations.Gaza.Id,
            "21",
            "Omar Al Mukhtar St");

        context.Warehouses.AddRange(
            warehouse1,
            warehouse2,
            warehouse3);

        var stocks = new List<WarehouseStock>();

        foreach (var product in _catalog.Products)
        {
            stocks.Add(
                WarehouseStock.Create(
                    Guid.NewGuid(),
                    warehouse1.Id,
                    product.Id,
                    5,
                    InitialStock(product.SKU, 1)).Value);

            stocks.Add(
                WarehouseStock.Create(
                    Guid.NewGuid(),
                    warehouse2.Id,
                    product.Id,
                    3,
                    InitialStock(product.SKU, 2)).Value);

            stocks.Add(
                WarehouseStock.Create(
                    Guid.NewGuid(),
                    warehouse3.Id,
                    product.Id,
                    2,
                    InitialStock(product.SKU, 3)).Value);
        }

        context.WarehouseStocks.AddRange(stocks);

        _warehouses = new SeedWarehouses(
            warehouse1,
            warehouse2,
            warehouse3,
            stocks);

        await Task.CompletedTask;
    }

    private async Task SeedOrdersAndInvoicesAsync(
        CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.UtcNow;
        var p = _catalog.Products;
        var suppliers = _partners.Suppliers;
        var customers = _partners.Customers;

        var purchase = CreateCompletedOrder(
            OrderType.Purchase,
            suppliers[0].Id,
            null,
            _warehouses.Nablus.Id,
            null,
            "Monthly electronics replenishment",
            10m,
            new[]
            {
                Detail(p[0], 40, 4.25m),
                Detail(p[1], 20, 17.50m)
            },
            now.AddDays(1));

        AddInvoice(
            purchase,
            InvoiceType.Purchase,
            10m,
            new[]
            {
                ("USB-C Cable", 40m, 4.25m, 17m),
                ("Power Adapter", 20m, 17.50m, 35m)
            });

        var purchase2 = CreateCompletedOrder(
            OrderType.Purchase,
            suppliers[1].Id,
            null,
            _warehouses.Ramallah.Id,
            null,
            "Workshop tools replenishment",
            0m,
            new[]
            {
                Detail(p[2], 12, 105m),
                Detail(p[3], 15, 51m)
            },
            now.AddDays(2));

        AddInvoice(
            purchase2,
            InvoiceType.Purchase,
            0m,
            new[]
            {
                ("Drill Set", 12m, 105m, 126m),
                ("Socket Set", 15m, 51m, 76.50m)
            });

        var sale = CreateCompletedOrder(
            OrderType.Sale,
            null,
            customers[0].Id,
            _warehouses.Nablus.Id,
            null,
            "Fleet maintenance order",
            12m,
            new[]
            {
                Detail(p[4], 18, 24m),
                Detail(p[5], 6, 55m)
            },
            now.AddDays(3));

        AddInvoice(
            sale,
            InvoiceType.Sale,
            12m,
            new[]
            {
                ("Engine Oil", 18m, 24m, 43.20m),
                ("Brake Pads", 6m, 55m, 33m)
            });

        var sale2 = CreateCompletedOrder(
            OrderType.Sale,
            null,
            customers[1].Id,
            _warehouses.Ramallah.Id,
            null,
            "Office supply order",
            5m,
            new[]
            {
                Detail(p[7], 30, 5.25m),
                Detail(p[1], 4, 29m)
            },
            now.AddDays(4));

        AddInvoice(
            sale2,
            InvoiceType.Sale,
            5m,
            new[]
            {
                ("Printer Paper", 30m, 5.25m, 4.73m),
                ("Power Adapter", 4m, 29m, 11.60m)
            });

        CreateCompletedOrder(
            OrderType.Transfer,
            null,
            null,
            _warehouses.Nablus.Id,
            _warehouses.Gaza.Id,
            "Inter-branch stock balancing",
            null,
            new[]
            {
                Detail(p[0], 10, 4.25m),
                Detail(p[4], 8, 15.50m)
            },
            now.AddDays(5));

        var returnIn = CreateCompletedOrder(
            OrderType.ReturnIn,
            null,
            customers[0].Id,
            _warehouses.Nablus.Id,
            null,
            "Customer returned unopened brake pads",
            0m,
            new[]
            {
                Detail(p[5], 1, 55m)
            },
            now.AddDays(6));

        AddInvoice(
            returnIn,
            InvoiceType.ReturnIn,
            0m,
            new[]
            {
                ("Brake Pads", 1m, 55m, 0m)
            });

        var returnOut = CreateCompletedOrder(
            OrderType.ReturnOut,
            suppliers[0].Id,
            null,
            _warehouses.Nablus.Id,
            null,
            "Damaged power adapters returned to supplier",
            0m,
            new[]
            {
                Detail(p[1], 2, 17.50m)
            },
            now.AddDays(7));

        AddInvoice(
            returnOut,
            InvoiceType.ReturnOut,
            0m,
            new[]
            {
                ("Power Adapter", 2m, 17.50m, 0m)
            });

        CreatePendingOrder(
            OrderType.Purchase,
            suppliers[2].Id,
            null,
            _warehouses.Gaza.Id,
            null,
            "Upcoming automotive replenishment",
            0m,
            new[]
            {
                Detail(p[4], 50, 15.50m),
                Detail(p[5], 10, 36m)
            },
            now.AddDays(10));

        CreateCompletedOrder(
            OrderType.Transfer,
            null,
            null,
            _warehouses.Ramallah.Id,
            _warehouses.Gaza.Id,
            "Planned safety stock transfer",
            null,
            new[]
            {
                Detail(p[6], 20, 3.25m)
            },
            now.AddDays(12));

        await Task.CompletedTask;
    }

    private static OrderDetail Detail(
        Product product,
        decimal quantity,
        decimal unitPrice)
    {
        return OrderDetail.Create(
            Guid.NewGuid(),
            product.Id,
            quantity,
            unitPrice).Value;
    }

    private Order CreateCompletedOrder(
        OrderType type,
        Guid? supplierId,
        Guid? customerId,
        Guid sourceWarehouseId,
        Guid? destinationWarehouseId,
        string notes,
        decimal? discount,
        IEnumerable<OrderDetail> details,
        DateTimeOffset dueDate)
    {
        var order = Order.Create(
            Guid.NewGuid(),
            type,
            supplierId,
            customerId,
            sourceWarehouseId,
            destinationWarehouseId,
            notes,
            discount,
            details.ToList(),
            dueDate).Value;

        var statusResult = order.UpdateStatus(OrderStatus.Completed);

        if (statusResult.IsError)
        {
            throw new InvalidOperationException(
                statusResult.TopError.Description);
        }

        context.Orders.Add(order);

        return order;
    }

    private Order CreatePendingOrder(
        OrderType type,
        Guid? supplierId,
        Guid? customerId,
        Guid sourceWarehouseId,
        Guid? destinationWarehouseId,
        string notes,
        decimal? discount,
        IEnumerable<OrderDetail> details,
        DateTimeOffset dueDate)
    {
        var order = Order.Create(
            Guid.NewGuid(),
            type,
            supplierId,
            customerId,
            sourceWarehouseId,
            destinationWarehouseId,
            notes,
            discount,
            details.ToList(),
            dueDate).Value;

        context.Orders.Add(order);

        return order;
    }

    private void AddInvoice(
        Order order,
        InvoiceType invoiceType,
        decimal discount,
        IEnumerable<(string Description, decimal Quantity, decimal UnitPrice, decimal Tax)> lines)
    {
        var invoiceId = Guid.NewGuid();

        var lineItems = lines
            .Select((line, index) =>
                InvoiceLineItem.Create(
                    index,
                    invoiceId,
                    line.Description,
                    line.Tax,
                    line.Quantity,
                    line.UnitPrice).Value)
            .ToList();

        var invoice = Invoice.Create(
            invoiceId,
            invoiceType,
            discount,
            lineItems,
            order.Id).Value;

        var issueResult = order.IssueInvoice(invoice);

        if (issueResult.IsError)
        {
            throw new InvalidOperationException(
                issueResult.TopError.Description);
        }

        context.Invoices.Add(invoice);
    }

    private Supplier CreateSupplier(
        string name,
        string code,
        string email,
        string phone,
        Guid countryId,
        Guid cityId,
        string postalCode,
        string building,
        string street,
        bool active,
        string notes)
    {
        var contact = ContactInfo.Create(
            Guid.NewGuid(),
            email,
            phone,
            string.Empty,
            null,
            "https://example.test").Value;

        var address = Address.Create(
            Guid.NewGuid(),
            countryId,
            cityId,
            postalCode,
            building,
            street,
            null).Value;

        return Supplier.Create(
            Guid.NewGuid(),
            name,
            code,
            contact,
            address,
            active,
            notes).Value;
    }

    private Customer CreateCustomer(
        string name,
        string code,
        string email,
        string phone,
        Guid countryId,
        Guid cityId,
        string building,
        string street,
        string notes)
    {
        var contact = ContactInfo.Create(
            Guid.NewGuid(),
            email,
            phone,
            string.Empty,
            null,
            "https://example.test").Value;

        var address = Address.Create(
            Guid.NewGuid(),
            countryId,
            cityId,
            "00000",
            building,
            street,
            null).Value;

        return Customer.Create(
            Guid.NewGuid(),
            name,
            code,
            contact,
            address,
            notes).Value;
    }

    private static User CreateUser(
        string username,
        string passwordHash,
        string email,
        Role role,
        Guid employeeId,
        int lastLoginDaysAgo)
    {
        var user = User.Create(
            username,
            passwordHash,
            email,
            role,
            true,
            employeeId).Value;

        user.LastLoginAt =
            DateTimeOffset.UtcNow.AddDays(lastLoginDaysAgo);

        return user;
    }

    private Employee CreateEmployee(
        string jobTitle,
        string firstName,
        string secondName,
        string? thirdName,
        string lastName,
        string emailKey,
        string phone,
        Guid warehouseId,
        DateOnly hiringDate)
    {
        var countryId = _locations.Palestine.Id;

        var cityId = warehouseId == _warehouses.Nablus.Id
            ? _locations.Nablus.Id
            : warehouseId == _warehouses.Ramallah.Id
                ? _locations.Ramallah.Id
                : _locations.Gaza.Id;

        var contact = ContactInfo.Create(
            Guid.NewGuid(),
            emailKey.Replace("EMP-EMAIL", "employee") + "@example.test",
            phone,
            string.Empty,
            null,
            null).Value;

        var address = Address.Create(
            Guid.NewGuid(),
            countryId,
            cityId,
            "00000",
            "1",
            "Main St",
            null).Value;

        var person = Person.Create(
            Guid.NewGuid(),
            UniqueNationalNo(),
            firstName,
            secondName,
            thirdName,
            lastName,
            true,
            new DateOnly(1990, 1, 1),
            contact,
            address).Value;

        return Employee.Create(
            jobTitle,
            person,
            hiringDate,
            warehouseId).Value;
    }

    private Warehouse CreateWarehouse(
        string name,
        string code,
        Guid countryId,
        Guid cityId,
        string building,
        string street)
    {
        var address = Address.Create(
            Guid.NewGuid(),
            countryId,
            cityId,
            "00000",
            building,
            street,
            "Warehouse address").Value;

        return Warehouse.Create(
            Guid.NewGuid(),
            name,
            code,
            address).Value;
    }

    private static decimal InitialStock(
        string sku,
        int warehouse)
    {
        return (sku, warehouse) switch
        {
            ("ELEC-001", 1) => 75m,
            ("ELEC-001", 2) => 25m,
            ("ELEC-001", 3) => 18m,

            ("ELEC-002", 1) => 28m,
            ("ELEC-002", 2) => 12m,
            ("ELEC-002", 3) => 8m,

            ("TOOL-001", 1) => 7m,
            ("TOOL-001", 2) => 18m,
            ("TOOL-001", 3) => 4m,

            ("TOOL-002", 1) => 9m,
            ("TOOL-002", 2) => 25m,
            ("TOOL-002", 3) => 6m,

            ("AUTO-001", 1) => 46m,
            ("AUTO-001", 2) => 22m,
            ("AUTO-001", 3) => 15m,

            ("AUTO-002", 1) => 17m,
            ("AUTO-002", 2) => 8m,
            ("AUTO-002", 3) => 6m,

            ("SAFE-001", 1) => 35m,
            ("SAFE-001", 2) => 15m,
            ("SAFE-001", 3) => 8m,

            ("OFF-001", 1) => 42m,
            ("OFF-001", 2) => 28m,
            ("OFF-001", 3) => 10m,

            _ => 10m
        };
    }

    private static string UniqueNationalNo()
    {
        return Random.Shared
            .NextInt64(10000000, 99999999)
            .ToString();
    }

    private SeedLocations _locations = null!;
    private SeedCatalog _catalog = null!;
    private SeedPartners _partners = null!;
    private SeedWarehouses _warehouses = null!;

    private sealed record SeedLocations(
        Country Palestine,
        Country Jordan,
        Country Egypt,
        City Nablus,
        City Ramallah,
        City Gaza,
        City Amman,
        City Cairo);

    private sealed record SeedCatalog(
        Category Electronics,
        Category Tools,
        Category Automotive,
        Category Safety,
        Category Office,
        Product[] Products);

    private sealed record SeedPartners(
        Supplier[] Suppliers,
        Customer[] Customers);

    private sealed record SeedWarehouses(
        Warehouse Nablus,
        Warehouse Ramallah,
        Warehouse Gaza,
        List<WarehouseStock> Stocks);
}

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(
        this WebApplication webApplication,
        CancellationToken cancellationToken = default)
    {
        await using var scope =
            webApplication.Services.CreateAsyncScope();

        var initialiser = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync(cancellationToken);
        await initialiser.SeedAsync(cancellationToken);
    }
}