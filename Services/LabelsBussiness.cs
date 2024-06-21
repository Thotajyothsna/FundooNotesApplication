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
    public class LabelsBussiness : ILabelsBussiness
    {
        private readonly ILabelsRepo iLabelsRepo;
        public LabelsBussiness(ILabelsRepo iLabelsRepo)
        {
            this.iLabelsRepo = iLabelsRepo;
        }
        public LabelsEntity AddLabel(string LabelName, int UserId)
        {
            return iLabelsRepo.AddLabel(LabelName, UserId);
        }
        public LabelsEntity AddlabelToNotes(LabelsModel labelsModel, int UserId)
        {
            return iLabelsRepo.AddlabelToNotes(labelsModel, UserId);
        }
        public string RenameLabel(RenameLabelModel rename, int UserId)
        {
            return iLabelsRepo.RenameLabel(rename, UserId);
        }
        public List<LabelsEntity> GetAllLabels()
        {
            return iLabelsRepo.GetAllLabels();
        }
        public object GetAllLabelsOfUser(int UserId)
        {
            return iLabelsRepo.GetAllLabelsOfUser(UserId);
        }
        public object DeleteLabelbyId(long LabelId, int UserId)
        {
            return iLabelsRepo.DeleteLabelbyId(LabelId, UserId); 
        }


    }
}
