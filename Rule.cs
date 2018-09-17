using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a collection of shops which have to be
/// server by the same car
/// </summary>
public class Rule : IComparable<Rule>
{
    /// <summary>
    /// Shops for one car
    /// </summary>
    public List<Shop> adjShops;

    /// <summary>
    /// Checks what shop collection has more goods to deliver to
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Rule other)
    {
        return (other == null) ? 1 : (this.adjShops.Select(x => x.Pallets).Sum().CompareTo(other.adjShops.Select(x => x.Pallets).Sum()));
    }
}

