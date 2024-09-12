using System.ComponentModel.DataAnnotations;

namespace WAMS.Components.Model
{
    public class Room
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Floor { get; set; }

    }
}
