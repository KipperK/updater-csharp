using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordInvasionUpdater
{
    class HashTable
    {
        public List<HashNode> files { get; set; }
        public string host { get; set; }
        public string version { get; set; }
    }

    class HashNode
    {
        public string name { get; set; }
        public string path { get; set; }
        public string hash { get; set; }
        public List<HashNode> children { get; set; }
    }
}
