using System.Collections.Generic;

namespace Demo.AutoMapper
{
    public interface IOuter
    {
        string PropertyOne { get; set; }
        string PropertyTwo { get; set; }
        IEnumerable<IInner> PropertyFive { get; set; } 
    }
}