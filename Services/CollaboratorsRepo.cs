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
    public class CollaboratorsRepo : ICollaboratorsRepo
    {
        private readonly FundooNotesContext fundooNotesContext;
        private readonly IConfiguration configuration;
        public CollaboratorsRepo(FundooNotesContext fundooNotesContext, IConfiguration configuration)
        {
            this.fundooNotesContext = fundooNotesContext;
            this.configuration = configuration;
        }
        public object AddCollaborator(CollaboratorsModel model,int UserId)
        {
            var UserExists=fundooNotesContext.Users.FirstOrDefault(u => u.UserId == UserId);
            if (UserExists != null)
            {
                var NotesExists = fundooNotesContext.Notes.FirstOrDefault(u => u.UserId == UserId && u.NotesId == model.NotesId);
                var SameCollaborator = fundooNotesContext.Collaborators.FirstOrDefault(u => u.CollaboratorEmail == model.CollaboratorEmail && u.NotesId == model.NotesId);
                
                if (NotesExists != null && SameCollaborator == null)
                {
                    CollaboratorsEntity collaborator = new CollaboratorsEntity();
                    collaborator.UserId = UserId;
                    collaborator.NotesId = model.NotesId;
                    collaborator.CollaboratorEmail = model.CollaboratorEmail;

                    fundooNotesContext.Collaborators.Add(collaborator);
                    fundooNotesContext.SaveChanges();

                    return collaborator;
                }
                else if(SameCollaborator != null)
                {
                    return "SameCollaboratorExists";
                }
                else
                    return "NotesId_NotExists";
            }
            else
                return "User_NotExists";
        }
        public object GetAllCollaborators()
        {
            var AllCollaborators = fundooNotesContext.Collaborators.ToList();
            if (AllCollaborators != null)
                return AllCollaborators;
            else
                return null;
        }
        public object GetAllCollaboratorsOfUser(int UserId)
        {
            var UserExists=fundooNotesContext.Users.FirstOrDefault(u => u.UserId == UserId);
            if (UserExists != null)
            {
                var AllCollaborators = fundooNotesContext.Collaborators.Where(u => u.UserId == UserId);
                if (AllCollaborators != null)
                    return AllCollaborators;
                else
                    return "NoCollaborator";
            }
            else
                return "UserNotExists";

        }
        public object DeleteCollaboratorById(long CollaboratorId,int UserId)
        {
            var UserExists= fundooNotesContext.Users.FirstOrDefault(u => u.UserId == UserId);
            if (UserExists != null)
            {
                var CollaboratorIdExists = fundooNotesContext.Collaborators.FirstOrDefault(u => u.UserId == UserId && u.CollaboratorsId == CollaboratorId);
                if (CollaboratorIdExists != null)
                {
                    fundooNotesContext.Collaborators.Remove(CollaboratorIdExists);
                    fundooNotesContext.SaveChanges();
                    return "Deleted";
                }
                else
                    return "IdNotExists";
            }
            else
                return "UserNotExists";
        }
    }
}
