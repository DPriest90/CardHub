using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardHub.Classes
{
    public class BoosterPack
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

        public BoosterPack(string name)
        {
            Name = name;
        }
    }

}
