using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>
    /// Сущность
    /// </summary>
    public abstract class BaseEntity
    {
        [Key] // Указание на то, что свойство является первичным ключем таблицы
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Требование для БД устанавливать значение данного свойства при добавлении записи в таблицу
               public int Id { get; set; }
    }
}
