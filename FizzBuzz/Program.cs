using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz2
{
    interface IPlayer
    {
        void Play(int num);
    }

    interface IGameRules
    {
        string MakeTheWord(int num);
    }

    class FizzBuzzRules : IGameRules
    {
        public string MakeTheWord(int num)
        {
            if (num % 15 == 0)
                return "FizzBuzz";

            if (num % 3 == 0)
                return "Fizz";

            if (num % 5 == 0)
                return "Buzz";

            return num.ToString();
        }
    }

    class BumRules : IGameRules
    {
        public string MakeTheWord(int num)
        {
            if (num % 3 == 0)
                return "Bum";

            if (num % 10 == 3)
                return "Bum";

            return num.ToString();
        }
    }

    class Player : IPlayer
    {
        private readonly string _name;
        private readonly IGameRules _rules;
        private readonly IOutputChannel _channel;

        public Player(string name, IGameRules rules, IOutputChannel channel)
        {
            _name = name;
            _rules = rules;
            _channel = channel;
        }

        public void Play(int num)
        {
            var word = _rules.MakeTheWord(num);
            _channel.Say($"{_name}: {word}");
        }
    }

    interface IOutputChannel
    {
        void Say(string word);
    }

    class MyConsole : IOutputChannel
    {
        public void Say(string word)
        {
            Console.WriteLine($"console: {word}");
        }
    }

    class MyFile : IOutputChannel
    {
        public void Say(string word)
        {
            Console.WriteLine($"file: {word}");
        }
    }

    class Program
    {
        static string ChooseOutputMethod()
        {
            return "some";
        }

        static void Main(string[] args)
        {
/*
            var method = ChooseOutputMethod();
            IOutputChannel channel;

            switch (method)
            {
                case "console": 
                    channel = new MyConsole();
                    break;

                case "file":
                    channel = new MyFile();
                    break;

                default:
                    throw new ApplicationException($"Unknown output method {method}");
            }
*/
            var console = new MyConsole();
            var file = new MyFile();

            var fizzBuzzRules = new FizzBuzzRules();
            var bumRules = new BumRules();

            IPlayer[] players = new[] {
                new Player("Sasha", fizzBuzzRules, console),
                new Player("Dasha", bumRules, file),
                new Player("Masha", fizzBuzzRules, console)
            };

            for (int i = 1; i < 100; i++)
            {
                var player = players[i % players.Length];
                player.Play(i);
            }
        }
    }
}
