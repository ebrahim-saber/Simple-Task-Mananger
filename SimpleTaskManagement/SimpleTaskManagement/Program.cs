

internal class Program
{
    private static void Main(string[] args)
    {
        LoadTasksFromFile();
        ShowMenu();
    }

    static List<string> taskTitles = new List<string>();
    static List<string> taskDescriptions = new List<string>();
    static List<bool> taskStatus = new List<bool>();

    static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to Task Management System 'Ibrahim Saber Gaber' ");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Save and Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    MarkTaskAsCompleted();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    SaveTasksToFile();
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }

        }
    }

    static void AddTask()
    {
        bool addMorTasks = true;
        while (addMorTasks)
        {
            Console.Clear();
            Console.Write(" Enter task title: ");
            string title = Console.ReadLine();
            Console.Write(" Enter task description: ");
            string description = Console.ReadLine();

            taskTitles.Add(title);
            taskDescriptions.Add(description);
            taskStatus.Add(false);

            Console.WriteLine("\nDo you want to add another task? (y/n(any key)): ");
            string response = Console.ReadLine().ToLower();
            if (response != "y") 
            {
                addMorTasks = false;
            }
        }
        Console.WriteLine("\n Press Enter to return to the main menu...");
        Console.ReadLine();
    }

    static void ViewTasks()
    {
        Console.Clear();
        Console.WriteLine("\n Tasks:");
        for (int i = 0; i < taskStatus.Count; i++)
        {
            string status = taskStatus[i] ? "Complete" : "Pending";      //Ternary Operator

            //string status;
            //if (taskStatus[i])
            //{
            //    Console.WriteLine("Complete");
            //}
            //else 
            //{
            //    Console.WriteLine("Pending");
            //}

            Console.WriteLine($"{i + 1} - Task Title : {taskTitles[i]}");
            Console.WriteLine($"    Task Descriptions : {taskDescriptions[i]} ");
            Console.WriteLine($"    Task Status : {status}");
            Console.WriteLine("-------------------------------");
        }
        Console.WriteLine("\n Press Enter to return to the main menu...");
        Console.ReadLine();
    }

    static void MarkTaskAsCompleted()
    {
        Console.Clear();
        ViewTasksAddition();
        Console.Write("Enter the task number to mark as completed: ");
        int taskNumber; //= int.Parse(Console.ReadLine()); // if taskNumber == null then error will happen 
        if (int.TryParse(Console.ReadLine(), out taskNumber) && taskNumber <= taskTitles.Count)
        {
            taskStatus[taskNumber - 1] = true;
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }

        Console.WriteLine("\n Press Enter to return to the main menu...");
        Console.ReadLine();
    }

    static void DeleteTask()
    {
        Console.Clear();
        ViewTasksAddition();
        Console.Write("Enter the task number to delete: ");
        int taskNumber; //= int.Parse(Console.ReadLine());
        if (int.TryParse(Console.ReadLine() , out taskNumber) && taskNumber <= taskTitles.Count)
        {
            taskTitles.RemoveAt(taskNumber -1);
            taskDescriptions.RemoveAt(taskNumber -1);
            taskStatus.RemoveAt(taskNumber - 1);
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
        Console.WriteLine("\n Press Enter to return to the main menu...");
        Console.ReadLine();
    }

    static void SaveTasksToFile()
    {
        using (StreamWriter SW = new StreamWriter("tasks.txt"))
        for (int i = 0; i < taskTitles.Count; i++)
        {
            SW.WriteLine($"{taskTitles[i]}|{taskDescriptions[i]}|{taskStatus[i]}");
        }
    }

    static void LoadTasksFromFile()
    {
        if (File.Exists("tasks.txt"))
        {
            Console.WriteLine("Loading tasks from file...");

            string[] lines = File.ReadAllLines("tasks.txt");

            foreach (string line in lines)
            {
                // تقسيم السطر إلى الأجزاء الثلاثة: العنوان، الوصف، والحالة
                string[] parts = line.Split('|');

                // التحقق من أن السطر يحتوي على 3 أجزاء قبل إضافتها إلى القوائم
                if (parts.Length == 3)
                {
                    taskTitles.Add(parts[0].Trim());
                    taskDescriptions.Add(parts[1].Trim());

                    // محاولة تحويل الحالة إلى قيمة منطقية (true/false)
                    if (bool.TryParse(parts[2].Trim(), out bool isCompleted))
                    {
                        taskStatus.Add(isCompleted);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid task status in line: {line}. Skipping...");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid task format in line: {line}. Skipping...");
                }
            }

            Console.WriteLine("Tasks loaded successfully from file.");
        }
        else
        {
            Console.WriteLine("Task file not found.");
        }

        Console.WriteLine("\nPress Enter to Continue...");
        Console.ReadLine();
    }


    static void ViewTasksAddition()
    {
        Console.Clear();
        Console.WriteLine("\n Tasks:");
        for (int i = 0; i < taskStatus.Count; i++)
        {
            string status = taskStatus[i] ? "Complete" : "Pending";      //Ternary Operator
            Console.WriteLine($"{i + 1} - Task Title : {taskTitles[i]}");
            Console.WriteLine($"    Task Descriptions : {taskDescriptions[i]} ");
            Console.WriteLine($"    Task Status : {status}");
            Console.WriteLine("-------------------------------");
        }
    }

}
