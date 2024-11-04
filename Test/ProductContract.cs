using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public interface IProductPresenter
    {
        //取得商品資料
        void getProduct(String productID);
    }

    public interface IProductView
    {
        //取得資料的Callback
        void OnGetResult(ProductResponse productResponse);
    }
}
