using System;
namespace ReelWords
{
	public struct Letter
	{
		public char displayLetter;
        public int reelIndex;
        public int pointValue;

		public Letter(char dl, int i, int p = 0) {
			displayLetter = dl;
            reelIndex = i;
			pointValue = p;
        }
	}
}

