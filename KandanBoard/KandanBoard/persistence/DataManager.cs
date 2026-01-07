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
                    int userId = br.ReadInt32();
                    string firstName = br.ReadString();
                    string lastName = br.ReadString();
                    string email = br.ReadString();
                    string password = br.ReadString();

                    User user = new User(userId, firstName, lastName, email, password);
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
            FileStream file = File.Open(taskFilePath, FileMode.Create); // opens file if exists, creates file if doesn't exist
            BinaryWriter bw = new BinaryWriter(file);

            try
            {
                bw.Write(taskList.Count); //returns number of users 

                foreach (Task task in taskList)
                {
                    bw.Write(task.GetTaskId());
                    bw.Write(task.GetTitle());
                    bw.Write(task.GetDescription());
                    bw.Write(task.GetPriority().ToString());
                    bw.Write(task.GetStoryPoints());
                    bw.Write(task.GetDueDate().ToString());
                    bw.Write(task.GetCreatorId());
                    bw.Write(task.GetAssignedId());
                    bw.Write(task.GetStatus().ToString());

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

        public static List<Task> LoadTask()
        {
            if (!File.Exists(taskFilePath))
            {
                return new List<Task>();
            }
    
            List<Task> TaskList = new List<Task>();

            FileStream file = File.Open(taskFilePath, FileMode.Open);

            BinaryReader br = new BinaryReader(file);

            try
            {
                int taskCount = br.ReadInt32();

                for (int i = 0; i < taskCount; i++)
                {
                    int taskId = br.ReadInt32();
                    string title = br.ReadString();
                    string description = br.ReadString();
                    string priorityString = br.ReadString();
                    int storyPoints = br.ReadInt32();
                    string dueDateString = br.ReadString();
                    int creatorId = br.ReadInt32();
                    int assignedId = br.ReadInt32();
                    string statusString = br.ReadString();

                    Task.TaskPriority priority = (Task.TaskPriority)Enum.Parse(typeof(Task.TaskPriority), priorityString);
                    DateOnly dueDate = DateOnly.Parse(dueDateString);
                    Task.TaskStatus status = (Task.TaskStatus)Enum.Parse(typeof(Task.TaskStatus), statusString);

                    Task task = new Task(taskId, title, description, priority, storyPoints, dueDate, creatorId, assignedId, status);

                    TaskList.Add(task);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to load task data. {ex.Message}");
            }
            finally
            {
                br.Close();
                file.Close();
            }
            return TaskList;
        }

    }
}