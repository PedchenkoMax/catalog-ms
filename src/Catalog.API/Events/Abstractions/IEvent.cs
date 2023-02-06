using Catalog.Domain.Abstractions;

namespace Catalog.API.Events.Abstractions;

public interface IEvent<out T> where T : Entity
{
    public T ToEntity();
}