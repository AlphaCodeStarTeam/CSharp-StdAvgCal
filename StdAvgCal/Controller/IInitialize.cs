using System;
using System.Collections.Generic;

namespace StdAvgCal.Controller
{
    public interface IInitialize
    {
        public List<Init> Initializers { get; }
        delegate void Init();

        public void InitMe()
        {
            foreach (var initializer in Initializers)
                initializer.Invoke();
        }
    }
}