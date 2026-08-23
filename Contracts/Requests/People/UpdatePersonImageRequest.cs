using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Requests.People
{
    public class UpdatePersonImageRequest
    {
        //[Required(ErrorMessage = "Image is required.")]
        public IFormFile? Image { get; set; } = default!;
    }
}


