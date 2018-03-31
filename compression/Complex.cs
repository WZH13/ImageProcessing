using System;
using System.Collections.Generic;
using System.Text;

namespace compression
{
    class Complex
    {
        private double real = 0.0;
        private double imaginary = 0.0;

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

        public Complex()
        {
        }

        public Complex(double dbreal, double dbimag)
        {
            real = dbreal;
            imaginary = dbimag;
        }

        public Complex(Complex other)
        {
            real = other.real;
            imaginary = other.imaginary;
        }

        public static Complex operator +(Complex comp1, Complex comp2)
        {
            return comp1.Add(comp2);
        }

        public static Complex operator -(Complex comp1, Complex comp2)
        {
            return comp1.Subtract(comp2);
        }

        public static Complex operator *(Complex comp1, Complex comp2)
        {
            return comp1.Multiply(comp2);
        }

        public Complex Add(Complex comp)
        {
            double x = real + comp.real;
            double y = imaginary + comp.imaginary;

            return new Complex(x, y);
        }

        public Complex Subtract(Complex comp)
        {
            double x = real - comp.real;
            double y = imaginary - comp.imaginary;

            return new Complex(x, y);
        }

        public Complex Multiply(Complex comp)
        {
            double x = real * comp.real - imaginary * comp.imaginary;
            double y = real * comp.imaginary + imaginary * comp.real;

            return new Complex(x, y);
        }

        public double Abs()
        {
            double x = Math.Abs(real);
            double y = Math.Abs(imaginary);

            if (real == 0)
            {
                return y;
            }
            if (imaginary == 0)
            {
                return x;
            }

            if (x > y)
            {
                return (x * Math.Sqrt(1 + (y / x) * (y / x)));
            }
            else
            {
                return (y * Math.Sqrt(1 + (x / y) * (x / y)));
            }
        }

        public double Angle()
        {
            if (real == 0 && imaginary == 0)
                return 0;

            if (real == 0)
            {
                if (imaginary > 0)
                    return Math.PI / 2;
                else
                    return -Math.PI / 2;
            }
            else
            {
                if (real > 0)
                    return Math.Atan2(imaginary, real);
                else
                {
                    if (imaginary >= 0)
                        return Math.Atan2(imaginary, real) + Math.PI;
                    else
                        return Math.Atan2(imaginary, real) - Math.PI;
                }
            }
        }

        public Complex Conjugate()
        {
            return new Complex(this.real, -this.imaginary);
        }
    }
}
