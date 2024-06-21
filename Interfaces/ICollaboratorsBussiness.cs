using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BussinessLayer.Interfaces
{
    public interface ICollaboratorsBussiness
    {
        public object AddCollaborator(CollaboratorsModel model, int UserId);
        public object GetAllCollaborators();
        public object GetAllCollaboratorsOfUser(int UserId);
        public object DeleteCollaboratorById(long CollaboratorId, int UserId);
    }
}
