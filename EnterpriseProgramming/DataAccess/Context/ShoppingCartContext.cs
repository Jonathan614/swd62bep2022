using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    //this will represent the database - it is an abstraction
    public class ShoppingCartContext : IdentityDbContext<CustomUser>
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
          : base(options)
        {
        }


        //1. autonumber for ids
        //2. commands to generate the db

        //names you are giving to these properties, will be used for the table names
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
      
    }
}
