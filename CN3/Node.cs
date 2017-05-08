using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN3
{
    class Node : IEquatable<Node>
    {
        public string Name { get; private set; }
        private Dictionary<Node, int> _neighbours = new Dictionary<Node, int>();
        private Dictionary<Node, DistanceViktor> _viktors = new Dictionary<Node, DistanceViktor>();
        public DistanceViktor Viktor { get; private set; }
        public Node(string name)
        {
            Name = name;
        }

        public void AddNeighbour(Node node, int weight)
        {
            if (!_neighbours.ContainsKey(node))
                _neighbours.Add(node, weight);
            else _neighbours[node] = weight;
        }

        public void RemoveNeighbour(Node node)
        {
            if (!_neighbours.ContainsKey(node)) return;
            _neighbours.Remove(node);
        }

        public bool NeighbourExists(Node node)
        {
            return _neighbours.ContainsKey(node);
        }

        public void Update()
        {

        }
        #region Equals
        public bool Equals(Node other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        #endregion
    }
}
