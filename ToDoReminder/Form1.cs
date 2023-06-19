using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDoReminder
{
    public partial class Form1 : Form
    {
        private TaskManager taskManager;
        private TimeSpan elapsedTime;
        private string fileName = Application.StartupPath + "\\Tasks.txt";


        public Form1()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Initializes the GUI components and sets default values.
        /// </summary>
        private void InitializeGUI()
        {
            fileName = Path.Combine(Application.StartupPath, "Tasks.txt");

            this.Text = "Todo reminder by Fenix Kanat";

            taskManager = new TaskManager();

            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(Enum.GetNames(typeof(PriorityType)));
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;

            lstTasks.Items.Clear();
            lblClock.Text = string.Empty;
            clockTimer.Start();

            txtDescription.Text = string.Empty;

            DateTimePicker1.Format = DateTimePickerFormat.Custom;
            DateTimePicker1.CustomFormat = "yyyy-MM-dd  HH:mm";

            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(DateTimePicker1, "Click to open calendar");
            toolTip1.SetToolTip(cmbPriority, "Select Priority");

            string tips = "TO CHANGE: Select an item first! " + Environment.NewLine;
            tips += "Make changes in the input controls," + Environment.NewLine;
            tips += "Click the Change button." + Environment.NewLine;

            toolTip1.SetToolTip(lstTasks, tips);
            toolTip1.SetToolTip(btnChange, tips);

            string delTips = "Select an item to remove";
            toolTip1.SetToolTip(DeleteBtn, delTips);

            string desTips = "Write your tasks here";
            toolTip1.SetToolTip(txtDescription, desTips);

            MenuFileOpen.Enabled = true;
            MenuFileSave.Enabled = true;
            UpdateGUI();
        }

        /// <summary>
        /// Reads the input values from the GUI controls and creates a new Task object.
        /// </summary>
        /// <returns>The newly created Task object.</returns>
        private Task ReadInput()
        {
            Task task = new Task();

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Input needed");
                return null;
            }

            task.Description = txtDescription.Text;
            task.TaskDate = DateTimePicker1.Value;
            task.Priority = (PriorityType)cmbPriority.SelectedIndex;

            return task;
        }

        /// <summary>
        /// Updates the tasks in the list box based on the task manager.
        /// </summary>
        private void UpdateGUI()
        {
            lstTasks.Items.Clear();
            string[] infoStrings = taskManager.GetInfo();
            if (infoStrings != null)
            {
                lstTasks.Items.AddRange(infoStrings);
            }
        }

        /// <summary>
        /// Event handler for the "Add" button click event.
        /// Adds a new task to the task manager and updates the GUI.
        /// </summary>
        private void Add_Click(object sender, EventArgs e)
        {
            Task task = ReadInput();

            if (taskManager.AddNewTask(task))
            {
                UpdateGUI();
            }
        }

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            // Event handler for the DateTimePicker's "ValueChanged" event
        }

        private void Priority_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for the cmbPriority's "SelectedIndexChanged" event
        }

        private void Todo_TextChanged(object sender, EventArgs e)
        {
            // Event handler for the txtDescription's "TextChanged" event
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for the lstTasks' "SelectedIndexChanged" event
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Event handler for the Form's "Load" event
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Event handler for the "New" menu item's "Click" event
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            // Event handler for the toolStripTextBox's "Click" event
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Event handler for the "Open" menu item's "Click" event
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Event handler for the "Open" menu item's "Click" event
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            //Event handler for groupBox1
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Event handler for the toolStripMenuItem's "Click" event
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Event handler for the button1's "Click" event
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Event handler for the menuStrip1's "ItemClicked" event
        }

        /// <summary>
        /// Event handler for the "New" menu item's "Click" event.
        /// Initializes the GUI and updates the elapsed time.
        /// </summary>
        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            InitializeGUI();
            UpdateElapsedTime();
        }

        /// <summary>
        /// Updates the elapsed time and displays it in the label.
        /// </summary>
        private void UpdateElapsedTime()
        {
            elapsedTime = TimeSpan.Zero;
            lblClock.Text = $"Elapsed Time: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
        }

        /// <summary>
        /// Event handler for the "Open" menu item's "Click" event.
        /// Reads tasks from a file and updates the task manager and GUI.
        /// </summary>
        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            List<Task> todoTask = taskManager.ReadFile(fileName);

            if (todoTask != null)
            {
                taskManager.SetTasks(todoTask);
                UpdateGUI();
            }
        }

        /// <summary>
        /// Event handler for the "Save" menu item's "Click" event.
        /// Writes tasks to a file and updates the GUI.
        /// </summary>
        private void MenuFileSave_Click(object sender, EventArgs e)
        {
            string errMessage = "Something went wrong with saving the file.";

            bool ok = taskManager.WriteFile(fileName);
            if (!ok)
            {
                MessageBox.Show(errMessage);
            }
            else
            {
                MessageBox.Show("Saved" + Environment.NewLine + fileName);
                UpdateGUI();
            }
        }

        /// <summary>
        /// Event handler for the "Exit" menu item's "Click" event.
        /// Asks for confirmation before closing the application.
        /// </summary>
        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo);

            if (dlgResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void lblClock_Click(object sender, EventArgs e)
        {
            // Event handler for the lblClock's "Click" event
        }

        /// <summary>
        /// Event handler for the clockTimer's "Tick" event.
        /// Updates the elapsed time and displays it in the label.
        /// </summary>
        private void clockTimer_Tick_1(object sender, EventArgs e)
        {
            elapsedTime += TimeSpan.FromSeconds(1);
            lblClock.Text = $"Elapsed Time: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
        }

        /// <summary>
        /// Event handler for the "Delete" button's "Click" event.
        /// Deletes the selected task from the task manager and updates the GUI.
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index >= 0)
            {
                taskManager.DeleteTaskAt(index);
                UpdateGUI();
            }
        }

        /// <summary>
        /// Event handler for the "Change" button's "Click" event.
        /// Changes the selected task in the task manager based on the input values and updates the GUI.
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index >= 0)
            {
                Task task = ReadInput();
                bool ok = taskManager.ChangeTaskAt(task, index);
                if (ok)
                {
                    UpdateGUI();
                }
                else
                {
                    MessageBox.Show("Select an element to change");
                }
            }
        }
    }
}
