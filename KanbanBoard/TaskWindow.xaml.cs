using KanbanBoard.DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KanbanBoard
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private int boardID;
        private int taskID;
        private KanbanBoardWindow board;

        public TaskWindow(int boardID, int? taskID, KanbanBoardWindow board)
        {
            InitializeComponent();
            this.boardID = boardID;
            this.board = board;
            // Hide warning labels
            hideWarningLabels();

            if ( taskID.HasValue)
            {
                // get column position
                this.taskID = taskID.Value;
                int columnPos = TaskDO.GetTask(this.taskID).ColumnPos - 1;

                // Set combobox values 
                comboBox.ItemsSource = ColumnsDO.GetBoardColumnNames(boardID);
                comboBox.SelectedIndex = columnPos;

                // Fill the fields
                fillFields(this.taskID);
            }
            else
            {
                this.taskID = 0;
                // set combobox values
                comboBox.ItemsSource = ColumnsDO.GetBoardColumnNames(boardID);
                comboBox.SelectedIndex = 0;
            }

        }

        // Method for saving task info 
        private void saveTaskInformationButton_Click(object sender, RoutedEventArgs e)
        {
            hideWarningLabels();
            if ( verifyBoardNameInput() == true )
            {
                // get  chosen column ID 
                int colPos = comboBox.SelectedIndex + 1;
                int colID = ColumnsDO.GetColumnID(boardID, colPos);

                if (this.taskID == 0)
                {
                    // If task is new - create one 
                    TaskDO.CreateTask(colID, taskNameInput.Text, taskDescriptionInput.Text);
                    this.Close();
                    this.board.LoadBoard(boardID);
                }
                else
                {
                    // if task was already created update info 
                    TaskDO.UpdateTask(this.taskID, colID, taskNameInput.Text, taskDescriptionInput.Text);
                    this.Close();
                    this.board.LoadBoard(boardID);
                }
            }
        }

        // Method for verifiying task information - returns true/false value
        private bool verifyBoardNameInput()
        {
            // Validate Input String 
            if (!String.IsNullOrWhiteSpace(taskNameInput.Text))
            {
                // Verify Task Name Input Length
                if (taskNameInput.Text.Length <= 30)
                {
                    // Verify Task Description Length
                    if (taskDescriptionInput.Text.Length >= 150)
                    {
                        longTaskDescWarningTextBlock.Visibility = Visibility.Visible;
                    }
                    else return true;
                }
                else longTaskNameWarningTextBlock.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                // Show Data Grid View with Warning message
                nullTaskNameWarningTextBlock.Visibility = Visibility.Visible;
                return false;
            }
        }

        // Method for hiding warning labels
        private void hideWarningLabels()
        {
            nullTaskNameWarningTextBlock.Visibility = Visibility.Hidden;
            longTaskNameWarningTextBlock.Visibility = Visibility.Hidden;
            longTaskDescWarningTextBlock.Visibility = Visibility.Hidden;
        }

        // Method for pre-filling fields
        private void fillFields(int taskID)
        {
            taskNameInput.Text = TaskDO.GetTask(taskID).TaskName;
            taskDescriptionInput.Text = TaskDO.GetTask(taskID).TaskDesc;
        }
    }
}
