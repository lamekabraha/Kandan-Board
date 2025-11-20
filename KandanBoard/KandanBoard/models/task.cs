using KandanBoard.models;
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
        Backlog,
        ToDo,
        InProgress,
        InReview,
        Done
    }

    private int _taskId;
    private string _title;
    private string _description;
    private TaskPriority _priority;
    private int _storyPoints;
    private DateOnly _dueDate;
    private int _creatorId;
    private int _assignedId;
    private TaskStatus _status;

    public int GetTaskId() {  return _taskId; }
    public string GetTitle() { return _title; }
    public string GetDescription() { return _description; }
    public TaskPriority GetPriority() { return _priority; }
    public int GetStoryPoints() { return _storyPoints; }
    public DateOnly GetDueDate() { return _dueDate; }
    public int GetCreatorId() { return _creatorId; }
    public int GetAssignedId() { return _assignedId; }
    public TaskStatus GetStatus() { return _status; }

    public void SetTaskId(int TaskId) { this._taskId = TaskId; }
    public void SetTitle(string Title) { this._title = Title; }
    public void SetDescription(string Description) { this._description = Description; }
    public void SetPriority(TaskPriority Priority) { this._priority = Priority; }
    public void SetStoryPoints(int StoryPoints) {  this._storyPoints = StoryPoints; }
    public void SetDueDate(DateOnly Date) { this._dueDate = Date; }
    public void SetCreatorId(int CreatorId) { this._creatorId = CreatorId; }
    public void SetAssignedId(int AssignedId) { this._assignedId = AssignedId; }
    public void SetTaskStatus(TaskStatus Status) { this._status = Status; }

    public Task(int taskId, string title, string description, TaskPriority priority, int storyPoints, DateOnly dueDate, int creatorId, int assignedId, TaskStatus status)
    {
        this._taskId = taskId;
        this._title = title;
        this._description = description;
        this._priority = priority;
        this._storyPoints = storyPoints;
        this._dueDate = dueDate;
        this._creatorId = creatorId;
        this._assignedId = assignedId;
        this._status = status;
    }    
}