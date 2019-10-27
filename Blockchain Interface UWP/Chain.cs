using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain_Interface_UWP
{
    class Chain
    {
        public Chain()
        {
            
        }

        public List<Block> chain { get; set; }
        public int length { get; set; }
    }
}
