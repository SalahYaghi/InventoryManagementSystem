using System;
using System.ComponentModel.DataAnnotations;
namespace Contract.Requests.Cities
{
public class UpdateCityRequest
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;
}
}


