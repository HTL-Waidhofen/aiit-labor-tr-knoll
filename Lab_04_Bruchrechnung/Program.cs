using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04_Bruchrechnung
{
    class Bruch
    {
        int zaehler;
        int nenner;

        public Bruch(int zaehler, int nenner)
        {
            this.zaehler = zaehler;
            this.nenner = nenner;
        }

        public int getZaehler() 
        {
            return zaehler; 
        }

        public int getNenner() 
        {
            return nenner;
        }

        public void setZaehler(int zaehler) 
        {
            this.zaehler = zaehler;
        }
        public void setNenner(int nenner) 
        {
            this.nenner = nenner;
        }

        public override string ToString()
        {
            if ( nenner == 1)
            {
                return zaehler.ToString();
            }
            else
            {
                return zaehler + "/" + nenner;
            }
               
        }

        public static Bruch Parse(string str) 
        {
            string[] teile= str.Split('/');
            int zaehler = int.Parse(teile[0]);
            int nenner = int.Parse(teile[1]);

            return new Bruch(zaehler, nenner);
        }

        public void kuerzen() 
        {
            int a = zaehler;
            int b = nenner;
            while (b != 0) 
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            int ggt = a;
            zaehler /= ggt;
            nenner /= ggt;

        }

        public void add(Bruch bruch) 
        { 
            int n =this.nenner * bruch.getNenner();
            int z = this.zaehler * bruch.getNenner() + bruch.getZaehler() * this.nenner;
            this.zaehler = z;
            this.nenner = n;
            kuerzen();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Geben Sie einen Bruch im Format Zähler/Nenner ein:");
            string line = Console.ReadLine();
            Bruch bruch = Bruch.Parse(line);
            Console.WriteLine("+");
            string line1 = Console.ReadLine();
            Bruch bruch1 = Bruch.Parse(line1);
            bruch.add(bruch1);
            Console.WriteLine("--------------");
            Console.WriteLine("Ergebnis: ");
            Console.Write(bruch);

            Console.ReadKey();


        }
    }
}
