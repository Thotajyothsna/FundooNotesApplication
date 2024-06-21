using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooNotesContext fundooNotesContext;
        private readonly IConfiguration configuration;
        public NotesRepo(FundooNotesContext fundooNotesContext, IConfiguration configuration)
        {
            this.fundooNotesContext = fundooNotesContext;
            this.configuration = configuration;
        }

        public string CreateNotes(NotesModel notesmodel, int UserId)
        {
            try
            {
                UserEntity userEntity=fundooNotesContext.Users.FirstOrDefault(u => u.UserId == UserId);
                if (userEntity != null)
                {
                    NotesEntity notesEntity = new NotesEntity();
                    notesEntity.Title = notesmodel.Title;
                    notesEntity.Description = notesmodel.Description;
                    notesEntity.Remainder = notesmodel.Remainder;
                    notesEntity.Color = notesmodel.Color;
                    notesEntity.Image = notesmodel.Image;
                    notesEntity.Archive = notesmodel.Archive;
                    notesEntity.PinNotes = notesmodel.PinNotes;
                    notesEntity.Trash = notesmodel.Trash;
                    notesEntity.Created = DateTime.Now;
                    notesEntity.Modified = DateTime.Now;
                    notesEntity.UserId = UserId;

                    fundooNotesContext.Notes.Add(notesEntity);
                    fundooNotesContext.SaveChanges();
                    return "Notes Created Successfully";

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotesEntity> GetAllNotes()
        {
            var Notes = fundooNotesContext.Notes.ToList();
            if (Notes != null)
                return Notes;
            else
                return null;
        }
        public List<NotesEntity>? GetByUserId(int UserId)
        {
            var Notes = fundooNotesContext.Notes.Where(x => x.UserId == UserId).ToList();
            if (Notes != null)
                return Notes;
            else
                return null;
        }
        public NotesEntity UpdateNotes(int UserId,long NotesId,UpdateNotesModel updateNotesModel)
        {
            NotesEntity Notes=fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId==NotesId);
            if (Notes != null)
            { 
                
                    Notes.Title = updateNotesModel.Title;
                
                    Notes.Description = updateNotesModel.Description;
               
                    Notes.Remainder = updateNotesModel.Remainder;
                
                    Notes.Color = updateNotesModel.Color;
                
                    Notes.Image = updateNotesModel.Image;
                
                    Notes.Archive = updateNotesModel.Archive;
                
                    Notes.PinNotes = updateNotesModel.PinNotes;
                
                    Notes.Trash = updateNotesModel.Trash;
                    Notes.Modified = DateTime.Now;

                    fundooNotesContext.SaveChanges();
                    return Notes;
            }
            else
                return null;
        }
        public NotesEntity DeleteNote(int UserId,long NotesId)
        {
            var deleteNote=fundooNotesContext.Notes.FirstOrDefault(x=>x.UserId==UserId && x.NotesId == NotesId);
            if (deleteNote != null)
            {
                fundooNotesContext.Notes.Remove(deleteNote);
                fundooNotesContext.SaveChanges();
                return deleteNote;
            }
            else
                return null;
        }
        public string PinNotes(int UserId,long NotesId)
        {
            var NotesExists = fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId == NotesId);
            if (NotesExists != null)
            {
                if (NotesExists.PinNotes == false)
                {
                    NotesExists.PinNotes = true;
                    fundooNotesContext.SaveChanges();
                    return "Note Pinned";
                }
                else if (NotesExists.PinNotes == true)
                {
                    NotesExists.PinNotes = false;
                    fundooNotesContext.SaveChanges();
                    return "Note UnPinned";
                }
                else
                    return "Unable to change due to some issues";
            }
            else
                return null;
        }
        public string ArchiveNotes(int UserId, long NotesId)
        {
            var NotesExists = fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId == NotesId);
            if (NotesExists != null)
            {
                if (NotesExists.Archive == false)
                {
                    NotesExists.Archive = true;
                    fundooNotesContext.SaveChanges();
                    return "Note Archived";
                }
                else if (NotesExists.PinNotes == true)
                {
                    NotesExists.Archive = false;
                    fundooNotesContext.SaveChanges();
                    return "Note UnArchived";
                }
                else
                    return "Unable to change due to some issues";
            }
            else
                return null;
        }
        public string TrashNotes(int UserId, long NotesId)
        {
            var NotesExists = fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId == NotesId);
            if (NotesExists != null)
            {
                if (NotesExists.Trash == false)
                {
                    NotesExists.Trash = true;
                    fundooNotesContext.SaveChanges();
                    return "Note Moved To Trash";
                }
                else if (NotesExists.Trash == true)
                {
                    NotesExists.Trash = false;
                    fundooNotesContext.SaveChanges();
                    return "Note Moved from Trash Into Notes";
                }
                else
                    return "Unable to change due to some issues";
            }
            else
                return null;
        }
        public string ColorChange(int UserId, long NotesId,string Color) {
            var NotesExists = fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId == NotesId);
            if (NotesExists != null)
            {
                string[] ColorsList = new string[] { "WHITE","BLUE","RED","ORANGE","BROWN","PINK","VIOLET"};
                var ColorExists = ColorsList.Any(x => x == Color);
                if (ColorExists)
                {
                    NotesExists.Color = Color;
                    fundooNotesContext.SaveChanges();
                    return Color;
                }
                else
                    return "INVALID";
            }
            else
                return null;
        }
        public string ImageChange(int UserId, long NotesId, string Image)
        {
            var NotesExists = fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId == NotesId);
            if (NotesExists != null)
            {
                NotesExists.Image = Image;
                fundooNotesContext.SaveChanges();
                return Image;

            }
            else
                return null;
        }
        public string RemainderChange(int UserId, long NotesId,DateTime Remainder)
        {
            var NotesExists = fundooNotesContext.Notes.FirstOrDefault(x => x.UserId == UserId && x.NotesId == NotesId);
            if (NotesExists != null)
            {
                NotesExists.Remainder = Remainder;
                fundooNotesContext.SaveChanges();
                return "REMAINDER_SET";

            }
            else
                return null;
        }
    }
}
