using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watershed2
{
    
    class Node
    {
        Node N;
        Node NE;
        Node E;
        Node SE;
        Node S;
        Node SW;
        Node W;
        Node NW;
        static Node dummy;
        public Node sourcePixel;
        int val;
        public bool ignore;
        public bool wshed;
        public bool mask;
        public bool local_max;
        public bool local_min;
        Node[] listNeighbour = new Node[8];
        public Node(bool dummy)
        {
            val = -1;
        }
        public Node(int inpVal,
            Node inpN = null,
            Node inpNE = null,
            Node inpE = null,
            Node inpSE = null,
            Node inpS = null,
            Node inpSW = null,
            Node inpW = null,
            Node inpNW = null)
        {
            val = inpVal;
            if (dummy is null)
            {
                dummy = new Node(true);
            }
        }
        public Node climbHighest()
        {
            int hVal = this.val;
            Node resultus = this;
            for (int i = 0; i < listNeighbour.Length; i++)
            {
                if ((!(listNeighbour[i] is null)) && (!(listNeighbour[i].Equals(Node.dummy))) && listNeighbour[i].val > hVal /*&& (!ignore)*/)
                {
                    hVal = listNeighbour[i].val;
                    resultus = listNeighbour[i];
                }
            }
            return resultus;
        }
        public Node callHighest()
        {
            int hVal = 0;
            Node resultus = null;
            for (int i = 0; i < listNeighbour.Length; i++)
            {
                if (listNeighbour[i].val > hVal && (!ignore))
                {
                    hVal = listNeighbour[i].val;
                    resultus = listNeighbour[i];
                }
            }
            return resultus;
        }
        public Node callLowest()
        {
            int point = val;
            Node resultus = this;
            for (int i = 0; i < listNeighbour.Length; i++)
            {
                if ((!(listNeighbour[i] is null)) && (!(listNeighbour[i].Equals(Node.dummy))) && listNeighbour[i].val < point)
                {
                    point = listNeighbour[i].val;
                    resultus = listNeighbour[i];
                }
            }
            return resultus;
        }
        public int callVal()
        {
            return val;
        }
        public Node callNeighbour(String target)
        {
            Node resultus;
            if (target == "N")
            {
                resultus = N;
            }
            else if (target == "NE")
            {
                resultus = NE;
            }
            else if (target == "E")
            {
                resultus = E;
            }
            else if (target == "SE")
            {
                resultus = SE;
            }
            else if (target == "S")
            {
                resultus = S;
            }
            else if (target == "SW")
            {
                resultus = SW;
            }
            else if (target == "W")
            {
                resultus = W;
            }
            else if (target == "NW")
            {
                resultus = NW;
            }
            else
            {
                resultus = dummy;
            }
            if (resultus is null)
            {
                return dummy;
            }
            else
            {
                return resultus;
            }
        }
        public void assignNeighbour(String direction, Node target)
        {
            if (direction == "N")
            {
                N = target;
            }
            else if (direction == "NE")
            {
                NE = target;
            }
            else if (direction == "E")
            {
                E = target;
            }
            else if (direction == "SE")
            {
                SE = target;
            }
            else if (direction == "S")
            {
                S = target;
            }
            else if (direction == "SW")
            {
                SW = target;
            }
            else if (direction == "W")
            {
                W = target;
            }
            else if (direction == "NW")
            {
                NW = target;
            }
        }
        public void mark(String opt)
        {
            if (opt.Equals("ignore"))
            {
                ignore = true;
            }
            else if (opt.Equals("wshed"))
            {
                wshed = true;
            }
            else if (opt.Equals("mask"))
            {
                mask = true;
            }
            else if (opt.Equals("local_max"))
            {
                local_max = true;
            }
            else if (opt.Equals("local_min"))
            {
                local_min
 = true;
            }
        }
        public bool isMask()
        {
            return mask;
        }
        public bool isIgnored()
        {
            return ignore;
        }
        public bool isWshed()
        {
            return wshed;
        }
        public bool isMax()
        {
            return local_max;
        }
        public bool isMin()
        {
            return local_min;
        }
        public void updateNeighbour()
        {
            listNeighbour[0] = N;
            listNeighbour[1] = NE;
            listNeighbour[2] = E;
            listNeighbour[3] = SE;
            listNeighbour[4] = S;
            listNeighbour[5] = SW;
            listNeighbour[6] = W;
            listNeighbour[7] = NW;
        }
        public Node nearMask()
        {
            foreach (Node pixel in listNeighbour)
            {
                if (!(pixel is null) && !(pixel.Equals(dummy)) && pixel.mask)
                {
                    return pixel;
                }
            }
            return null;
        }
    }
    class Matrix
    {
        Node[,] matrix;
        Random rndME = new Random();
        List<Node> listMinimaF;
        List<Node> listMaximaF;
        int floor;
        int roof;
        int s;
        int minimal = 99999999;
        public Matrix(int size, int inpFloor, int inpRoof)
        {
            s = size;
            floor = inpFloor;
            roof = inpRoof;
            matrix = new Node[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int num = rndME.Next(inpFloor,inpRoof);
                    //Console.WriteLine(num);
                    matrix[i, j] = new Node(num);
                    if (num < minimal)
                    {
                        minimal = num;
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bool assignN = true;
                    bool assignNE = true;
                    bool assignE = true;
                    bool assignSE = true;
                    bool assignS = true;
                    bool assignSW = true;
                    bool assignW = true;
                    bool assignNW = true;
                    //Console.WriteLine(i);
                    if (i == 0)
                    {
                        assignN = false;
                        assignNE = false;
                        assignNW = false;
                    }
                    else if (i == size - 1)
                    {
                        assignS = false;
                        assignSE = false;
                        assignSW = false;
                    }
                    if (j == 0)
                    {
                        assignW = false;
                        assignNW = false;
                        assignSW = false;
                    }
                    else if (j == size - 1)
                    {
                        assignE = false;
                        assignSE = false;
                        assignNE = false;
                    }


                    if (assignN)
                    {
                        matrix[i, j].assignNeighbour("N", matrix[i - 1, j]);
                    }
                    if (assignNE)
                    {
                        matrix[i, j].assignNeighbour("NE", matrix[i - 1, j + 1]);
                    }
                    if (assignE)
                    {
                        matrix[i, j].assignNeighbour("E", matrix[i, j + 1]);
                    }
                    if (assignSE)
                    {
                        matrix[i, j].assignNeighbour("SE", matrix[i + 1, j + 1]);
                    }
                    if (assignS)
                    {
                        matrix[i, j].assignNeighbour("S", matrix[i + 1, j]);
                    }
                    if (assignSW)
                    {
                        matrix[i, j].assignNeighbour("SW", matrix[i + 1, j - 1]);
                    }
                    if (assignW)
                    {
                        matrix[i, j].assignNeighbour("W", matrix[i, j - 1]);
                    }
                    if (assignNW)
                    {
                        matrix[i, j].assignNeighbour("NW", matrix[i - 1, j - 1]);
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j].updateNeighbour();
                }
            }
        }
        public Matrix(Node[,] inpM)
        {
            //Console.WriteLine(inpM.Length);
            s = Convert.ToInt32(Math.Sqrt(inpM.Length));
            //Console.WriteLine(s);
            matrix = inpM;
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                   //Console.WriteLine(matrix[i, j].callVal());
                    bool assignN = true;
                    bool assignNE = true;
                    bool assignE = true;
                    bool assignSE = true;
                    bool assignS = true;
                    bool assignSW = true;
                    bool assignW = true;
                    bool assignNW = true;
                    //Console.WriteLine(i);
                    if (i == 0)
                    {
                        assignN = false;
                        assignNE = false;
                        assignNW = false;
                    }
                    else if (i == s - 1)
                    {
                        assignS = false;
                        assignSE = false;
                        assignSW = false;
                    }
                    if (j == 0)
                    {
                        assignW = false;
                        assignNW = false;
                        assignSW = false;
                    }
                    else if (j == s - 1)
                    {
                        //Console.WriteLine(matrix[i, j]);
                        assignE = false;
                        assignSE = false;
                        assignNE = false;
                    }


                    if (assignN)
                    {
                        matrix[i, j].assignNeighbour("N", matrix[i - 1, j]);
                    }
                    if (assignNE)
                    {
                        matrix[i, j].assignNeighbour("NE", matrix[i - 1, j + 1]);
                    }
                    if (assignE)
                    {
                        matrix[i, j].assignNeighbour("E", matrix[i, j + 1]);
                    }
                    if (assignSE)
                    {
                        matrix[i, j].assignNeighbour("SE", matrix[i + 1, j + 1]);
                    }
                    if (assignS)
                    {
                        matrix[i, j].assignNeighbour("S", matrix[i + 1, j]);
                    }
                    if (assignSW)
                    {
                        matrix[i, j].assignNeighbour("SW", matrix[i + 1, j - 1]);
                    }
                    if (assignW)
                    {
                        matrix[i, j].assignNeighbour("W", matrix[i, j - 1]);
                    }
                    if (assignNW)
                    {
                        matrix[i, j].assignNeighbour("NW", matrix[i - 1, j - 1]);
                    }
                }
            }
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    matrix[i, j].updateNeighbour();
                }
            }
        }
        public void show()
        {
            //Console.WriteLine(matrix[1, 1].callLowest().callVal());
            Console.WriteLine("Matrix");
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    Console.Write(matrix[i, j].callVal() + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Masked and Watershed");
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    if (matrix[i, j].isWshed())
                    {
                        Console.Write("1 ");
                    }
                    else
                    {
                        if (matrix[i, j].isMask())
                        {
                            Console.Write("0 ");
                        }
                        else
                        {
                            Console.Write("_ ");
                        }
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Local Minima and Maxima");
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    if (matrix[i, j].local_min)
                    {
                        Console.Write("0 ");
                    }
                    else
                    {
                        if (matrix[i, j].local_max)
                        {
                            Console.Write("1 ");
                        }
                        else
                        {
                            Console.Write("_ ");
                        }
                    }
                }
                Console.WriteLine("");
            }
        }
        public Node conDescend(Node pixel)
        {
            Node nextPixel = pixel.callLowest();
            Node resultus;
            if (nextPixel == pixel)
            {
                resultus = pixel;
            }
            else
            {
                resultus = conDescend(nextPixel);
            }
            return resultus;
        }
        public List<Node> markLocalMinima()
        {
            List<Node> listMinima = new List<Node>();
            Node target;
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    target = conDescend(matrix[i, j]);
                    target.local_min = true;
                    listMinima.Add(target);
                }
            }
            listMinimaF = listMinima;
            return listMinima;
        }
        public static Node[,] generate(int size)
        {
            Node[,] resultus = new Node[size, size]; 
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Row number: " + i);
                String[] inp = Console.ReadLine().Split(' ');
                for (int j = 0; j < size; j++)
                {
                    //Console.WriteLine(inp[j]);
                    resultus[i, j] = new Node(Convert.ToInt16(inp[j]));
                    //Console.WriteLine(resultus[i, j].callVal());
                }
            }
            return resultus;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Matrix M = new Matrix(25, 0, 101);
            List<Node> listMinima = M.markLocalMinima();
            //M.ws2();
            M.show();
            //Node[,] V = Matrix.generate(3);
            //Matrix W = new Matrix(V);
            //W.show();
            Console.ReadKey();
        }
    }
}
