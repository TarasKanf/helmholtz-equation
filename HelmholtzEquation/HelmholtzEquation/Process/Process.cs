using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelmholtzEquation.ChainLinks;

namespace HelmholtzEquation.Process
{
    public class Process
    {
        private Container Container { get; set; }

        private Storage Storage { get; set; }

        private IEnumerator<IChainLink> Enumerator { get; set; }

        public Process()
        {
            // TODO use fabric to create container
            Container = new Container();
            Storage = new Storage();
            Enumerator = Container.GetEnumerator();
        }

        public bool ExecuteStep()
        {
            bool nextLinkExists = Enumerator.MoveNext();
            if (nextLinkExists)
            {
                IChainLink currentLink = Enumerator.Current;
                currentLink.Execute(Storage.Data);
            }
            //else
            //{
            //    Enumerator.Reset();
            //}

            return nextLinkExists;
        }

        public State GetState()
        {
            // TODO replace whec custom enumerator would be created
            var enumerator = Container.GetEnumerator();

            bool currentReached = false;
            int index = 0;
            while (!currentReached)
            {
                if (enumerator.MoveNext())
                {
                    index++;
                    currentReached = enumerator.Current == Enumerator.Current;
                }
            }

            enumerator.Reset();
            for (int i = 0; i < index - 1; i++)
            {
                enumerator.MoveNext();
            }

            return  new State( enumerator, (Storage)Storage.Clone());
        }

        public void SetState(State state)
        {
            Enumerator = state.Enumerator;
            Storage = state.Storage;
        }
    }
}
