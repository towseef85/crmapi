using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.GeneralDto
{
    public class PostImageDto
    {
        public IFormFile File { get; set; }
    }
}
