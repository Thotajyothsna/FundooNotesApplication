using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace ModelLayer.Model
{
    public class UpdateNotesModel
    {
        
        public string? Title { get; set; }

       
        public string? Description { get; set; }

        
        public DateTime Remainder { get; set; }

        
        public string? Color { get; set; }

       
        public string? Image { get; set; }

        
        public bool Archive { get; set; }

       
        public bool PinNotes { get; set; }
        public bool Trash { get; set; }
    }
}
