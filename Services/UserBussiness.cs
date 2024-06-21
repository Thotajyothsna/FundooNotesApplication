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
    public class UserBussiness : IUserBussiness
    {
        private readonly IUserRepo iuserrepo;
        public UserBussiness(IUserRepo iuserrepo)
        {
            this.iuserrepo = iuserrepo;
        }
        public UserEntity RegisterAccount(UserRegModel model)
        {
            return iuserrepo.RegisterAccount(model);
        }
        public bool CheckUserNames(string Email)
        { 
            return iuserrepo.CheckUserNames(Email);
        }
        public object Login(UserLoginModel logModel)
        { 
            return iuserrepo.Login(logModel);
        }
        public List<UserEntity> GetDetails()
        {
            return iuserrepo.GetDetails();
        }
        public List<UserEntity> GetDetailsByID(int ID)
        { 
            return iuserrepo.GetDetailsByID(ID);
        }
        public List<UserEntity> DeleteByID(int ID)
        { 
            return iuserrepo.DeleteByID(ID);
        }
        public ForgotPasswordModel ForgotPassword(string Email)
        { 
            return iuserrepo.ForgotPassword(Email);
        }
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        { 
            return iuserrepo.ResetPassword(Email, resetPasswordModel);
        }
        public int NoOfUsers()
        { 
            return iuserrepo.NoOfUsers();
        }
    }
}
