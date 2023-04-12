using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FélévesFeladat_Z6O9JF
{
    sealed partial class Zoo
    {
        public static Random rnd = new Random();
        public static void RandomEmployeeGenerator(int amount)
        {
            StreamReader sr = new StreamReader("RandomNames.txt");
            string[] jobs = new string[] { "Vet", "Janitor", "CareTaker", "Cashier" };
            List<string> names = new List<string>();

            while (!sr.EndOfStream)
            {
                names.Add(sr.ReadLine());
            }
            for (int i = 0; i < amount; i++)
            {
                string input = $"{names[rnd.Next(0, names.Count())]}_{jobs[rnd.Next(0, 4)]}_{Convert.ToString(rnd.Next(0, 10))}";
                using (StreamWriter sw = File.AppendText("Zoo_Employees.txt"))
                {
                    sw.WriteLine(input);
                }
            }
        }
        public static void RandomJobGenerator(int amount)
        {
            StreamReader sr = new StreamReader("RandomAnimals.txt");
            string[] jobs = new string[] { "Quarantine", "Promoting", "CageCleaning", "Feeding" };
            List<string> names = new List<string>();

            while (!sr.EndOfStream)
            {
                names.Add(sr.ReadLine());
            }
            for (int i = 0; i < amount; i++)
            {
                string input = $"{names[rnd.Next(0, names.Count())]}_{jobs[rnd.Next(0, 4)]}_{Convert.ToString(rnd.Next(1, 10))}";
                using (StreamWriter sw = File.AppendText("Zoo_Tasks.txt"))
                {
                    sw.WriteLine(input);
                }
            }
        }
    }
}
