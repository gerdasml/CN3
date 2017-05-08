using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN3
{
    class Graph
    {
        private List<Node> _nodes = new List<Node>();
        public void AddNode(string name)
        {
            Node node = new Node(name);
            _nodes.Add(node);
        }

        public void RemoveNode(string name)
        {
            _nodes.RemoveAll(x => x.Name == name);
            //uzupdate'int lenteles dar reikes
        }

        public void AddEdge(string name1, string name2, int weight)
        {
            if(_nodes.Exists(x => x.Name == name1) && _nodes.Exists(x => x.Name == name2))
            {
                var node1 = _nodes.Where(x => x.Name == name1).FirstOrDefault();
                var node2 = _nodes.Where(x => x.Name == name2).FirstOrDefault();
                node1.AddNeighbour(node2, weight);
                node2.AddNeighbour(node1, weight);
                return;
            }
            throw new Exception("At least one of the specifiend nodes does not exist.");
        }

        public void RemoveEdge(string name1, string name2)
        {
            if(_nodes.Exists(x => x.Name == name1) && _nodes.Exists(x => x.Name == name2))
            {
                var node1 = _nodes.Where(x => x.Name == name1).FirstOrDefault();
                var node2 = _nodes.Where(x => x.Name == name2).FirstOrDefault();
                node1.RemoveNeighbour(node2);
                node2.RemoveNeighbour(node1);
                return;
            }
            throw new Exception("At least one of the specified nodes does not exist.");
        }

        public void EditEdge(string name1, string name2, int weight)
        {
            if (_nodes.Exists(x => x.Name == name1) && _nodes.Exists(x => x.Name == name2))
            {
                var node1 = _nodes.Where(x => x.Name == name1).FirstOrDefault();
                var node2 = _nodes.Where(x => x.Name == name2).FirstOrDefault();
                if(node1.NeighbourExists(node2) && node2.NeighbourExists(node1))
                {
                    node1.AddNeighbour(node2, weight);
                    node2.AddNeighbour(node1, weight);
                    return;
                }
                throw new Exception("Edge does not exist.");
            }
            throw new Exception("At least one of the specified nodes does not exist.");
        }

        public void Update()
        {
            foreach (var item in _nodes)
            {
                item.Update();
            }
        }
    }
}
