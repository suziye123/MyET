using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
     public  static class ValueExtension
    {
        public static byte ToByte(this string str)
        {
            return (byte)str.ToInt();
        }

        public static int ToInt(this string str)
        {
            int temp = int.Parse(str);
            return temp;
        }

        public static UInt32 ToUInt32(this string str)
        {
            UInt32 temp = UInt32.Parse(str);
            return temp;
        }

        public static byte ToByte(this uint _t)
        {
            return Convert.ToByte(_t);
        }
    }

    public static class ByteExtension
    {
        public static byte ToByte(this ushort us)
        {
            return Convert.ToByte(us);
        }

        public static string BytesToString(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2") + " ");
            }
            return sb.ToString();
        }

        public static string BytesToString(this ushort[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i] + " ");
            }
            return sb.ToString();
        }

        public static string InitToString(this Int64[] ints)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ints.Length; i++)
            {
                sb.Append(ints[i] + " ");
            }
            return sb.ToString();
        }

        public static string LongToString(this long[] longs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < longs.Length; i++)
            {
                sb.Append(longs[i] + " ");
            }
            return sb.ToString();
        }

        public static byte IntToByte(this int _int)
        {
            return Convert.ToByte(_int);
        }

        public static int ToInt(this ushort us)
        {
            return Convert.ToInt32(us);
        }
    }
}
