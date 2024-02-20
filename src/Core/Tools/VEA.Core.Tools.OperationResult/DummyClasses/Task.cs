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
    public Result<None> SetTitle(string title)
    {
        List<Error> errors = new List<Error>();
        
        if (title.Length < 3)
        {
            errors.Add(EventErrors.InvalidName());
        }
        if (title.Length > 12)
        {
            errors.Add(EventErrors.InvalidName());
        }
        if (!title.EndsWith("!"))
        {
            errors.Add(EventErrors.InvalidName());
        }

        return errors.Count > 0 ? Result<None>.Failure(errors) : Result<None>.Success();

    }
    
    
    //Returns something, cannot fail
    public Result<SubTask> CreateSubTask_Cannot_Fail(string title, int timeToCompleteHours)
    {
        var subTask = new SubTask(title, timeToCompleteHours);
        SubTasks.Add(subTask);
        return Result<SubTask>.Success(subTask);
    }
    
    //Returns something, can fail
    public Result<SubTask> CreateSubTask_Can_Fail(string title, int timeToCompleteHours)
    {
        List<Error> errors = new List<Error>();
        
        if (title.Length < 3)
        {
            errors.Add(EventErrors.InvalidName());
        }
        if (title.Length > 12)
        {
            errors.Add(EventErrors.InvalidName());
        }
        if (!title.EndsWith("!"))
        {
            errors.Add(EventErrors.InvalidName());
        }

        SubTask subTask = new SubTask(title, timeToCompleteHours);
        
        if (errors.Any())
        {
            return Result<SubTask>.Failure(errors);
        }
        
        SubTasks.Add(subTask);
        return Result<SubTask>.Success(subTask);
    }
}