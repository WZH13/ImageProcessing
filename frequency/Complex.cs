using System;
using System.Collections.Generic;
using System.Text;

namespace frequency
{
    class Complex
    {
        /// <summary>
        /// 复数的实部
        /// </summary>
        private double real = 0.0;
        /// <summary>
        /// 复数的虚部
        /// </summary>
        private double imaginary = 0.0;

        /// <summary>
        /// 实部的属性
        /// </summary>
        public double Real
        {
            get
            {
                return real;
            }
            set
            {
                real = value;
            }
        }

        /// <summary>
        /// 虚部的属性
        /// </summary>
        public double Imaginary
        {
            get
            {
                return imaginary;
            }
            set
            {
                imaginary = value;
            }
        }

        /// <summary>
        /// 基本构造函数
        /// </summary>
        public Complex()
        {
        }

        /// <summary>
        /// 指定值的构造函数
        /// </summary>
        public Complex(double dbreal, double dbimag)
        {
            real = dbreal;
            imaginary = dbimag;
        }

        /// <summary>
        /// 复制构造函数
        /// </summary>
        public Complex(Complex other)
        {
            real = other.real;
            imaginary = other.imaginary;
        }

        /// <summary>
        /// 重载+运算符
        /// </summary>
        public static Complex operator +(Complex comp1, Complex comp2)
        {
            return comp1.Add(comp2);
        }

        /// <summary>
        /// 重载-运算符
        /// </summary>
        public static Complex operator -(Complex comp1, Complex comp2)
        {
            return comp1.Subtract(comp2);
        }

        /// <summary>
        /// 重载*运算符
        /// </summary>
        public static Complex operator *(Complex comp1, Complex comp2)
        {
            return comp1.Multiply(comp2);
        }

        /// <summary>
        /// 实现复数加法
        /// </summary>
        public Complex Add(Complex comp)
        {
            double x = real + comp.real;
            double y = imaginary + comp.imaginary;

            return new Complex(x, y);
        }

        /// <summary>
        /// 实现复数减法
        /// </summary>
        public Complex Subtract(Complex comp)
        {
            double x = real - comp.real;
            double y = imaginary - comp.imaginary;

            return new Complex(x, y);
        }

        /// <summary>
        /// 实现复数乘法
        /// </summary>
        public Complex Multiply(Complex comp)
        {
            double x = real * comp.real - imaginary * comp.imaginary;
            double y = real * comp.imaginary + imaginary * comp.real;

            return new Complex(x, y);
        }

        /// <summary>
        /// 求幅度
        /// </summary>
        public double Abs()
        {
            //取得实部的绝对值
            double x = Math.Abs(real);
            //取得虚部的绝对值
            double y = Math.Abs(imaginary);

            //实部为0
            if (real == 0)
            {
                return y;
            }
            //虚部为0
            if (imaginary == 0)
            {
                return x;
            }

            //计算模
            if (x > y)
            {
                return (x * Math.Sqrt(1 + (y / x) * (y / x)));      //模计算公式：∣z∣=√（a^2+b^2)
            }
            else
            {
                return (y * Math.Sqrt(1 + (x / y) * (x / y)));
            }
        }

        /// <summary>
        /// 求相位角
        /// </summary>
        public double Angle()
        {
            //实部和虚部都为0
            if (real == 0 && imaginary == 0)
                return 0;

            if (real == 0)
            {
                //实部为0
                if (imaginary > 0)
                    return Math.PI / 2;
                else
                    return -Math.PI / 2;
            }
            else
            {
                if (real > 0)
                    //实部大于0
                    return Math.Atan2(imaginary, real);
                else
                {
                    //实部小于0
                    if (imaginary >= 0)
                        return Math.Atan2(imaginary, real) + Math.PI;
                    else
                        return Math.Atan2(imaginary, real) - Math.PI;
                }
            }
        }

        /// <summary>
        /// 共轭复数
        /// </summary>
        public Complex Conjugate()
        {
            return new Complex(this.real, -this.imaginary);
        }
    }
}
