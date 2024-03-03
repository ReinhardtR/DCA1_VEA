using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

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
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesStartDateAfterEnd_ThenFailure()
    {
        
    }
    
    // UC4.F2
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesStartTimeAfterEnd_ThenFailure()
    {
        
    }
    
    // UC4.F3
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesDurationLessThanMinimumDurationSameDate_ThenFailure()
    {
        
    }
    
    // UC4.F4
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesDurationLessThanMinimumDurationDifferentDate_ThenFailure()
    {
        
    }
    
    // UC4.F5
    [Fact]
    public void GivenExistingEvent_WhenSettingTimeBeforeEarliestTime_ThenFailure()
    {
        
    }
    
    // UC4.F6
    [Fact]
    public void GivenExistingEvent_WhenSettingTimeBeforeLatestTime_ThenFailure()
    {
        
    }
    
    // UC4.F7
    [Fact]
    public void GivenExistingEventActive_WhenSettingTimes_ThenFailure()
    {
        
    }
    
    // UC4.F8
    [Fact]
    public void GivenExistingEventCancelled_WhenSettingTimes_ThenFailure()
    {
        
    }
    
    // UC4.F9
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesDurationHigherThanMaximumDuration_ThenFailure()
    {
        
    }
    
    // UC4.F10
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesStartTimeInPast_ThenFailure()
    {
        
    }
    
    // UC4.F11
    [Fact]
    public void GivenExistingEvent_WhenSettingTimesEventSpansBetweenLatestAndEarliestTime_ThenFailure()
    {
        
    }
}