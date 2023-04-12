using System;
using System.Diagnostics;

namespace FélévesFeladat_Z6O9JF
{
    sealed class Program
    {
        public static string UIConsoleInput()
        {
            return Console.ReadLine();
        }
        static void EventWriter(string content)
        {
            Console.WriteLine(content);
        }
        static void DataGen(int amount) 
        {
            //*500 dolgozo és 2500 feladat felett már belassul :((
            Zoo.RandomEmployeeGenerator(amount);
            Zoo.RandomJobGenerator(amount*5);
        }
        static void ZooMaker()
        {
            Zoo.emptyJobs += EventWriter;
            Zoo.freeWorkers += EventWriter;
            Zoo.UIevent += EventWriter;
            Zoo.ExceptionEvent += EventWriter;
            Zoo.UI("Zoo_Tasks.txt", "Zoo_Employees.txt");

            Stopwatch s = Stopwatch.StartNew();

            MyLinkedList<Zoo.Jobs> jobs = Zoo.Reader("Zoo_Tasks.txt");
            MyLinkedList<Zoo.Employees> employees = Zoo.Reader("Zoo_Employees.txt", jobs);
            Zoo.JobGiver(jobs, employees);
            ;
            s.Stop(); 
            Console.WriteLine($"Elapsed Time: {s.ElapsedMilliseconds} ms");
        }
        static void Main(string[] args)
        {
            try
            {
                //DataGen(200);
                ZooMaker();
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            Console.ReadLine();
        }
    }
}
