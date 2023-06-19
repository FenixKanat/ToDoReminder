using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder
{
    /// <summary>
    /// Represents a task in the ToDoReminder application.
    /// </summary>
    public class Task
    {
        private DateTime date;
        private string description;
        private PriorityType priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class with default values.
        /// </summary>
        public Task()
        {
            priority = PriorityType.Normal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class with the specified task date.
        /// </summary>
        /// <param name="taskDate">The date of the task.</param>
        public Task(DateTime taskDate) : this(taskDate, string.Empty, PriorityType.Normal) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class with the specified task date, description, and priority.
        /// </summary>
        /// <param name="taskDate">The date of the task.</param>
        /// <param name="description">The description of the task.</param>
        /// <param name="prio">The priority of the task.</param>
        public Task(DateTime taskDate, string description, PriorityType prio)
        {
            this.date = taskDate;
            this.description = description;
            this.priority = prio;
        }

        /// <summary>
        /// Gets or sets the date and time of the task.
        /// </summary>
        public DateTime DateAndTime
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Gets or sets the date of the task.
        /// </summary>
        public DateTime TaskDate
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Gets or sets the priority of the task.
        /// </summary>
        public PriorityType Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    description = value;
            }
        }

        /// <summary>
        /// Gets the string representation of the task's time.
        /// </summary>
        /// <returns>The formatted time string.</returns>
        private string GetTimeString()
        {
            string time = string.Format("{0}:{1}", date.Hour.ToString("00"),
                date.Minute.ToString("00"));

            return time;
        }

        /// <summary>
        /// Gets the string representation of the task's priority.
        /// </summary>
        /// <returns>The priority string.</returns>
        public string GetPriorityString()
        {
            string txtOut = Enum.GetName(typeof(PriorityType), priority);
            txtOut = txtOut.Replace("_", " ");
            return txtOut;
        }

        /// <summary>
        /// Returns a string that represents the current task.
        /// </summary>
        /// <returns>A string representation of the task.</returns>
        public override string ToString()
        {
            string textOut = $"{date.ToLongDateString(),-25} {GetTimeString(),12} {" ",6}" +
                        $"{GetPriorityString(),-16} {description,-20}";

            return textOut;
        }
    }
}
