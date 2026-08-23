using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Files
{

    public record FileDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string? FileUrl { get; set; } = string.Empty;
        public Stream?Stream { get; set; }
        public byte[] Data { get; set; } = [];
    }
  





}

