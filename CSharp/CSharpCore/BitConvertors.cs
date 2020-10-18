using System;
using System.Collections.Generic;
using System.Text;

namespace BitConvertors
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = BitConverter.GetBytes(1);
            int resultInt = BitConverter.ToInt32(bytes);
            bool resultBool = BitConverter.ToBoolean(bytes);
            char resultChar = BitConverter.ToChar(bytes);
            double resultDouble = BitConverter.ToDouble(bytes);
            short resultInt16 = BitConverter.ToInt16(bytes);
            long resultInt64 = BitConverter.ToInt64(bytes);
            ushort resultUInt16 = BitConverter.ToUInt16(bytes);
            uint resultUInt32 = BitConverter.ToUInt32(bytes);
            ulong resultUInt64 = BitConverter.ToUInt64(bytes);
        }
    }
}
