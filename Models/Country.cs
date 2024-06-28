using System;
using System.Collections.Generic;

namespace APBD_8.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TripCountry> TripCountries { get; set; } = new List<TripCountry>();
}
