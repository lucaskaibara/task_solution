namespace TaskSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("task_solution.tarefa")]
    public partial class tarefa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tarefa()
        {
            tarefa1 = new HashSet<tarefa>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string nome { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string descricao { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string estado { get; set; }

        public double horas_estimadas { get; set; }

        public double horas_realizadas { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string cancelado { get; set; }

        public int usuario_id { get; set; }

        public int categoria_id { get; set; }

        public int? tarefa_id { get; set; }

        public virtual categoria categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tarefa> tarefa1 { get; set; }

        public virtual tarefa tarefa2 { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
