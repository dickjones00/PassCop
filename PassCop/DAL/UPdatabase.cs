using PassCop.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassCop.DAL
{
    public partial class UPdatabase : DbContext
    {
        public UPdatabase()
            : base("name=UPdatabase")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<int>()
                        .Where(x => x.Name == "ID")
                        .Configure(x => x.IsKey().HasColumnOrder(1));

            modelBuilder.Properties()
                        .Where(x => x.Name == "Username")
                        .Configure(x => x.HasColumnOrder(2));

            modelBuilder.Properties()
                        .Where(x => x.Name == "Password")
                        .Configure(x => x.HasColumnOrder(3));

        }
        public virtual DbSet<DataVM> UpData { get; set; }
    }
}
