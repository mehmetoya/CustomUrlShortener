using Microsoft.EntityFrameworkCore;
using CustomUrlShortener.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomUrlShortener.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("");
        //}

        public DbSet<UrlMap> UrlMaps { get; set; }
    }
}
