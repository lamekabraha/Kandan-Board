using KandanBoard.models;
using Microsoft.VisualBasic;
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

    protected int taskId;
    protected string title;
    protected string desc;
    protected TaskPriority priority;
    protected TaskStatus status;

    public int GetTaskId()
    {
        return taskId;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDesc()
    {
        return desc;
    }

    public TaskPriority GetPriority()
    {
        return priority;
    }

    public TaskStatus GetStatus()
    {
        return status;
    }

    public void SetTaskId(int taskId)
    {
        this.taskId = taskId;
    }

    public void SetTitle(string title)
    {
        this.title = title;
    }

    public void SetDesc(string desc)
    {
        this.desc = desc;
    }

    public void SetPriority(TaskPriority priority)
    {
        this.priority = priority;
    }

    public void SetStatus(TaskStatus status)
    {
        this.status = status;
    }

    public Task(int taskId, string title, string desc, TaskPriority priority, TaskStatus status)
    {
        this.taskId = taskId;
        this.title = title;
        this.desc = desc;
        this.priority = priority;
        this.status = status;
    }

    public virtual string DisplayTask()
    {
        return $"[Task {taskId}] {title} {status}";
    }
}