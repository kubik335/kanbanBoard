using KanbanBoard.DataObjects;
using KanbanBoard.PageTools;
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
    /// Interaction logic for LoadBoardWindow.xaml
    /// </summary>
    public partial class LoadBoardWindow : Window
    {
        private int boardID;
        private KanbanBoardWindow board;

        public LoadBoardWindow(int? boardID, KanbanBoardWindow board = null)
        {
            InitializeComponent();
            hideWarningLabels();
            
            // verify if another board is loaded in the background
            if (board != null && boardID.HasValue)
            {
                this.board = board;
                this.boardID = boardID.Value;
            }
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

        private void loadBoardButton_Click(object sender, RoutedEventArgs e)
        {

            hideWarningLabels();
            if (verifyBoardNameInput() == true)
            {
                // verify board is already loaded on the background
                if (board != null)
                {
                    // verify loaded boardID is same as searched one
                    if (BoardsDO.GetBoardID(kanbanBoardTextInput.Text) == boardID)
                    {
                        // if they are the same return loaded board
                        board.loadBoard(boardID);
                        this.Close();
                    }
                    else
                    {
                        // Create Board Window
                        KanbanBoardWindow newBoardWindow =
                                    new KanbanBoardWindow(BoardsDO.GetBoardID(kanbanBoardTextInput.Text));

                        board.Close();
                        this.Close();

                        // Show Kanban Board Window
                        newBoardWindow.Top = 100;
                        newBoardWindow.Left = 150;
                        newBoardWindow.Show();
                    }
                }

                // if board is not loaded on the background create new one
                else
                {
                    // Create Board Window
                    KanbanBoardWindow kanbanBoardWindow =
                                new KanbanBoardWindow(BoardsDO.GetBoardID(kanbanBoardTextInput.Text));
                    // Closes Create Board WIndow
                    this.Close();
                    // Show Kanban Board Window
                    kanbanBoardWindow.Top = 100;
                    kanbanBoardWindow.Left = 150;
                    kanbanBoardWindow.Show();
                }
            }

        }

        // Method for verifiying board name input - returns true/false value
        public bool verifyBoardNameInput()
        {
            // Validate Input String 
            if (!String.IsNullOrWhiteSpace(kanbanBoardTextInput.Text))
            {
                // Verify table exists
                if (CreateBoardTools.tableExists(kanbanBoardTextInput.Text) == false)
                    {
                        boardNotExistsWarningTextBlock.Visibility = Visibility.Visible;
                        return false;
                    }
                    else return true;
            }
            else
            {
                // Show Data Grid View with Warning message
                nullBoardWarningTextBlock.Visibility = Visibility.Visible;
                return false;
            }

        }

        // Hide warning labels
        private void hideWarningLabels()
        {
            nullBoardWarningTextBlock.Visibility = Visibility.Hidden;
            boardNotExistsWarningTextBlock.Visibility = Visibility.Hidden;
        }
    }
}
