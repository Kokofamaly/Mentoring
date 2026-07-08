using System;
using System.Collections.Generic;

namespace NorthwindMvc.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int AddressId { get; set; }

    public int PersonId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Position { get; set; }

    public string? EmployeeName { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
