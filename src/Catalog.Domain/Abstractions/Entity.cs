namespace Catalog.Domain.Abstractions;

public class Entity
{
    protected Entity()
    {
    }

    public Guid Id { get; set; }
}