
using RestauranteApi.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestauranteApi.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Restaurante> Restaurante { get; set; }
        public DbSet<Prato> Prato { get; set; }

        public Contexto() {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contexto, Configuration>());
        }
    }
}