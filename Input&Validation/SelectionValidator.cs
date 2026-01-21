using System.Text.RegularExpressions;

namespace FinalProject
{
    public class SelectionValidator
    {
        public readonly int DefaultMax;

        public SelectionValidator(int defaultMax)
        {
            DefaultMax = defaultMax;
        }
        public SelectionValidator HandleValidators(string input)
        {
            if (!Regex.IsMatch(input, @"^[0-9]+(\.[0-9]+)?$"))
            {
                throw new FormatException("Input must be a valid number!");
            }

            return CheckSelection(input);
        }

        public SelectionValidator CheckSelection(string input)
        {
            if (!double.TryParse(input, out double val))
                throw new FormatException("Invalid numeric format!");

            double currentMaxLimit;

            if (val > 100)
            {
                currentMaxLimit = 700;
            }
            else if (val > DefaultMax && val <= 100)
            {
                currentMaxLimit = 100;
            }
            else
            {
                currentMaxLimit = DefaultMax;
            }

            if (val < 0 || val > currentMaxLimit)
            {
                throw new ArgumentOutOfRangeException(null, $"Value must be between 0 and {currentMaxLimit}!");
            }

            return this; 
        }
    }
}
