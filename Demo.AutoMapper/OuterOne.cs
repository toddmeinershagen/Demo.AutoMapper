using System.Collections.Generic;

namespace Demo.AutoMapper
{
    public class OuterOne : IOuter
    {
        public string PropertyOne { get; set; }
        public string PropertyTwo { get; set; }
        public IEnumerable<IInner> PropertyFive { get; set; }
    }
}