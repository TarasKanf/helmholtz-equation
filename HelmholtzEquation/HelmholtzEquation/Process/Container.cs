using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelmholtzEquation.ChainLinks;

namespace HelmholtzEquation.Process
{
    public class Container : IEnumerable<IChainLink>
    {
        private IEnumerable<IChainLink> ChainLinks { get; set; }

        public Container()
        {
            ChainLinks = new IChainLink[]
            {
                // TODO step names !!!
                new ChainLink1("chain link 1"),
                new ChainLink2("chain link 2"),
                new ChainLink3("chain link 3")
            };
        }

        public Container(IEnumerable<IChainLink> _chainLinks)
        {
            ChainLinks = _chainLinks;
        }

        public IEnumerator<IChainLink> GetEnumerator()
        {
            return ChainLinks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ChainLinks.GetEnumerator();
        }
    }
}
