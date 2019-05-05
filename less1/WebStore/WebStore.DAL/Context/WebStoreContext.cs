using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreContext:IdentityDbContext<User>
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Таблица элемента заказа
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }
        /// <summary>
        /// Таблица заказов
        /// </summary>
        public DbSet<Order> Orders { get; set; }


        public WebStoreContext(DbContextOptions<WebStoreContext> options)
            :base(options)
        {

        }
    }
}
