using System.ComponentModel.DataAnnotations;
namespace Contract.Requests.Categories
{
public class CreateCategoryRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(20, ErrorMessage = "Name must not exceed 20 characters.")]
    public string Name { get; set; } = string.Empty;
}
}


