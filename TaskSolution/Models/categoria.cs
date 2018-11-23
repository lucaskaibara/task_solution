namespace TaskSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("task_solution.categoria")]
    public partial class categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public categoria()
        {
            tarefa = new HashSet<tarefa>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string nome { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string cancelado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tarefa> tarefa { get; set; }
    }
}
