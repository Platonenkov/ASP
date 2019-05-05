using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>
        /// Имя человека сделавшего заказ
        /// </summary>
        ///
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        /// 
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        /// 
        [Required]
        public string Address { get; set; }
    }
}
