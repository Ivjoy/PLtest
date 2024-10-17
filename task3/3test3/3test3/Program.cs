using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;

class Task3
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Not enough data");
            return;
        }

        string testsFilePath = args[0];
        string valuesFilePath = args[1];

        if (!File.Exists(testsFilePath) || !File.Exists(valuesFilePath))
        {
            Console.WriteLine("One of the specified files does not exist");
            return;
        }

        try
        {
            var testsJson = JObject.Parse(File.ReadAllText(testsFilePath));
            var valuesJson = JObject.Parse(File.ReadAllText(valuesFilePath));

            var testResults = new Dictionary<string, string>();
            var valuesArray = (JArray)valuesJson["values"];

            foreach (var value in valuesArray)
            {
                var id = value["id"].ToString();
                var result = value["value"].ToString();
                testResults[id] = result;
            }

            UpdateTestValues(testsJson, testResults);

            File.WriteAllText("report.json", testsJson.ToString(Newtonsoft.Json.Formatting.Indented));

            Console.WriteLine("Report generated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }

    private static void UpdateTestValues(JObject testObj, Dictionary<string, string> testResults)
    {
        var queue = new Queue<JObject>();
        var jsonArray = (JArray)testObj["tests"];

        foreach (var jsonElement in jsonArray)
        {
            if (jsonElement is JObject jsonObject)
            {
                queue.Enqueue(jsonObject);
            }
        }

        while (queue.Count > 0)
        {
            var jsonObject = queue.Dequeue();
            var idKey = jsonObject["id"].ToString();

            if (testResults.ContainsKey(idKey))
            {
                jsonObject["value"] = testResults[idKey];
            }

            if (jsonObject["values"] != null)
            {
                var values = (JArray)jsonObject["values"];
                foreach (var jsonElement in values)
                {
                    if (jsonElement is JObject childJsonObject)
                    {
                        queue.Enqueue(childJsonObject);
                    }
                }
            }
        }
    }
}
