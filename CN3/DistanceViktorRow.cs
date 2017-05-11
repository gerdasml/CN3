using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN3
{
    class DistanceViktorRow
    {
        public Node Destination { get; private set; }
        public int Distance { get; private set; }
        public Node Hop { get; private set; }

        public DistanceViktorRow(Node destination, int distance, Node hop)
        {
            Destination = destination;
            Distance = distance;
            Hop = hop;
        }
    }
}
