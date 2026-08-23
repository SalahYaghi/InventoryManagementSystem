using Contract.Common;
using Contract.Common.Interfaces;
using Contract.Features.User.Commands.CreateUser;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Contacts.ContactInfo;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using Domain.People;
using Domain.Warehouses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDbContextInitialiser
        (AppDbContext context  , 
        ILogger<ApplicationDbContextInitialiser> logger,
        IHashingHelper hashingHelper)
    {

        private static string UniqueSuffix()
        {
            return Guid.NewGuid().ToString("N")[..8];
        }

        private static string UniqueNationalNo()
        {
            return Random.Shared.Next(100000000, 999999999).ToString();
        }

        public static Person CreateValidPerson(
            Guid? id = null,
            string nationalNo = "123456789",
            string firstName = "Salah",
            string secondName = "Mohd",
            string? thirdName = "Ali",
            string lastName = "Ahmad",
            bool gender = true,
            DateOnly? dateOfBirth = null,
            ContactInfo? contact = null,
            Address? address = null)
        {
            var result = Person.Create(
                id ?? Guid.NewGuid(),
                nationalNo,
                firstName,
                secondName,
                thirdName,
                lastName,
                gender,
                dateOfBirth ?? new DateOnly(2000, 1, 1),
                contact ?? CreateValidContact(),
                address ?? CreateValidAddress());

            if (result.IsError)
                throw new InvalidOperationException(result.TopError.Description);

            return result.Value;
        }
        public static Address CreateValidAddress(
    Guid? id = null,
    Guid? countryId = null,
    Guid? cityId = null,
    string? postalCode = "12345",
    string? buildingNumber = "10",
    string? street = "Main Street",
    string? description = "Valid address")
        {
            var result = Address.Create(
                id ?? Guid.NewGuid(),
                countryId ?? Guid.NewGuid(),
                cityId ?? Guid.NewGuid(),
                postalCode,
                buildingNumber,
                street,
                description);

            if (result.IsError)
                throw new InvalidOperationException(result.TopError.Description);

            return result.Value;
        }
        public static ContactInfo CreateValidContact(
        Guid? id = null,
        string email = "person@test.com",
        string phoneNumber = "+970599123456",
        string alternitavePhoneNumber = "+970598123456",
        string? faxNumber = null,
        string? websiteUrl = "https://example.com")
        {
            var result = ContactInfo.Create(
                id ?? Guid.NewGuid(),
                email,
                phoneNumber,
                alternitavePhoneNumber,
                faxNumber,
                websiteUrl);

            if (result.IsError)
                throw new InvalidOperationException(result.TopError.Description);

            return result.Value;
        }


        public static Employee CreateValidWarehouse(string jobTitle = "Manager", Person? person = null, DateOnly? hiringDate = null, Guid? warehouseId = null)
        {
            var result = Employee.Create(jobTitle, person ?? CreateValidPerson(), hiringDate ?? new DateOnly(2024, 1, 1), warehouseId ?? Guid.NewGuid());
            if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
            return result.Value;
        }

        public static Warehouse CreateValidWarehouse(
            Guid? id = null,
            string name = "Main Warehouse",
            string code = "WH-1",
            Address? address = null)
        {

            var result = Warehouse.Create(id ?? Guid.NewGuid(), name, code, address ?? CreateValidAddress());

            if (result.IsError)
                throw new InvalidOperationException(result.TopError.Description);

            return result.Value;
        }
        private async Task<Employee> SeedEmployeeAsync()
        {
            var suffix = UniqueSuffix();

            var country = Country.Create($"Country{suffix}").Value;
            var city = City.Create(Guid.NewGuid(), country.Id, $"City{suffix}");
            var address = CreateValidAddress(
                countryId: country.Id,
                cityId: city.Value.Id,
                postalCode: "12345",
                buildingNumber: "10",
                street: "Street",
                description: "Address");

            var warehouseAddress = CreateValidAddress(
                countryId: country.Id,
                cityId: city.Value.Id,
                postalCode: "54321",
                buildingNumber: "20",
                street: "Warehouse",
                description: "Warehouse address");

            var contact = CreateValidContact(email: "salah@gmail.com");
            var person = CreateValidPerson(
                nationalNo: UniqueNationalNo(),
                contact: contact,
                address: address);

            var warehouse = CreateValidWarehouse(
                id: Guid.NewGuid(),
                name: $"Warehouse{suffix}",
                code: $"WH{suffix}",
                address: warehouseAddress);

            var employee = CreateValidWarehouse(
                jobTitle: "Manager",
                person: person,
                warehouseId: warehouse.Id);


            await context.Countries.AddAsync(country);
            await context.Cities.AddAsync(city.Value);
            await context.Addresses.AddAsync(address);
            await context.Addresses.AddAsync(warehouseAddress);
            await context.ContactInfos.AddAsync(contact);
            await context.People.AddAsync(person);
            await context.Warehouses.AddAsync(warehouse);
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync(CancellationToken.None);

            return employee;
        }



        

        public async Task InitialiseAsync()
        {
            try
            {
               await context.Database.MigrateAsync(); 
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw; 
            }

        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }

        }

        public  async Task TrySeedAsync() {

            
            if (context.Users.Any())
                return; 

            var pass = hashingHelper.Hash<User>("Salahnour$");

            var emp = await SeedEmployeeAsync();

            var command = User.Create(
            "SalahYaghi", pass
            , "salah@gmail.com",
            Domain.Identity.Users.Role.Admin,
            true , 
            emp.Id);


            await context.AddAsync(command.Value);

            await context.SaveChangesAsync(default); 

        }

    }

    public static class InitialiserExtensions
    {

        public static async Task InitialiseDatabaseAsync(this WebApplication webApplication) {

            using var scope = webApplication.Services.CreateScope();

            var initlizer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initlizer.InitialiseAsync();

            await initlizer.SeedAsync();


        }

    }

}
