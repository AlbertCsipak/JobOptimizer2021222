using System;
using System.Collections.Generic;

namespace FélévesFeladat_Z6O9JF
{
    interface IÁllatkertiDolgozó : IComparable
    {
        string Name { get; }
        string Job { get; }
        int Skill { get; }
        int JobCounter { get; }
        List<Zoo.Jobs> JobList { get; }
    }
    sealed partial class Zoo
    {
        public class Employees : IÁllatkertiDolgozó
        {
            public string Name { get; private set; }
            public string Job { get; private set; }
            public int Skill { get; private set; }
            public int JobCounter { get; set; } = 0;
            public string BadgeNumber { get { return Convert.ToString(Job[0]) + Convert.ToString(Skill) + GetHashCode() % 100000; } }
            public List<Jobs> JobList { get; set; }
            public Employees(string name, string job, int skill)
            {
                this.Name = name;
                this.Job = job;
                this.Skill = skill;
                this.JobList = new List<Jobs>();
            }
            public int CompareTo(object obj)
            {
                return Skill.CompareTo((obj as Employees).Skill);
            }
            public override string ToString()
            {
                return Convert.ToString(Name + " the " + Job + " (" + Skill + ") BN:" + BadgeNumber);
            }
            public sealed class Janitor : Employees
            {
                public Janitor(string name, string job, int skill) : base(name, job, skill) { }
            }
            public sealed class CareTaker : Employees
            {
                public CareTaker(string name, string job, int skill) : base(name, job, skill) { }
            }
            public sealed class Vet : Employees
            {
                public Vet(string name, string job, int skill) : base(name, job, skill) { }
            }
            public sealed class Cashier : Employees
            {
                public Cashier(string name, string job, int skill) : base(name, job, skill) { }
            }
        }
    }
}

