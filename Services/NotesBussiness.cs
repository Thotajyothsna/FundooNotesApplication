using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;
using ModelLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace BussinessLayer.Services
{
    public class NotesBussiness : INotesBussiness
    {
        private readonly INotesRepo inotesRepo;
        public NotesBussiness(INotesRepo inotesRepo)
        {
            this.inotesRepo = inotesRepo;
        }
        public string CreateNotes(NotesModel notesmodel, int UserId)
        { 
            return inotesRepo.CreateNotes(notesmodel, UserId);
        }
        public List<NotesEntity> GetAllNotes()
        { 
            return inotesRepo.GetAllNotes();
        }
        public List<NotesEntity> GetByUserId(int UserId)
        { 
            return inotesRepo.GetByUserId(UserId);
        }
        public NotesEntity UpdateNotes(int UserId, long NotesId, UpdateNotesModel updateNotesModel)
        {
            return inotesRepo.UpdateNotes(UserId,NotesId,updateNotesModel);
        }
        public NotesEntity DeleteNote(int UserId, long NotesId)
        { 
            return inotesRepo.DeleteNote(UserId, NotesId);
        }
        public string PinNotes(int UserId, long NotesId)
        { 
            return inotesRepo.PinNotes(UserId, NotesId);
        }
        public string ArchiveNotes(int UserId, long NotesId)
        { 
            return inotesRepo.ArchiveNotes(UserId, NotesId);
        }
        public string TrashNotes(int UserId, long NotesId)
        { 
            return inotesRepo.TrashNotes(UserId, NotesId);
        }
        public string ColorChange(int UserId, long NotesId, string Color)
        { 
            return inotesRepo.ColorChange(UserId, NotesId, Color);
        }
        public string ImageChange(int UserId, long NotesId, string Image)
        { 
            return inotesRepo.ImageChange(UserId, NotesId, Image);
        }
        public string RemainderChange(int UserId, long NotesId, DateTime Remainder)
        {
            return inotesRepo.RemainderChange(UserId, NotesId, Remainder);
        }
    }
}
