using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringInterpolation
{
    class Program
    {
        static void Main(string[] arge)
        {
            var name = "Suresh";
            Console.WriteLine($"Hello, {name}. It's a pleasure to meet you!");

            Console.WriteLine($"Hello {"Suresh",15}!"); //Adds spaces before Suresh
            Console.WriteLine($"Hello {"Suresh",-15}!"); //Adds spaces after Suresh
            Console.WriteLine($"Hello {"Suresh",5}!"); //Uses the length of Suresh and no extra spaces

            NumberFormats();
            DateFormats();
            EnumFormats();

            Console.WriteLine($"This is an example of {{ braces }}");
            Console.WriteLine($"This is how you use conditional operator{(true ? "!" : "")}");
        }
        static void EnumFormats()
        {
            FileAttributes attributes = FileAttributes.Hidden | FileAttributes.Archive;
            //F or f : Displays the enumeration entry as a string value
            Console.WriteLine($"{attributes:F}"); //Output : Hidden, Archive

            //D or d : Displays the enumeration entry as an integer value in the shortest representation possible.
            Console.WriteLine($"{attributes:D}"); //Output : 34

            //X or x : Displays the enumeration entry as a hexadecimal value
            Console.WriteLine($"{attributes:X}"); //Output : 00000022
        }
        static void DateFormats()
        {
            DateTime date = DateTime.Now;
            //d : Short date pattern.
            Console.WriteLine($"{date:d}"); //Output : 9/27/2020

            //D : Long date pattern.
            Console.WriteLine($"{date:D}"); //Output : Sunday, September 27, 2020

            //f : Full date/ time pattern(short time)
            Console.WriteLine($"{date:f}"); //Output : Sunday, September 27, 2020 7:59 PM

            //F : Full date/ time pattern(long time).
            Console.WriteLine($"{date:F}"); //Output : Sunday, September 27, 2020 7:59:24 PM

            //M or m : Month / day pattern.
            Console.WriteLine($"{date:m}"); //Output : September 27

            //t : Short time pattern.
            Console.WriteLine($"{date:t}"); //Output : 7:59 PM

            //T : Long time pattern.
            Console.WriteLine($"{date:T}"); //Output : 7:59:24 PM

            //U : Universal full date / time pattern.
            Console.WriteLine($"{date:U}"); //Output : Sunday, September 27, 2020 2:29:24 PM

            //Y or y : Year month pattern.
            Console.WriteLine($"{date:y}"); //Output : September 2020

            //standard format string
            Console.Write($"{date:MMMM-dd-yyyy}");//Output : "September-27-2020"
            Console.WriteLine($"Hello at { DateTime.Now: dddd, MMMM dd, yyyy}");

        }
        static void NumberFormats()
        {
            //C : A currency value.
            Console.WriteLine($"{50000:C}"); //Output : $50,000.00
            Console.WriteLine($"{50000:C1}"); //Output : $50,000.00 -- precision for floating point

            //D : Integer digits with optional negative sign.
            Console.WriteLine($"{50000:D}");  //Output : 50000
            Console.WriteLine($"{50000:D6}"); //Output : 050000

            //E or e : Exponential notation.
            Console.WriteLine($"{1052.0329112756:E}");  //Output : 1.052033E+003
            Console.WriteLine($"{1052.0329112756:e}");  //Output : 1.052033e+003
            Console.WriteLine($"{1052.0329112756:E2}"); //Output : 1.05E+003
            Console.WriteLine($"{1052.0329112756:e2}"); //Output : 1.05e+003

            //F : Integral and decimal digits with optional negative sign.
            Console.WriteLine($"{1234.567:F}");  //Output : 1234.57
            Console.WriteLine($"{1234.567:F3}"); //Output : 1234.567

            //G : The more compact of either fixed-point or scientific notation.
            Console.WriteLine($"{1234.567:G}");  //Output : 1234.567 -- Shows as General Fomat. Same as floating F but the limit applies to both before and after decimal.
            Console.WriteLine($"{1234.567:G3}"); //Output : 1.23E+03 -- Because we limited total number to 3, it shows in exp form

            //N : Integral and decimal digits, group separators, and a decimal separator with optional negative sign.
            Console.WriteLine($"{1234.567:N}");  //Output : 1234.567
            Console.WriteLine($"{1234.567:N2}"); //Output : 1234.57 -- 2 is for floating point precision.

            //P : Number multiplied by 100 and displayed with a percent symbol.
            Console.WriteLine($"{0.32:P}"); //Output : 32%

            //X :  A hexadecimal string.
            Console.WriteLine($"{100:X}"); //Output : 64

            var culture = System.Globalization.CultureInfo.GetCultureInfo("fr-FR");
            FormattableString message = $"{50000:C2}";
            var cultureVariantMessage = message.ToString(culture);
        }
    }
}
