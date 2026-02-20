using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07_Object_reference
{

    class Person
    {
        private string vorname;   // Attribut / Member-Variable

        public string Nachname { get; set; }

        public Adresse Adresse { get; set; }
        public string Vorname   // Property
        {
            get
            {
                return vorname;
            }
            set
            {
                vorname = value[0].ToString().ToUpper()
                    + value.ToLower().Substring(1);
            }
        }

        public Person(string vorname, string nachname)
        {

            this.vorname = vorname;
            this.Nachname = nachname;
        }

        //public string GetVorname()
        //{
        //    return vorname;
        //}
    }

    class Ort
    {
        public string Name { get; set; }
        public int PLZ { get; set; }
        public Person Bürgermeister { get; set; }

        public Ort(string name, int plz)
        {
            Name = name;
            PLZ = plz;


        }
    }

    class Adresse
    {
        public string Strasse { get; set; }
        public int Hausnummer { get; set; }
        public Ort Ort { get; set; }  


        public Adresse(string strasse, int hausnummer)
        {
            Strasse = strasse;
            Hausnummer = hausnummer;


        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 10;

            Ort ort = new Ort("Musterstadt", 1234);

            Person p1 = new Person("Max", "Mustermann");
            Person p2 = new Person("Dagobert", "Duck");
            Person p3 = new Person("Susi", "Beispielfrau");
            Person p4 = new Person("Daisy", "Duck");

            Adresse a1 = new Adresse("Musterstraße", 4);
            Adresse a2 = new Adresse("Musterplatz", 7);

            a1.Ort = ort;
            a2.Ort = ort;

            p1.Adresse = a1;
            p2.Adresse = a2;
            p3.Adresse = a1;
            p4.Adresse = a2;


            Console.WriteLine(p1.Vorname);

            Console.ReadKey();
        }
    }

}
    
