using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelmholtzEquation.ChainLinks;

namespace HelmholtzEquation.Process
{
    public class State
    {
        public IEnumerator<IChainLink> Enumerator { get; private set; }
        
        public Storage Storage { get; private set; }

        public State(IEnumerator<IChainLink> enumerator, Storage storage)
        {
            Enumerator = enumerator;
            Storage = storage;
        }
    }
}
