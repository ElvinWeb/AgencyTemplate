using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Models
{
    public class Portfolio : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string SubTitle { get; set; }
        public string Client { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }

        public string? ImgUrl { get; set; }

    }
}
