using System;
using System.Collections.Generic;

namespace DfsBfs
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker boss = new Worker("Rishat");
            Worker wPetya = new Worker("Petya");//1
            Worker wAlisa = new Worker("Alisa");//2
            Worker wIvan = new Worker("Ivan");//3
            Worker wNikolay = new Worker("Nikolay");//4
            Worker wGrisha = new Worker("Grisha");//5
            Worker wIrina = new Worker("Irina");//6
            Worker wTimur = new Worker("Timur");//7
            Worker wAnton = new Worker("Anton");//8

            boss.AddSubordinates(wPetya);//1
            boss.AddSubordinates(wAlisa);//2

            wPetya.AddSubordinates(wIvan);//3
            wPetya.AddSubordinates(wNikolay);//4

            wAlisa.AddSubordinates(wGrisha);//5

            wGrisha.AddSubordinates(wIrina);//6
            wGrisha.AddSubordinates(wTimur);//7
            wGrisha.AddSubordinates(wAnton);//8

            Company ikea = new Company(boss);

            AbstractIterator dfs = ikea.GetDFSIterator();
            AbstractIterator bfs = ikea.GetBFSIterator();
            List<AbstractIterator> iterators = new List<AbstractIterator> {dfs, bfs};

            string wantedName = "Nikolay";
            foreach (AbstractIterator iterator in iterators)
            {
                Console.WriteLine("Iterator : " + iterator.GetName());
                bool success = false;

                while (iterator.HasMore())
                {
                    Worker w = iterator.GetNext();
                    Console.WriteLine("now we are on " + w.name);
                    if (w.name == wantedName)
                    {
                        Console.WriteLine("We have found worker with name " + wantedName);
                        success = true;
                        break;
                    }
                }
                if (success == false)
                {
                    Console.WriteLine("There is no worker with name " + wantedName);
                }
            }
            Console.ReadKey();
        }
    }
    public class Worker
    {
        public string name;
        public List<Worker> subordinates; 

        public Worker(string n)
        {
            this.name = n;
            this.subordinates = new List<Worker>();
        }

        public void AddSubordinates(Worker w)
        {
            subordinates.Add(w);
        }
    }
    public class Company
    {
        public Worker boss;
        public Company(Worker b)
        {
            this.boss = b;
        }
        public AbstractIterator GetDFSIterator()
        {
            DFSIterator dFS = new DFSIterator(this);
            return dFS;
        }
        public AbstractIterator GetBFSIterator()
        {
            BFSIterator bFS = new BFSIterator(this);
            return bFS;
        }
    }
    public abstract class AbstractIterator
    {
        public AbstractIterator(Company company) { }
        public abstract Worker GetNext();
        public abstract bool HasMore();
        public abstract string GetName();
    }
    public class DFSIterator : AbstractIterator
    {
        Stack<Worker> stack;
        public DFSIterator(Company company) : base(company)
        {
            this.stack = new Stack<Worker>();
            this.stack.Push(company.boss);
        }
        public override Worker GetNext()
        {
            Worker temp_worker = stack.Pop();
            foreach (Worker worker in temp_worker.subordinates)
            {
                stack.Push(worker);
            }
            return temp_worker;
        }
        public override bool HasMore()
        {
            return (stack.Count > 0);
        }
        public override string GetName()
        {
            return "DFS Iterator";
        }
    }
    public class BFSIterator : AbstractIterator
    {
        Queue<Worker> queue;
        public BFSIterator(Company company) : base(company)
        {
            this.queue = new Queue<Worker>();
            this.queue.Enqueue(company.boss);
        }
        public override Worker GetNext()
        {
            Worker temp_worker = queue.Dequeue();
            foreach (Worker worker in temp_worker.subordinates)
            {
                queue.Enqueue(worker);
            }
            return temp_worker;
        }
        public override bool HasMore()
        {
            return (queue.Count > 0);
        }
        public override string GetName()
        {
            return "BFS Iterator";
        }
    }
}


