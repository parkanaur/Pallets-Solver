using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Solves partition problem for vehicles
/// </summary>
public class Solver
{
    private List<Rule> rules;

    public Solver(List<Rule> rules)
    {
        this.rules = rules;
    }

    /// <summary>
    /// Returns a list of shops to visit for each car
    /// </summary>
    /// <param name="cars"></param>
    /// <returns></returns>
    public List<List<Rule>> Solve(int cars)
    {
        List<List<Rule>> r = new List<List<Rule>>();
        for (int i = 0; i < cars; ++i)
        {
            r.Add(new List<Rule>());
        }
        rules.Sort();
        rules.Reverse();

        // using simplest partition problem algorithm
        foreach (var rule in rules)
        {
            r.First().Add(rule);
            r.Sort((list, other) => list.Sum(x => x.adjShops.Sum(shop => shop.Pallets)).CompareTo(other.Sum(x => x.adjShops.Sum(shop => shop.Pallets))));
        }

        return r;
    }
}

