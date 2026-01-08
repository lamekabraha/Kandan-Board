using KandanBoard.persistence;
using KandanBoard.models;
using KandanBoard.models.TaskClass;
using KandanBoard.models.UserClass;
using KandanBoard;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KandanBoard.managers
{
    internal class TaskManager
    {
        public static void viewBoard(KandanBoardApp app, User? currentUser)
        {
            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ Kandan Board ~~~~~~~~~~ ");
            // Load tasks
            List<Task> taskList = DataManager.LoadTask();

            if (taskList != null && taskList.Count > 0)
            {
                foreach (var task in taskList)
                {
                    Console.WriteLine(task.DisplayTask());
                }
            }
            else
            {
                Console.WriteLine("\n No Tasks");
            }
            while (true)
            {
                Console.WriteLine("\n Do you want to create a new task or return?");
                Console.Write("\n  1. New Task \n  2. Edit Task");
                
                // Only show Assign Task option for Admins
                if (currentUser != null && currentUser.CanAssignTask())
                {
                    Console.Write(" \n  3. Assign Task");
                    Console.Write(" \n  4. Return");
                }
                else
                {
                    Console.Write(" \n  3. Return");
                }
                
                Console.Write("\n\n Select an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        createTask(currentUser);
                        break;
                    case "2":
                        editTask();
                        break;
                    case "3":
                        if (currentUser != null && currentUser.CanAssignTask())
                        {
                            assignTask(currentUser);
                        }
                        else
                        {
                            app.mainMenu();
                        }
                        break;
                    case "4":
                        if (currentUser != null && currentUser.CanAssignTask())
                        {
                            app.mainMenu();
                        }
                        else
                        {
                            Console.WriteLine("\n\n Please enter a valid number");
                        }
                        break;
                    default:
                        Console.WriteLine("\n\n Please enter a valid number");
                        break;
                }

            }
        }
        public static void createTask(User? currentUser)
        {
            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ Create New Task ~~~~~~~~~~ ");

            List<Task> tasks = DataManager.LoadTask();
            int newTaskId = 1;
            if (tasks.Count > 0)
            {
                // create new task id
                foreach (var task in tasks)
                {
                    if (task.GetTaskId() >= newTaskId)
                    {
                        newTaskId = task.GetTaskId() + 1;
                    }
                }
            }

            // Determine assigned user ID: auto-assign to Member who creates it, 0 for Admins
            int assignedUserId = 0;
            if (currentUser != null && currentUser is Member)
            {
                assignedUserId = currentUser.GetUserId();
            }

            // Read task type
            Console.WriteLine("\n What type of task would you like to create?");
            Console.WriteLine("  0. General Task");
            Console.WriteLine("  1. Bug");
            Console.WriteLine("  2. User Story");
            Console.WriteLine("  3. Improvement");
            Console.WriteLine("\n Task Type: ");
            int taskType = Convert.ToInt32(Console.ReadLine());

            // Read new task info
            Console.Write("\n Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("\n Description");
            string desc = Console.ReadLine() ?? "";

            Console.WriteLine("\n Select Priority Level: \n  1.Very Low\n  2.Low\n  3.Medium\n  4.High\n  5.Very High");
            Console.Write("\n Number: ");
            int priorityNum = Convert.ToInt32(Console.ReadLine());
            Task.TaskPriority priority = Task.TaskPriority.Medium;

            if (priorityNum == 1)
            {
                priority = Task.TaskPriority.VeryLow;
            }
            else if (priorityNum == 2)
            {
                priority = Task.TaskPriority.Low;
            }
            else if (priorityNum == 3)
            {
                priority = Task.TaskPriority.Medium;
            }
            else if (priorityNum == 4)
            {
                priority = Task.TaskPriority.High;
            }
            else if (priorityNum == 5)
            {
                priority = Task.TaskPriority.VeryHigh;
            }

            Task newTask = null;

            if (taskType == 1)
            {
                Console.WriteLine("\n\n Severity: \n  1.Trivial\n  2.Minor\n  4.Moderate\n  4.Major\n  5.Critical");
                Console.Write("\n Number: ");
                int severityNum = Convert.ToInt32(Console.ReadLine());
                Bug.BugSeverity severity = Bug.BugSeverity.Moderate;
                Console.Write(" Reproduction Steps: ");
                string steps = Console.ReadLine() ?? "";

                if (severityNum == 1)
                {
                    severity = Bug.BugSeverity.Trivial;
                }
                else if (severityNum == 2)
                {
                    severity = Bug.BugSeverity.Minor;
                }
                else if (severityNum == 3)
                {
                    severity = Bug.BugSeverity.Moderate;
                }
                else if (severityNum == 4)
                {
                    severity = Bug.BugSeverity.Major;
                }
                else if (severityNum == 5)
                {
                    severity = Bug.BugSeverity.Critical;
                }
                newTask = new Bug(newTaskId, title, desc, priority, Task.TaskStatus.BACKLOG, severity, steps);
            }
            else if (taskType == 2)
            {
                Console.WriteLine(" Story Points (1, 2, 3, 5, 8, 13):");
                int storyPoints = Convert.ToInt32(Console.ReadLine());
                Console.Write(" Acceptance Criteria: ");
                string criteria = Console.ReadLine() ?? "";

                newTask = new UserStory(newTaskId, title, desc, priority, Task.TaskStatus.BACKLOG, storyPoints, criteria);
            }
            else if (taskType == 3)
            {
                Console.Write(" Affected Component: ");
                string component = Console.ReadLine() ?? "";
                newTask = new Improvement(newTaskId, title, desc, priority, Task.TaskStatus.BACKLOG, component);
            }
            else
            {
                newTask = new Task(newTaskId, title, desc, priority, Task.TaskStatus.BACKLOG);
            }

            if (newTask != null)
            {
                // Set assigned user ID (auto-assigned if Member, unassigned if Admin)
                newTask.SetAssignedUserId(assignedUserId);
                
                tasks.Add(newTask);
                DataManager.SaveTasks(tasks);
                
                string assignmentInfo = assignedUserId > 0 ? $" (Auto-assigned to you)" : "";
                Console.WriteLine($"\n\n SUCCESS: Task {newTaskId} {title} created successfully!{assignmentInfo}");
            }
            else
            {
                Console.WriteLine("Press any key to contintue...");
                Console.ReadKey();
            }
        }

        public static void editTask()
        {
            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ Edit Task ~~~~~~~~~~ ");
            Console.WriteLine("\n\n Select a task ID to edit: \n");
            
            List<Task> taskList = DataManager.LoadTask(); // Load tasks
            if (taskList == null || taskList.Count == 0) // if no tasks in tasks.dat
            {
                Console.WriteLine("\n No tasks available to edit.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            foreach (var taskToDisplay in taskList) // display tasks
            {
                Console.WriteLine(taskToDisplay.DisplayTask());
            }
            
            Console.Write("\n Task ID: ");
            string? taskIdInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(taskIdInput) || !int.TryParse(taskIdInput, out int taskId)) //invalid taskId
            {
                Console.WriteLine("\n Invalid task ID.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Task? task = taskList.FirstOrDefault(t => t.GetTaskId() == taskId); //search taskId in list
            if (task == null)
            {
                Console.WriteLine("\n Task not found.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ Task Found ~~~~~~~~~~ ");
            Console.WriteLine("\n Task Details: \n");
            Console.WriteLine(task.DisplayTask());
            Console.WriteLine($"\n Current Title: {task.GetTitle()}");
            Console.WriteLine($" Current Description: {task.GetDesc()}");
            Console.WriteLine($" Current Priority: {task.GetPriority()}");
            Console.WriteLine($" Current Status: {task.GetStatus()}");

            // Display type specific fields
            if (task is Bug bug)
            {
                Console.WriteLine($" Current Severity: {bug.Severity}");
                Console.WriteLine($" Current Reproduction Steps: {bug.ReproductionSteps}");
            }
            else if (task is UserStory userStory)
            {
                Console.WriteLine($" Current Story Points: {userStory.StoryPoints}");
                Console.WriteLine($" Current Acceptance Criteria: {userStory.AcceptanceCriteria}");
            }
            else if (task is Improvement improvement)
            {
                Console.WriteLine($" Current Affected Component: {improvement.AffectedComponent}");
            }

            Console.WriteLine("\n\n Select an option to edit: ");
            Console.WriteLine("  1. Title");
            Console.WriteLine("  2. Description");
            Console.WriteLine("  3. Priority");
            Console.WriteLine("  4. Status");

            // Add type specific edit options
            if (task is Bug)
            {
                Console.WriteLine("  5. Severity");
                Console.WriteLine("  6. Reproduction Steps");
            }
            else if (task is UserStory)
            {
                Console.WriteLine("  5. Story Points");
                Console.WriteLine("  6. Acceptance Criteria");
            }
            else if (task is Improvement)
            {
                Console.WriteLine("  5. Affected Component");
            }

            Console.WriteLine("  0. Cancel");
            Console.Write("\n Option: ");
            string? editChoice = Console.ReadLine();

            bool taskUpdated = false;

            switch (editChoice)
            {
                case "1": 
                    Console.Write("\n Enter new title: ");
                    string? newTitle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        task.SetTitle(newTitle);
                        taskUpdated = true;
                    }
                    break;

                case "2": 
                    Console.Write("\n Enter new description: ");
                    string? newDesc = Console.ReadLine();
                    if (newDesc != null)
                    {
                        task.SetDesc(newDesc);
                        taskUpdated = true;
                    }
                    break;

                case "3": 
                    Console.Write("\n Select Priority Level: \n  1.Very Low\n  2.Low\n  3.Medium\n  4.High\n  5.Very High\n Number: ");
                    string? priorityInput = Console.ReadLine();
                    if (int.TryParse(priorityInput, out int priorityNum))
                    {
                        Task.TaskPriority newPriority = Task.TaskPriority.Medium;
                        if (priorityNum == 1) newPriority = Task.TaskPriority.VeryLow;
                        else if (priorityNum == 2) newPriority = Task.TaskPriority.Low;
                        else if (priorityNum == 3) newPriority = Task.TaskPriority.Medium;
                        else if (priorityNum == 4) newPriority = Task.TaskPriority.High;
                        else if (priorityNum == 5) newPriority = Task.TaskPriority.VeryHigh;
                        task.SetPriority(newPriority);
                        taskUpdated = true;
                    }
                    break;

                case "4": 
                    Console.Write("\n Select Status: \n  1.Backlog\n  2.Todo\n  3.In Progress\n  4.In Review\n  5.Done\n Number: ");
                    string? statusInput = Console.ReadLine();
                    if (int.TryParse(statusInput, out int statusNum))
                    {
                        Task.TaskStatus newStatus = Task.TaskStatus.BACKLOG;
                        if (statusNum == 1) newStatus = Task.TaskStatus.BACKLOG;
                        else if (statusNum == 2) newStatus = Task.TaskStatus.TODO;
                        else if (statusNum == 3) newStatus = Task.TaskStatus.INPROGRESS;
                        else if (statusNum == 4) newStatus = Task.TaskStatus.INREVIEW;
                        else if (statusNum == 5) newStatus = Task.TaskStatus.DONE;
                        task.SetStatus(newStatus);
                        taskUpdated = true;
                    }
                    break;

                case "5": 
                    if (task is Bug bugTask)
                    {
                        Console.Write("\n Select Severity: \n  1.Trivial\n  2.Minor\n  3.Moderate\n  4.Major\n  5.Critical\n Number: ");
                        string? severityInput = Console.ReadLine();
                        if (int.TryParse(severityInput, out int severityNum))
                        {
                            Bug.BugSeverity newSeverity = Bug.BugSeverity.Moderate;
                            if (severityNum == 1) newSeverity = Bug.BugSeverity.Trivial;
                            else if (severityNum == 2) newSeverity = Bug.BugSeverity.Minor;
                            else if (severityNum == 3) newSeverity = Bug.BugSeverity.Moderate;
                            else if (severityNum == 4) newSeverity = Bug.BugSeverity.Major;
                            else if (severityNum == 5) newSeverity = Bug.BugSeverity.Critical;
                            bugTask.SetSeverity(newSeverity);
                            taskUpdated = true;
                        }
                    }
                    else if (task is UserStory userStoryTask)
                    {
                        Console.Write("\n Enter new story points (1, 2, 3, 5, 8, 13): ");
                        string? pointsInput = Console.ReadLine();
                        if (int.TryParse(pointsInput, out int storyPoints))
                        {
                            userStoryTask.SetStoryPoints(storyPoints);
                            taskUpdated = true;
                        }
                    }
                    else if (task is Improvement improvementTask)
                    {
                        Console.Write("\n Enter new affected component: ");
                        string? component = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(component))
                        {
                            improvementTask.SetAffectedComponent(component);
                            taskUpdated = true;
                        }
                    }
                    break;

                case "6": 
                    if (task is Bug bugTask2)
                    {
                        Console.Write("\n Enter new reproduction steps: ");
                        string? steps = Console.ReadLine();
                        if (steps != null)
                        {
                            bugTask2.SetReproductionSteps(steps);
                            taskUpdated = true;
                        }
                    }
                    else if (task is UserStory userStoryTask2)
                    {
                        Console.Write("\n Enter new acceptance criteria: ");
                        string? criteria = Console.ReadLine();
                        if (criteria != null)
                        {
                            userStoryTask2.SetAcceptanceCriteria(criteria);
                            taskUpdated = true;
                        }
                    }
                    break;

                case "0":
                    Console.WriteLine("\n Edit cancelled.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                    return;

                default:
                    Console.WriteLine("\n Invalid option.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                    return;
            }

            if (taskUpdated)
            {
                // update task to list and save
                int index = taskList.FindIndex(t => t.GetTaskId() == taskId);
                if (index >= 0)
                {
                    taskList[index] = task;
                    DataManager.SaveTasks(taskList);
                    Console.WriteLine("\n SUCCESS: Task updated successfully!");
                }
                else
                {
                    Console.WriteLine("\n ERROR: Failed to update task in list.");
                }
            }
            else
            {
                Console.WriteLine("\n No changes were made.");
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        public static void assignTask(User currentUser)
        {
            // Check if user is admin
            if (!currentUser.CanAssignTask())
            {
                Console.WriteLine("\n ERROR: Only Administrators can assign tasks.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ Assign Task ~~~~~~~~~~ ");
            
            List<Task> taskList = DataManager.LoadTask();
            if (taskList == null || taskList.Count == 0)
            {
                Console.WriteLine("\n No tasks available to assign.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Display all tasks
            Console.WriteLine("\n Available Tasks:\n");
            foreach (var task in taskList)
            {
                string assignedInfo = task.GetAssignedUserId() > 0 ? $"(Assigned to User ID: {task.GetAssignedUserId()})" : "(Unassigned)";
                Console.WriteLine($"  [{task.GetTaskId()}] {task.GetTitle()} - {task.GetStatus()} {assignedInfo}");
            }

            Console.Write("\n Enter Task ID to assign: ");
            string? taskIdInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(taskIdInput) || !int.TryParse(taskIdInput, out int taskId))
            {
                Console.WriteLine("\n Invalid task ID.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Task? selectedTask = taskList.FirstOrDefault(t => t.GetTaskId() == taskId);
            if (selectedTask == null)
            {
                Console.WriteLine("\n Task not found.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Load all users (both Members and Admins)
            List<User> userList = DataManager.LoadUser();

            if (userList.Count == 0)
            {
                Console.WriteLine("\n No users available to assign tasks to.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // Display all users with their types
            Console.WriteLine("\n Available Users:\n");
            foreach (var user in userList)
            {
                string userType = user is Admin ? "Admin" : "Member";
                Console.WriteLine($"  [{user.GetUserId()}] {user.GetFirstName()} {user.GetLastName()} ({user.GetEmail()}) - {userType}");
            }

            Console.Write("\n Enter User ID to assign task to (or 0 to unassign): ");
            string? userIdInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userIdInput) || !int.TryParse(userIdInput, out int userId))
            {
                Console.WriteLine("\n Invalid user ID.");
                Console.WriteLine("\n Press any key to continue...");
                Console.ReadKey();
                return;
            }

            if (userId == 0)
            {
                // Unassign task
                selectedTask.SetAssignedUserId(0);
                Console.WriteLine($"\n SUCCESS: Task {taskId} has been unassigned.");
            }
            else
            {
                // Validate user ID exists
                User? assignedUser = userList.FirstOrDefault(u => u.GetUserId() == userId);
                if (assignedUser == null)
                {
                    Console.WriteLine("\n Invalid user ID. User not found.");
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                selectedTask.SetAssignedUserId(userId);
                string userType = assignedUser is Admin ? "Admin" : "Member";
                Console.WriteLine($"\n SUCCESS: Task {taskId} '{selectedTask.GetTitle()}' has been assigned to {assignedUser.GetFirstName()} {assignedUser.GetLastName()} ({userType}).");
            }

            // Update task in list and save
            int index = taskList.FindIndex(t => t.GetTaskId() == taskId);
            if (index >= 0)
            {
                taskList[index] = selectedTask;
                DataManager.SaveTasks(taskList);
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
