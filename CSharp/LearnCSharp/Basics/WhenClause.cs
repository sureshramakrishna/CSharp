using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenClause
{
    class Square
    {
        public int Area;
    }
    class Program
    {
        private static void ShowShapeInfo(Square sh)
        {
            try
            {
                switch (sh)
                {
                    case null:
                        Console.WriteLine($"An uninitialized shape");
                        break;
                    case Square shape when sh.Area == 0:
                        Console.WriteLine("No dimensions");
                        break;
                    case Square sq when sh.Area > 0:
                        Console.WriteLine($"Area: {sq.Area}");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex) when (ex.Message.Contains("null"))
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
