using KanbanBoard.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KanbanBoard.PageTools
{   
    public partial class CreateBoardTools
    {
        // Method verifies if Input String contains only allowed characters
        public static bool IsValidInput(string inputText)
        {
            int i = 0;
            var regex = new Regex("[A-Za-z0-9]");

            // Check all char in string match the rule
            foreach (char c in inputText)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    i++;
                }
            }

            // set bool type
            if (i > 0)
            {
                return false;
            }
            return true;
        }

        // Method verifies if Column Name String contains only allowed characters
        public static bool IsValidColumnName(string inputText)
        {
            int i = 0;
            var regex = new Regex("[A-Za-z0-9 ]");

            // Check all char in string match the rule
            foreach (char c in inputText)
            {
                if (!regex.IsMatch(c.ToString()))
                {
                    i++;
                }
            }

            // Evaluate bool type
            if (i > 0)
            {
                return false;
            }
            return true;
        }

        // Method verifies if table exists in DB
        public static bool TableExists(string boardName)
        {
            if (BoardsDO.GetBoardsCount(boardName) == 0)
            {
                // if table doesn t exists - return false
                return false;
            }
            // if table already exists return true
            return true;
        }
        
    }
}
