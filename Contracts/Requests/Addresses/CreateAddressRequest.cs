using System;
using System.ComponentModel.DataAnnotations;
namespace Contract.Requests.Addresses
{
public class CreateAddressRequest
{
    [Required(ErrorMessage = "CountryId is required.")]
    public Guid CountryId { get; set; }

    [Required(ErrorMessage = "CityId is required.")]
    public Guid CityId { get; set; }

    [MaxLength(20, ErrorMessage = "PostalCode must not exceed 20 characters.")]
    public string PostalCode { get; set; }

    [MaxLength(20, ErrorMessage = "BuildingNumber must not exceed 20 characters.")]
    public string BuildingNumber { get; set; }

    [MaxLength(20, ErrorMessage = "Street must not exceed 20 characters.")]
    public string Street { get; set; }

    [MaxLength(200, ErrorMessage = "Description must not exceed 200 characters.")]
    public string Description { get; set; }
}
}

