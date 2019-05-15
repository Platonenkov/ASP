using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO
{
    public class BrandDTO : INamedEntity
    {
        public  string Name { get; set; }

        public int Id { get; set; }
    }
}
