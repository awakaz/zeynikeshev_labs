using System;
using System.Collections.Generic;

namespace InversionOfControlConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int totalGames = 38;
            List<Match> games = new List<Match>();

            MatchStatistic statsPasses = new Passes("Пассы");
            MatchStatistic statsVictory = new Victory("Победы");
            SeasonStatistic statsSeason = new Sum();
            Calculator statistic = new Calculator();
            for (int i = 0; i < totalGames; i++)
            {
                int passes = random.Next(432, 600);
                int goals = random.Next(0, 7);
                int goalsAgainst = random.Next(0, 7);
                games.Add(new Match(passes, goals, goalsAgainst));
                //Console.Write(games[i].goals + " ");
            }
            //Console.WriteLine();
            int result = statistic.Calculate(games, statsPasses, statsSeason);
            int result1 = statistic.Calculate(games, statsVictory, statsSeason);
            Console.WriteLine("Общее количество пассов за сезон: " + result);
            Console.WriteLine("Количество побед за сезон: " + result1);
            Console.ReadKey();
        }
    }
    public class Match
    {
        public int passes;
        public int goals;
        public int goalsAgainst;
        public Match(int passes, int goals, int goalsAgainst)
        {
            this.passes = passes;
            this.goals = goals;
            this.goalsAgainst = goalsAgainst;
        }
    }
    public abstract class MatchStatistic
    {
        public string name;
        public MatchStatistic(string n)
        {
            name = n;
        }
        public abstract int F(Match match);
    }
    public class Passes : MatchStatistic
    {
        public Passes(string n) : base(n) { }
        public override int F(Match match)
        {
            return match.passes;
        }
    }
    public class Victory : MatchStatistic
    {
        public Victory(string n) : base(n) { }
        public override int F(Match match)
        {
            if (match.goals > match.goalsAgainst)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
    public abstract class SeasonStatistic
    {
        public abstract int G(List<int> gameResults);
    }
    public class Sum : SeasonStatistic
    {
        public override int G(List<int> gameResults)
        {
            int sum = 0;
            for(int i = 0; i < gameResults.Count; i++)
            {
                sum = sum + gameResults[i];
            }
            return sum;
        }
    }
    public class Calculator
    {
        public int Calculate(List<Match> matches, MatchStatistic matchStatistic, SeasonStatistic seasonStatistic)
        {
            List<int> results  = new List<int>();
            Console.WriteLine("{0} за каждый матч в сезоне", matchStatistic.name);
            for (int i = 0; i < matches.Count; i++)
            {
                results.Add(matchStatistic.F(matches[i]));
                Console.Write(matchStatistic.F(matches[i]) + " ");
            }
            Console.WriteLine();
            return seasonStatistic.G(results);
        }
    }
}
