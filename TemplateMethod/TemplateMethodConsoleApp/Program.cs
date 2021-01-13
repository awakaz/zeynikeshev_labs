using System;

namespace TemplateMethodConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Приготовим кашу:");
            MakeFood porridge = new MakePorridge();
            porridge.Make();
           
            Console.WriteLine("\n Приготовим суп:");
            MakeFood soup = new MakeSoup();
            soup.Make();
           
            Console.WriteLine("\n Приготовим блины:");
            MakeFood pancakes = new MakePancakes();
            pancakes.Make();

            Console.ReadKey();
        }
    }
    abstract class MakeFood
    {
        public void Make()
        {
            PrepareIngredients();
            Start();
            Process();
            Eat();
        }
        public abstract void PrepareIngredients();
        public abstract void Start();
        public virtual void Process()
        {
            Console.WriteLine("Варить до готовности, периодически помешивая.");
        }
        public virtual void Eat()
        {
            Console.WriteLine("Приятного аппетита!");
        }
    }
    class MakePorridge : MakeFood
    {
        public override void PrepareIngredients()
        {
            Console.WriteLine("Приготовить 1 стакан молока, 0,5 стакана хлопьев.");
        }

        public override void Start()
        {
            Console.WriteLine("Налить молоко в кастрюлю, добавить хлопья, поставить на огонь.");
        }
    }
    class MakeSoup : MakeFood
    {
        public override void PrepareIngredients()
        {
            Console.WriteLine("Нарезать кусочками 2 картошки, 1 лук, 1 морковь, 300 г курицы.");
        }

        public override void Start()
        {
            Console.WriteLine("Вскипятить воду, положить ингредиенты в кастрюлю, посолить.");
        }
    }
    class MakePancakes : MakeFood
    {
        public override void PrepareIngredients()
        {
            Console.WriteLine("Взять 1 яйцо, 1,5 стакана молока, 1 стакан муки, 0,5 стакана сахара.");
        }

        public override void Start()
        {
            Console.WriteLine("Взбить яйцо, влить молоко, всыпать муку и перемешать до однородной массы.");
        }

        public override void Process()
        {
            Console.WriteLine("Подогреть сковороду, вылить тесто, жарить с обеих сторон до румяного цвета.");
        }
    }
}
