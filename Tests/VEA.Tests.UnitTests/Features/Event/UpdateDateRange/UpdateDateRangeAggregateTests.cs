using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;
using Xunit.Abstractions;

namespace VEA.Tests.UnitTests.Features.Event.UpdateDateRange;

public class UpdateDateRangeAggregateTests
{
    // UC4.S1
    [Theory]
    [InlineData("2023/08/25 19:00", "2023/08/25 23:59")]
    [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
    [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
    [InlineData("2023/08/25 10:00", "2023/08/25 20:00")]
    [InlineData("2023/08/25 13:00", "2023/08/25 23:00")]
    public void GivenExistingEvent_WhenSettingTimesSameDate_ThenSuccess(string start, string end)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(eventDateRange, veaEvent.DateRange);
    }

    // UC4.S2
    [Theory]
    [InlineData("2023/08/25 19:00", "2023/08/26 01:00")]
    [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
    [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
    public void GivenExistingEvent_WhenSettingTimesDifferentDates_ThenSuccess(string start, string end)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(eventDateRange, veaEvent.DateRange);
    }

    // UC4.S3
    [Theory]
    [InlineData("2023/08/25 19:00", "2023/08/25 23:59")]
    [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
    [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
    [InlineData("2023/08/25 10:00", "2023/08/25 20:00")]
    [InlineData("2023/08/25 13:00", "2023/08/25 23:00")]
    [InlineData("2023/08/25 19:00", "2023/08/26 01:00")]
    public void GivenExistingEvent_WhenSettingTimes_ThenSuccessAndStatusDraft(string start, string end)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(eventDateRange, veaEvent.DateRange);
        Assert.Equal(EventStatus.Draft, veaEvent.Status);
    }

    // UC4.S4
    [Theory]
    [InlineData("2030/08/25 19:00", "2030/08/25 23:59")]
    [InlineData("2030/08/25 12:00", "2030/08/25 16:30")]
    [InlineData("2030/08/25 08:00", "2030/08/25 12:15")]
    [InlineData("2030/08/25 10:00", "2030/08/25 20:00")]
    [InlineData("2030/08/25 13:00", "2030/08/25 23:00")]
    [InlineData("2030/08/25 19:00", "2030/08/26 01:00")]
    public void GivenExistingEvent_WhenSettingTimesInFuture_ThenSuccess(string start, string end)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(eventDateRange, veaEvent.DateRange);
    }

    // UC4.S5
    [Theory]
    [InlineData("2023/08/25 19:00", "2023/08/25 23:59")]
    [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
    [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
    [InlineData("2023/08/25 10:00", "2023/08/25 20:00")]
    [InlineData("2023/08/25 13:00", "2023/08/25 23:00")]
    [InlineData("2023/08/25 19:00", "2023/08/26 01:00")]
    public void GivenExistingEvent_WhenSettingTimesForEventDurationUnderLimit_ThenSuccess(string start, string end)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(eventDateRange, veaEvent.DateRange);
    }

    // UC4.F1
    [Theory]
    [InlineData("2023/08/26 19:00", "2023/08/25 01:00")]
    [InlineData("2023/08/26 19:00", "2023/08/25 23:59")]
    [InlineData("2023/08/27 12:00", "2023/08/25 16:30")]
    [InlineData("2023/08/01 08:00", "2023/07/31 12:15")]
    public void GivenExistingEvent_WhenSettingTimesStartDateAfterEnd_ThenFailure(string start, string end)
    {
        //Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        //Act
        var result = EventDateRange.Create(dateRange);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeStartMustBeBeforeEnd(), result.Errors);
    }

    // UC4.F2
    [Theory] 
    [InlineData("2023/08/26 19:00", "2023/08/26 14:00")]
    [InlineData("2023/08/26 16:00", "2023/08/26 00:00")]
    [InlineData("2023/08/26 19:00", "2023/08/26 18:59")]
    [InlineData("2023/08/26 12:00", "2023/08/26 10:10")]
    [InlineData("2023/08/26 08:00", "2023/08/26 00:30")]
    public void GivenExistingEvent_WhenSettingTimesStartTimeAfterEnd_ThenFailure(string start, string end)
    {
        //Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        //Act
        var result = EventDateRange.Create(dateRange);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeStartMustBeBeforeEnd(), result.Errors);
    }
    
    // UC4.F3
    [Theory]
    [InlineData("2023/08/26 14:00", "2023/08/26 14:50")]
    [InlineData("2023/08/26 18:00", "2023/08/26 18:59")]
    [InlineData("2023/08/26 12:00", "2023/08/26 12:30")]
    [InlineData("2023/08/26 08:00", "2023/08/26 08:00")]
    public void GivenExistingEvent_WhenSettingTimesDurationLessThanMinimumDurationSameDate_ThenFailure(string start, string end)
    {
        //Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        //Act
        var result = EventDateRange.Create(dateRange);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeStartTimeMustBeMinimumDurationBeforeEndTime(), result.Errors);
    }
    
