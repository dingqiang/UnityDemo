using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Student : EntityBase
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }
        [DefaultValue((int)Enum.Sex.未知)]
        public byte Sex { get; set; }
    }
}