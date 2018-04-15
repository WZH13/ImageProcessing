using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    /// <summary>
    /// 本类提供了对byte数据的常用操作函数
    /// </summary>
    public class ByteUtil
    {
        private static char[] HEX_CHARS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static byte[] BITS = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };


        /// <summary>
        /// 将字节数组转换为HEX形式的字符串, 使用指定的间隔符
        /// </summary>
        public static string ByteToHex(byte[] buf, string separator)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(HEX_CHARS[buf[i] >> 4]).Append(HEX_CHARS[buf[i] & 0x0F]);
            }
            return sb.ToString();
        }




        /// <summary>
        /// 将字节数组转换为HEX形式的字符串, 使用指定的间隔符
        /// </summary>
        public static string ByteToHex(byte[] buf, char c)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(c);
                }
                sb.Append(HEX_CHARS[buf[i] >> 4]).Append(HEX_CHARS[buf[i] & 0x0F]);
            }
            return sb.ToString();
        }
        //byte[] b = { 8, 46, 58, 127, 239, 255 };
        //08|2E|3A|7F|EF|FF


        /// <summary>
        /// 判断字节数组前几位是否符合一定规则
        /// </summary>
        /// <param name="data">需要判断的字节数组</param>
        /// <param name="pattern">匹配规则</param>
        /// <returns>如果匹配返回true</returns>
        public static bool IsMatch(byte[] data, params byte[] pattern)
        {
            if (data == null || data.Length < pattern.Length)
                return false;


            for (int i = 0; i < pattern.Length; i++)
            {
                if (data[i] != pattern[i])
                    return false;
            }
            return true;
        }


        /// <summary>
        /// 判断指定字节是否为列举的某个值
        /// </summary>
        /// <param name="value">需要判断的值</param>
        /// <param name="choice">可能值</param>
        /// <returns>如果与任一个可能值相等则返回true</returns>
        public static bool IsMatch(byte value, params byte[] choice)
        {
            if (choice == null || choice.Length == 0)
                return false;


            foreach (byte item in choice)
            {
                if (item == value)
                    return true;
            }
            return false;
        }




        /// <summary>
        /// 将字节数组转换为HEX形式的字符串, 没有间隔符
        /// </summary>
        public static string ByteToHex(byte[] buf)
        {
            return ByteToHex(buf, string.Empty);
        }




        /// <summary>
        /// 将字节数组转换为HEX形式的字符串
        /// 转换后的字符串长度为字节数组长度的两倍
        /// 如: 1, 2 转换为 0102
        /// </summary>
        public static string ByteToHex(byte b)
        {
            return string.Empty + HEX_CHARS[b >> 4] + HEX_CHARS[b & 0x0F];
        }




        /// <summary>
        /// 将字节流信息转换为HEX字符串
        /// </summary>
        public static string DumpBytes(byte[] bytes)
        {
            return DumpBytes(bytes, 0, bytes.Length);
        }


        /// <summary>
        /// 将字节流信息转换为HEX字符串
        /// </summary>
        public static string DumpBytes(byte[] bytes, int offset, int len)
        {
            StringBuilder buf = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                if (i == 0 || i % 16 == 0)
                    buf.AppendLine();

                buf.Append(ByteToHex(bytes[i + offset]));
                buf.Append(' ');
            }
            buf.AppendLine();
            return buf.ToString();
        }




        /// <summary>
        /// 计算字节块的模256校验和
        /// </summary>
        public static byte SumBytes(byte[] bytes, int offset, int len)
        {
            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += bytes[i + offset];
                if (sum >= 256)
                {
                    sum = sum % 256;
                }
            }
            return (byte)sum;
        }


        /// <summary>
        /// 计算字节块的模256双字节校验和(低位在前)
        /// </summary>
        public static byte[] Sum2Bytes(byte[] bytes, int offset, int len)
        {
            int sum = 0;
            for (int i = 0; i < len; i++)
                sum += bytes[i + offset];
            return new byte[] { (byte)(sum % 256), (byte)(sum / 256) };
        }


        /// <summary>
        /// 计算字节块的异或校验和
        /// </summary>
        public static byte XorSumBytes(byte[] bytes, int offset, int len)
        {
            byte sum = bytes[0 + offset];
            for (int i = 1; i < len; i++)
            {
                sum = (byte)(sum ^ bytes[i + offset]);
            }
            return sum;
        }




        /// <summary>
        /// 计算字节块的异或校验和
        /// </summary>
        public static byte XorSumBytes(byte[] bytes)
        {
            return XorSumBytes(bytes, 0, bytes.Length);
        }




        /// <summary>
        /// 比较两个字节块是否相等。相等返回true否则false
        /// </summary>
        public static bool CompareBytes(byte[] bytes1, int offset1, byte[] bytes2, int offset2, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (bytes1[i + offset1] != bytes2[i + offset2])
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 将两个字符的hex转换为byte
        /// </summary>
        public static byte HexToByte(char[] hex, int offset)
        {
            byte result = 0;
            for (int i = 0; i < 2; i++)
            {
                char c = hex[i + offset];
                byte b = 0;
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        b = (byte)(c - '0');
                        break;
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                        b = (byte)(10 + c - 'A');
                        break;
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                        b = (byte)(10 + c - 'a');
                        break;
                }
                if (i == 0)
                {
                    b = (byte)(b * 16);
                }
                result += b;
            }


            return result;
        }


        /// <summary>
        /// 将两个字符的hex转换为byte
        /// </summary>
        public static byte HexToByte(byte[] hex, int offset)
        {
            char[] chars = { (char)hex[offset], (char)hex[offset + 1] };
            return HexToByte(chars, 0);
        }


        /// <summary>
        /// 转换16进制字符串为字节数组
        /// <param name="hex">有分隔或无分隔的16进制字符串，如“AB CD EF 12 34...”或“ABCDEF1234...”</param>
        /// <param name="dot">任意分隔字符，但不能是16进制字符</param>
        /// <returns>字节数组</returns>
        /// </summary>
        public static byte[] HexToByte(string hex, params char[] dot)
        {
            char[] ca = new char[2];
            List<byte> list = new List<byte>();
            for (int i = 0, n = 0; i < hex.Length; i++)
            {
                if (Array.IndexOf<char>(dot, hex[i]) >= 0)
                {
                    continue;
                }


                switch (++n)
                {
                    case 1:
                        ca[0] = hex[i];
                        break;


                    case 2:
                        ca[1] = hex[i];
                        list.Add(ByteUtil.HexToByte(ca, 0));
                        n = 0;
                        break;
                }
            }


            return list.ToArray();
        }


        /// <summary>
        /// 将uint变量分解为四个字节。高位在前。
        /// </summary>
        public static void UintToBytes(uint i, byte[] bytes, int offset)
        {
            bytes[offset] = (byte)((i & 0xFF000000) >> 24);
            bytes[offset + 1] = (byte)((i & 0x00FF0000) >> 16);
            bytes[offset + 2] = (byte)((i & 0x0000FF00) >> 8);
            bytes[offset + 3] = (byte)(i & 0x000000FF);
        }


        /// <summary>
        /// 将uint变量分解为四个字节。高位在前。
        /// </summary>
        public static byte[] UintToBytes(uint i)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)((i & 0xFF000000) >> 24);
            bytes[1] = (byte)((i & 0x00FF0000) >> 16);
            bytes[2] = (byte)((i & 0x0000FF00) >> 8);
            bytes[3] = (byte)(i & 0x000000FF);
            return bytes;
        }


        /// <summary>
        /// 将int变量分解为四个字节。高位在前。
        /// </summary>
        public static byte[] IntToBytes(int i)
        {
            byte[] data = BitConverter.GetBytes(i);
            Array.Reverse(data);
            return data;


            //byte[] bytes = new byte[4];
            //bytes[0] = (byte)((i & 0xFF000000) >> 24);
            //bytes[1] = (byte)((i & 0x00FF0000) >> 16);
            //bytes[2] = (byte)((i & 0x0000FF00) >> 8);
            //bytes[3] = (byte)(i & 0x000000FF);
            //return bytes;
        }


        /// <summary>
        /// 将四个字节合成为一个int
        /// </summary>
        public static uint BytesToUint(byte[] bytes, int offset)
        {
            uint a = ((uint)bytes[offset]) << 24;
            uint b = ((uint)bytes[offset + 1]) << 16;
            uint c = ((uint)bytes[offset + 2]) << 8;
            uint d = bytes[offset + 3];
            return a + b + c + d;
        }


        /// <summary>
        /// 将ulong变量分解为八个字节。高位在前。
        /// </summary>
        public static byte[] UlongToBytes(ulong i)
        {
            byte[] bytes = new byte[8];
            bytes[0] = (byte)((i & 0xFF00000000000000) >> 56);
            bytes[1] = (byte)((i & 0x00FF000000000000) >> 48);
            bytes[2] = (byte)((i & 0x0000FF0000000000) >> 40);
            bytes[3] = (byte)((i & 0x000000FF00000000) >> 32);
            bytes[4] = (byte)((i & 0x00000000FF000000) >> 24);
            bytes[5] = (byte)((i & 0x0000000000FF0000) >> 16);
            bytes[6] = (byte)((i & 0x000000000000FF00) >> 8);
            bytes[7] = (byte)(i & 0x00000000000000FF);
            return bytes;
        }


        /// <summary>
        /// 将八个字节合成为一个ulong
        /// </summary>
        public static ulong BytesToUlong(byte[] bytes, int offset)
        {
            ulong a = ((ulong)bytes[offset]) << 56;
            ulong b = ((ulong)bytes[offset + 1]) << 48;
            ulong c = ((ulong)bytes[offset + 2]) << 40;
            ulong d = ((ulong)bytes[offset + 3]) << 32;
            ulong e = ((ulong)bytes[offset + 4]) << 24;
            ulong f = ((ulong)bytes[offset + 5]) << 16;
            ulong g = ((ulong)bytes[offset + 6]) << 8;
            ulong h = bytes[offset + 7];
            return a + b + c + d + e + f + g + h;
        }




        /// <summary>
        /// 设置某个字节的指定位
        /// </summary>
        /// <param name="b">需要设置的字节</param>
        /// <param name="pos">1-8, 1表示最低位, 8表示最高位</param>
        /// <param name="on">true表示设置1, false表示设置0</param>
        public static void ByteSetBit(ref byte b, int pos, bool on)
        {
            int temp = BITS[pos - 1];


            if (!on)
            {
                //取反
                temp = temp ^ 0xFF;
            }


            b = (byte)(on ? (b | temp) : (b & temp));
        }


        /// <summary>
        /// 判断某个byte的某个位是否为1
        /// </summary>
        /// <param name="pos">第几位，大于等于1</param>
        public static bool ByteGetBit(byte b, int pos)
        {
            int temp = BITS[pos - 1];
            return (b & temp) != 0;
        }




        /// <summary>
        /// 设置双比特值
        /// </summary>
        /// <param name="b">需要设置的字节</param>
        /// <param name="low">低位, 1-7</param>
        /// <param name="val">值，0-3</param>
        /// <returns></returns>
        public static void ByteSetBitPair(ref byte b, int low, int val)
        {
            if (low < 1 || low > 7)
            {
                throw new ArgumentException(string.Format("无效的low值:{0}", low));
            }


            switch (val)
            {
                case 0:
                    {
                        ByteUtil.ByteSetBit(ref b, low, false);
                        ByteUtil.ByteSetBit(ref b, low + 1, false);
                        break;
                    }
                case 1:
                    {
                        ByteUtil.ByteSetBit(ref b, low, true);
                        ByteUtil.ByteSetBit(ref b, low + 1, false);
                        break;
                    }
                case 2:
                    {
                        ByteUtil.ByteSetBit(ref b, low, false);
                        ByteUtil.ByteSetBit(ref b, low + 1, true);
                        break;
                    }
                case 3:
                    {
                        ByteUtil.ByteSetBit(ref b, low, true);
                        ByteUtil.ByteSetBit(ref b, low + 1, true);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException(string.Format("无效的val值:{0}", val));
                    }
            }
        }




        /// <summary>
        /// 读取双比特值
        /// </summary>
        /// <param name="b">需要读取的字节</param>
        /// <param name="low">低位, 0-6</param>
        /// <returns>0-3</returns>
        public static byte ByteGetBitPair(byte b, int low)
        {
            if (low < 0 || low > 7)
            {
                throw new ArgumentException(string.Format("无效的low值:{0}", low));
            }


            int x = 0;
            x += ByteUtil.ByteGetBit(b, low) ? 1 : 0;
            x += ByteUtil.ByteGetBit(b, low + 1) ? 2 : 0;


            return (byte)x;
        }


        /// <summary>
        /// 将short转换为两个字节
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ShortToByte(short s)
        {
            return UshortToByte((ushort)s);
        }


        /// <summary>
        /// 将ushort转换为两个字节
        /// </summary>
        public static byte[] UshortToByte(ushort u)
        {
            return new byte[]{
(byte)(u >> 8),
(byte)(u & 0x00FF)
};
        }


        /// <summary>
        /// 将两个字节转换为一个short
        /// </summary>
        public static short BytesToShort(byte[] data, int offset)
        {
            short a = data[offset], b = data[offset + 1];
            return (short)((a << 8) + b);
        }


        /// <summary>
        /// 将两个字节转换为一个short
        /// </summary>
        public static ushort BytesToUshort(byte[] data, int offset)
        {
            ushort a = data[offset], b = data[offset + 1];
            return (ushort)((a << 8) + b);
        }

        /// <summary>
        /// 将四个字节转换为int
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int BytesToInt(byte[] data, int offset)
        {
            return (data[offset] << 24) + (data[offset + 1] << 16) + (data[offset + 2] << 8) + data[offset + 3];
        }


        /// <summary>
        /// 将guid字符串转换为等价的16维字节数组
        /// </summary>
        public static byte[] GuidToBytes(string s)
        {
            byte[] guid = new byte[16];
            char[] hex = s.Replace("-", string.Empty).Replace(" ", string.Empty).ToCharArray();
            for (int i = 0; i < 32; i += 2)
            {
                guid[i / 2] = ByteUtil.HexToByte(hex, i);
            }
            return guid;
        }


        /// <summary>
        /// CRC16校验表
        /// </summary>
        static ushort[] wCRCTalbeAbs = { 0x0000, 0xCC01, 0xD801, 0x1400, 0xF001, 0x3C00, 0x2800, 0xE401, 0xA001, 0x6C00, 0x7800, 0xB401, 0x5000, 0x9C01, 0x8801, 0x4400 };


        /// <summary>
        /// 计数字节块的CRC16校验值
        /// </summary>
        public static int CRC16Bytes(byte[] bytes, int offset, int len)
        {
            int wCRC = 0xFFFF;
            byte chChar;


            for (int i = offset; i < len; i++)
            {
                chChar = bytes[i];
                wCRC = wCRCTalbeAbs[(chChar ^ wCRC) & 15] ^ (wCRC >> 4);
                wCRC = wCRCTalbeAbs[((chChar >> 4) ^ wCRC) & 15] ^ (wCRC >> 4);
            }


            return wCRC;
        }


        /// <summary>
        /// 字节格式化，将字节转换为字节、KB、MB、GB显示
        /// </summary>
        /// <param name="bytes">字节数</param>
        /// <returns>格式化后的字符串</returns>
        public static string ByteFormater(long bytes)
        {
            const long KB = 1024;
            const long MB = 1024 * 1024;
            const long GB = 1024 * 1024 * 1024;


            if (bytes >= GB)
            {
                double result = bytes * 1.0 / GB;
                return result.ToString("#,##0.0") + "GB";
            }
            if (bytes >= MB)
            {
                double result = bytes * 1.0 / MB;
                return result.ToString("#,##0.0") + "MB";
            }
            if (bytes >= KB)
            {
                double result = bytes * 1.0 / KB;
                return result.ToString("#,##0.0") + "KB";
            }
            return bytes.ToString("#,##0") + "字节";
        }
    }
}
