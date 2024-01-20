﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.DTO;

public class AppointmentDTO
{
    public int? Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = "";
    public string? Notes { get; set; }
}