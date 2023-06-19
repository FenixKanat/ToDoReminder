using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ToDoReminder
{
    /// <summary>
    /// Handles file operations for saving and reading tasks.
    /// </summary>
    class FileManager
    {
        private const string fileToken = "ToDoRe_21";
        private const double fileVersionNr = 1.0;

        /// <summary>
        /// Saves the list of tasks to a file.
        /// </summary>
        /// <param name="todoTask">The list of tasks to save.</param>
        /// <param name="fileName">The name of the file to save to.</param>
        /// <returns>True if the file was saved successfully, false otherwise.</returns>
        public bool SaveFile(List<Task> todoTask, string fileName)
        {
            bool ok = true;
            StreamWriter writeTask = null;

            try
            {
                writeTask = new StreamWriter(fileName);
                writeTask.WriteLine(fileToken);
                writeTask.WriteLine(fileVersionNr);
                writeTask.WriteLine(todoTask.Count);

                for (int i = 0; i < todoTask.Count; i++)
                {
                    writeTask.WriteLine(todoTask[i].Description);
                    writeTask.WriteLine(todoTask[i].Priority.ToString());
                    writeTask.WriteLine(todoTask[i].TaskDate.Year);
                    writeTask.WriteLine(todoTask[i].TaskDate.Month);
                    writeTask.WriteLine(todoTask[i].TaskDate.Day);
                    writeTask.WriteLine(todoTask[i].TaskDate.Hour);
                    writeTask.WriteLine(todoTask[i].TaskDate.Minute);
                    writeTask.WriteLine(todoTask[i].TaskDate.Second);
                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                if (writeTask != null)
                {
                    writeTask.Close();
                }
            }

            return ok;
        }

        /// <summary>
        /// Reads tasks from a file and populates the provided list.
        /// </summary>
        /// <param name="todoTask">The list to populate with the read tasks.</param>
        /// <param name="fileName">The name of the file to read from.</param>
        /// <returns>True if the file was read successfully and the tasks were populated, false otherwise.</returns>
        public bool ReadFile(List<Task> todoTask, string fileName)
        {
            bool ok = true;
            StreamReader readTask = null;

            try
            {
                if (todoTask != null)
                {
                    todoTask.Clear();
                }
                else
                {
                    todoTask = new List<Task>();
                }

                readTask = new StreamReader(fileName);

                string versionTest = readTask.ReadLine();

                double version = double.Parse(readTask.ReadLine());

                if ((versionTest == fileToken) && (version == fileVersionNr))
                {
                    int count = int.Parse(readTask.ReadLine());

                    for (int i = 0; i < count; i++)
                    {
                        Task task = new Task();
                        task.Description = readTask.ReadLine();
                        task.Priority = (PriorityType)Enum.Parse(typeof(PriorityType), readTask.ReadLine());

                        int year = 0, month = 0, day = 0;
                        int hour = 0, minute = 0, second = 0;

                        year = int.Parse(readTask.ReadLine());
                        month = int.Parse(readTask.ReadLine());
                        day = int.Parse(readTask.ReadLine());
                        hour = int.Parse(readTask.ReadLine());
                        minute = int.Parse(readTask.ReadLine());
                        second = int.Parse(readTask.ReadLine());

                        task.TaskDate = new DateTime(year, month, day, hour, minute, second);
                        todoTask.Add(task);
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                if (readTask != null)
                {
                    readTask.Close();
                }
            }

            return ok;
        }
    }
}
