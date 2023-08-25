using System;

namespace ReelWords
{
    public static class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            Console.Out.WriteLine("Welcome to ReelWords!");

            Trie trie = Trie.Instance;

            // Ingest word dictionary from file and store in the trie




            while (playing)
            {
                string input = Console.ReadLine();

                // TODO:  Run game logic here using the user input string

                // TODO:  Create simple unit tests to test your code in the ReelWordsTests project,
                // don't worry about creating tests for everything, just important functions as
                // seen for the Trie tests
            }
        }
    }
}