using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class BitBuffer
    {
        private byte[] m_data = null;
        private int m_dataLen = 0;
        private int m_pos = 0;

        public BitBuffer(byte[] data)
        {
            m_data = data;
            m_dataLen = data.Length;
        }

        public BitBuffer(byte[] data, int len)
        {
            m_data = data;
            m_dataLen = len;
        }

        public void Skip(int offset)
        {
            m_pos += offset;

            if (m_dataLen * 8 < m_pos)
            {
                throw new Exception("m_data.Length * 8 < m_offset");
            }
        }

        public int Read(int size, bool noseek = false)
        {
            int starBytePos = m_pos / 8;
            int starBitOffset = m_pos % 8;
            int starBitMask = 0;
            for (int i = 0; i < 8 - starBitOffset; ++i)
            {
                starBitMask <<= 1;
                starBitMask += 1;
            }

            int spanByte = (size + 7) / 8;
            if (4 < spanByte)
            {
                throw new Exception("4 < spanByte");
            }

            int value = 0;
            for (int i = 0; i < spanByte; ++i)
            {
                value <<= 8;
                value += m_data[starBytePos + i];

                if (0 == i)
                {
                    value &= starBitMask;
                }
            }

            int endBitOffset = (m_pos + size) % 8;
            if (0 < endBitOffset)
            {
                value >>= (8 - endBitOffset);
            }

            if (!noseek)
            {
                Skip(size);
            }

            return value;
        }

        public byte[] ReadBytes(int size, bool noseek = false)
        {
            if (size <= 0)
            {
                return null;
            }

            byte[] temp = new byte[size / 8];

            Array.Copy(m_data, m_pos / 8, temp, 0, size / 8);

            if (!noseek)
            {
                Skip(size);
            }

            return temp;
        }
    }
}
