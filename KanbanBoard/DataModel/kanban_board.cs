//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KanbanBoard.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class kanban_board
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kanban_board()
        {
            this.kanban_columns = new HashSet<kanban_columns>();
        }
    
        public int ID { get; set; }
        public string BOARD_NAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<kanban_columns> kanban_columns { get; set; }
    }
}
