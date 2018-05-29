using KanbanBoard.DataObjects;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace KanbanBoard
{
    /// <summary>
    /// Interaction logic for KanbanBoardWindow.xaml
    /// </summary>
    public partial class KanbanBoardWindow
    {
        public int boardID;

        public KanbanBoardWindow(int boardID)
        {
            InitializeComponent();
            this.boardID = boardID;

            // set bindings
            setDataBindings();

        }

        public void loadBoard(int boardID)
        {
            InitializeComponent();
            this.boardID = boardID;

            // set bindings
            setDataBindings();
        }

        // Method to return back to Menu Page
        private void backToMainPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Go back to Main Menu Page and close Create Board Window
            MainWindow mainWindow =
                     new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        // Method for editing task
        private void editTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // get taskID
            int taskID = Convert.ToInt16(((Button)sender).Tag);
         
            TaskWindow taskWindow = new TaskWindow(boardID, taskID, this);
            taskWindow.Owner = Window.GetWindow(this);

            // Show Kanban Board Window
            taskWindow.Top = 100;
            taskWindow.Left = 400;
            taskWindow.Show();

        }

        // Method to reload board information
        private void reloadBoardButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBoardWindow loadBoardWindow = new LoadBoardWindow(boardID, this);
            
            // Show Kanban Board Window
            loadBoardWindow.Top = 100;
            loadBoardWindow.Left = 400;
            loadBoardWindow.Show();
        }

        // Method to add new task to the board
        private void createTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Go back to Main Menu Page and close Create Board Window
            TaskWindow taskWindow =
                     new TaskWindow(boardID, null, this);
            // Show Kanban Board Window
            taskWindow.Top = 100;
            taskWindow.Left = 400;
            taskWindow.Show();
        }

        // Set bindings on objects 
        private void setDataBindings()
        {
            // Bing grid and list views to board data object 
            boardGridView.DataContext = BoardsDO.GetBoard(this.boardID);

            // Bind columns to DO 
            firstColumn.DataContext = ColumnsDO.GetBoardColumns(this.boardID)[0];
            secondColumn.DataContext = ColumnsDO.GetBoardColumns(this.boardID)[1];
            thirdColumn.DataContext = ColumnsDO.GetBoardColumns(this.boardID)[2];

            // Bind list views to taskDO
            firstColumnView.ItemsSource = TaskDO.GetListOfTasks(ColumnsDO.GetColumnID(this.boardID, 1));
            secondColumnView.ItemsSource = TaskDO.GetListOfTasks(ColumnsDO.GetColumnID(this.boardID, 2));
            thirdColumnView.ItemsSource = TaskDO.GetListOfTasks(ColumnsDO.GetColumnID(this.boardID, 3));
        }
    }
}
