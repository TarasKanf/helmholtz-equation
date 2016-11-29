using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.ChainLinks
{
    public interface IChainLink
    {
        Dictionary<string, object> Storage { get; set; }

        void Execute(Dictionary<string, object> storage);
    }
}
