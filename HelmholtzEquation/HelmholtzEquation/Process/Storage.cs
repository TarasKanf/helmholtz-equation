using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.Process
{
    public class Storage : ICloneable
    {
        public Storage()
        {
            Data = new Dictionary<string, object>();
        }

        public Storage(Dictionary<string, object> _data)
        {
            Data = _data;
        }

        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

        public object Clone()
        {
            Dictionary<string, object> clonedData = new Dictionary<string, object>();
            foreach (var keyValuepair in Data)
            {
                clonedData.Add(keyValuepair.Key, keyValuepair.Value);
            }

            return new Storage(clonedData);
        }
    }
}
