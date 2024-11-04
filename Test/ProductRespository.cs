using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ProductRespository : IProductRespository
    {
        public ProductResponse getProduct(string productID)
        {
            ProductResponse response = new ProductResponse();
            response.ProductId = productID;
            response.Productprice = 1000;
            response.ProductName = "MP3";
            return response;

        }

    }
}
