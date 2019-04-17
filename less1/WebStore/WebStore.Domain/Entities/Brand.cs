using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Бренд
    /// </summary>
    [Table("Brands")] //Насильно указываем имя таблицы, Если это не сделать - то таблица будет Brand (по имени класса)
    public class Brand:NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        // virtual - Указание EntityF на то, что Products должно быть навигационным свойством!
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
