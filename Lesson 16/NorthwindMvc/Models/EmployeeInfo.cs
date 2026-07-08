using System;
using System.Collections.Generic;

namespace NorthwindMvc.Models;

public partial class EmployeeInfo
{
    public int Id { get; set; }

    public string? EmployeeFullName { get; set; }

    public string EmployeeFullAddress { get; set; } = null!;

    public string EmployeeCompanyInfo { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? City { get; set; }
}
