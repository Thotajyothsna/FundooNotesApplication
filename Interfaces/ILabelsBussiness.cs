using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entities;

namespace BussinessLayer.Interfaces
{
    public interface ILabelsBussiness
    {
        public LabelsEntity AddLabel(string LabelName, int UserId);
        public LabelsEntity AddlabelToNotes(LabelsModel labelsModel, int UserId);
        public string RenameLabel(RenameLabelModel rename, int UserId);
        public List<LabelsEntity> GetAllLabels();
        public object GetAllLabelsOfUser(int UserId);
        public object DeleteLabelbyId(long LabelId, int UserId);
    }
}
