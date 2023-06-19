using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToDoReminder
{
    /// <summary>
    /// Manages tasks and provides operations for adding, deleting, and reading tasks.
    /// </summary>
    class TaskManager
    {
        List<Task> todoTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskManager"/> class.
        /// </summary>
        public TaskManager()
        {
            todoTask = new List<Task>();
        }

        /// <summary>
        /// Gets the task at the specified index.
        /// </summary>
        /// <param name="index">The index of the task to retrieve.</param>
        /// <returns>The task at the specified index, or null if the index is invalid.</returns>
        public Task GetTask(int index)
        {
            if (CheckIndex(index))
            {
                return todoTask[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the specified index is within the valid range of tasks.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if the index is valid, false otherwise.</returns>
        public bool CheckIndex(int index)
        {
            bool ok = false;

            if ((index >= 0) && (index < todoTask.Count))
            {
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Adds a new task to the task list.
        /// </summary>
        /// <param name="newTask">The task to add.</param>
        /// <returns>True if the task was added successfully, false otherwise.</returns>
        public bool AddNewTask(Task newTask)
        {
            bool ok = true;

            if (newTask != null)
            {
                todoTask.Add(newTask);
            }
            else
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Adds a new task with the specified parameters to the task list.
        /// </summary>
        /// <param name="time">The task's date and time.</param>
        /// <param name="description">The task's description.</param>
        /// <param name="priority">The task's priority.</param>
        /// <returns>True if the task was added successfully, false otherwise.</returns>
        public bool AddNewTask(DateTime time, string description, PriorityType priority)
        {
            bool ok = true;

            Task newTask = new Task(time, description, priority);

            if (newTask != null)
            {
                todoTask.Add(newTask);
            }
            else
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Deletes the task at the specified index.
        /// </summary>
        /// <param name="index">The index of the task to delete.</param>
        /// <returns>True if the task was deleted successfully, false otherwise.</returns>
        public bool DeleteTaskAt(int index)
        {
            bool ok = false;

            if ((index >= 0) && (index < todoTask.Count))
            {
                todoTask.RemoveAt(index);
                ok = true;
            }

            return ok;
        }

        /// <summary>
        /// Changes the task at the specified index to the provided task.
        /// </summary>
        /// <param name="task">The new task to replace the existing task.</param>
        /// <param name="index">The index of the task to change.</param>
        /// <returns>True if the task was changed successfully, false otherwise.</returns>
        public bool ChangeTaskAt(Task task, int index)
        {
            bool ok = true;

            if ((task != null) && CheckIndex(index))
            {
                todoTask[index] = task;
                ok = true;
            }
            else
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Retrieves an array of strings containing information about each task in the task list.
        /// </summary>
        /// <returns>An array of strings containing task information.</returns>
        public string[] GetInfo()
        {
            string[] infoStrings = new string[todoTask.Count];

            for (int i = 0; i < infoStrings.Length; i++)
            {
                infoStrings[i] = todoTask[i].ToString();
            }

            return infoStrings;
        }

        /// <summary>
        /// Writes the task list to a file with the specified file name.
        /// </summary>
        /// <param name="fileName">The name of the file to write the task list to.</param>
        /// <returns>True if the task list was successfully written to the file, false otherwise.</returns>
        public bool WriteFile(string fileName)
        {
            FileManager fileManager = new FileManager();
            return fileManager.SaveFile(todoTask, fileName);
        }

        /// <summary>
        /// Sets the task list to the provided list of tasks.
        /// </summary>
        /// <param name="tasks">The list of tasks to set as the task list.</param>
        public void SetTasks(List<Task> tasks)
        {
            todoTask = tasks;
        }

        /// <summary>
        /// Reads tasks from a file with the specified file name.
        /// </summary>
        /// <param name="fileName">The name of the file to read tasks from.</param>
        /// <returns>A list of tasks read from the file, or null if an error occurred.</returns>
        public List<Task> ReadFile(string fileName)
        {
            List<Task> todoTask = new List<Task>();
            FileManager fileManager = new FileManager();

            bool ok = fileManager.ReadFile(todoTask, fileName);

            if (!ok)
            {
                string errMessage = "Something went wrong";
                MessageBox.Show(errMessage);
                return null;
            }

            return todoTask;
        }
    }
}
