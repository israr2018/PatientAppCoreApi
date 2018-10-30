using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientCoreApi.Contracts;
using PatientCoreApi.Data;
using PatientCoreApi.Models;

namespace PatientCoreApi.Repositories
{
    public class PatientRepository:IPatientRepository
    {

        private PatientDbContext _context;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> Add(Patient Patient)
        {
            await _context.Patients.AddAsync(Patient);
            await _context.SaveChangesAsync();
            return Patient;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Patients.AnyAsync(c => c.Id == id);
        }

        public async Task<Patient> Find(int id)
        {
            return await _context.Patients.SingleOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients;
        }

        public async Task<Patient> Remove(int id)
        {
            var Patient = await _context.Patients.SingleAsync(a => a.Id == id);
            _context.Patients.Remove(Patient);
            await _context.SaveChangesAsync();
            return Patient;
        }

        public async Task<Patient> Update(int id,Patient patient)
        {
        

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

    }
}
