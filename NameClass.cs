using System;
using System.Collections.Generic;

namespace School_tametable
{
    public partial class NameClass
    {
        public NameClass()
        {
            Classes = new HashSet<Class>();
        }

        public long IdNameCl { get; set; }
        public string? NameClass1 { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
