using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.CustomExceptions
{
    public class InvalidAlreadyCreated : Exception
    {
        public string PropertyName { get; set; }
        public InvalidAlreadyCreated()
        {
        }

        public InvalidAlreadyCreated(string? message) : base(message)
        {
        }

        public InvalidAlreadyCreated(string propName, string? message) : base(message)
        {
            PropertyName = propName;
        }
    }
}
