using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelsBussiness iLabelsBussiness;
        public LabelsController(ILabelsBussiness iLabelsBussiness)
        {
            this.iLabelsBussiness = iLabelsBussiness;
        }
        [Authorize]
        [HttpPost]
        [Route("AddLabelToNote")]
        public IActionResult AddLabelToNotes(LabelsModel model)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var LabelAdded = iLabelsBussiness.AddlabelToNotes(model, UserId);
            if(LabelAdded!=null)
            {
                return Ok(new { msg = "label Successfully Added to Note",data=LabelAdded });
            }
            else
            {
                return BadRequest(new {msg="Label Doesn't added or Wrong Inputs"});
            }
        }
        [Authorize]
        [HttpPost]
        [Route("AddOnlyLabelToUser")]
        public IActionResult AddLabel(string LabelName)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var LabelAdded = iLabelsBussiness.AddLabel(LabelName, UserId);
            if (LabelAdded != null)
            {
                return Ok(new { msg = "label Successfully Added ", data = LabelAdded });
            }
            else
            {
                return BadRequest(new { msg = "Label Doesn't added or Wrong Inputs" });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("RenameLabel")]
        public IActionResult RenameLabel(RenameLabelModel model)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var RenamedLabel=iLabelsBussiness.RenameLabel(model, UserId);
            if (RenamedLabel != null)
            {
                return Ok(new { msg = "Label Renamed to", data = RenamedLabel });
            }
            else
                return BadRequest(new { msg = "label Doesn't exists or wrong inputs" });
        }
        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetAllLabels()
        {
            var AllLabels=iLabelsBussiness.GetAllLabels();
            if (AllLabels != null)
            {
                return Ok(AllLabels);
            }
            else
            {
                return BadRequest(new { msg = "No Labels Exists" });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllLabelsOfUser")]
        public IActionResult GetAllLabelsOfUser()
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var AllLabels = iLabelsBussiness.GetAllLabelsOfUser(UserId);
            if (AllLabels != null)
            {
                return Ok(AllLabels);
            }
            else
            {
                return BadRequest(new { msg = "No Labels present to this user" });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("DeleteLabelById")]
        public IActionResult DeleteLabelById(long LabelId)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var DeletedLabel=iLabelsBussiness.DeleteLabelbyId(LabelId,UserId);
            if (DeletedLabel != null)
            {
                return Ok(new { msg = "Label Deleted", data = DeletedLabel });
            }
            else
                return BadRequest(new { msg = "label Doesnot Exists" });
        }
    }
}
