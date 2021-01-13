using System;

namespace IteratorAndVisitorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] scheme = new string[] { "---T-", "-STT-", "T---T", "--GS-" };
            // ---T-
            // -STT-
            // T---T
            // --GS-
            TreasureField treasure_field = new TreasureField(4, 5, scheme);
            Iterator udIterator = treasure_field.GetUpDownIterator();
            Iterator lrIterator = treasure_field.GetLeftRightIterator();
            Visitor visitor = new Visitor();

            while(udIterator.hasMore() == true || lrIterator.hasMore() == true)
            {
                Cell cell1 = udIterator.getNext();
                Cell cell2 = lrIterator.getNext();
                Console.Write(udIterator.getName() + ":     ");
                bool isTreasure1 = cell1.Accept(visitor);
                Console.Write(lrIterator.getName() + ":  ");
                bool isTreasure2 = cell2.Accept(visitor);
                if(isTreasure1 == true || isTreasure2 == true)
                {
                    Console.WriteLine("We found a treasure and therefore finish the search");
                    break;
                }
            }
            Console.ReadKey();
        }
    }
    class TreasureField
    {
        public int n;
        public int m;
        public Cell[,] map;

        public TreasureField(int nRows, int mColumns, string[] fieldMap)
        {
            n = nRows;
            m = mColumns;
            map = new Cell[n, m];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (fieldMap[i][j] == 'T')
                    {
                        map[i, j] = new CellTree(i, j, random.Next(5, 100)); //random height
                    }
                    if (fieldMap[i][j] == 'G')
                    {
                        map[i, j] = new CellTreasure(i, j, random.Next(10, 20)); //random amount of treasure
                    }
                    if (fieldMap[i][j] == 'S')
                    {
                        map[i, j] = new CellSwamp(i, j, random.Next(2, 15)); // random depth of swamp
                    }
                    if (fieldMap[i][j] == '-')
                    {
                        map[i, j] = new CellEmpty(i, j);
                    }
                }
            }
            
        }
        public UpDownIterator GetUpDownIterator()
        {
            UpDownIterator upDown = new UpDownIterator(this);
            return upDown;
        }
        public LeftRightIterator GetLeftRightIterator()
        {
            LeftRightIterator leftRight = new LeftRightIterator(this);
            return leftRight;
        }
    }
    abstract class Iterator
    {
        public TreasureField field;
        public int x;  //i
        public int y;  //j

        public Iterator(TreasureField f)
        {
            this.field = f;
            x = 0;
            y = 0;
        }
        public bool hasMore()
        {
            return x < field.n && y < field.m;
        }
        abstract public Cell getNext();
        abstract public string getName();
    }
    class UpDownIterator : Iterator
    {
        public UpDownIterator(TreasureField f) : base(f) { }
        public override Cell getNext()
        {
            Cell cell = field.map[x, y];
            if(x < field.n - 1)
            {
                x = x + 1;  
            }
            else
            {
                x = 0;
                y = y + 1;
            }
            return cell;
        }
        public override string getName()
        {
            return "UpDownIterator";
        }
    }

    class LeftRightIterator : Iterator
    {
        public LeftRightIterator(TreasureField f) : base(f) { }
        public override Cell getNext()
        {
            Cell cell = field.map[x, y];
            if (y < field.m - 1)
            {
                y = y + 1;
            }
            else
            {
                y = 0;
                x = x + 1;
            }
            return cell;
        }
        public override string getName()
        {
            return "LeftRightIterator";
        }
    }
    abstract class Cell
    {
        public int a;
        public int b;
        public Cell(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public abstract bool Accept(Visitor visitor);
    }

    class CellTree : Cell
    {
        public int height;

        public CellTree(int a, int b, int height) : base(a, b)
        {
            this.height = height;
        }
        public override bool Accept(Visitor visitor)
        {
            return visitor.VisitCellTree(this);
        }
    }

    class CellEmpty : Cell
    {
        public CellEmpty(int a, int b) : base(a, b) { }
        public override bool Accept(Visitor visitor)
        {
            return visitor.VisitCellEmpty(this);
        }
    }
    class CellTreasure : Cell
    {
        public int amount;

        public CellTreasure(int a, int b, int amount) : base(a, b)
        {
            this.amount = amount;
        }
        public override bool Accept(Visitor visitor)
        {
            return visitor.VisitCellTreasure(this);
        }
    }
    class CellSwamp : Cell
    {
        public int depth;

        public CellSwamp(int a, int b, int depth) : base(a, b)
        {
            this.depth = depth;
        }
        public override bool Accept(Visitor visitor)
        {
            return visitor.VisitCellSwamp(this);
        }
    }
    
    class Visitor
    {
        public bool VisitCellTree(CellTree cell)
        {
            string output = string.Format("Cell ({0},{1}) has a tree of height {2} meters!", cell.a + 1, cell.b + 1, cell.height);
            Console.WriteLine(output);
            return false;
        }
        public bool VisitCellEmpty(CellEmpty cell)
        {
            string output = string.Format("Cell ({0},{1}) is empty!", cell.a + 1, cell.b + 1);
            Console.WriteLine(output);
            return false;
        }
        public bool VisitCellTreasure(CellTreasure cell)
        {
            string output = string.Format("Cell ({0},{1}) has a {2}kg of gold!", cell.a + 1, cell.b + 1, cell.amount);
            Console.WriteLine(output);
            return true;
        }
        public bool VisitCellSwamp(CellSwamp cell)
        {
            string output = string.Format("Cell ({0},{1}) has a swamp of depth {2} meters!", cell.a + 1, cell.b + 1, cell.depth);
            Console.WriteLine(output);
            return false;
        }
    }
}
