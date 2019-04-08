using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModel
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public string Name { get ; set ; }
        public int Id { get; set; }
        public int Order { get; set; }

        /// <summary>
        /// Дочерние секции
        /// </summary>
        public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();
        /// <summary>
        /// Родительская секция
        /// </summary>
        public SectionViewModel ParentSection { get; set; }
    }
}
