using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Routing
    {
        public List<int> W;
        public List<int> V = new List<int>();
        public int V_size = 18;
        public List<Node> E = new List<Node>();
        public int[] L = new int[18];
        public int[] M = new int[18];
        public int vs = 0;
        public int vq = 18;
        public Dictionary<int, List<int>> Neighbour = new Dictionary<int, List<int>>();
        public int[] Prevoius;
        private void Initialize()
        {
            W = new List<int>();
            L[vq] = 0;
            M[vq] = 1;
            for(var vi = 1; vi< V_size; vi++)
            {
                V.Add(vi);
                L[vi] = 1200394833;
                M[vi] = 0;
                Prevoius[vi] = -1;
            }
        }
        public void FindPath()
        {
            Initialize();
            while ((!W.Contains(vs)) && Neighbour[W.LastOrDefault()].Any())
            {
                int selectedVJ;
                for (int vj = 1; vj < V_size; vj++)
                {
                    bool isLarger = false;
                    V.Remove(vj);
                    foreach (var vk in V)
                    {
                        if (L[vj] > L[vk])
                        {
                            isLarger = true;
                        }
                    }
                    if (!isLarger)
                    {
                        W.Add(vj);
                        selectedVJ = vj;
                        break;
                    }
                    else V.Add(vj);
                }

            }
        }

    }
}
