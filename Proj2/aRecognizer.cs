using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using static Proj2.stockDisplay;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Proj2
{
    internal abstract class aRecognizer
    {
        /// <summary>
        /// initializes the values needed for the lists and pattern names and sizes to properly match them
        /// </summary>
        public string patternName { get; set; }
        protected abstract List<int> patternMatch(List<aCandlestick> subset);
        protected abstract List<int> patternCheck(List<multiCandlesticks> ss);
        public int patternSize { get; set; }
        /// <summary>
        /// Makes an aRecognizer class to set parametrs for the pattern name and size
        /// </summary>
        /// <param name="patternName"></param>
        /// <param name="patternSize"></param>
        protected aRecognizer(string patternName, int patternSize)
        {
            this.patternName = patternName;
            this.patternSize = patternSize;
        }
        /// <summary>
        /// Gets the exact list to go through for both of these
        /// </summary>
        /// <param name="subset"></param>
        /// <returns></returns>
        public List<int> GetMatchingIndices(List<aCandlestick> subset)
        {
            return patternMatch(subset);
        }
        public List<int> GetMatchingIndex(List<multiCandlesticks> ss)
        {
            return patternCheck(ss);
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Neutral Doji patterns
    /// </summary
    internal class Recognizer_Neutral : aRecognizer
    {
        public Recognizer_Neutral() : base("Neutral Doji", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }

        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isNeutralDoji)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Dragonfly Doji patterns
    /// </summary
    internal class Recognizer_Dragonfly : aRecognizer
    {
        public Recognizer_Dragonfly() : base("Dragonfly Doji", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isDragonflyDoji)
                {
                    Console.WriteLine("hello");
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Gravestone Doji patterns
    /// </summary
    internal class Recognizer_Gravestone : aRecognizer
    {
        public Recognizer_Gravestone() : base("Gravestone Doji", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isGravestoneDoji)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Long Legged Doji patterns
    /// </summary
    internal class Recognizer_LongLegged : aRecognizer
    {
        public Recognizer_LongLegged() : base("Long-Legged Doji", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isLongLeggedDoji)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Spinning Top patterns
    /// </summary
    internal class Recognizer_SpinningTop : aRecognizer
    {
        public Recognizer_SpinningTop() : base("Spinning Top", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isSpinningTop)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any White Marubozu patterns
    /// </summary
    internal class Recognizer_WhiteMarubozu : aRecognizer
    {
        public Recognizer_WhiteMarubozu() : base("White Marubozu", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isWhiteMarubozu)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Black Marubozu patterns
    /// </summary
    internal class Recognizer_BlackMarubozu : aRecognizer
    {
        public Recognizer_BlackMarubozu() : base("Black Marubozu", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isBlackMarubozu)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Bullish Hammers patterns
    /// </summary
    internal class Recognizer_BullishHammer : aRecognizer
    {
        public Recognizer_BullishHammer() : base("Bullish Hammer", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isBullishHammer)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Bearish Hammers patterns
    /// </summary
    internal class Recognizer_BearishHammer : aRecognizer
    {
        public Recognizer_BearishHammer() : base("Bearish Hammer", 1) { }

        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isBearishHammer)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Bullish Inverted Hammers patterns
    /// </summary
    internal class Recognizer_BullishInvertedHammer : aRecognizer
    {
        public Recognizer_BullishInvertedHammer() : base("Bullish Inverted Hammer", 1) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isBullishInvertedHammer)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the aCandlestick class to recognize any Bearish Inverted Hammers patterns
    /// </summary
    internal class Recognizer_BearishInvertedHammer : aRecognizer
    {
        public Recognizer_BearishInvertedHammer() : base("Bearish Inverted Hammer", 1) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            return null;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < subset.Count; i++)
            {
                if (subset[i].isBearishInvertedHammer)
                {
                    matchingIndices.Add(i);
                }
            }
            return matchingIndices;
        }
    }
    /// <summary>
    /// Goes through and references the multiCandlestick class to recognize any Bullish engulfings patterns
    /// </summary
    internal class Recognizer_BullishEngulfing : aRecognizer
    {
        public Recognizer_BullishEngulfing() : base("Bullish Engulfing", 2) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            List<int> matchingIndexes = new List<int>();
            for(int i = 0; i < ss.Count; i++)
            {
                if (ss[i].isBullishEngulfing)
                {
                    matchingIndexes.Add(i);
                }
            }    
            return matchingIndexes;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            return null;
        }
    }
    /// <summary>
    /// Goes through and references the multiCandlestick class to recognize any Bearish engulfings patterns
    /// </summary>
    internal class Recognizer_BearishEngulfing : aRecognizer
    {
        public Recognizer_BearishEngulfing() : base("Bearish Engulfing", 2) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            List<int> matchingIndexes = new List<int>();
            for (int i = 0; i < ss.Count; i++)
            {
                if (ss[i].isBearishEngulfing)
                {
                    matchingIndexes.Add(i);
                }
            }
            return matchingIndexes;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            return null;
        }
    }
    /// <summary>
    /// Goes through and references the multiCandlestick class to recognize any Bullish Haramis patterns
    /// </summary>
    internal class Recognizer_BullishHarami : aRecognizer
    {
        public Recognizer_BullishHarami() : base("Bullish Harami", 2) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            List<int> matchingIndexes = new List<int>();
            for (int i = 0; i < ss.Count; i++)
            {
                if (ss[i].isBullishHarami)
                {
                    matchingIndexes.Add(i);
                }
            }
            return matchingIndexes;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            return null;
        }
    }
    /// <summary>
    /// Goes through and references the multiCandlestick class to recognize any Bearish Haramis patterns
    /// </summary>
    internal class Recognizer_BearishHarami : aRecognizer
    {
        public Recognizer_BearishHarami() : base("Bearish Harami", 2) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            List<int> matchingIndexes = new List<int>();
            for (int i = 0; i < ss.Count; i++)
            {
                if (ss[i].isBearishHarami)
                {
                    matchingIndexes.Add(i);
                }
            }
            return matchingIndexes;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            return null;
        }
    }
    /// <summary>
    /// Goes through and references the multiCandlestick class to recognize any morning star engulfings patterns
    /// </summary>
    internal class Recognizer_MorningStar : aRecognizer
    {
        public Recognizer_MorningStar() : base("Morning Star", 3) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            List<int> matchingIndexes = new List<int>();
            for (int i = 0; i < ss.Count; i++)
            {
                if (ss[i].isMorningStar)
                {
                    matchingIndexes.Add(i);
                }
            }
            return matchingIndexes;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            return null;
        }
    }
    /// <summary>
    /// Goes through and references the multiCandlestick class to recognize any evening star engulfings patterns
    /// </summary>
    internal class Recognizer_EveningStar : aRecognizer
    {
        public Recognizer_EveningStar() : base("Evening Star", 3) { }
        protected override List<int> patternCheck(List<multiCandlesticks> ss)
        {
            List<int> matchingIndexes = new List<int>();
            for (int i = 0; i < ss.Count; i++)
            {
                if (ss[i].isEveningStar)
                {
                    matchingIndexes.Add(i);
                }
            }
            return matchingIndexes;
        }
        protected override List<int> patternMatch(List<aCandlestick> subset)
        {
            return null;
        }
    }
}
