using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.DataModels
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        public string Id { set; get; }

        [Required]
        public string Content { set; get; }
    }
}