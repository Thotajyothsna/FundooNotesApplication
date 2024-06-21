using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorsController : ControllerBase
    {
        private readonly ICollaboratorsBussiness icollaboratorsBussiness;
        private readonly ILogger<CollaboratorsController> logger;
        public CollaboratorsController(ICollaboratorsBussiness icollaboratorsBussiness,ILogger<CollaboratorsController> logger)
        {
            this.icollaboratorsBussiness= icollaboratorsBussiness;
            this.logger = logger;
        }
        [Authorize]
        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(CollaboratorsModel model)
        {
            int UserId = Convert.ToInt32(User.FindFirst(u => u.Type == "UserId").Value);
            var Added = icollaboratorsBussiness.AddCollaborator(model, UserId);
            if(Added=="SameCollaboratorExists")
            {
                return Ok(new { status = false, msg = "Already Same Collaborator Exists to this Notes" });
            }
            else if(Added== "NotesId_NotExists")
            {
                return BadRequest(new { status = false, msg = "NotesId is not Valid/Not Exists to this User" });
            }
            else if (Added== "User_NotExists")
            {
                return BadRequest(new { status = false, msg = "User Is Not Exists" });
            }
            else
            {
                return Ok(new {status=true,msg="Collaborator Added to Notes"});
            }
        }
        [HttpGet]
        [Route("GetAllCollaborators")]
        public IActionResult GetAllCollaborators()
        {
            var AllCollaborators=icollaboratorsBussiness.GetAllCollaborators();
            if(AllCollaborators!=null)
            {
                return Ok(new { status=true,msg="All Collaborators Data",data=AllCollaborators }); 
            }
            else
            {
                return BadRequest(new { status = false, msg = "NoCollaborator" });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllCollaboratorsOfUser")]
        public IActionResult GetAllCollaboratorsOfUser()
        {
            int UserId = Convert.ToInt32(User.FindFirst(u => u.Type == "UserId").Value);
            var AllCollaborators=icollaboratorsBussiness.GetAllCollaboratorsOfUser(UserId);
            if(AllCollaborators== "NoCollaborator") 
            {
                return BadRequest(new { status = false, msg = "No Note is Collaborated with others" });
            }
            else if (AllCollaborators == "UserNotExists")
            {
                return BadRequest(new { status = false, msg = "No Note is Collaborated with others" });
            }
            else
            { 
                return Ok(new { status=true,masg="All Collaborators",data=AllCollaborators});
            }
            
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteCollaboratorById")]
        public IActionResult DeleteCollaboratorById(long CollaboratorId)
        {
            int UserId = Convert.ToInt32(User.FindFirst(u => u.Type == "UserId").Value);
            var Deleted=icollaboratorsBussiness.DeleteCollaboratorById(CollaboratorId,UserId);
            if (Deleted == "Deleted")
                return Ok(new { status = true, msg = "Collaborator Deleted Successfully" });
            else if (Deleted == "IdNotExists")
                return BadRequest(new { status = false, msg = "Collaborator Id Not Exists or Not Belong to this User" });
            else
                return BadRequest(new { status = false, msg = "User Doesnot Exists" });
        }

    }
}
