using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Tests.IntegrationTests.Events.Common;

// The only purpose of this wrapper is to resolve race condition.
// It occurs at the moment between event publishing and assert part where we get the entity from DB.
// Maybe, there's a more straightforward solution. You can try to do it better.
public class TestCatalogContext : CatalogContext
{
    public readonly AutoResetEvent ResetEvent;

    public TestCatalogContext(DbContextOptions<CatalogContext> options, AutoResetEvent resetEvent) : base(options)
    {
        ResetEvent = resetEvent;
    }

    public override ValueTask DisposeAsync()
    {
        ResetEvent.Set();
        return base.DisposeAsync();
    }
}