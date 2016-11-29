using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.ChainLinks
{
    public class TemporaryChainLink : IChainLink
    {
        private string stepName;
        private static int index = 0;

        public Dictionary<string, object> Storage { get; set; }

        public TemporaryChainLink(string _stepName)
        {
            stepName = _stepName;
        }

        public void Execute(Dictionary<string, object> storage)
        {
            Storage = storage;
            Console.WriteLine(stepName);
            storage.Add("key"+ index.ToString(), "aloha");
            object value;
            storage.TryGetValue("key1", out value);
            Console.WriteLine((string)value);
            index++;
        }
    }
}
