using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entities;

namespace BussinessLayer.Interfaces
{
    public interface IUserBussiness
    {
        public UserEntity RegisterAccount(UserRegModel model);
        public bool CheckUserNames(string Email);
        public object Login(UserLoginModel logModel);
        public List<UserEntity> GetDetails();
        public List<UserEntity> GetDetailsByID(int ID);
        public List<UserEntity> DeleteByID(int ID);
        public ForgotPasswordModel ForgotPassword(string Email);
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
        public int NoOfUsers();
    }
}
