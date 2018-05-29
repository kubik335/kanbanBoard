using KanbanBoard.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.DataObjects
{
    class TaskDO
    {
        // declare all variables
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public int TaskId { get; set; }
        public int ColumnId { get; set; }
        public int ColumnPos { get; set; }

        // Method for creating task in DB
        public static void CreateTask(int columnID, string taskName, string taskDesc)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // Create new entity
                kanban_task newTask = new kanban_task();
                newTask.ID_COLUMN = columnID;
                newTask.TASK_NAME = taskName;
                newTask.TASK_DESCRIPTION = taskDesc;

                // Add entity into the table and save changes
                context.kanban_task.Add(newTask);
                context.SaveChanges();
            }
        }       
        
        // Method for creating task in DB
        public static void UpdateTask(int taskID, int columnID, string taskName, string taskDesc)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // Create new entity
                kanban_task task = context.kanban_task.First( x=> x.ID_TASK == taskID);
                task.ID_COLUMN = columnID;
                task.TASK_NAME = taskName;
                task.TASK_DESCRIPTION = taskDesc;

                // Add entity into the table and save changes
                context.SaveChanges();
            }
        }

        // Method for getting task
        public static TaskDO GetTask(int taskID)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // select rows from db
                return context.kanban_task
                    .Where(x => x.ID_TASK == taskID)
                    .Join
                    (
                    context.kanban_columns
                        .Select(y => new
                        {
                            ColumnID = y.ID,
                            ColumnPos = y.COLUMN_POS
                        }),
                    task => task.ID_COLUMN,
                    col => col.ColumnID,
                    (task, col) => new { Task = task, Column = col })
                    .Select(x => new TaskDO()
                    {
                        TaskName = x.Task.TASK_NAME,
                        TaskDesc = x.Task.TASK_DESCRIPTION,
                        TaskId = x.Task.ID_TASK,
                        ColumnId = x.Task.ID_COLUMN,
                        ColumnPos = x.Column.ColumnPos
                    }).First();
            }
        }

        // Method returns list of tasks for certain column
        public static List<TaskDO> GetListOfTasks(int columnID)
        {
            // Create new connection
            using (QKOLA01_VSE_DBEntities context =
                new QKOLA01_VSE_DBEntities())
            {
                // select rows from db
                return context.kanban_task
                    .Where(x => x.ID_COLUMN == columnID)
                    .Select(x => new TaskDO()
                    {
                        TaskName = x.TASK_NAME,
                        TaskDesc = x.TASK_DESCRIPTION,
                        TaskId = x.ID_TASK,
                        ColumnId = x.ID_COLUMN
                    }).ToList();
            }
        }
    }
}
