using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Order : NamedEntity
    {
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

       // public virtual ICollection<OrderItem> OrderItems { get; set; }
        /// <summary>Элементы заказа</summary>
        public virtual ICollection<OrderItem> Orders { get; set; } = new HashSet<OrderItem>();
    }
}