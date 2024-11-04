using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test
{
    public partial class Form1 : Form, IProductView
    {
        IProductPresenter presenter;
        public Form1(DIContainer.PresenterFactory factory)
        {
            InitializeComponent();
            presenter = factory.Create<IProductPresenter,IProductView>(this); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            presenter.getProduct("No1");
        }

        void IProductView.OnGetResult(ProductResponse productResponse)
        {
            label1.Text = productResponse.ProductId.ToString();
            label2.Text = productResponse.ProductName.ToString();
            label3.Text = productResponse.Productprice.ToString();
        }
    }
}
