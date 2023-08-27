using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReelWords
{
	public class WordMatcher
	{
        public static List<int> FindIndexMatches(Letter[] usableLetters, char[] inputLetters)
        {
            Array.Sort(inputLetters);
            Array.Sort(usableLetters, (x, y) => x.letterValue.CompareTo(y.letterValue));
            List<int> indexMatches = new List<int>();
            int i = 0, j = 0;

            while (i < usableLetters.Length && j < inputLetters.Length)
            {
                if (usableLetters[i].letterValue == inputLetters[j])
                {
                    indexMatches.Add(usableLetters[i].reelIndex);
                    j++;
                }
                i++;
            }
            return indexMatches;
        }

        public static string GenerateHint(Letter[] usableLetters, bool showMatches = false)
        {
            // Check if there's any valid solution with the current usable letters 
            List<char> letterValues = usableLetters.Select(l => l.letterValue).ToList<char>();
            List<string> validWords = new List<string>();

            GenerateCombinationsRecursive(letterValues, "", validWords);

            string hintOutput = "There are " + validWords.Count + " possible word combinations from these letters.\n";

            if (showMatches)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var word in validWords)
                    sb.Append(word + "\n");
                hintOutput += sb.ToString();
            }

            return hintOutput;
        }

        private static void GenerateCombinationsRecursive(List<char> letterValues, string currentCombination, List<string> validWords)
        {
            var (isValidPath, isValidWord) = Trie.Instance.getPathAndWordValidity(currentCombination);

            if (!isValidPath)
                return;

            if (isValidWord)
                validWords.Add(currentCombination);

            for (int i = 0; i < letterValues.Count; i++)
            {
                string newCombination = currentCombination + letterValues[i];
                List<char> updatedLetterValues = new List<char>(letterValues);
                updatedLetterValues.RemoveAt(i);
                GenerateCombinationsRecursive(updatedLetterValues, newCombination, validWords);
            }
        }
    }
}

