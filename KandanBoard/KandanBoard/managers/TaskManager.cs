using KandanBoard.persistence;
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
        public static void viewBoard()
        {
            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ Kandan Board ~~~~~~~~~~ ");
            // Load tasks
            List<Task> taskList = DataManager.LoadTask();

            if (taskList != null)
            {
                if (taskList.Count > 0)
                {
                    foreach (var task in taskList)
                    {
                        Console.WriteLine(task.GetTitle());
                    }
                }
                else
                {
                    Console.WriteLine("\n No Task");
                    Console.WriteLine("\n Do you want to create a new task or return?");
                    Console.WriteLine("\n  1. New Task \n  2. Return");
                    string input = Console.ReadLine();

                    while (true)
                    {
                        switch (input)
                        {
                            case "1":
                                createTask();
                                break;
                            case "2":
                                KandanBoardApp app = new KandanBoardApp();
                                app.runApp();
                                break;
                            default:
                                Console.WriteLine("\n\n Please enter a valid number");
                                break;
                        }
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR: Can't find taskList");
            }

            Console.WriteLine("\n\n Return to the Main Menu by hitting 'Esc'.\n\n");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {

            }
        }

        public static void createTask()
        {

        }
    }
}
