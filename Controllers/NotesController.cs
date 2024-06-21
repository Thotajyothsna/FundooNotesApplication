using System.Text;
using BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.Model;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBussiness iNotesBussiness;
        private readonly IDistributedCache distributedCache;
        private readonly FundooNotesContext fundooNotesContext;
        public NotesController(INotesBussiness iNotesBussiness,IDistributedCache distributedCache,FundooNotesContext fundooNotesContext)
        {
            this.iNotesBussiness = iNotesBussiness;
            this.distributedCache = distributedCache;
            this.fundooNotesContext = fundooNotesContext;
        }

        [HttpPost]
        [Route("CreateNotes")]
        [Authorize]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            { 

                //int id = (int)HttpContext.Session.GetInt32("userId");
                int UserId =Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var notes = iNotesBussiness.CreateNotes(notesModel, UserId);
                if(notes!=null)
                {
                    return Ok(new { IsSuccess = true, Message = notes });
                }
                else
                {
                    return BadRequest(new { IsSuccess=false,Message="Notes not Created"});
                }
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        { 
            var Notes=iNotesBussiness.GetAllNotes();
            if(Notes!=null)
                return Ok(Notes);
            else
                return NotFound();
        }
        [HttpGet("Reddis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var CacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            try
            {

                byte[] redisNotesList = await distributedCache.GetAsync(CacheKey);
                if (redisNotesList != null)
                {
                    serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                    NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
                }
                else
                {
                    NotesList = fundooNotesContext.Notes.ToList();
                    serializedNotesList = JsonConvert.SerializeObject(NotesList);
                    redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(CacheKey, redisNotesList, options);
                }
                return Ok(NotesList);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }
        [Authorize]
        [HttpGet]
        [Route("GetAllNotesOfUser")]
        public IActionResult GetByUserId()
        { 
            int UserId=Convert.ToInt32(User.Claims.FirstOrDefault(x=>x.Type=="UserId").Value);
            var Notes=iNotesBussiness.GetByUserId(UserId);
            if (Notes != null)
            {
                return Ok(new { msg = "Your Notes ", data = Notes });
            }
            else
                return BadRequest(new { msg = "Notes are not yet created by you" });
        }
        [Authorize]
        [HttpPut]
        [Route("Update Notes Of User")]
        public IActionResult UpdateNotes(long NotesId, UpdateNotesModel updateNotesModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
                
                var UpdatedNotes = iNotesBussiness.UpdateNotes(UserId, NotesId, updateNotesModel);
                if (UpdatedNotes != null)
                    return Ok(new { msg = "Notes After Updation", data = UpdatedNotes });
                else
                    return BadRequest(new { status=false, msg = "Invalid Inputs" });
               
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg=ex});
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteNoteOfUser")]
        public IActionResult DeleteNotes(long NotesId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);

                var DeletedNotes = iNotesBussiness.DeleteNote(UserId, NotesId);
                if (DeletedNotes != null)
                    return Ok(new { msg = "Notes that deleted", data = DeletedNotes });
                else
                    return BadRequest(new { status = false, msg = "Invalid Inputs" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("PinNotes_Or_UnPinNotes")]
        public IActionResult PinNotes(long NotesId)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var PinNotes=iNotesBussiness.PinNotes(UserId, NotesId);
            if (PinNotes != null)
                return Ok(new { msg = PinNotes });
            else
                return BadRequest(new { status = false, msg = "Wrong Inputs Or NoteId" });
        }
        [Authorize]
        [HttpPut]
        [Route("ArchiveNotes_Or_UnArchiveNotes")]
        public IActionResult ArchiveNotes(long NotesId)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var ArchiveNotes = iNotesBussiness.ArchiveNotes(UserId, NotesId);
            if (ArchiveNotes != null)
                return Ok(new { msg = ArchiveNotes });
            else
                return BadRequest(new { status = false, msg = "Wrong Inputs Or NoteId" });
        }
        [Authorize]
        [HttpPut]
        [Route("TrashNotes_Or_UnTrashNotes")]
        public IActionResult TrashNotes(long NotesId)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var TrashNotes = iNotesBussiness.TrashNotes(UserId, NotesId);
            if (TrashNotes != null)
                return Ok(new { msg = TrashNotes });
            else
                return BadRequest(new { status = false, msg = "Wrong Inputs Or NoteId" });
        }
        [Authorize]
        [HttpPut]
        [Route("ChangeColor")]
        public IActionResult ColorChange(long NotesId,string  Color)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var ColorChanged = iNotesBussiness.ColorChange(UserId, NotesId, Color);
            if (ColorChanged != null)
            {
                if (ColorChanged == "INVALID")
                    return BadRequest(new { status = false, msg = "Invalid Color Name or Color is not available" });
                else
                    return Ok(new { status = true, msg = "Colored Changed to ", data = ColorChanged });
            }
            else
                return BadRequest(new { status = false, msg = "Authentications Invalid or Invalid NOte Id" });

        }
        [Authorize]
        [HttpPut]
        [Route("ChangeImage")]
        public IActionResult ImageChange(long NotesId,string Image)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var ImageChanged=iNotesBussiness.ImageChange(UserId, NotesId, Image);
            if (ImageChanged != null)
            {
                return Ok(new { status = true, msg = "Image set to desired Notes" });
            }
            else
                return BadRequest(new { status = false, msg = "Image not set.There may be invalid Authentication" });
        }
        [Authorize]
        [HttpPut]
        [Route("ChangeOrSetRemainder")]
        public IActionResult RemainderChange(long NotesId,DateTime Remainder)
        {
            int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var RemainderSet=iNotesBussiness.RemainderChange(UserId, NotesId, Remainder);
            if (RemainderSet == "REMAINDER_SET")
                return Ok(new { status = true, msg = "Remainder was set to desired time " });
            else
                return NotFound();
        }
    }
}
