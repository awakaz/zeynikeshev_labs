using System;
using System.Collections.Generic;

namespace BuilderConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractMovieFactory marvelFactory = new MarvelMovieFactory();
            AbstractMovieFactory dcFactory = new DCMovieFactory();
            Movie marvelMovie = marvelFactory.MakeMovie();
            marvelMovie.PrintDescription();
            Console.WriteLine();

            Movie DCMovie = dcFactory.MakeMovie();
            DCMovie.PrintDescription();
            Console.ReadKey();
        }
    }
    public class Movie
    {
        public string name;
        public string studio;
        public List<string> heroes;
        public int year;
        
        public Movie()
        {
            heroes = new List<string>();
        }
        public void PrintDescription()
        {
            Console.WriteLine("Movie: " + name);
            Console.WriteLine("Studio: " + studio);
            Console.WriteLine("Heroes: " + String.Join(", ",heroes));
            Console.WriteLine("Year: " + year);
        }
    }
    abstract public class AbstractMovieFactory
    {
        abstract public Movie MakeMovie();
    }
    public class MarvelMovieFactory : AbstractMovieFactory
    {
        public override Movie MakeMovie()
        {
            Builder builder = new MarvelBuilder();
            builder.SetStudio();
            builder.SetName();
            builder.SetHeroes();
            builder.SetYear();
            return builder.GetMovie();
        }
    }
    public class DCMovieFactory : AbstractMovieFactory
    {
        public override Movie MakeMovie()
        {
            Builder builder = new DCBuilder();
            builder.SetStudio();
            builder.SetName();
            builder.SetHeroes();
            builder.SetYear();
            return builder.GetMovie();   
        }
    }
    abstract public class Builder
    {
        protected Movie movie;
        protected Random random;
        public Builder() 
        {
            movie = new Movie();
            random = new Random();
        }
        abstract public void SetName();
        abstract public void SetStudio();
        abstract public void SetHeroes();
        abstract public void SetYear();
        public Movie GetMovie()
        {
            return movie;
        }
    }
    public class MarvelBuilder : Builder
    {
        public MarvelBuilder() : base() { } 
        public override void SetHeroes()
        {
            List<string> marvelHeroes = new List<string> { "Iron Man", "Hulk", "Captain America", "Thor", "Ant-Man", "Black Widow", "Hawkeye", "Black Panther" };
            this.movie.heroes.Add(marvelHeroes[random.Next(0, 8)]);
            this.movie.heroes.Add(marvelHeroes[random.Next(0, 8)]);
        }
        public override void SetName()
        {
            List<string> nameOfMovies = new List<string> { "Iron Man", "Avengers", "Captain America: Civil War", "Incredible Hulk", "Thor: Ragnarok", "Black Widow", "Black Panther" };
            this.movie.name = nameOfMovies[random.Next(0, 7)];
        }
        public override void SetStudio()
        {
            this.movie.studio = "Marvel";
        }
        public override void SetYear()
        {
            this.movie.year = random.Next(2008, 2020);
        }
    }
    public class DCBuilder : Builder
    {
        public DCBuilder() : base() { }
        public override void SetHeroes()
        {
            List<string> DCHeroes = new List<string> { "Batman", "Wonder Women", "Joker", "Superman", "Flash", "Aquaman"};
            this.movie.heroes.Add(DCHeroes[random.Next(0, 6)]);
            this.movie.heroes.Add(DCHeroes[random.Next(0, 6)]);
        }
        public override void SetName()
        {
            List<string> nameOfMovies = new List<string> { "Batman", "Justice League", "Flash", "Aquaman", "Man of Steel", "Dawn of Justice"};
            movie.name = nameOfMovies[random.Next(0, 6)];
        }
        public override void SetStudio()
        {
            this.movie.studio = "DC";
        }
        public override void SetYear()
        {
            this.movie.year = random.Next(2014, 2020);
        }
    }  
}
