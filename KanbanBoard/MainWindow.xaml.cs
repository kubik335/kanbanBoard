using System;
using System.Windows;

namespace KanbanBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Top = 100;
            this.Left = 400;
            InitializeComponent();
        }

        // Method for loading Create Kanban Board Window
        private void createNewBoardButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            CreateBoardWindow createBoardWindow =
           new CreateBoardWindow();
            // Closes Main WIndow
            this.Close();
            // Show Create Kanban Board Window
            createBoardWindow.Top = 100;
            createBoardWindow.Left = 400;
            createBoardWindow.Show();
        }

        // Method for loading Existing Kanban Board Window
        private void loadExistingKanbanBoard_Click(
            object sender,
            RoutedEventArgs e)
        {   
            // Create Board Window
            LoadBoardWindow loadBoardWindow =
                        new LoadBoardWindow(null, null);
            // Closes Create Board WIndow
            this.Close();
            // Show Load Kanban Board Window
            loadBoardWindow.Top = 100;
            loadBoardWindow.Left = 400;
            loadBoardWindow.Show();
        }

        // Method for loading Application Information WIndow
        private void applictionInfoButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            // Show Application Information
            AppInformationWindow applicationInformation =
                    new AppInformationWindow();
            applicationInformation.ShowDialog();
        }
    }
}
