using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04_Bruchrechnung
{
    internal class Bruchrechnung
    {
        Bruch bruch1;
        Bruch bruch2;

        public static Bruchrechnung Parse(string str)
        {
            string[] teile = str.Split('-', '+', '*', ':');
            Bruch bruch1 = Bruch.Parse(teile[0]);
            Bruch bruch2 = Bruch.Parse(teile[1]);
            //return Bruchrechnung(bruch1, bruch2);
            return null;
        }

        public Bruch GetResult()
        {
            return null;
        }
    }
}
