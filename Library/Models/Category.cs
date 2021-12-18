using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("NameTaken", TypeName = "Varchar(50)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string NameTaken  { get; set; }


        [Required]
        [Column("Type", TypeName = "varchar(30)")]
        public string Type { get; set; }


        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Book.Category))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
