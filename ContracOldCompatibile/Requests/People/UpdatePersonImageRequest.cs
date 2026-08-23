using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Requests.People
{
    public class UpdatePersonImageRequest
    {
        //[Required(ErrorMessage = "Image is required.")]
        public byte[] Image { get; set; } = null;
    }
}



