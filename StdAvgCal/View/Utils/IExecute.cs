using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StdAvgCal.View.Utils
{
    public interface IExecute
    {
        public Dictionary<string, Execute> Executors { get; }
        
        delegate void Execute(params string[] args);

        void ExecuteCommand(String command)
        {
            Match match;
            foreach (var inputRegex in Executors.Keys)
            {
                match = Regex.Match(command, inputRegex);
                if (match.Success)
                {
                    Executors[inputRegex].Invoke(GetParameters(match.Groups));
                    return;
                }
            }

            throw new MethodNotFoundException();
        }

        private static string[] GetParameters(GroupCollection groupCollection)
        {
            return groupCollection
                .Values
                .Skip(1)
                .Select(group => group.Value)
                .ToArray();
        }

    }
    
    public class MethodNotFoundException : Exception { }
}