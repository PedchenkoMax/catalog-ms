using Catalog.API.Events.Category;
using Catalog.Domain.Entities;
using Catalog.Tests.IntegrationTests.Events.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Catalog.Tests.IntegrationTests.Events;

[Collection("Event Fixture")]
public sealed class CategoryEventsTest :
    BaseEventTest<CategoryCreatedEventConsumer, CategoryUpdatedEventConsumer, CategoryDeletedEventConsumer>
{
    public CategoryEventsTest(EventFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task CreatedEvent_DoesntExist_CreatesCategory()
    {
        // Act
        var categoryCreatedEvent = new CategoryCreatedEvent(Guid.NewGuid(), "Name", "Image");
        await Publish(categoryCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var category = await FirstOrDefault<CategoryEntity>(categoryCreatedEvent.Id);

        category.Should().NotBeNull();
        category.Name.Should().Be(categoryCreatedEvent.Name);
        category.Image.Should().Be(categoryCreatedEvent.Image);
    }

    [Fact]
    public async Task CreatedEvent_Exists_LogCritical()
    {
        // Arrange
        var alreadyExistEntity = new CategoryEntity(Guid.NewGuid(), "Name", "Image");
        await AddEntity(alreadyExistEntity);


        // Act
        var categoryCreatedEvent = new CategoryCreatedEvent(alreadyExistEntity.Id, "Name", "Image");
        await Publish(categoryCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var category = await FirstOrDefault<CategoryEntity>(categoryCreatedEvent.Id);

        category.Should().NotBeNull();
        category.Name.Should().Be(alreadyExistEntity.Name);
        category.Image.Should().Be(alreadyExistEntity.Image);
    }

    [Fact]
    public async Task UpdatedEvent_UpdatesCategory()
    {
        // Arrange
        var initialEntity = new CategoryEntity(Guid.NewGuid(), "Name", "Image");
        await AddEntity(initialEntity);


        // Act
        var categoryUpdatedEvent = new CategoryUpdatedEvent(initialEntity.Id, "New Name", "New Image");
        await Publish(categoryUpdatedEvent);


        // Assert
        UpdatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var category = await FirstOrDefault<CategoryEntity>(categoryUpdatedEvent.Id);

        category.Should().NotBeNull();
        category.Name.Should().Be(categoryUpdatedEvent.Name);
        category.Image.Should().Be(categoryUpdatedEvent.Image);
    }

    [Fact]
    public async Task DeletedEvent_Exists_DeletesCategory()
    {
        // Arrange
        var entity = new CategoryEntity(Guid.NewGuid(), "Name", "Image");
        await AddEntity(entity);


        // Act
        var categoryDeletedEvent = new CategoryDeletedEvent(entity.Id);
        await Publish(categoryDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var category = await FirstOrDefault<CategoryEntity>(categoryDeletedEvent.Id);

        category.Should().BeNull();
    }

    [Fact]
    public async Task DeletedEvent_DoesntExist_LogCritical()
    {
        // Act
        var categoryDeletedEvent = new CategoryDeletedEvent(Guid.Empty);
        await Publish(categoryDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var category = await FirstOrDefault<CategoryEntity>(categoryDeletedEvent.Id);

        category.Should().BeNull();
    }
}