using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessProviders;
using Common.Contracts.Model;

namespace ShiftTracker.Controllers
{
    [ApiController]
    [Route("api/shift")]
    public class ShiftTrackerController : ControllerBase
    {
        private readonly IShiftTrackerBusinessProvider _shiftTrackerBusinessProvider;
        public ShiftTrackerController(IShiftTrackerBusinessProvider shiftTrackerBusinessProvider)
        {
            _shiftTrackerBusinessProvider = shiftTrackerBusinessProvider;

        }
        [HttpGet]
        public ActionResult<List<Shift>> GetAll()
        {
            var record = _shiftTrackerBusinessProvider.GetAllRecord();

            if (record.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_shiftTrackerBusinessProvider.GetAllRecord());
        }

        [HttpGet("{id}")]
        public ActionResult<List<Shift>> GetByID(int id)
        {
            var record = _shiftTrackerBusinessProvider.GetRecordByID(id);

            if (record.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_shiftTrackerBusinessProvider.GetRecordByID(id));
        }

        [HttpPost]
        [Route("Post")]
        public ActionResult Post(Shift shifts)
        {
            _shiftTrackerBusinessProvider.PostRecord(shifts);

            return Ok(201);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteAll()
        {
            _shiftTrackerBusinessProvider.DeleteAllRecord();

            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult DeleteByID(int id)
        {
            var record = _shiftTrackerBusinessProvider.GetRecordByID(id);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            _shiftTrackerBusinessProvider.DeleteRecordById(id);

            return Ok();
        }




    }
}
