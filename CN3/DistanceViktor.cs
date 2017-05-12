using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN3
{
    class DistanceViktor
    {
        private const int MaxDistance = 5000;
        private Dictionary<Node, int> _distances = new Dictionary<Node, int>();
        private Dictionary<Node, Node> _hopes = new Dictionary<Node, Node>();
        public IEnumerable<DistanceViktorRow> Rows
        {
            get
            {
                foreach (var item in _distances.Keys)
                {
                    yield return new DistanceViktorRow(item, _distances[item], _hopes[item]);
                }
            }
        }
        public DistanceViktor(Node source)
        {
            _distances.Add(source, 0);
            _hopes.Add(source, source);
        }
        public void AddRow(Node destination, int distance, Node hop)
        {
            if (distance > MaxDistance)
                return;
            if (!(_distances.ContainsKey(destination) && _hopes.ContainsKey(destination)))
            {
                _distances.Add(destination, distance);
                _hopes.Add(destination, hop);
            }
            if (_distances[destination] > distance)
            {
                _distances[destination] = distance;
                _hopes[destination] = hop;
            }
        }
    }
}
