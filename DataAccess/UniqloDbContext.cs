﻿using Microsoft.EntityFrameworkCore;
using UniqloMVC.Models;

namespace UniqloMVC.DataAccess
{
    public class UniqloDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public UniqloDbContext(DbContextOptions opt):base(opt)
        {
            
        }
    }
}
