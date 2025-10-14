using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab4
{
    interface ITextFilter
    {
        string Apply(string input);
        int GetChangesCount();
    }

    abstract class TextFilterBase : ITextFilter
    {
        protected int changesCount = 0;

        public abstract string Apply(string input);

        public int GetChangesCount()
        {
            return changesCount;
        }
    }

    class SpaceRemoverFilter : TextFilterBase
    {
        public override string Apply(string input)
        {
            int before = input.Length;
            string result = input.Replace(" ", "");
            changesCount = before - result.Length;
            return result;
        }
    }

    class BannedWordsFilter : TextFilterBase
    {
        private List<string> bannedWords;

        public BannedWordsFilter(List<string> bannedWords)
        {
            this.bannedWords = bannedWords;
        }

        public override string Apply(string input)
        {
            string result = input;
            foreach (var word in bannedWords)
            {
                if (result.Contains(word))
                {
                    result = result.Replace(word, new string('*', word.Length));
                    changesCount++;
                }
            }
            return result;
        }
    }

    class TextProcessor
    {
        private List<ITextFilter> filters = new List<ITextFilter>();

        public void AddFilter(ITextFilter filter)
        {
            filters.Add(filter);
        }

        public string ProcessText(string input)
        {
            string result = input;
            foreach (var filter in filters)
            {
                result = filter.Apply(result);
            }
            return result;
        }

        public int TotalChanges()
        {
            int total = 0;
            foreach (var filter in filters)
            {
                total += filter.GetChangesCount();
            }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Фільтр рядків");

            string text = "Це тестовий рядок із забороненим словом дурень та зайвими пробілами.";
            Console.WriteLine($"Початковий текст:\n{text}");

            var spaceRemover = new SpaceRemoverFilter();
            var bannedFilter = new BannedWordsFilter(new List<string> { "дурень" });

            var processor = new TextProcessor();
            processor.AddFilter(bannedFilter);
            processor.AddFilter(spaceRemover);

            string result = processor.ProcessText(text);

            Console.WriteLine($"\nРезультат після фільтрації:\n{result}");
            Console.WriteLine($"\nЗагальна кількість змін: {processor.TotalChanges()}");
        }
    }
}
