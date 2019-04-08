namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public abstract class NamedEntity :BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
