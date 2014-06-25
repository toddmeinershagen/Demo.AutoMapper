using System.Collections.Generic;

namespace Demo.AutoMapper
{
    public class OuterTwo 
    {
        public string PropertyOne { get; set; }
        public string PropertyTwo { get; set; }
        public IEnumerable<InnerTwo> PropertyFive { get; set; }
    }
}