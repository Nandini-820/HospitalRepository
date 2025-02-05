using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class DoctorSpecialization
{
    public int DoctorId { get; set; }

    public string SpecializationCode { get; set; } = null!;

    public DateTime SpecializationDate { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Specilization SpecializationCodeNavigation { get; set; } = null!;
}
