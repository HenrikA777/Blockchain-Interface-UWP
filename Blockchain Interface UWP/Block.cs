using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain_Interface_UWP
{
    class Block
    {
        public Block()
        {
            
        }
        public int index { get; set; }
        public string timestamp { get; set; }
        public List<Message> transactions { get; set; }
        public long proof { get; set; }
        public string previous_hash { get; set; }
    }
}
