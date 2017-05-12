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

        public IEnumerable<Node> Neighbours
        {
            get
            {
                return _neighbours.Keys;
            }
        }

        public Node(string name)
        {
            Name = name;
            Viktor = new DistanceViktor(this);
        }

        public void AddNeighbour(Node node, int weight)
        {
            if (!_neighbours.ContainsKey(node))
            {
                _neighbours.Add(node, weight);
                _viktors.Add(node, node.Viktor);
            }
            else _neighbours[node] = Math.Min(weight, _neighbours[node]);
        }

        public bool RemoveNeighbour(Node node)
        {
            return _neighbours.Remove(node) & _viktors.Remove(node);
        }

        public bool NeighbourExists(Node node)
        {
            return _neighbours.ContainsKey(node);
        }

        public void Update()
        {
            var viktor = new DistanceViktor(this);
            foreach (var neighbour in _neighbours.Keys)
            {
                var nv = _viktors[neighbour];
                foreach (var row in nv.Rows)
                {
                    viktor.AddRow(row.Destination, row.Distance + _neighbours[neighbour], neighbour);
                }
            }
            Viktor = viktor;
            foreach (var neighbour in _neighbours.Keys)
            {
                neighbour.SetDistanceViktor(this, Viktor);
            }
        }

        public void SetDistanceViktor(Node node, DistanceViktor dv)
        {
            if (!_viktors.ContainsKey(node))
            {
                _viktors.Add(node, dv);
            }
            _viktors[node] = dv;
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
