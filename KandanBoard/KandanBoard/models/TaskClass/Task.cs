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
        BACKLOG,
        TODO,
        INPROGRESS,
        INREVIEW,
        DONE
    }

    protected int taskId;
    protected string title;
    protected string desc;
    protected TaskPriority priority;
    protected TaskStatus status;
    protected int assignedUserId; // 0 or -1 means unassigned

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

    public int GetAssignedUserId()
    {
        return assignedUserId;
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

    public void SetAssignedUserId(int userId)
    {
        this.assignedUserId = userId;
    }

    public Task(int taskId, string title, string desc, TaskPriority priority, TaskStatus status, int assignedUserId = 0)
    {
        this.taskId = taskId;
        this.title = title;
        this.desc = desc;
        this.priority = priority;
        this.status = status;
        this.assignedUserId = assignedUserId;
    }

    public virtual string DisplayTask()
    {
        string assignedInfo = assignedUserId > 0 ? $"    ASSIGNED TO: User ID {assignedUserId}" : "    ASSIGNED TO: Unassigned";
        return $"[TASK {taskId}]    TITLE: {title}    STATUS: {status}{assignedInfo}";
    }
}