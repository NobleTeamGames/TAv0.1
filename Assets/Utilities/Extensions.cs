using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mod = Assets.Model;

namespace Assets.Utilities
{
    public static class Extensions
    {
        public static Mod.Resource Dec(this Mod.Resource current, Mod.Resource price)
        {
            return new Mod.Resource(current.Water -= price.Water, current.Eat -= price.Eat, current.Wood -= price.Wood, current.Fassils -= price.Fassils, current.Genetics -= price.Genetics, current.Money -= price.Money, current.Energy -= price.Energy);
        }

        public static void Inc(this Mod.Resource current, Mod.Resource price)
        {
            current = new Mod.Resource(current.Water += price.Water, current.Eat += price.Eat, current.Wood += price.Wood, current.Fassils += price.Fassils, current.Genetics += price.Genetics, current.Money += price.Money, current.Energy += price.Energy);
        }

        public static bool ConfirmPrice(this Mod.Resource current, Mod.Resource price)
        {
            return current.Water > price.Water &&
                current.Eat > price.Eat &&
                current.Wood > price.Wood &&
                current.Fassils > price.Fassils &&
                current.Genetics > price.Genetics &&
                current.Money > price.Money &&
                current.Energy > price.Energy;
        }

        public static void Multiply(this Mod.Resource current, int price)
        {
            current = new Mod.Resource(current.Water *= price, current.Eat *= price, current.Wood *= price, current.Fassils *= price, current.Genetics *= price, current.Money *= price, current.Energy *= price);
        }
    }
}
