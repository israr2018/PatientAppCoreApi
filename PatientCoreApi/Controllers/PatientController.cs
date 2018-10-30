using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientCoreApi.Models;
using PatientCoreApi.Data;
using Microsoft.EntityFrameworkCore;

using PatientCoreApi.Contracts;

namespace PatientCoreApi.Controllers
{

    [Produces("application/json")]
    [Route("api/patients")]
    
    public class PatientController : ControllerBase
    {
        public class PatientModel
        {

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
        }
        private readonly IPatientRepository repository;
      
        public PatientController(IPatientRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        
        public IEnumerable<Models.Patient> Get()
        {
            //  return patientDbContext.Patients; 

            return this.repository.GetAll();
        }


        
        [HttpGet("{id}")]
        public ActionResult<Patient> GetById([FromRoute] int id)
        {
            try
            {

                // var result = patientDbContext.Patients.Find(id);
                var result = this.repository.Find(id);
                if (result!=null)
                {
                    return Ok(result);

                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest("Something goes wrong on Server , try latter");

            }
           
        }

        // POST api/values
        [HttpPost("Create")]
        public  ActionResult Create([FromBody] PatientModel patient)
        {

            try
            {



              this.repository.Add(new Patient() {

                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    Address = patient.Address,
                    Mobile = patient.Mobile


                });

               
               
                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest("Patione Could not created");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id,[FromBody] Patient patientToUpdate )
        {

            if (id != patientToUpdate.Id)
                return BadRequest();
          
           var patient= this.repository.Update(id,patientToUpdate);
           


            return Ok(patient);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           if( await repository.Exist(id))
           return Ok();
            else{

                return BadRequest();
            }


        }

       
        
    }
}
