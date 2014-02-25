using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Model
{
    public class Resource
    {
        public int Water;
        public int Eat;
        public int Wood;
        public int Fassils;
        public int Genetics;
        public int Money;
        public int Energy;

        public Resource()
        {
            Water = 200;
            Eat = 500;
            Wood = 300;
            Fassils = 100;
            Money = 1000;
            Energy = 500;
            Genetics = 0;
        }

        public Resource(int water, int eat, int wood, int fassils, int genetics, int money, int energy)
        {
            Water = water;
            Eat = eat;
            Wood = wood;
            Fassils = fassils;
            Genetics = genetics;
            Money = money;
            Energy = energy;
        }

        public override string ToString()
        {
            return @"Water = " +Water+" Eat ="+ Eat+ " Wood ="+ Wood+" Fassils = "+Fassils+" Genetics = "+Genetics+" Money = "+Money+" Energy = "+Energy;
        }
    }
}
