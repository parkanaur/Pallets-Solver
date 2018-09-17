using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class ConfigParser
    {
        public List<Rule> Data { get; private set; }
        public int Cars { get; private set; }
        string filename;
        char shopStart;
        char ruleStart;
        char carStart;
        char shopSep;
        char ruleSep;

        public ConfigParser(string filename = "data.txt", char shopStart = ';', char ruleStart = '.',
            char shopSep = ':', char ruleSep = ' ', char carStart = '*')
        {
            this.filename = filename;
            this.shopStart = shopStart;
            this.ruleStart = ruleStart;
            this.shopSep = shopSep;
            this.carStart = carStart;
            this.ruleSep = ruleSep;
            Data = new List<Rule>();
            Cars = 0;
        }

        public string ReadData()
        {
            List<Shop> shops = new List<Shop>();
            if (!File.Exists(filename))
            {
                return string.Format(ParseStatus.NoFileFound, filename);
            }

            IEnumerable<string> file = File.ReadAllLines(filename);
            try
            {
                Cars = int.Parse(file.Where(x => x.StartsWith(carStart.ToString())).First().Substring(1));
            }
            catch
            {
                return ParseStatus.BadCars;
            }
            foreach (string shop in file.Where(x => x.StartsWith(shopStart.ToString())))
            {
                try
                {
                    IEnumerable<int> shopData = shop.Substring(1).Split(shopSep).Select(x => int.Parse(x));
                    shops.Add(new Shop { Num = shopData.First(), Pallets = shopData.Last() });
                }
                catch
                {
                    return ParseStatus.BadShop;
                }
            }

            List<Shop> usedShops = new List<Shop>();
            foreach (string rule in file.Where(x => x.StartsWith(ruleStart.ToString())))
            {
                try
                {
                    IEnumerable<int> ruleData = rule.Substring(1).Split(ruleSep).Select(x => int.Parse(x));
                    List<Shop> ruleShops = shops.Where(shop => ruleData.Contains(shop.Num)).ToList();
                    Data.Add(new Rule { adjShops = ruleShops });
                    usedShops.AddRange(ruleShops);
                }
                catch
                {
                    return ParseStatus.BadRule;
                }
            }

            List<Shop> unusedShops = shops.Where(shop => usedShops.All(usedShop => usedShop.Num != shop.Num)).ToList();
            Data.AddRange(unusedShops.Select(x => new Rule { adjShops = new List<Shop> { x } }));

            return ParseStatus.Success;
        }
    }
