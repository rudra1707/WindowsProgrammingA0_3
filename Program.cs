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
