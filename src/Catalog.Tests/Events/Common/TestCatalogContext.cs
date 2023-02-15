namespace Catalog.Tests.Events.Common;

public class TestCatalogContext : CatalogContext
{
    public static AutoResetEvent ResetEvent = new(false);

    public TestCatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public override ValueTask DisposeAsync()
    {
        ResetEvent.Set();
        return base.DisposeAsync();
    }
}