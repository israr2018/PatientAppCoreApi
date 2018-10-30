using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientCoreApi.Models;

namespace PatientCoreApi.Contracts
{
   public interface IPatientRepository
    {


        Task<Patient> Add(Patient patient);

        IEnumerable<Patient> GetAll();

        Task<Patient> Find(int id);

        Task<Patient> Update(int id,Patient customer);

        Task<Patient> Remove(int id);

        Task<bool> Exist(int id);


    }
}
