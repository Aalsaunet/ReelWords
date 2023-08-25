﻿using System;
using System.Collections.Generic;

namespace ReelWords
{
	public class Reel
	{
        public bool randomizeIndices = true;
        private static Reel instance = null;
        private List<char[]> wordWheelsLetters;
        private List<int> wordWheelsCurrentIndices;

        private Reel() {
            wordWheelsLetters = new List<char[]>();
            wordWheelsCurrentIndices = new List<int>();
        }

        public static Reel Instance
        {
            get
            {
                if (instance == null)
                    instance = new Reel();
                return instance;
            }
        }

        public void Insert(string s)
        {
            string[] entries = s.Split();
            char[] letters = new char[entries.Length];

            for (int i = 0; i < letters.Length; i++) {
                if (char.IsLetter(entries[i][0]))
                    letters[i] = entries[i][0];
            }

            wordWheelsLetters.Add(letters);

            int initialIndex = randomizeIndices ? new Random().Next(0, letters.Length) : 0;
            wordWheelsCurrentIndices.Add(initialIndex);
        }

        public string GetCurrentLetters() {
            char[] letters = new char[wordWheelsLetters.Count];
            for (int i = 0; i < letters.Length; i++)
                letters[i] = wordWheelsLetters[i][wordWheelsCurrentIndices[i]];
            return new string(letters);
        }   
    }
}

