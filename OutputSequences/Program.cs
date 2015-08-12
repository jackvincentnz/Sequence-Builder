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
            int[] base4 = { 0, 0, 0, 0 };

            var firstSequence = string.Join("", ToSequence(base4));
            sequences.Add(firstSequence);

            while (true)
            {
                // Incremement least significant digit
                var currentIndex = 0;
                base4[currentIndex]++;

                // While within bounds of sequence and carry over needs to occur
                while (currentIndex < base4.Length && base4[currentIndex] >= acids.Length)
                {
                    // Set current digit to 0
                    base4[currentIndex] = 0;
                    // Move to next digit
                    currentIndex++;

                    // If still within bounds of sequence
                    if (currentIndex < base4.Length)
                    {
                        // Incremement this digit
                        base4[currentIndex]++;
                    }
                }

                // If within bounds of sequence and last increment is allowed
                if (currentIndex < base4.Length && base4[currentIndex] < acids.Length)
                {
                    var sequence = string.Join("", ToSequence(base4));
                    sequences.Add(sequence);

                    // continue to next sequence
                    continue;
                }

                // No more sequences
                break;
            }

            foreach (var sequence in sequences)
            {
                Console.WriteLine(sequence);
            }
            Console.ReadLine();
        }
        
        private static int[] ToBase4(char[] sequence)
        {
            int[] Base4 = new int[sequence.Length];
            for (var i = 0; i < sequence.Length; i++)
            {
                Base4[i] = Array.IndexOf(acids, sequence[i]);
            }
            return Base4;
        }

        private static char[] ToSequence(int[] Base4)
        {
            char[] Sequence = new char[Base4.Length];
            for (var i = 0; i < Base4.Length; i++)
            {
                Sequence[i] = acids[Base4[i]];
            }
            return Sequence;
        }
    }
}
