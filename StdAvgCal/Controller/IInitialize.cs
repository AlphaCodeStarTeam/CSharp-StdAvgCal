using System;
using System.Collections.Generic;

namespace StdAvgCal.Controller
{
    public interface IInitialize
    {
        public bool IsInit { get; set; }
        public List<Init> Initializers { get; }
        delegate void Init();
        bool CheckInit();

        public void InitMe()
        {
            IsInit = true;
            foreach (var initializer in Initializers)
                initializer.Invoke();
        }
    }
    
    public class NotInitializedException : Exception {}
}