using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notarius.Shared.DTO;
using Notarius.WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notarius.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleServiceRepository _scheduleRepository;

        public ScheduleController(IScheduleServiceRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        // GET: api/<PatientController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            StreamWriter sw = new StreamWriter(@"C:\temp\AAALogFile.txt");
            sw.WriteLine("Controller.GetPatient");
            try
            {
                IEnumerable<ScheduleDTO> list = await _scheduleRepository.GetAll();

                return Ok(list);
            }
            catch (Exception ex)
            {
                sw.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                sw.Close();
            }
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public IActionResult GetPatient(int key)
        {

            ScheduleDTO pat = _scheduleRepository.GetScheduleByKey(key);
            return Ok(pat);

        }

        // POST api/<PatientController>
        [HttpPost]
        public IActionResult SavePatient([FromBody] ScheduleDTO sched)
        {
            if (sched == null)
                return BadRequest();

            if (sched.MRN == string.Empty)
            {
                ModelState.AddModelError("MRN", "Medical record number should not be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSchedule = _scheduleRepository.Add(sched);

            return Created("schedule", createdSchedule);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] ScheduleDTO sched)
        {
            if (sched == null)
                return BadRequest();

            if (sched.MRN == string.Empty)
            {
                ModelState.AddModelError("MRN", "Medical record number can not be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patToUpdate = _scheduleRepository.GetScheduleByKey(sched.Key);

            if (patToUpdate == null)
                return NotFound();

            _scheduleRepository.Update(sched);

            return NoContent(); //success
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{mrn}")]
        public IActionResult Delete(int key)
        {
            if (key==0)
                return BadRequest();

            var employeeToDelete = _scheduleRepository.GetScheduleByKey(key);
            if (employeeToDelete == null)
                return NotFound();

            _scheduleRepository.Delete(key);

            return NoContent();//success
        }
    }
}
