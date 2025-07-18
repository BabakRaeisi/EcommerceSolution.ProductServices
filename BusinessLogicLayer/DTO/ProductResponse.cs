using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public record ProductResponse(Guid ProductId, string ProductName, CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
    {
        public ProductResponse() : this(default, default, default, default, default)
        {
            // Default constructor initializes with default values
        }
    }
}
