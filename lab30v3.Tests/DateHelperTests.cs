using lab30v3;
using Xunit;

namespace lab30v3.Tests;

public class DateHelperTests
{
    private readonly DateHelper helper = new DateHelper();

    [Fact]
    public void IsWeekend_ShouldReturnTrue_ForSaturday()
    {
        var date = new DateTime(2024, 6, 15);
        Assert.True(helper.IsWeekend(date));
    }

    [Fact]
    public void IsWeekend_ShouldReturnTrue_ForSunday()
    {
        var date = new DateTime(2024, 6, 16);
        Assert.True(helper.IsWeekend(date));
    }

    [Fact]
    public void IsWeekend_ShouldReturnFalse_ForWeekday()
    {
        var date = new DateTime(2024, 6, 17);
        Assert.False(helper.IsWeekend(date));
    }

    [Theory]
    [InlineData("2024-06-10", "2024-06-15", 5)]
    [InlineData("2024-01-01", "2024-01-02", 1)]
    [InlineData("2024-05-01", "2024-05-01", 0)]
    public void DaysBetween_ShouldReturnCorrectDays(string start, string end, int expected)
    {
        var result = helper.DaysBetween(DateTime.Parse(start), DateTime.Parse(end));
        Assert.Equal(expected, result);
    }

    [Fact]
    public void DaysBetween_ShouldWorkWithReversedDates()
    {
        var result = helper.DaysBetween(
            new DateTime(2024, 6, 10),
            new DateTime(2024, 6, 5)
        );

        Assert.Equal(5, result);
    }

    [Fact]
    public void AddBusinessDays_ShouldSkipWeekend()
    {
        var start = new DateTime(2024, 6, 14);
        var result = helper.AddBusinessDays(start, 1);

        Assert.Equal(new DateTime(2024, 6, 17), result);
    }

    [Fact]
    public void AddBusinessDays_AddFiveDays()
    {
        var start = new DateTime(2024, 6, 10);
        var result = helper.AddBusinessDays(start, 5);

        Assert.Equal(new DateTime(2024, 6, 17), result);
    }

    [Theory]
    [InlineData("2024-06-10", 1, "2024-06-11")]
    [InlineData("2024-06-10", 2, "2024-06-12")]
    [InlineData("2024-06-14", 1, "2024-06-17")]
    public void AddBusinessDays_MultipleCases(string start, int days, string expected)
    {
        var result = helper.AddBusinessDays(
            DateTime.Parse(start),
            days
        );

        Assert.Equal(DateTime.Parse(expected), result);
    }
}