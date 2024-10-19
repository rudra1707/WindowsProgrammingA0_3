/*
* FILE : A0_3
* PROJECT : PROG2121 - Assignment #3
* PROGRAMMER : RUDRA NITESHKUMAR BHATT
* FIRST VERSION : 2024-10-18
* DESCRIPTION :
* Using randomly generated GUIDs, a console application may be used to assess the performance of a dictionary, 
* list, and string array.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        // Check the quantity of command line parameters supplied is correct.
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: Program <number of elements> <number of test elements>");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        // Parse arguments
        if (!int.TryParse(args[0], out int numElements) || !int.TryParse(args[1], out int numTestElements))
        {
            Console.WriteLine("Invalid arguments. Both arguments must be integers.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        if (numElements < 100 || numElements >= 5000000 || numTestElements < 1 || numTestElements > (numElements / 100))
        {
            Console.WriteLine("Error: Number of elements must be between 100 and 5,000,000, and test elements must be <= 1% of total elements.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }
        // Initialize data structures to store GUID
        var stringArray = new string[numElements];
        var stringList = new List<string>(numElements);
        var stringDictionary = new Dictionary<string, string>(numElements);

        // Create random GUIDs and fill in data structures.
        GenerateRandomGUIDs(numElements, stringArray, stringList, stringDictionary);

        // Generate test data to search
        var (validTestData, invalidTestData) = GenerateTestData(numTestElements, stringArray);

        // Running the performance test
        TestPerformance("Array", stringArray, validTestData, invalidTestData, (data, key) => Array.Find(data, e => e == key));
        TestPerformance("List", stringList, validTestData, invalidTestData, (data, key) => data.BinarySearch(key) >= 0 ? key : null);
        TestPerformance("Dictionary", stringDictionary, validTestData, invalidTestData, (data, key) => data.ContainsKey(key) ? data[key] : null);

        // This Displays command line arguments
        Console.WriteLine($"Command Line Arguments: Elements = {numElements}, Test Elements = {numTestElements}");

        // Waits for the user to press any key to close the console
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    // Method for creating random GUIDs and filling the given data structures
    static void GenerateRandomGUIDs(int numElements, string[] stringArray, List<string> stringList, Dictionary<string, string> stringDictionary)
    {
        for (int i = 0; i < numElements; i++)
        {
            string guid = Guid.NewGuid().ToString();
            stringArray[i] = guid;
            stringList.Add(guid);
            stringDictionary[guid] = guid;
        }
    }

    // Method to provide both valid and false test data for searching
    static (string[], string[]) GenerateTestData(int numTestElements, string[] stringArray)
    {
        Random rand = new Random();
        var validTestData = new string[numTestElements];
        var invalidTestData = new string[numTestElements];

        for (int i = 0; i < numTestElements; i++)
        {
            validTestData[i] = stringArray[rand.Next(0, stringArray.Length)];
            invalidTestData[i] = Guid.NewGuid().ToString(); // Generate new random GUIDs for invalid test data
        }

        return (validTestData, invalidTestData);
    }

    // Method to evaluate different data structures' performance

    static void TestPerformance<T>(
    string structureName,
    T dataStructure,
    string[] validTestData,
    string[] invalidTestData,
    Func<T, string, string> searchFunc)
    {
        // Calculating time for valid searches
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (var key in validTestData)
        {
            searchFunc(dataStructure, key);
        }
        stopwatch.Stop();
        double validTotalTime = stopwatch.Elapsed.TotalMilliseconds;
        double validAverageTime = validTotalTime / validTestData.Length;

        // Calculating time for invalid searches
        stopwatch.Restart();
        foreach (var key in invalidTestData)
        {
            searchFunc(dataStructure, key);
        }
        stopwatch.Stop();
        double invalidTotalTime = stopwatch.Elapsed.TotalMilliseconds;
        double invalidAverageTime = invalidTotalTime / invalidTestData.Length;

        // Displaying results
        Console.WriteLine($"{structureName} - Total time for valid data: {validTotalTime} ms, Average time per search: {validAverageTime} ms");
        Console.WriteLine($"{structureName} - Total time for invalid data: {invalidTotalTime} ms, Average time per search: {invalidAverageTime} ms");
    }
}
