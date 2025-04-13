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
            int numOfBinaryNumbers = 4;
            int[] binaryArr, decimalArr;

            binaryArr = requestInputFromUser(numOfBinaryNumbers);
            decimalArr = parseBinaryArrToDecimalArr(numOfBinaryNumbers, binaryArr);
            printDecimalInDescending(decimalArr);
            printAverage(decimalArr);
            findLongestSequenceOfOnes(binaryArr);
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

            foreach (int num in i_binaryArr)
            {
                int n = num;
                int currentStreak = 0;

                while (n > 0)
                {
                    int digit = n % 10;
                    if (digit == 1)
                    {
                        currentStreak++;
                        // localMax = Math.Max(localMax, currentStreak);
                    }
                    else
                    {
                        currentStreak = 0;
                    }
                    n /= 10;
                }

                if (currentStreak > maxStreak)
                {
                    maxStreak = currentStreak;
                    numberWithMaxStreak = num;
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
            Console.Write(numberWithMaxStreak);

        }
    }
}
