using System;
using System.Collections.Generic;

namespace OutputSequences
{
    class Program
    {
        static readonly int[] lengths = { 3, 6, 9 };
        static readonly char[] acids = { 'a', 'c', 'g', 'u' };
        static readonly List<string> sequences = new List<string>();

        static void Main(string[] args)
        {
            int[] baseCode = { 0, 0, 0, 0 };

            var firstSequence = string.Join("", ToSequence(baseCode));
            sequences.Add(firstSequence);

            while (true)
            {
                // Incremement least significant digit
                var currentIndex = 0;
                baseCode[currentIndex]++;

                // While within bounds of sequence and carry over needs to occur
                while (currentIndex < baseCode.Length && baseCode[currentIndex] >= acids.Length)
                {
                    // Set current digit to 0
                    baseCode[currentIndex] = 0;
                    // Move to next digit
                    currentIndex++;

                    // If still within bounds of sequence
                    if (currentIndex < baseCode.Length)
                    {
                        // Incremement this digit
                        baseCode[currentIndex]++;
                    }
                }

                // If within bounds of sequence and last increment is allowed
                if (currentIndex < baseCode.Length && baseCode[currentIndex] < acids.Length)
                {
                    var sequence = string.Join("", ToSequence(baseCode));
                    sequences.Add(sequence);

                    // continue to next sequence
                    continue;
                }

                // No more sequences
                break;
            }

            // Output all of the sequences
            foreach (var sequence in sequences)
            {
                Console.WriteLine(sequence);
            }
            Console.ReadLine();
        }
        
        private static int[] ToBaseCode(char[] sequence)
        {
            int[] baseCode = new int[sequence.Length];
            for (var i = 0; i < sequence.Length; i++)
            {
                baseCode[i] = Array.IndexOf(acids, sequence[i]);
            }
            return baseCode;
        }

        private static char[] ToSequence(int[] baseCode)
        {
            char[] Sequence = new char[baseCode.Length];
            for (var i = 0; i < baseCode.Length; i++)
            {
                Sequence[i] = acids[baseCode[i]];
            }
            return Sequence;
        }
    }
}
