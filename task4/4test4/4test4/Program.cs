using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Необходимо указать путь к файлу с числами.");
            return;
        }

        string filePath = args[0];

        try
        {

            var nums = File.ReadAllLines(filePath)
                           .Select(int.Parse)
                           .ToArray();


            int minMoves = CalculateMinMoves(nums);
            Console.WriteLine(minMoves);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    static int CalculateMinMoves(int[] nums)
    {

        Array.Sort(nums);
        int median = nums[nums.Length / 2];


        int moves = nums.Sum(num => Math.Abs(num - median));
        return moves;
    }
}