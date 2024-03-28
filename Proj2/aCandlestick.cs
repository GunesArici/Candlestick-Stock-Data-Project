using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Proj2
{
    /// <summary>
    /// Gets and sets data from a CSV file and converts it into an object called "aCandlestick"
    /// </summary>
    public class aCandlestick
    {
        public bool isNeutralDoji => IsNeutralDoji();
        public bool isDragonflyDoji => IsDragonflyDoji();
        public bool isGravestoneDoji => IsGravestoneDoji();
        public bool isLongLeggedDoji => IsLongLeggedDoji();
        public bool isSpinningTop => IsSpinningTop();
        public bool isWhiteMarubozu => IsWhiteMarubozu();
        public bool isBlackMarubozu => IsBlackMarubozu();
        public bool isBullishHammer => IsBullishHammer();
        public bool isBearishHammer => IsBearishHammer();
        public bool isBullishInvertedHammer => IsBullishInvertedHammer();
        public bool isBearishInvertedHammer => IsBearishInvertedHammer();
        

        /// Gets or sets the date of the stock data entry
        [Name("Date")]
        public String Date { get; set; }
        /// Gets or sets the open price of the stock data entry.
        [Name("Open")]
        public Decimal Open { get; set; }
        /// Gets or sets the high price of the stock data entry.
        [Name("High")]
        public Decimal High { get; set; }
        /// Gets or sets the low price of the stock data entry.
        [Name("Low")]
        public Decimal Low { get; set; }
        /// Gets or sets the closing price of the stock data entry.
        [Name("Close")]
        public Decimal Close { get; set; }
        /// Gets or sets the volume of the stock data entry.
        [Name("Volume")]
        public Decimal Volume { get; set; }
        private bool IsNeutralDoji()
        {
            decimal range = High - Low;
            decimal midPoint = Low + (range / 2);
            decimal tolerance = range * 0.01m;

            return Math.Abs(Close - Open) <= tolerance && Math.Abs(midPoint - Close) <= tolerance && range > 0;
        }
        /// <summary>
        /// This function checks if the candlestick is a Dragonfly Doji
        /// </summary>
        /// <returns></returns>
        private bool IsDragonflyDoji()
        {
            /// Calculate the range and tolerance of the candlestick
            decimal range = High - Low;
            decimal tolerance = range * 0.05m;

            /// Return true if the candlestick meets the criteria for a Dragonfly Doji
            return Open == Close && High == Close && Low < Close - tolerance && range > 0 && Math.Abs(High - Open) < tolerance;
        }

        /// <summary>
        /// This function checks if the candlestick is a Gravestone Doji
        /// </summary>
        /// <returns></returns>
        private bool IsGravestoneDoji()
        {
            /// Calculate the range and tolerance of the candlestick
            decimal range = High - Low;
            decimal tolerance = range * 0.05m;

            /// Return true if the candlestick meets the criteria for a Gravestone Doji
            return Open == Close && Low == Close && High > Close + tolerance && range > 0 && Math.Abs(High - Open) < tolerance;
        }

        /// <summary>
        /// This function checks if the candlestick is a Long-Legged Doji
        /// </summary>
        /// <returns></returns>
        private bool IsLongLeggedDoji()
        {
            /// Calculate the range, midpoint, and tolerance of the candlestick
            decimal range = High - Low;
            decimal midPoint = Low + (range / 2);
            decimal tolerance = range * 0.05m;

            /// Return true if the candlestick meets the criteria for a Long-Legged Doji
            return Math.Abs(Close - Open) <= tolerance && Math.Abs(midPoint - Close) <= tolerance && range > 0;
        }

        /// <summary>
        /// This function checks if the candlestick is a Spinning Top
        /// </summary>
        /// <returns></returns>
        private bool IsSpinningTop()
        {
            /// Calculate various lengths and ratios of the candlestick
            decimal bodyLength = Math.Abs(Open - Close);
            decimal upperShadowLength = High - Math.Max(Open, Close);
            decimal lowerShadowLength = Math.Min(Open, Close) - Low;
            decimal totalLength = upperShadowLength + bodyLength + lowerShadowLength;
            decimal bodyToTotalRatio = bodyLength / totalLength;
            decimal tolerance = 0.2m;

            /// Return true if the candlestick meets the criteria for a Spinning Top
            if (Close > Open)
            {
                return bodyToTotalRatio < 0.4m && upperShadowLength > 1.5m * bodyLength && lowerShadowLength > 1.5m * bodyLength &&
                    Math.Abs(Open - Close) <= tolerance * totalLength && Open <= (High + Low) / 2 && Close >= (High + Low) / 2;
            }
            else
            {
                return bodyToTotalRatio < 0.4m && upperShadowLength > 1.5m * bodyLength && lowerShadowLength > 1.5m * bodyLength &&
                    Math.Abs(Open - Close) <= tolerance * totalLength && Close <= (High + Low) / 2 && Open >= (High + Low) / 2;
            }
        }

        /// <summary>
        /// This function checks if the candlestick is a White Marubozu
        /// </summary>
        /// <returns></returns>
        private bool IsWhiteMarubozu()
        {
            /// Calculate the range and tolerance of the candlestick
            decimal range = High - Low;

            /// Return true if the candlestick meets the criteria for a White Marubozu
            return Open == Low && Close == High && Close - Open > range * 0.6m && range > 0;
        }

        /// <summary>
        /// This method checks if the candlestick is a Black Marubozu pattern
        /// </summary>
        /// <returns></returns>
        private bool IsBlackMarubozu()
        {
            /// Calculate the length of the body and shadow of the candlestick
            decimal bodyLength = Math.Abs(Open - Close);
            decimal shadowLength = High - Low;

            /// Check if the candlestick satisfies the conditions for a Black Marubozu
            return Close < Open && Open == High && Close == Low && shadowLength <= bodyLength * 0.05m;
        }

        /// <summary>
        /// This method checks if the candlestick is a Bullish Hammer pattern
        /// </summary>
        /// <returns></returns>
        private bool IsBullishHammer()
        {
            /// Calculate the length of the body and shadow of the candlestick
            decimal bodyLength = Math.Abs(Open - Close);
            decimal upperShadowLength = High - Math.Max(Open, Close);
            decimal lowerShadowLength = Math.Min(Open, Close) - Low;

            /// Check if the candlestick satisfies the conditions for a Bullish Hammer
            return lowerShadowLength >= 2 * bodyLength && upperShadowLength <= 0.5m * bodyLength && Close > Open;
        }

        /// <summary>
        /// This method checks if the candlestick is a Bearish Hammer pattern
        /// </summary>
        /// <returns></returns>
        private bool IsBearishHammer()
        {
            /// Calculate the length of the body and shadow of the candlestick
            decimal bodyLength = Math.Abs(Open - Close);
            decimal upperShadowLength = High - Math.Max(Open, Close);
            decimal lowerShadowLength = Math.Min(Open, Close) - Low;
            decimal totalRange = High - Low;

            /// Check if the candlestick satisfies the conditions for a Bearish Hammer
            return lowerShadowLength >= 2 * bodyLength && upperShadowLength <= 0.03m * totalRange && Close < Open && bodyLength >= 0.005m * totalRange && bodyLength <= 0.5m * totalRange;
        }

        /// <summary>
        /// This method checks if the candlestick is a Bullish Inverted Hammer pattern
        /// </summary>
        /// <returns></returns>
        private bool IsBullishInvertedHammer()
        {
            /// Calculate the length of the body and shadow of the candlestick
            decimal bodyLength = Math.Abs(Open - Close);
            decimal upperShadowLength = High - Math.Max(Open, Close);
            decimal lowerShadowLength = Math.Min(Open, Close) - Low;

            /// Check if the candlestick satisfies the conditions for a Bullish Inverted Hammer
            return upperShadowLength >= 2 * bodyLength && lowerShadowLength <= 0.3m * bodyLength && Close > Open;
        }

        /// <summary>
        /// This method checks if the candlestick is a Bearish Inverted Hammer pattern
        /// </summary>
        /// <returns></returns>
        private bool IsBearishInvertedHammer()
        {
            /// Calculate the length of the body and shadow of the candlestick
            decimal bodyLength = Math.Abs(Open - Close);
            decimal upperShadowLength = High - Math.Max(Open, Close);
            decimal lowerShadowLength = Math.Min(Open, Close) - Low;

            /// Check if the candlestick satisfies the conditions for a Bearish Inverted Hammer
            return upperShadowLength >= 2 * bodyLength && lowerShadowLength <= 0.3m * bodyLength && Close < Open;
        }
    }
}