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
    public class LabelsRepo : ILabelsRepo
    {
        private readonly FundooNotesContext fundooNotesContext;
        private readonly IConfiguration configuration;
        public LabelsRepo(FundooNotesContext fundooNotesContext, IConfiguration configuration)
        {
            this.fundooNotesContext = fundooNotesContext;
            this.configuration = configuration;
        }
        public LabelsEntity AddLabel(string LabelName,int UserId)
        {
            var UserExists=fundooNotesContext.Users.FirstOrDefault(u => u.UserId == UserId);
            if (UserExists != null)
            {
                LabelsEntity labelsEntity = new LabelsEntity();
                labelsEntity.UserId = UserId;
                labelsEntity.LabelName = LabelName;

                fundooNotesContext.Labels.Add(labelsEntity);
                fundooNotesContext.SaveChanges();

                return labelsEntity;

            }
            else
                return null;
        }
        public LabelsEntity AddlabelToNotes(LabelsModel labelsModel,int UserId)
        {
            var UserExists = fundooNotesContext.Notes.FirstOrDefault(u => u.UserId == UserId && u.NotesId==labelsModel.NotesId);
            if (UserExists != null)
            {
                var AlreadySameLabel = fundooNotesContext.Labels.FirstOrDefault(x => x.NotesId == labelsModel.NotesId && x.LabelName == labelsModel.LabelName);
                if (AlreadySameLabel == null)
                {
                    LabelsEntity labelsEntity = new LabelsEntity();
                    labelsEntity.UserId = UserId;
                    labelsEntity.LabelName = labelsModel.LabelName;
                    labelsEntity.NotesId = labelsModel.NotesId;

                    fundooNotesContext.Labels.Add(labelsEntity);
                    fundooNotesContext.SaveChanges();

                    return labelsEntity;
                }
                else
                    return AlreadySameLabel;
            }
            else
                return null;
        }
        public string RenameLabel(RenameLabelModel rename,int UserId)
        {
            var OldNameExists = fundooNotesContext.Labels.Where(x => x.UserId == UserId && x.LabelName == rename.OldName).ToList();
            if (OldNameExists != null)
            { 
                OldNameExists.ForEach(x => x.LabelName = rename.NewName);
                //var RenamedLabel=OldNameExists.Select(x => x.LabelName=NewName).ToList();
                fundooNotesContext.Labels.UpdateRange(OldNameExists);
                fundooNotesContext.SaveChanges();
                return rename.NewName;
            }
            else
                return null;
        }
        public List<LabelsEntity> GetAllLabels()
        {
            var AllLabels = fundooNotesContext.Labels.ToList();
            if (AllLabels != null)
            {
                return AllLabels;
            }
            else
                return null;
        }
        public object GetAllLabelsOfUser(int UserId)
        {
            var AllLabels=fundooNotesContext.Labels.Where(x=>x.UserId==UserId );
            if (AllLabels != null)
            {
                return AllLabels;
            }
            else
                return null;
        }
        public object DeleteLabelbyId(long LabelId,int UserId)
        {
            var DeleteLabel = fundooNotesContext.Labels.Where(x => x.UserId == UserId && x.LabelId==LabelId);
            if(DeleteLabel!=null)
            {
                fundooNotesContext.Labels.RemoveRange(DeleteLabel);
                fundooNotesContext.SaveChanges();
                return DeleteLabel;
            }
            return null;
        }
    }
}
