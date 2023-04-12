using System;

namespace FélévesFeladat_Z6O9JF
{
    sealed partial class Exceptions
    {
        public class NotQualifiedException : Exception
        {
            public NotQualifiedException(string name, string job)
                : base($"{name} is not qualified for any of the available '{job}' jobs, we're sending him to training!") { }
        }
    }
}
