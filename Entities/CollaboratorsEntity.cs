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
    public class CollaboratorsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorsId { get; set; }
        public string CollaboratorEmail { get; set; }
        [ForeignKey("Notes")]
        public long NotesId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual NotesEntity Notes { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
