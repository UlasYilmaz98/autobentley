using autobentley1.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autobentley1.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        //TABLOLARA KARŞILIK GELEN DB SETLERİ.
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Form> Formlar { get; set; }
        public DbSet<SoruCevap> SoruCevaplar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }


    }
}
