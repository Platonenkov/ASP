using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Упорядочеваемая сущность
    /// </summary>
    public interface IOrderedEntity
    {
        /// <summary>
        /// Порядок
        /// </summary>
        int Order { get; set; }
    }
}
