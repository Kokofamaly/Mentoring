using System;
using System.Collections.Generic;

namespace NorthwindMvc.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;
}
