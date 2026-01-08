using KandanBoard.models.TaskClass;
using KandanBoard.models.UserClass;

namespace KandanBoard.persistence
{
    public static class DataManager
    {

        // Saving User Data
        private const string usersFilePath = "data/users.dat";

        private static void UserFileExists()
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
        }

        public static void SaveUsers(List<User> userList)
        {
            UserFileExists();
            FileStream file = File.Open(usersFilePath, FileMode.Create); // opens file if exists, creates file if doesn't exist
            BinaryWriter bw = new BinaryWriter(file);

            try
            {
                bw.Write(userList.Count); //returns number of users 

                foreach (User user in userList)
                {
                    // Identify User Type
                    if (user is Admin)
                    {
                        bw.Write(1);
                    }
                    else if (user is Member)
                    {
                        bw.Write(2);
                    }
                    else
                    {
                        bw.Write(0); // Base User type
                    }

                    bw.Write(user.GetUserId());
                    bw.Write(user.GetFirstName());
                    bw.Write(user.GetLastName());
                    bw.Write(user.GetEmail());
                    bw.Write(user.GetPassword());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to save binary data. {ex.Message}");
            }
            finally
            {
                //used to close the writer and file connection
                bw.Close();
                file.Close();
            }
        }

        public static List<User> LoadUser()
        {
            if (!File.Exists(usersFilePath))
            {
                return new List<User>();
            }

            List<User> userList = new List<User>();

            FileStream file = File.Open(usersFilePath, FileMode.Open);

            BinaryReader br = new BinaryReader(file);

            try
            {
                int userCount = br.ReadInt32();

                // loop through .dat to read user info
                for (int i = 0; i < userCount; i++)
                {
                    // Read user type first (must match SaveUsers order)
                    int typeId = br.ReadInt32();
                    
                    int userId = br.ReadInt32();
                    string firstName = br.ReadString();
                    string lastName = br.ReadString();
                    string email = br.ReadString();
                    string password = br.ReadString();

                    User user;
                    if (typeId == 1) // Admin user type
                    {
                        user = new Admin(userId, firstName, lastName, email, password);
                    }
                    else  // Member user type
                    {
                        user = new Member(userId, firstName, lastName, email, password);
                    }
                    userList.Add(user);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to load user data. {ex.Message}");
            }
            finally
            {
                br.Close();
                file.Close();
            }
            return userList;
        }

        // Saving Task Data
        private const string taskFilePath = "data/tasks.dat";

        private static void TasksFileExists()
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
        }

        public static void SaveTasks(List<Task> taskList)
        {
            TasksFileExists();
            FileStream file = File.Open(taskFilePath, FileMode.Create);
            BinaryWriter binary = new BinaryWriter(file);

            try
            {
                binary.Write(taskList.Count);

                foreach (Task task in taskList)
                {

                    // Identify Task Type
                    if (task is Bug) binary.Write(1);
                    else if (task is UserStory) binary.Write(2);
                    else if (task is Improvement) binary.Write(3);
                    else binary.Write(0);

                    // Get base data
                    binary.Write(task.GetTaskId());
                    binary.Write(task.GetTitle());
                    binary.Write(task.GetDesc());
                    binary.Write((int)task.GetPriority());
                    binary.Write((int)task.GetStatus());
                    binary.Write(task.GetAssignedUserId()); // Save assigned user ID

                    if (task is Bug bug)
                    {
                        binary.Write((int)bug.Severity);
                        binary.Write(bug.ReproductionSteps);
                    }
                    else if (task is UserStory userstory)
                    {
                        binary.Write(userstory.StoryPoints);
                        binary.Write(userstory.AcceptanceCriteria);
                    }
                    else if (task is Improvement improve)
                    {
                        binary.Write(improve.AffectedComponent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to save binary data. {ex.Message}");
            }
            finally
            {
                binary.Close();
                file.Close();
            }
        }

        public static List<Task> LoadTask()
        {
            if (!File.Exists(taskFilePath))
            {
                return new List<Task>();
            }

            List<Task> TaskList = new List<Task>();
            FileStream file = File.Open(taskFilePath, FileMode.Open);
            BinaryReader binary = new BinaryReader(file);

            try
            {
                int taskCount = binary.ReadInt32();

                for (int i = 0; i < taskCount; i++)
                { 
                    // read task type
                    int typeId = binary.ReadInt32();

                    // read task info
                    int taskId = binary.ReadInt32();
                    string title = binary.ReadString();
                    string desc = binary.ReadString();
                    Task.TaskPriority priority = (Task.TaskPriority)binary.ReadInt32();
                    Task.TaskStatus status = (Task.TaskStatus)binary.ReadInt32();
                    int assignedUserId = binary.ReadInt32();
                
                    // read bug task info
                    if (typeId == 1)
                    {
                        Bug.BugSeverity severity = (Bug.BugSeverity)binary.ReadInt32();
                        string reproductionSteps = binary.ReadString();
                        Bug bug = new Bug(taskId, title, desc, priority, status, severity, reproductionSteps);
                        bug.SetAssignedUserId(assignedUserId);
                        TaskList.Add(bug);
                    }
                    else if (typeId == 2) // read user story task info
                    {
                        int storyPoint = binary.ReadInt32();
                        string acceptanceCriteria = binary.ReadString();
                        UserStory userStory = new UserStory(taskId, title, desc, priority, status, storyPoint, acceptanceCriteria);
                        userStory.SetAssignedUserId(assignedUserId);
                        TaskList.Add(userStory);
                    }
                    else if (typeId == 3) // read improvement task info - Fixed bug: was taskId == 3
                    {
                        string affectedComponent = binary.ReadString(); // Fixed bug: was binary.ToString()
                        Improvement improvement = new Improvement(taskId, title, desc, priority, status, affectedComponent);
                        improvement.SetAssignedUserId(assignedUserId);
                        TaskList.Add(improvement);
                    }
                    else
                    {
                        Task baseTask = new Task(taskId, title, desc, priority, status, assignedUserId);
                        TaskList.Add(baseTask);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Failed to load task data");
            }
            finally
            {
                binary.Close();
                file.Close();
            }
            return TaskList;
        }


    }
}