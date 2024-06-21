using BussinessLayer.Interfaces;
using BussinessLayer.Services;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using WireMock.Admin.Mappings;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBussiness iuserbussiness;
        private readonly IBus bus;
        public UserController(IUserBussiness iuserbussiness, IBus bus)
        {
            this.iuserbussiness = iuserbussiness;
            this.bus = bus;
        }


        //public ActionResult UserNameCheck(UserModel model)
        //{
        //    var checkusername = iuserbussiness.CheckUserNames(model);
        //    if (checkusername)
        //        return Ok(new { status = true });
        //    else
        //        return BadRequest(new { status = false, msg = "We cannot register you with this user name.. It is already exist .. So try giving another" });
        //}
        [HttpPost]
        // [HttpGet]
        [Route("Registration")]

        public ActionResult RegisterAccount(UserRegModel model)
        {

            var checkusername = iuserbussiness.CheckUserNames(model.Email);
            if (checkusername)
            {
                var checkregister = iuserbussiness.RegisterAccount(model);
                if (checkregister != null)
                    return Ok(new { status = true, msg = "Registration successfull", data = checkregister });
                else
                    return BadRequest(new { status = false, msg = "Registration Failed", data = checkregister });

            }
            else
                return BadRequest(new { status = false, msg = "User name Already exists try to create another user name to continue registration process" });
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserLoginModel logModel)
        {
            var checkLogin = iuserbussiness.Login(logModel);
            if (checkLogin != null)
            {
                return Ok(new { status = true, msg = "Login SuccessFull", data = checkLogin });
            }
            else

                return BadRequest(new { status = false, msg = "Incorrect Email or Password If Not Registered Yet go and get register" });
            //switch (checkLogin)
            //{
            //    case -1:
            //        return BadRequest(new { status = false, msg = "Incorrect Email/UserName If Not Registered Yet go and get register" });
            //    case 0:
            //        return BadRequest(new { status = false, msg = "Incorrect Password" });
            //    case 1:
            //        return Ok(new { status = true, msg = "Login SuccessFull",data=checkLogin });
            //    default
            //         : return BadRequest(new { status = false, msg = "Unable to login Sorry for inconvinience,try again" });

            //}
        }
        [HttpGet]
        [Route("UsersDetails")]
        public IActionResult GetDetails()

        {
            var details = iuserbussiness.GetDetails();
            if (details == null)
                return NotFound();
            else
                return Ok(details);
        }

        [HttpGet]
        [Route("UserDetailsByID/{ID}")]
        public IActionResult GetDetailsByID(int ID)

        {
            var details = iuserbussiness.GetDetailsByID(ID);
            if (details == null)
                return NotFound();
            else
                return Ok(details);
        }

        [HttpDelete]
        [Route("DeleteByID/{ID}")]
        public IActionResult DeleteByID(int ID)
        {
            var details = iuserbussiness.DeleteByID(ID);
            if (details == null)
                return BadRequest(new { msg = "No match with this ID" });
            else
                return Ok(new { msg = "These reords got deleted", data = details });
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {

            var password = iuserbussiness.ForgotPassword(Email);
            if (password != null)
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = iuserbussiness.ForgotPassword(Email);
                    send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FunDooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);
                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new { IsSuccess = true, Message = "Mail Sent Successfully" ,Data="Token Sent to Mail"});

                }
                else
                {
                    return BadRequest(new { IsSuccess = false, MessageProcessingHandler = "Email Does not Exists", Data = Email });
                }
            }
        [Authorize]
        [HttpPost]
        [Route("ResetPasswordModel")]
        public IActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("Email").Value;
                if (iuserbussiness.ResetPassword(Email, reset))
                {
                    return Ok(new { IsSuccess = true, message = "Password successfully got Reset" });
                }
                else
                {
                    return BadRequest(new { IsSuccess = false, message = "Password Not Reset Try again by giving valid inputs" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("NoOfUsers")]
        public IActionResult NoOfUsers()
        { 
            var count=iuserbussiness.NoOfUsers();
            if (count > 0)
                return Ok(new { msg = "No of Users =", data = count });
            else
                return BadRequest(new { msg = "No Users Found" });
        }
    }

 }

