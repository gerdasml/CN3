using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN3
{
    class Graph
    {
        private HashSet<Node> _nodes = new HashSet<Node>();

        public IEnumerable<Node> Nodes
        {
            get
            {
                foreach (var node in _nodes)
                    yield return node;
            }
        }
        public bool AddNode(string name)
        {
            Node node = new Node(name);
            return _nodes.Add(node);
        }

        public bool RemoveNode(string name)
        {
            var node = _nodes.Where(x => x.Name == name).FirstOrDefault();
            if (node == null)
                return false;
            foreach (var neigh in node.Neighbours.ToList())
                RemoveEdge(name, neigh.Name);
            return _nodes.Remove(new Node(name));
            //uzupdate'int lenteles dar reikes
        }

        public bool AddEdge(string name1, string name2, int weight)
        {
            if(_nodes.Contains(new Node(name1)) && _nodes.Contains(new Node(name2)))
            {
                var node1 = _nodes.Where(x => x.Name == name1).FirstOrDefault();
                var node2 = _nodes.Where(x => x.Name == name2).FirstOrDefault();
                node1.AddNeighbour(node2, weight);
                node2.AddNeighbour(node1, weight);
                return true;
            }
            return false;
        }

        public bool RemoveEdge(string name1, string name2)
        {
            if(_nodes.Contains(new Node(name1)) && _nodes.Contains(new Node(name2)))
            {
                var node1 = _nodes.Where(x => x.Name == name1).FirstOrDefault();
                var node2 = _nodes.Where(x => x.Name == name2).FirstOrDefault();
                return (node1.RemoveNeighbour(node2) & node2.RemoveNeighbour(node1));
            }
            return false;
        }

        public bool EditEdge(string name1, string name2, int weight)
        {
            if (_nodes.Contains(new Node(name1)) && _nodes.Contains(new Node(name2)))
            {
                var node1 = _nodes.Where(x => x.Name == name1).FirstOrDefault();
                var node2 = _nodes.Where(x => x.Name == name2).FirstOrDefault();
                if(node1.NeighbourExists(node2) && node2.NeighbourExists(node1))
                {
                    RemoveEdge(name1, name2);
                    node1.AddNeighbour(node2, weight);
                    node2.AddNeighbour(node1, weight);
                    return true;
                }
            }
            return false;
        }

        public void Update()
        {
            foreach (var item in _nodes)
            {
                item.Update();
            }
        }

        public List<Node> GetShortestPath(string startName, string endName)
        {
            List<Node> list = new List<Node>();
            var start = _nodes.Where(x => x.Name == startName).FirstOrDefault();
            var end = _nodes.Where(x => x.Name == endName).FirstOrDefault();
            if (start == null || end == null) return null;
            list.Add(start);
            var current = start;
            while (current != end)
            {
                var smth = current.Viktor.Rows.Where(x => x.Destination == end).FirstOrDefault();
                if (smth == null) return null;
                if (list.Contains(smth.Hop)) return null;
                list.Add(smth.Hop);
                current = smth.Hop;
            }
            return list;
        }
    }
}
