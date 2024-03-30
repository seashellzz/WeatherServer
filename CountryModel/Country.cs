using System;
using System.Collections.Generic;

namespace CountryModel;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string Iso2 { get; set; } = null!;

    public string Iso3 { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
