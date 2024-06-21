using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorsRepo
    {
        public object AddCollaborator(CollaboratorsModel model, int UserId);
        public object GetAllCollaborators();
        public object GetAllCollaboratorsOfUser(int UserId);
        public object DeleteCollaboratorById(long CollaboratorId, int UserId);
    }
}
