using System;
using System.Collections.Generic;
using System.Linq;


public class Program
{
    static void Main(string[] args)
    {
        Print print = new Print();
        print.PrintWork();
    }

    class Print
    {
        public void PrintWork()
        {
            var numberString = Console.ReadLine().Trim();
            bool result = ulong.TryParse(numberString, out ulong number);
            if (result)
            {
                Console.WriteLine(GetNextNumber(number));
            }
            else Console.WriteLine("введите валидное число");
        }




        ulong GetNextNumber(ulong number)
        {
            var word = number.ToString();
            var combinations = new List<ulong>();

            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (i < j)
                        combinations.Add(ulong.Parse(word[i] + word.Substring(j) + word.Substring(0, i) + word.Substring(i + 1, j - (i + 1))));
                    else if (i > j)
                    {
                        if (i == word.Length - 1)
                            combinations.Add(ulong.Parse(word[i] + word.Substring(0, i)));
                        else combinations.Add(ulong.Parse(word[i] + word.Substring(0, i) + word.Substring(i + 1)));
                    }
                }
            }
            combinations = combinations.Distinct().OrderBy(x => x).ToList();
            var index = combinations.IndexOf(number);
            return index == combinations.Count - 1 ? combinations[index] : combinations[index + 1];
        }
    }






}
