using KanbanBoard.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.DataObjects
{
    class ColumnsDO
    {
        // declare all variables
        public string ColumnName { get; set; }
        public int ColumnPos { get; set; }
        public int ColumnId { get; set; }
        public string BoardId { get; set; }

        // Method for creating Columns in DB
        public static void CreateColumns(int boardID, List<string> columnList)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                int columnPos = 0;
                foreach (string column in columnList)
                {
                    columnPos++;
                    // Create new entity
                    kanban_columns newColumn = new kanban_columns();
                    newColumn.ID_BOARD = boardID;
                    newColumn.NAME = column;
                    newColumn.COLUMN_POS = columnPos;

                    // Add entity into the table and save changes
                    context.kanban_columns.Add(newColumn);
                    context.SaveChanges();
                }
            }
        }

        // Method returns list of columns
        public static List<ColumnsDO> GetBoardColumns(
            int boardID)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                return context.kanban_columns
                     .Where(x => x.ID_BOARD == boardID)
                     .OrderBy(x => x.COLUMN_POS)
                     .Select(x => new ColumnsDO()
                     {
                         ColumnName = x.NAME,
                         ColumnPos = x.COLUMN_POS,
                         ColumnId = x.ID
                     })
                     .ToList();
            }
        }

        // Method returns list of column names
        public static List<string> GetBoardColumnNames(
            int boardID)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                List<ColumnsDO> columns;
                // Select that returns OrderID, OrderDate & EmployeesLastName
               columns = context.kanban_columns
                    .Where(x => x.ID_BOARD == boardID)
                    .OrderBy(x => x.COLUMN_POS)
                    .Select(x => new ColumnsDO()
                    {
                        ColumnName = x.NAME,
                        ColumnPos = x.COLUMN_POS,
                        ColumnId = x.ID
                    })
                    .ToList();

                List<string> columnNames = new List<string>();
                foreach (ColumnsDO column in columns)
                {
                  columnNames.Add(column.ColumnName);
                }

                return columnNames;
            }
        }

        // Method returnt Column ID 
        public static int GetColumnID(int boardID, int colPos)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // select row from db
                kanban_columns column = context.kanban_columns.First(x => (x.ID_BOARD == boardID) && (x.COLUMN_POS == colPos));
                int columnID = column.ID;
                // return column ID
                return columnID;
            }
        }
    }
}
