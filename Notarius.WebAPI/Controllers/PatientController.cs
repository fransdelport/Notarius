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
    public class PatientController : Controller
    {
        private readonly IPatientServiceRepository _patientRepository;

        public PatientController(IPatientServiceRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        // GET: api/<PatientController>
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            StreamWriter sw = new StreamWriter(@"C:\temp\AAALogFile.txt");
            sw.WriteLine("Controller.GetPatient");
            try
            {
                IEnumerable<PatientDTO> list = await _patientRepository.GetAllPatients();

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
        public IActionResult GetPatient(string mrn)
        {
           
            PatientDTO pat = _patientRepository.GetPatientByMRN(mrn);
            return Ok(pat);

        }

        // POST api/<PatientController>
        [HttpPost]
        public IActionResult SavePatient([FromBody] PatientDTO pat)
        {
            if (pat == null)
                return BadRequest();

            if (pat.Lastname == string.Empty)
            {
                ModelState.AddModelError("Name", "Last name should not be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPatient = _patientRepository.AddPatient(pat);

            return Created("patient", createdPatient);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] PatientDTO pat)
        {
            if (pat == null)
                return BadRequest();

            if (pat.Lastname == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The last name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patToUpdate = _patientRepository.GetPatientByMRN(pat.MRN);

            if (patToUpdate == null)
                return NotFound();

            _patientRepository.UpdateAccount(pat);

            return NoContent(); //success
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{mrn}")]
        public IActionResult Delete(string mrn)
        {
            if (string.IsNullOrEmpty(mrn))
                return BadRequest();

            var employeeToDelete = _patientRepository.GetPatientByMRN(mrn);
            if (employeeToDelete == null)
                return NotFound();

            _patientRepository.DeleteAccount(mrn);

            return NoContent();//success
        }
    }
}
