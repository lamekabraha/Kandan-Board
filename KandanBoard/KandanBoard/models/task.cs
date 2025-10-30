using System;

public class Task
{
    public enum TaskPriority
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        InReview,
        Done
    }

    public int TaskId { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public TaskPriority Priority { get; set; }
    public int StoryPoints { get; set; }
    public DateTime DueDate { get; set; }
    public String CreatedBy { get; set; }
    public String UpdatedBy { get; set; }
    public String AssignedTo { get; set; }
    public TaskStatus Status { get; set; }

    public Task(int taskId, String title, TaskStatus status)
    {
        this.TaskId = taskId;
        this.Title = title;
        this.Status = status;
    }

    public override string ToString()
    {
        return $"{TaskId} {Title} has been set to '{Status}'";
    }
    
}