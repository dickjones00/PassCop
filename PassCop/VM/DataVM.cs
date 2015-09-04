using PassCop.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassCop.VM
{
    public class DataVM
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual List<DataVM> Data { get; set; }
    }
}
