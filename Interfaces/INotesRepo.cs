using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRepo
    {
        public string CreateNotes(NotesModel notesmodel, int UserId);
        public List<NotesEntity> GetAllNotes();
        public List<NotesEntity> GetByUserId(int UserId);
        public NotesEntity UpdateNotes(int UserId, long NotesId, UpdateNotesModel updateNotesModel);
        public NotesEntity DeleteNote(int UserId, long NotesId);
        public string PinNotes(int UserId, long NotesId);
        public string ArchiveNotes(int UserId, long NotesId);
        public string TrashNotes(int UserId, long NotesId);
        public string ColorChange(int UserId, long NotesId, string Color);
        public string ImageChange(int UserId, long NotesId, string Image);
        public string RemainderChange(int UserId, long NotesId, DateTime Remainder);


    }
}
