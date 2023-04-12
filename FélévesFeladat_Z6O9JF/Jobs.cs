using System;

namespace FélévesFeladat_Z6O9JF
{
    interface IFeladat : IComparable
    {
        string Name { get; }
        int Priority { get; }
    }
    sealed partial class Zoo
    {
        public class Jobs : IFeladat
        {
            public string Name { get; private set; }
            public int Priority { get; private set; }
            public string JobID { get {return Convert.ToString(Name[0]) + Convert.ToString(Priority) + GetHashCode() % 100000; } }
            public Jobs(string name, int priority)
            {
                this.Name = name;
                this.Priority = priority;
            }
            public int CompareTo(object obj)
            {
                return Priority.CompareTo((obj as Jobs).Priority);
            }
            public override string ToString()
            {
                return Convert.ToString(Name + " (" + Priority + ")");
            }
            public sealed class Feeding : Jobs
            {
                public Feeding(string name, int priority) : base(name, priority) { }
            }
            public sealed class Quarantine : Jobs
            {
                public Quarantine(string name, int priority) : base(name, priority) { }
            }
            public sealed class CageCleaning : Jobs
            {
                public CageCleaning(string name, int priority) : base(name, priority) { }
            }
            public sealed class Promoting : Jobs
            {
                public Promoting(string name, int priority) : base(name, priority) { }
            }
        }
    }
}
