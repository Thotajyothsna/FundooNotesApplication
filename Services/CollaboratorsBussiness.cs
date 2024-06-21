using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;
using ModelLayer.Model;
using RepositoryLayer.Interfaces;

namespace BussinessLayer.Services
{
    public class CollaboratorsBussiness : ICollaboratorsBussiness
    {
        private readonly ICollaboratorsRepo icollaboratorsRepo;
        public CollaboratorsBussiness(ICollaboratorsRepo icollaboratorsRepo)
        {
            this.icollaboratorsRepo = icollaboratorsRepo;
        }
        public object AddCollaborator(CollaboratorsModel model, int UserId)
        {
            return icollaboratorsRepo.AddCollaborator(model, UserId);
        }
        public object GetAllCollaborators()
        {
            return icollaboratorsRepo.GetAllCollaborators();
        }
        public object GetAllCollaboratorsOfUser(int UserId)
        {
            return icollaboratorsRepo.GetAllCollaboratorsOfUser(UserId);
        }
        public object DeleteCollaboratorById(long CollaboratorId, int UserId)
        {
            return icollaboratorsRepo.DeleteCollaboratorById(CollaboratorId, UserId);
        }
    }
}
