namespace TaskSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("task_solution.usuario")]
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            tarefa = new HashSet<tarefa>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string nome_completo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime data_nascimento { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string login { get; set; }

        [Required]
        [StringLength(32)]
        public string senha { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string cancelado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tarefa> tarefa { get; set; }
    }
}
