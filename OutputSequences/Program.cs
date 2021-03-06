﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OutputSequences
{
    class Program
    {
        static readonly int[] lengths = { 1, 2, 3 };
        static readonly char[] acids = { 'A', 'C', 'G', 'U', '?' };
        static readonly Dictionary<string, int> sequences = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            GenerateSequenceList();

            string line;
            // Read the file and check for sequence matches line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("D:\\Documents\\Personal\\Sequence-Builder\\RNAseqs.txt");
            while ((line = file.ReadLine()) != null)
            {
                // For every sequence
                foreach (var sequence in sequences.Keys.ToList())
                {
                    // Check if sequence appears at least once in the line
                    // foreach character in line, up until sequence can't fit in remaining line length
                    for (int i = 0; i <= (line.Length - sequence.Length); i++)
                    {
                        var matched = false;

                        // check if each sequence char matches the appropriate line char
                        for (int j = 0; j < sequence.Length; j++)
                        {
                            var lineChar = line[i+j];
                            var sequenceChar = sequence[j];

                            // if the the current sequenceChar doesn't have a wildcard or match the lineChar,
                            // stop checking at this location in the line
                            if (sequenceChar != '?' && sequenceChar != lineChar)
                            {
                                break;
                            }

                            // if we got here and there are no more chars to check in the sequence we have a match!
                            if (j == (sequence.Length - 1))
                            {
                                matched = true;
                            }
                        }

                        if (matched)
                        {
                            // increment count for this sequence
                            sequences[sequence]++;

                            // stop looking for a match on this line
                            break;
                        }
                    }
                }
            }

            file.Close();
                        
            // Output all of the sequences
            foreach (var sequence in sequences)
            {
                Console.WriteLine(sequence);
            }

            // Keep console open
            Console.ReadLine();
        }

        private static void GenerateSequenceList()
        {
            // Generate sequences for each length
            foreach (var length in lengths)
            {
                // Initialise first baseCode with 0s for this length
                int[] baseCode = new int[length];
                for (int i = 0; i < baseCode.Length; i++)
                {
                    baseCode[i] = 0;

                    // Add all sequences to list
                    GenerateSequences(baseCode);
                }
            }
        }

        private static void GenerateSequences(int[] baseCode)
        {
            var firstSequence = ToSequence(baseCode);
            if (IsValidSequence(firstSequence))
            {
                sequences[string.Join("", firstSequence)] = 0;
            }

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
                    var sequence = ToSequence(baseCode);
                    if (IsValidSequence(sequence))
                    {
                        sequences[string.Join("", sequence)] = 0;
                    }

                    // continue to next sequence
                    continue;
                }

                // No more sequences
                break;
            }
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

        private static bool IsValidSequence(char[] sequence)
        {
            return sequence[0] != '?' && sequence[sequence.Length-1] != '?';
        }

        private static string PatternType(char[] sequence)
        {
            foreach (var character in sequence)
            {
                if (character == '?')
                {
                    return "Variable";
                }
            }
            return "Fixed";
        }

        private static bool IsFixed(char[] sequence)
        {
            return PatternType(sequence) == "Fixed";
        }

        private static bool IsVariable(char[] sequence)
        {
            return PatternType(sequence) == "Variable";
        }
    }
}
