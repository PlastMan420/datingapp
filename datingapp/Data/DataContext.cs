using datingapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datingapp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) // base constructor of DbContext 
        {

        }

        //"in our class, in order to tell entity framework about our entities, we need to give
        // it some properties."
        // and these properties are of type "DbSet".

        // each property is a DB table:
        public DbSet<Value> Values { get; set; } //pluralize the name because this is gonna be the DB table name
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}

/*
    We deal with EntityFramework in this file.
     */