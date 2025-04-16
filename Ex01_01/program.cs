using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Ex01_01
{
    internal class program
    {
        static void Main()
        {
            Console.Write("Enter tree height (1 to 15): ");
            int height = int.Parse(Console.ReadLine());
            if (height < 1 || height > 15)
            {
                Console.WriteLine("Invalid height!");
                return;
            }

            char rowLetter = 'A';
            int currentDigit = 1;
            int trunkDigit = 1;

            // Width of the widest number line (the triangle base)
            int triangleRows = height - 2;
            int maxWidth = triangleRows * 2 - 1;

            for (int i = 0; i < height; i++)
            {
                Console.Write($"{rowLetter}   ");

                if (i >= height - 2) // Trunk rows
                {
                    int spacesBefore = (maxWidth - 1) / 2;
                    Console.Write(new string(' ', spacesBefore));
                    Console.WriteLine($"|{trunkDigit}|");
                }
                else
                {
                    int numbersInRow = i * 2 + 1;
                    int spacesBefore = (maxWidth - numbersInRow) / 2;

                    // Special case for row A — move it one step right
                    if (i == 0)
                        spacesBefore += 1;

                    Console.Write(new string(' ', spacesBefore));
                    for (int j = 0; j < numbersInRow; j++)
                    {
                        Console.Write($"{currentDigit}");
                        if (j < numbersInRow - 1)
                            Console.Write(" ");
                        currentDigit = currentDigit % 9 + 1;
                    }
                    Console.WriteLine();

                    trunkDigit = currentDigit; // Save next digit for trunk
                }

                rowLetter++;
            }


            int numOfBinaryNumbers = 4;
            int[] binaryArr, decimalArr;

            binaryArr = requestInputFromUser(numOfBinaryNumbers);
            decimalArr = parseBinaryArrToDecimalArr(numOfBinaryNumbers, binaryArr);
            printDecimalInDescending(decimalArr);
            printAverage(decimalArr);
            findLongestSequenceOfOnes(binaryArr);
            PrintSwitchCounts(binaryArr);
            PrintNumberWithMostOnes(binaryArr);
            PrintTotalOnes(binaryArr);
        }

        private static int[] requestInputFromUser(int i_numOfBinaryNumbers)
        {
            int[] binartArr = new int[i_numOfBinaryNumbers];
            string input;
            Console.WriteLine("Please enter " + i_numOfBinaryNumbers + " binary numbers with 7 digits each one");

            for (int i = 0; i < i_numOfBinaryNumbers; i++)
            {
                Console.WriteLine("Number " + (i + 1) + ":");
                input = Console.ReadLine();
                bool isValid = checkInputFromUser(input);
                while (!isValid)
                {
                    Console.WriteLine("illegal input");
                    input = Console.ReadLine();
                    isValid = checkInputFromUser(input);
                }
                binartArr[i] = int.Parse(input);
            }

            return binartArr;
        }
        private static bool checkInputFromUser(string i_input)
        {
            int inputLength = i_input.Length;
            if (inputLength != 7)
                return false;

            for (int i = 0; i < inputLength; i++)
            {
                if (i_input[i] != '0' && i_input[i] != '1')
                    return false;
            }

            return true;
        }
        private static int[] parseBinaryArrToDecimalArr(int i_numOfBinaryNumbers, int[] i_binaryArr)
        {

            int[] decimalArr = new int[i_numOfBinaryNumbers];

            for (int i = 0; i < i_numOfBinaryNumbers; i++)
            {
                int temp = i_binaryArr[i];
                int decimalNumber = 0;
                for (int j = 0; j < 7; j++)
                {
                    decimalNumber += (temp % 10) * (int)Math.Pow(2, j);
                    temp /= 10;
                }
                decimalArr[i] = decimalNumber; ;
            }

            return decimalArr;
        }
        private static void printDecimalInDescending(int[] i_decimalArr)
        {
            Array.Sort(i_decimalArr);
            Array.Reverse(i_decimalArr);

            Console.WriteLine("Decimal numbers in descending order:");
            for (int i = 0; i < i_decimalArr.Length-1; i++)
            {
                Console.Write(i_decimalArr[i] + ", ");
            }
            Console.WriteLine(i_decimalArr[i_decimalArr.Length -1]);
        }
        private static void printAverage(int[] i_decimalArr)
        {
            int sum = 0;
            int arrLen = i_decimalArr.Length;

            for (int i = 0; i < arrLen; i++)
            {
                sum += i_decimalArr[i];
            }

            float average = (float)sum / arrLen;
            Console.WriteLine("Average value: " + average);
        }
        private static void findLongestSequenceOfOnes(int[] i_binaryArr)
        {
            int maxStreak = 0;
            int numberWithMaxStreak = 0;

            foreach (int currentNumInArray in i_binaryArr)
            {
                int copyOfCurrentNumInArray = currentNumInArray;
                int currentStreak = 0;

                while (copyOfCurrentNumInArray > 0)
                {
                    int digit = copyOfCurrentNumInArray % 10;
                    if (digit == 1)
                    {
                        currentStreak++;
                        // localMax = Math.Max(localMax, currentStreak);
                    }
                    else
                    {
                        currentStreak = 0;
                    }
                    copyOfCurrentNumInArray /= 10;
                }

                if (currentStreak > maxStreak)
                {
                    maxStreak = currentStreak;
                    numberWithMaxStreak = currentNumInArray;
                }
            }
            Console.WriteLine($"Longest streak of 1s: {maxStreak}");
            int digitsInNum = numberWithMaxStreak.ToString().Length;
            Console.Write("Number with longest streak: ");
            if (digitsInNum < 7)
            {
                for (int i = digitsInNum; i < 7; i++)
                {
                    Console.Write("0");
                }
            }
            Console.WriteLine(numberWithMaxStreak);

        }
        private static void PrintSwitchCounts(int[] i_binaryNumbers)
        {
            foreach (int currentNumInArray in i_binaryNumbers)
            {
                int switches = 0;
                string binaryStr = currentNumInArray.ToString().PadLeft(7, '0');

                for (int i = 1; i < binaryStr.Length; i++)
                    if (binaryStr[i] != binaryStr[i - 1])
                        switches++;     

                Console.WriteLine($"{binaryStr} -> {switches}");
            }
        }
        private static void PrintNumberWithMostOnes(int[] i_binaryNumbers)
        {
            int maxOnes = -1;
            int numberWithMostOnes = 0;

            foreach (int currentNumInArray in i_binaryNumbers)
            {
                string binaryStr = currentNumInArray.ToString().PadLeft(7, '0');
                int countOnes = 0;

                foreach (char currentCharacter in binaryStr)
                    if (currentCharacter == '1') countOnes++;

                if (countOnes > maxOnes)
                {
                    maxOnes = countOnes;
                    numberWithMostOnes = currentNumInArray;
                }
            }

            string formatted = numberWithMostOnes.ToString().PadLeft(7, '0');
            Console.WriteLine($"The number with the most '1's is: {formatted} ({maxOnes} ones)");
        }
        private static void PrintTotalOnes(int[] i_binaryNumbers)
        {
            int totalOnes = 0;

            foreach (int currentNumInArray in i_binaryNumbers)
            {
                string binaryStr = currentNumInArray.ToString().PadLeft(7, '0');

                foreach (char currentCharacter in binaryStr)
                {
                    if (currentCharacter == '1')
                        totalOnes++;
                }
            }

            Console.WriteLine("Total number of '1's in all 4 binary numbers: " + totalOnes);
        }
    }
}
