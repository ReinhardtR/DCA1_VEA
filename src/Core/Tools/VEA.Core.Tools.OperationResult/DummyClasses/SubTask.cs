namespace VEA.Core.Tools.OperationResult.DummyClasses;

public class SubTask
{
    public string Title { get; set; }
    public int TimeToComplete_Hours { get; set; }
    
    public SubTask(string title, int timeToCompleteHours)
    {
        Title = title;
        TimeToComplete_Hours = timeToCompleteHours;
    }
    
    
    
}