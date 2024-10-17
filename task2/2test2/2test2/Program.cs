using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Необходимо указать два аргумента: путь к файлу с окружностью и путь к файлу с точками.");
            return;
        }

        string circleFilePath = args[0];
        string pointsFilePath = args[1];

        double centerX, centerY, radius;

        try
        {
            var circleData = File.ReadAllLines(circleFilePath);
            centerX = double.Parse(circleData[0].Split()[0]);
            centerY = double.Parse(circleData[0].Split()[1]);
            radius = double.Parse(circleData[1]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла окружности: {ex.Message}");
            return;
        }

        try
        {
            var pointsData = File.ReadAllLines(pointsFilePath);
            foreach (var line in pointsData)
            {
                var coordinates = line.Split();
                double pointX = double.Parse(coordinates[0]);
                double pointY = double.Parse(coordinates[1]);

                double distanceSquared = (pointX - centerX) * (pointX - centerX) + (pointY - centerY) * (pointY - centerY);
                double radiusSquared = radius * radius;

                if (distanceSquared < radiusSquared)
                {
                    Console.WriteLine(1);
                }
                else if (distanceSquared == radiusSquared)
                {
                    Console.WriteLine(0);
                }
                else
                {
                    Console.WriteLine(2);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла точек: {ex.Message}");
        }
    }
}