    // UC4.F4
    [Theory]
    [InlineData("2023/08/25 23:30", "2023/08/26 00:15")]
    [InlineData("2023/08/30 23:01", "2023/08/31 00:00")]
    [InlineData("2023/08/30 23:59", "2023/08/31 00:01")]
    public void GivenExistingEvent_WhenSettingTimesDurationLessThanMinimumDurationDifferentDate_ThenFailure(string start, string end)
    {
        //Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        //Act
        var result = EventDateRange.Create(dateRange);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeStartTimeMustBeMinimumDurationBeforeEndTime(), result.Errors);
    }
    
    // UC4.F5
    [Theory]
    [InlineData("2023/08/25 07:50", "2023/08/25 14:00")]
    [InlineData("2023/08/25 07:59", "2023/08/25 15:00")]
    [InlineData("2023/08/25 01:01", "2023/08/25 08:30")]
    [InlineData("2023/08/25 05:59", "2023/08/25 07:59")]
    [InlineData("2023/08/25 00:59", "2023/08/25 07:59")]
    public void GivenExistingEvent_WhenSettingTimeBeforeEarliestTime_ThenFailure(string start, string end)
    {
        // Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        // Act
        var result = EventDateRange.Create(dateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeStartTimeMustBeAfterEarliestTime(), result.Errors);
    }
    
    // UC4.F6
    [Theory]
    [InlineData("2023/08/24 23:50", "2023/08/25 01:01")]
    [InlineData("2023/08/24 22:00", "2023/08/25 07:59")]
    [InlineData("2023/08/30 23:00", "2023/08/31 02:30")]
    public void GivenExistingEvent_WhenSettingTimeBeforeLatestTime_ThenFailure(string start, string end)
    {
        // Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        // Act
        var result = EventDateRange.Create(dateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeEndTimeMustBeBeforeLatestTime(), result.Errors);
    }
    
    // UC4.F7
    // Todo - depends on UC 9
    [Fact]
    public void GivenExistingEventActive_WhenSettingTimes_ThenFailure()
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse("2023/08/25 08:00");
        var endDate = DateTime.Parse("2023/08/25 10:00");
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;
        // veaEvent.Activate();

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.UpdateDateRangeWhenEventActive(), result.Errors);
    }
    
    // UC4.F8
    // Todo - depends on UC 24
    [Fact]
    public void GivenExistingEventCancelled_WhenSettingTimes_ThenFailure()
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        var startDate = DateTime.Parse("2023/08/25 08:00");
        var endDate = DateTime.Parse("2023/08/25 10:00");
        var dateRange = new DateRange(startDate, endDate);
        EventDateRange eventDateRange = EventDateRange.Create(dateRange).Payload;
        // veaEvent.Cancel();

        //Act
        Result result = veaEvent.UpdateDateRange(eventDateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.UpdateDateRangeWhenEventActive(), result.Errors);
    }
    
    // UC4.F9
    [Theory]
    [InlineData("2023/08/30 08:00", "2023/08/30 18:01")]
    [InlineData("2023/08/30 14:59", "2023/08/31 01:00")]
    [InlineData("2023/08/30 14:00", "2023/08/31 00:01")]
    [InlineData("2023/08/30 14:00", "2023/08/31 18:30")]
    public void GivenExistingEvent_WhenSettingTimesDurationExceedsMaximum_ThenFailure(string start, string end)
    {
        // Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        // Act
        var result = EventDateRange.Create(dateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeDurationExceedsMaximum(), result.Errors);
    }
    
    // UC4.F10
    [Fact]
    public void GivenExistingEvent_WhenSettingStartTimeInPast_ThenFailure()
    {
        // Arrange
        var currentDate = DateTime.Now;
        var pastStartDate = currentDate.AddDays(-1); 
        var endDate = currentDate.AddHours(1); 
        var dateRange = new DateRange(pastStartDate, endDate);

        // Act
        var result = EventDateRange.Create(dateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.EventStartTimeCannotBeInPast(), result.Errors);
    }
    
    // UC4.F11
    [Theory]
    [InlineData("2023/08/31 00:30", "2023/08/31 08:30")]
    [InlineData("2023/08/30 23:59", "2023/08/31 08:01")]
    [InlineData("2023/08/31 01:00", "2023/08/31 08:00")]
    public void GivenExistingEvent_WhenSettingTimesEventSpansBetweenLatestAndEarliestTime_ThenFailure(string start, string end)
    {
        // Arrange
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var dateRange = new DateRange(startDate, endDate);

        // Act
        var result = EventDateRange.Create(dateRange);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DateRange.DateRangeSpansBetweenLatestAndEarliestTime(), result.Errors);
    }
}