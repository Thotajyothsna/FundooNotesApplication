using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model
{
    public class StockUpdateModel
    {
        public long ProductId { get; set; }
        public long WarehouseId { get; set; }
        public int Current_Stock {  get; set; }
    }
}
