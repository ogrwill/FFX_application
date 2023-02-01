using System;
using System.Linq;

namespace PostcodeParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter postcode:");
            var postcode = Console.ReadLine();
            ParsePostcode("M28 7JP");
            ParsePostcode("WC2H7DE");
            ParsePostcode("CT21     4LR");
            ParsePostcode("N33DP");
        }

        public static void ParsePostcode(string postcode)
        {
            
            Console.WriteLine("# POSTCODE: " + postcode);
            //normalise first
            postcode = new string(postcode.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray()); 

            string first = "";
            string last = "";
            first = postcode[0..^3];
            last = postcode.Substring(postcode.Length - 3, 3);


            Console.WriteLine("    OUTWORD CODE: " + first);

          
            
            string oLetter = string.Concat(first.TakeWhile(c => c < '0' || c > '9'));
            Console.WriteLine("         OUTWORD LETTER: " + oLetter);

            var test = first.Substring(first.IndexOf(oLetter) + oLetter.Length, first.Length- oLetter.Length);
            string oNum = string.Concat(first.TakeWhile(c => c < '0' || c > '9'));
            Console.WriteLine("         OUTWORD NUMBER: " + test);


           

            Console.WriteLine(" INWARD CODE: " + last);

            string inNum = new string(last.Where(char.IsDigit).ToArray());
            Console.WriteLine("         INWARD NUMBER: " + inNum);

            string inLetter = new string(last.Where(char.IsLetter).ToArray());
            Console.WriteLine("         INWARD LETTER: " + inLetter);

            
        }
    }
}
