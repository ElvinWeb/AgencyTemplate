using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.CustomExceptions.PortfolioExceptions
{
    public class InvalidImage : Exception
    {
        public string PropertyName { get; set; }
        public InvalidImage()
        {
        }

        public InvalidImage(string? message) : base(message)
        {
        }

        public InvalidImage(string propName, string? message) : base(message)
        {
            PropertyName = propName;
        }
    }
}
