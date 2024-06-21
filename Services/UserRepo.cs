using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooNotesContext fundooNotesContext;
        private readonly IConfiguration configuration;
        public UserRepo(FundooNotesContext fundooNotesContext, IConfiguration configuration)
        {
            this.fundooNotesContext = fundooNotesContext;
            this.configuration = configuration;
        }
        public UserEntity RegisterAccount(UserRegModel model)
        {
           // CheckUserNames(model);
            UserEntity userentity = new UserEntity();
            userentity.FirstName = model.FirstName;
            userentity.LastName = model.LastName;
            userentity.Email = model.Email;
            userentity.Password = model.Password;
            fundooNotesContext.Users.Add(userentity);
            fundooNotesContext.SaveChanges();

            return userentity;
        }

        public bool CheckUserNames(string Email)
        {
            var usernameExists = fundooNotesContext.Users.FirstOrDefault(x=> x.Email ==  Email);
            if (usernameExists!=null)
            {
                return false;
            }
            else
                return true;
        }
        public object Login(UserLoginModel logModel)
        {
            var userfound = fundooNotesContext.Users.FirstOrDefault(x => x.Email == logModel.Email);
            if (userfound!=null)
            {
                //var passwordCheck = fundooNotesContext.Users.FirstOrDefault(x => x.Email == logModel.UserName);
                if (userfound.Password==logModel.Password)
                {
                    var token = GenerateToken(userfound.UserId, userfound.Email);
                    return token;
                }
                else
                    return null;
                                  
            }
            else
                return null;
        }
        public  List<UserEntity> GetDetails()
        {
            var getDetails = fundooNotesContext.Users.ToList();
            if (getDetails == null)
            {
                return null; ;
            }
            else
                return  fundooNotesContext.Users.ToList();
        }
        private string GenerateToken(long UserId, string Email)
        {
            // Create a symmetric security key using the JWT key specified in the configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Define claims to be included in the JWT
            var claims = new[]
            {
                  new Claim("Email",Email),
                  new Claim("UserId", UserId.ToString())
            };
            // Create a JWT with specified issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public List<UserEntity> GetDetailsByID(int ID)
        {
            var GetByID = fundooNotesContext.Users.Where(x => x.UserId == ID).ToList();
            if (GetByID == null)
                return null;
            else 
                return GetByID;
        }
        public List<UserEntity> DeleteByID(int ID)
        {
            var deleteRecords = fundooNotesContext.Users.Where(x => x.UserId == ID).ToList();
            if (deleteRecords == null)
                return null;
            else
            {
                fundooNotesContext.Users.RemoveRange(deleteRecords);
                fundooNotesContext.SaveChanges();
                return deleteRecords;
            }
        }
        public ForgotPasswordModel ForgotPassword(string Email)
        {
            UserEntity User = fundooNotesContext.Users.ToList().Find(user => user.Email == Email);
            if (User == null)
                return null;
            else
            {
                ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
                forgotPassword.Email = User.Email;
                forgotPassword.UserId = User.UserId;
                forgotPassword.Token = GenerateToken(User.UserId, User.Email);
                return forgotPassword;
            }
        }
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            UserEntity User = fundooNotesContext.Users.FirstOrDefault(user => user.Email == Email);
            if (User!=null)
            {
                User.Password = resetPasswordModel.ConfirmPassword;
                //User.ChangesAt=DateTime.Now;
                fundooNotesContext.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public int NoOfUsers()
        {
            var count=fundooNotesContext.Users.Count();
            return count;
        }
    }
}
