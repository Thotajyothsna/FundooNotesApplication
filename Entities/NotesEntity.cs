using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Remainder { get; set; }
        public string? Color { get; set; }
        public string? Image { get; set; }
        public bool Archive { get; set; }
        public bool PinNotes { get; set; }
        public bool Trash { get; set; }
        public DateTime Created {  get; set; }
        public DateTime Modified { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity Users { get; set; }
    }
}
