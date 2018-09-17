using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


public class Program
{
    public static void Main()
    {
        ConfigParser parser = new ConfigParser();
        string status = parser.ReadData();
        if (status != ParseStatus.Success)
        {
            Console.WriteLine(status);
            Console.ReadLine();
            return;
        }

        Solver solver = new Solver(parser.Data);
        List<List<Rule>> cars = solver.Solve(parser.Cars);
        using (StreamWriter file = new StreamWriter("out.txt"))
        {
            for (int i = 0; i < cars.Count(); ++i)
            {
                file.WriteLine($"Машина {i + 1}");
                file.WriteLine($"\tМагазины:");
                int pallets = 0;
                foreach (var rule in cars[i])
                {
                    foreach (var shop in rule.adjShops)
                    {
                        file.WriteLine($"\t\tМагазин {shop.Num} - {shop.Pallets} паллетов");
                        pallets += shop.Pallets;
                    }
                }
                file.WriteLine($"\tИтого {pallets} паллетов");
            }
        }
    }
}