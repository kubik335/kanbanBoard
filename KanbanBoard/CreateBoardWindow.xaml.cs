using KanbanBoard.DataModel;
using KanbanBoard.DataObjects;
using KanbanBoard.PageTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace KanbanBoard
{
    /// <summary>
    /// Interaction logic for CreateBoardWindow.xaml
    /// </summary>
    public partial class CreateBoardWindow : Window
    {
        private BoardsDO myBoard = new BoardsDO();

        public CreateBoardWindow()
        {
            InitializeComponent();
            // Show panel for creating board
            boardNamePanel.Visibility = Visibility.Visible;
            //columnsPanel.Visibility = Visibility.Visible;
            //fillDefaultColumns();

            // Hide warning Labels
            hideBoardWarningLabels();
        }

        private void createBoardButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide warning labels
            hideBoardWarningLabels();

            // If input is valid create new board and return create columns window
            if (verifyBoardNameInput() == true)
            {
                myBoard.BoardName = kanbanBoardTextInput.Text;
                BoardsDO.CreateBoard(kanbanBoardTextInput.Text);
                
                // Hide panel for creating board
                boardNamePanel.Visibility = Visibility.Hidden;

                // Show panel for creating columns
                columnsPanel.Visibility = Visibility.Visible;
                fillDefaultColumns();
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

        // Method creates columns in boards
        private void createColumnsButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide warning labels
            hideColumnsWarningLabels();

            // Verify input 

            if (verifyColumnsInput(getColumnNames()) == true)
            {
                ColumnsDO.CreateColumns(BoardsDO.GetBoardID(myBoard.BoardName), getColumnNames());
                // Create Board Window
                KanbanBoardWindow kanbanBoardWindow =
                            new KanbanBoardWindow(BoardsDO.GetBoardID(myBoard.BoardName));
                // Closes Create Board WIndow
                this.Close();
                // Show Kanban Board Window
                kanbanBoardWindow.Top = 100;
                kanbanBoardWindow.Left = 150;
                kanbanBoardWindow.Show();
            };
        }

        // Method for verifiying board name input - returns true/false value
        private bool verifyBoardNameInput()
        {
            // Validate Input String 
            if (!String.IsNullOrWhiteSpace(kanbanBoardTextInput.Text))
            {
                // Verify Input Length
                if (kanbanBoardTextInput.Text.Length <= 15)
                {
                    // Verify characters allowed
                    if (CreateBoardTools.IsValidInput(kanbanBoardTextInput.Text) == false)
                    {
                        containsDigitsWarningTextBlock.Visibility = Visibility.Visible;
                    }
                    else if (CreateBoardTools.TableExists(kanbanBoardTextInput.Text) == true)
                    {
                        boardExistsWarningTextBlock.Visibility = Visibility.Visible;
                        return false;
                    }
                    else return true;
                }
                else longNameWarningTextBlock.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                // Show Data Grid View with Warning message
                nullWarningTextBlock.Visibility = Visibility.Visible;
                return false;
            }
        }

        // Method for filling default column names
        private void fillDefaultColumns()
        {
            if (String.IsNullOrWhiteSpace(firstColumnNameInput.Text))
            {
                firstColumnNameInput.Text = "TO DO";
            }
            if (String.IsNullOrWhiteSpace(secondColumnNameInput.Text))
            {
                secondColumnNameInput.Text = "IN PROGRESS";
            }
            if (String.IsNullOrWhiteSpace(thirdColumnNameInput.Text))
            {
                thirdColumnNameInput.Text = "DONE";
            }
        }

        // Method returns list of column names
        private List<string> getColumnNames()
        {
            List<string> mylist = 
                new List<string>(new string[] 
                { firstColumnNameInput.Text,
                  secondColumnNameInput.Text,
                  thirdColumnNameInput.Text });
            return mylist;
        } 

        // Method for verifiying columns input - returns true/false value
        private bool verifyColumnsInput(List<string> list)
        {
            int i = 0;
            foreach (string item in list)
            {
                // Validate Input String 
                if (!String.IsNullOrWhiteSpace(item))
                {
                    // Verify Input Length
                    if (item.Length <= 15)
                    {
                        // Verify characters allowed
                        if (CreateBoardTools.IsValidColumnName(item) == false)
                        {
                            hideColumnsWarningLabels();
                            charToUseColumnsWarningTextBlock.Visibility = Visibility.Visible;
                            i++;
                        }
                    }
                    else
                    {
                        // Hide warnings 
                        hideColumnsWarningLabels();
                        longColumnNameWarningTextBlock.Visibility = Visibility.Visible;
                        i++;
                    }

                }
                else
                {
                    // Show Data Grid View with Warning message
                    hideColumnsWarningLabels();
                    nullColumnsWarningTextBlock.Visibility = Visibility.Visible;

                    // Fill columns with default values
                    fillDefaultColumns();
                    i++;
                }
            }

            // Evaluate method state
            if ( i > 0)
            {
                return false;
            }
            return true;
        }

        // Hide board warning labels
        private void hideBoardWarningLabels()
        {
            nullWarningTextBlock.Visibility = Visibility.Hidden;
            longNameWarningTextBlock.Visibility = Visibility.Hidden;
            containsDigitsWarningTextBlock.Visibility = Visibility.Hidden;
            boardExistsWarningTextBlock.Visibility = Visibility.Hidden;
        }

        // Hide column warning labels
        private void hideColumnsWarningLabels()
        {
            nullColumnsWarningTextBlock.Visibility = Visibility.Hidden;
            longColumnNameWarningTextBlock.Visibility = Visibility.Hidden;
            charToUseColumnsWarningTextBlock.Visibility = Visibility.Hidden;
        }
    }
}
