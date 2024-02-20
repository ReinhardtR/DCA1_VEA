namespace VEA.Core.Tools.OperationResult.DummyClasses;

public class Task
{
    
    public string Title { get; }
    public List<SubTask> SubTasks { get; set; }
    
    public TaskStatus Status { get; set; }
    
    public enum TaskStatus
    {
        NotStarted,
        Started,
    }
    
    public Task(string title)
    {
        Title = title;
        SubTasks = new List<SubTask>();
        Status = TaskStatus.NotStarted;
    }
    
    //Returns Nothing, cannot fail.
    public void ReadyToStart()
    {
        //Send Emails to all my participants
        
        //Change the status of the task
        Status = TaskStatus.Started;
    }
    
    //Returns nothing, can Fail
    // 1. Title must be at least 3 characters long
    // 2. Title must be at most 12 characters long
    // 3. Must contain an "!" at the end
    public Result SetTitle(string title) 
    {
        if (title.Length < 3)
        {
            return Result.Failure("Title must be at least 3 characters long");
        }
        if (title.Length > 12)
        {
            return Result.Failure("Title must be at most 12 characters long");
        }
        if (!title.EndsWith("!"))
        {
            return Result.Failure("Title must contain an \"!\" at the end");
        }

        return Result.Success();
    }
    
    
    //Returns something, cannot fail
    public SubTask CreateSubTask_Cannot_Fail(string title, int timeToCompleteHours)
    {
        var subTask = new SubTask(title, timeToCompleteHours);
        SubTasks.Add(subTask);
        return subTask;
    }
    
    //Returns something, can fail
    public Result<SubTask> CreateSubTask_Can_Fail(string title, int timeToCompleteHours)
    {
        if (title.Length < 3)
        {
            return Result.Failure("Title must be at least 3 characters long");
        }
        if (title.Length > 12)
        {
            return Result.Failure("Title must be at most 12 characters long");
        }
        if (!title.EndsWith("!"))
        {
            return Result.Failure("Title must contain an \"!\" at the end");
        }
        
        var subTask = new SubTask(title, SubTasks.Count + 1, timeToCompleteHours);
        SubTasks.Add(subTask);
        return Result.Success(subTask);
    }
    
    
}