using System;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] partNumbers = { "Hello", "World", "!" };
            Regex rgx = new Regex(@"\w+");
            foreach (string partNumber in partNumbers)
                if (rgx.IsMatch(partNumber))
                    Console.WriteLine(partNumber);

            string pattern = @"\ba\w*\b";
            string input = "An extraordinary day dawns with each new day.";
            Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase); //searches for 1st match of the regular expression
            if (m.Success)
                Console.WriteLine("Found '{0}' at position {1}.", m.Value, m.Index);

            pattern = @"\b\w+es\b";
            rgx = new Regex(pattern);
            string sentence = "Who writes these notes?";
            foreach (Match match in rgx.Matches(sentence)) //matches all occurances of regex
                Console.WriteLine("Found '{0}' at position {1}",    match.Value, match.Index);


            input = "Hello   World   ";
            pattern = "\\s+";
            string replacement = " ";
            rgx = new Regex(pattern);
            string result = rgx.Replace(input, replacement);

            input = "plum-pear";
            pattern = "(-)"; //if paranthesis are not used output will be plum, pear. If its used, output will be plum, -(includes hifen), pear

            string[] substrings = Regex.Split(input, pattern);    
            foreach (string match in substrings)
            {
                Console.WriteLine("'{0}'", match);
            }
        }
    }
}
