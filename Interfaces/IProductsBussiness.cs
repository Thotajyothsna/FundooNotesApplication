using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entities;

namespace BussinessLayer.Interfaces
{
    public interface IProductsBussiness
    {
        public object AddProducts(ProductsModel productsModel);
        public List<ProductsEntity> GetByProductName(string productName);
    }
}
