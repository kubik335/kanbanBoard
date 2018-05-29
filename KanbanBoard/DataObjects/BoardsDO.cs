using KanbanBoard.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.DataObjects
{
    class BoardsDO
    {
        // declare all variables
        public string BoardName { get; set; }
        public int BoardID { get; set; }

        // Method for getting count of boards with boardName in DB
        public static int GetBoardsCount(string boardName)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // Returns count of searched Board Names 
                return context.kanban_board
                    .Where( x => x.BOARD_NAME == boardName)
                    .Select(x => new BoardsDO()
                    {
                        BoardName = x.BOARD_NAME
                    }).Count();
            }
        }

        // Method for getting boards ID
        public static int GetBoardID(string boardName)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // select row from db
                kanban_board board = context.kanban_board.First(x => x.BOARD_NAME == boardName);
                int boardID = board.ID;
                // return board ID
                return boardID;
            }
        }

        // Method for getting boards ID
        public static List<BoardsDO> GetBoard(int boardID)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // select row from db
                return context.kanban_board
                    .Where(x => x.ID == boardID)
                    .Select( x => new BoardsDO()
                {
                    BoardName = x.BOARD_NAME,
                    BoardID = x.ID
                }).ToList();
            }
        }

        // Method for creating new Board in DB
        public static void CreateBoard(string boardName)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // Create new entity
                kanban_board newBoard = new kanban_board();
                newBoard.BOARD_NAME = boardName;

                // Add entity into the table and save changes
                context.kanban_board.Add(newBoard);
                context.SaveChanges();
            }
        }
    }
}
