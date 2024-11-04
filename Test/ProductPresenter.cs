using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test
{
    internal class ProductPresenter : IProductPresenter
    {
        ProductRespository productRespository;
        private readonly IProductView _view;

        public ProductPresenter(IProductView view)
        {
            this._view = view;
            productRespository = new ProductRespository();
            
        }

        public void getProduct(string productID)
        {
            ProductResponse productResponse = productRespository.getProduct(productID);
            
            this._view.OnGetResult(productResponse);
        }

        //public void getProduct(string productID)
        //{
        //    ProductResponse productResponse = productRespository.getProduct(productID);
        //    this.productView.onGetResult(productResponse);
        //}


    }
}
