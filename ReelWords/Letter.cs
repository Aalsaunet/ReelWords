namespace ReelWords
{
    public struct Letter
    {
        public char letterValue;
        public int reelIndex;
        public int pointValue;

        public Letter(char l, int i, int p = 0)
        {
            letterValue = l;
            reelIndex = i;
            pointValue = p;
        }
    }
}

