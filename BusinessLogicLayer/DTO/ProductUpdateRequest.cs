using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ProductUpdateRequest(Guid ProductId,string ProductName, CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
{
    public ProductUpdateRequest() : this(default,default, default, default, default)
    {
        // Default constructor initializes with default values
    }
}