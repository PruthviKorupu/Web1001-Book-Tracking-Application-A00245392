using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("CategoryType")]
    public partial class Category
    {
        public CategoryType()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("Type", TypeName = "Varchar(50)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Type  { get; set; }


        [Required]
        [Column("Name", TypeName = "varchar(30)")]
        public string Name { get; set; }


        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Category.CategoryType))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
 
