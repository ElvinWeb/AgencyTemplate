using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.CustomExceptions.PortfolioExceptions
{
    public class InvalidContentTypeAndSize : Exception
    {
        public string PropertyName { get; set; }
        public InvalidContentTypeAndSize()
        {
        }

        public InvalidContentTypeAndSize(string? message) : base(message)
        {
        }

        public InvalidContentTypeAndSize(string propName, string? message) : base(message)
        {
            PropertyName = propName;
        }
    }
}
