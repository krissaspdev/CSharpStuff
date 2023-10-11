using AutoFixture.Xunit2;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace Sql.Extensions;

public class SqlExtensionsTests
{
    [Theory]
    [AutoData]
    public void ToDataTable_test(Guid id)
    {
        // Arrange
        var sizeData = new List<SampleUdtt>
        {
            new SampleUdtt
            {
                Id = id
            }
        };

        // Act
        var dataTable = sizeData.ToDataTable();

        // Assert
        dataTable.Should().NotBeNull();

        dataTable.Columns.Count.ShouldBe(1);
        dataTable.Columns[0].ColumnName.ShouldBe(nameof(SampleUdtt.Id));
        dataTable.Rows.Count.ShouldBe(1);
        dataTable.Rows[0][nameof(SampleUdtt.Id)].ShouldBe(sizeId);
    }

    [Theory]
    [AutoData]
    public void ToDataTable_complex_test(string name, int value)
    {
        // Arrange
        var sizeData = new List<DummyComplexClass>
        {
            new DummyComplexClass
            {
                Name = name,
                NullableValue = null,
                Value = value
            }
        };

        // Act
        var dataTable = sizeData.ToDataTable();

        // Assert
        dataTable.Should().NotBeNull();

        dataTable.Columns.Count.ShouldBe(3);
        dataTable.Columns[0].ColumnName.ShouldBe(nameof(DummyComplexClass.Name));
        dataTable.Columns[1].ColumnName.ShouldBe(nameof(DummyComplexClass.NullableValue));
        dataTable.Columns[2].ColumnName.ShouldBe(nameof(DummyComplexClass.Value));
        dataTable.Rows.Count.ShouldBe(1);
        dataTable.Rows[0][nameof(DummyComplexClass.Name)].ShouldBe(name);
        dataTable.Rows[0][nameof(DummyComplexClass.NullableValue)].ShouldBeOfType<DBNull>();
        dataTable.Rows[0][nameof(DummyComplexClass.Value)].ShouldBe(value);
    }

    private class DummyComplexClass
    {
        public string Name { get; set; } = string.Empty;
        public int? NullableValue { get; set; } = null;
        public int Value { get; set; }
    }

    private class SampleUdtt
    {
        public Guid Id { get; set; }
    }
}
