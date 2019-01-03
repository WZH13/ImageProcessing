using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class SeqStack<T>
    {
        private int maxSize;// 顺序栈的容量

        public int MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }
        public T[] data; // 数组，用于存储顺序栈中的数据元素
        private int top; // 指示顺序栈的栈顶

        public int Top
        {
            get { return top; }
        }
        //构造器
        public SeqStack(int size)
        {
            data = new T[size];
            maxSize = size;
            top = -1;
        }
        // 定义索引器
        public T this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
        // 由于数组是 0 基数组，即数组的最小索引为 0，所以，顺序栈的长度就是数组中最后一个元素的索引 top 加 1
        public int GetLength()
        {
            return top + 1;
        }

        public bool IsEmpty()
        {
            if (-1 == top)
            {
                return true;
            }
            return false;
        }
        public bool IsFull()
        {
            if (top == maxSize - 1)
            {
                return true;
            }
            return false;
        }
        public void Push(T item)
        {
            if (IsFull())
            {
                Console.WriteLine("栈已满，无法压栈");
                return;
            }
            data[++top] = item;
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                //Console.WriteLine("栈为空，无法弹栈");
                return default(T);
            }
            return data[top--];
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                //Console.WriteLine("栈为空，无法弹栈");
                return default(T);
            }
            return data[top];
        }
    }
}
