﻿using DEWebApi.Dto.Patient;
using DEWebApi.Models;
using DoctorEaseWebApi.Models;

namespace DEWebApi.Services.Patient
{
    public interface IPatientInterface
    {
        Task<ResponseModel<List<PatientModel>>> GetPatients();
        Task<ResponseModel<PatientModel>> CreatePatient(CreatePatientDto createPatientDto);
    }
}
