using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj2
{
    public class multiCandlesticks
    {
        /// <summary>
        /// Creates different values for initialzing data points, finding the indexes of each data point and creating different checkers used in the abstract class
        /// </summary>
        public List<aCandlestick> candlesticks {  get; set; }
        public int index { get; set; } 
        public bool isBullishEngulfing => IsBullishEngulfing(candlesticks, index);
        public bool isBearishEngulfing => IsBearishEngulfing(candlesticks, index);
        public bool isBullishHarami => IsBullishHarami(candlesticks, index);
        public bool isBearishHarami => IsBearishHarami(candlesticks, index);
        public bool isMorningStar => IsMorningStar(candlesticks, index);
        public bool isEveningStar => IsEveningStar(candlesticks, index);
        /// <summary>
        /// Checks if a Bullish Engulfing pattern exists at a particular index
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsBullishEngulfing(List<aCandlestick> candlesticks, int index)
        {
            /// Check if the index is out of range
            if (index < 1 || index >= candlesticks.Count) return false;

            /// Get the current and previous candlesticks
            aCandlestick currentCandlestick = candlesticks[index];
            aCandlestick previousCandlestick = candlesticks[index - 1];

            /// Check if the previous candlestick is bearish
            if (previousCandlestick.Close > previousCandlestick.Open) return false;

            /// Check if the current candlestick is bullish
            if (currentCandlestick.Close < currentCandlestick.Open) return false;

            /// Check if the current candlestick completely engulfs the previous candlestick
            if (currentCandlestick.High >= previousCandlestick.High && currentCandlestick.Low <= previousCandlestick.Low)
            {
                /// If all conditions are met, return true
                return true;
            }
            /// Otherwise, return false
            return false;
        }
        /// <summary>
        /// Checks if a Bearish Engulfing pattern exists at a particular index
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsBearishEngulfing(List<aCandlestick> candlesticks, int index)
        {
            if (index < 1 || index >= candlesticks.Count) return false;

            aCandlestick currentCandlestick = candlesticks[index];
            aCandlestick previousCandlestick = candlesticks[index - 1];

            /// Check if the previous candlestick is bullish
            if (previousCandlestick.Close < previousCandlestick.Open) return false;

            /// Check if the current candlestick is bearish
            if (currentCandlestick.Close > currentCandlestick.Open) return false;

            /// Check if the current candlestick completely engulfs the previous candlestick in a downward direction
            decimal currentCandlestickLow = currentCandlestick.Low;
            decimal currentCandlestickHigh = currentCandlestick.High;

            decimal previousCandlestickLow = previousCandlestick.Low;
            decimal previousCandlestickHigh = previousCandlestick.High;

            if (currentCandlestickLow < previousCandlestickLow &&
                currentCandlestickHigh > previousCandlestickHigh &&
                currentCandlestick.Open > previousCandlestick.Close &&
                currentCandlestick.Close < previousCandlestick.Open)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Checks if a Bullish Harami pattern exists at a particular index
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsBullishHarami(List<aCandlestick> candlesticks, int index)
        {
            if (index < 1 || index >= candlesticks.Count - 1)
            {
                /// Invalid index
                return false;
            }

            /// Get the first and second candlesticks
            aCandlestick firstCandlestick = candlesticks[index - 1];
            aCandlestick secondCandlestick = candlesticks[index];

            /// Calculate the body of the two candlesticks
            decimal firstCandlestickBody = Math.Abs(firstCandlestick.Close - firstCandlestick.Open);
            decimal secondCandlestickBody = Math.Abs(secondCandlestick.Close - secondCandlestick.Open);

            if (firstCandlestick.Close < firstCandlestick.Open && /// Bearish candle
                secondCandlestick.Close > secondCandlestick.Open && /// Bullish candle
                secondCandlestick.Open > firstCandlestick.Close && /// Close above previous open
                secondCandlestick.Close < firstCandlestick.Open && /// Open below previous close
                secondCandlestick.Low > firstCandlestick.Low && /// Higher low
                secondCandlestick.High < firstCandlestick.High)
            {
                return true; /// Bullish Harami pattern recognized
            }

            return false; /// Bullish Harami pattern not recognized
        }

        /// <summary>
        /// Checks if a Bearish Harami pattern exists at a particular index
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsBearishHarami(List<aCandlestick> candlesticks, int index)
        {
            /// Check if index is within valid range
            if (index < 1 || index >= candlesticks.Count - 1)
            {
                /// Invalid index, pattern cannot exist
                return false;
            }

            /// Get the previous and current candlestick
            aCandlestick firstCandle = candlesticks[index - 1];
            aCandlestick secondCandle = candlesticks[index];

            /// Check if the pattern matches the criteria for Bearish Harami
            if (firstCandle.Close > firstCandle.Open && // Bullish candle
                secondCandle.Close < secondCandle.Open && // Bearish candle
                secondCandle.Close > firstCandle.Open && // Close above previous open
                secondCandle.Open < firstCandle.Close && // Open below previous close
                secondCandle.Low > firstCandle.Low && // Higher low
                secondCandle.High < firstCandle.High) // Lower high
            {
                /// Bearish Harami pattern recognized
                return true;
            }

            /// Bearish Harami pattern not recognized
            return false;
        }

        /// <summary>
        /// Checks if a Morning Star pattern exists at a particular index
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsMorningStar(List<aCandlestick> candlesticks, int index)
        {
            /// Check if index is within valid range
            if (index < 2 || index >= candlesticks.Count - 1)
            {
                /// Invalid index, pattern cannot exist
                return false;
            }

            /// Check if the first two candlesticks match the criteria for a bearish candlestick followed by a small-bodied candlestick
            if (candlesticks[index - 2].Close > candlesticks[index - 2].Open && // Bearish candle
                candlesticks[index - 1].Close < candlesticks[index - 1].Open && // Bullish candle
                candlesticks[index - 1].Close < candlesticks[index - 2].Open && // Close below previous open
                candlesticks[index - 1].Open > candlesticks[index - 2].Close) // Open above previous close
            {
                /// Check if the third candlestick matches the criteria for a bullish candlestick that engulfs the first candlestick
                double db1 = Convert.ToDouble(candlesticks[index].Close);
                double db2 = Convert.ToDouble(candlesticks[index].Open);
                double db3 = Convert.ToDouble(candlesticks[index].High);
                double db4 = Convert.ToDouble(candlesticks[index].Low);
                double bodySize = Math.Abs(db1 - db2);
                double totalSize = db3 - db4;
                if (bodySize / totalSize < 0.1 && // Small-bodied candlestick
                    candlesticks[index + 1].Close > candlesticks[index + 1].Open && // Bullish candle
                    candlesticks[index + 1].Close > candlesticks[index - 2].Open && // Close above previous open
                    candlesticks[index + 1].Open < candlesticks[index - 2].Close && // Open below previous close
                    candlesticks[index + 1].Close > candlesticks[index - 2].Close) // Close above previous close
                {
                    /// Morning Star pattern recognized
                    return true;
                }
            }

            /// Morning Star pattern not recognized
            return false;
        }
        /// <summary>
        /// Checks if a Evening Star pattern exists at a particular index
        /// </summary>
        /// <param name="candlesticks"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsEveningStar(List<aCandlestick> candlesticks, int index)
        {
            if (index < 2 || index >= candlesticks.Count)
                return false;
            /// Get the previous two candlesticks and the current candlestick

            aCandlestick firstCandlestick = candlesticks[index - 2];
            aCandlestick secondCandlestick = candlesticks[index - 1];
            aCandlestick thirdCandlestick = candlesticks[index];

            // Calculate the body size of the previous two candlesticks and the current candlestick

            decimal firstCandlestickBody = Math.Abs(firstCandlestick.Close - firstCandlestick.Open);
            decimal secondCandlestickBody = Math.Abs(secondCandlestick.Close - secondCandlestick.Open);
            decimal thirdCandlestickBody = Math.Abs(thirdCandlestick.Close - thirdCandlestick.Open);

            /// Check if the first candlestick is bullish and the third candlestick is bearish

            bool firstIsBullish = firstCandlestick.Close > firstCandlestick.Open;
            bool thirdIsBearish = thirdCandlestick.Close < thirdCandlestick.Open;

            if (firstIsBullish && thirdIsBearish)
            {
                /// Check if the third candlestick completely engulfs the second candlestick and there is a gap between the second and first candlesticks

                bool isEngulfing = thirdCandlestick.Close < secondCandlestick.Open && thirdCandlestick.Open > secondCandlestick.Close;
                bool hasGap = secondCandlestick.High < firstCandlestick.Low;

                if (isEngulfing && hasGap)
                {
                    /// Check if the body size of the third candlestick is larger than that of the second candlestick
                    /// and if it meets a minimum size requirement based on its body size
                    decimal minimumBodySize = 0.1m * thirdCandlestickBody;
                    bool hasLargeBody = thirdCandlestickBody > secondCandlestickBody && thirdCandlestickBody > minimumBodySize;
                    bool hasLowerLow = thirdCandlestick.Low < secondCandlestick.Low;

                    if (hasLargeBody && hasLowerLow)
                        return true; /// The Evening Star pattern is recognized
                }
            }

            return false; /// The Evening Star pattern is not recognized
        }
    }
}
