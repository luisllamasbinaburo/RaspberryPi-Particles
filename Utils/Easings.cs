using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpi_Faces.Utils
{
    public static class Easings
    {
        // Linear Easing functions
        public static double EaseLinearNone(double t, double b, double c, double d)
        {
            return (c * t / d + b);
        }

        public static double EaseLinearIn(double t, double b, double c, double d)
        {
            return (c * t / d + b);
        }

        public static double EaseLinearOut(double t, double b, double c, double d)
        {
            return (c * t / d + b);
        }

        public static double EaseLinearInOut(double t, double b, double c, double d)
        {
            return (c * t / d + b);
        }

        // Sine Easing functions
        public static double EaseSineIn(double t, double b, double c, double d)
        {
            return (-c * Math.Cos(t / d * (Math.PI / 2)) + c + b);
        }

        public static double EaseSineOut(double t, double b, double c, double d)
        {
            return (c * Math.Sin(t / d * (Math.PI / 2)) + b);
        }

        public static double EaseSineInOut(double t, double b, double c, double d)
        {
            return (-c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b);
        }

        // Circular Easing functions
        public static double EaseCircIn(double t, double b, double c, double d)
        {
            return (-c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b);
        }

        public static double EaseCircOut(double t, double b, double c, double d)
        {
            return (c * Math.Sqrt(1 - (t = t / d - 1) * t) + b);
        }

        public static double EaseCircInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
            {
                return (-c / 2 * (Math.Sqrt(1 - t * t) - 1) + b);
            }
            return (c / 2 * (Math.Sqrt(1 - t * (t -= 2)) + 1) + b);
        }

        // Cubic Easing functions
        public static double EaseCubicIn(double t, double b, double c, double d)
        {
            return (c * (t /= d) * t * t + b);
        }

        public static double EaseCubicOut(double t, double b, double c, double d)
        {
            return (c * ((t = t / d - 1) * t * t + 1) + b);
        }

        public static double EaseCubicInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
            {
                return (c / 2 * t * t * t + b);
            }
            return (c / 2 * ((t -= 2) * t * t + 2) + b);
        }

        // Quadratic Easing functions
        public static double EaseQuadIn(double t, double b, double c, double d)
        {
            return (c * (t /= d) * t + b);
        }

        public static double EaseQuadOut(double t, double b, double c, double d)
        {
            return (-c * (t /= d) * (t - 2) + b);
        }

        public static double EaseQuadInOut(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
            {
                return (((c / 2) * (t * t)) + b);
            }
            return (-c / 2 * (((t - 2) * (--t)) - 1) + b);
        }

        // Exponential Easing functions
        public static double EaseExpoIn(double t, double b, double c, double d)
        {
            return (t == 0) ? b : (c * Math.Pow(2, 10 * (t / d - 1)) + b);
        }

        public static double EaseExpoOut(double t, double b, double c, double d)
        {
            return (t == d) ? (b + c) : (c * (-Math.Pow(2, -10 * t / d) + 1) + b);
        }

        public static double EaseExpoInOut(double t, double b, double c, double d)
        {
            if (t == 0)
            {
                return b;
            }
            if (t == d)
            {
                return (b + c);
            }
            if ((t /= d / 2) < 1)
            {
                return (c / 2 * Math.Pow(2, 10 * (t - 1)) + b);
            }
            return (c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b);
        }

        // Back Easing functions
        public static double EaseBackIn(double t, double b, double c, double d)
        {
            double s = 1.70158f;
            double postFix = t /= d;
            return (c * (postFix) * t * ((s + 1) * t - s) + b);
        }

        public static double EaseBackOut(double t, double b, double c, double d)
        {
            double s = 1.70158f;
            return (c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b);
        }

        public static double EaseBackInOut(double t, double b, double c, double d)
        {
            double s = 1.70158f;
            if ((t /= d / 2) < 1)
            {
                return (c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b);
            }

            double postFix = t -= 2;
            return (c / 2 * ((postFix) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b);
        }

        // Bounce Easing functions
        public static double EaseBounceOut(double t, double b, double c, double d)
        {
            if ((t /= d) < (1 / 2.75f))
            {
                return (c * (7.5625f * t * t) + b);
            }
            else if (t < (2 / 2.75f))
            {
                double postFix = t -= (1.5f / 2.75f);
                return (c * (7.5625f * (postFix) * t + 0.75f) + b);
            }
            else if (t < (2.5 / 2.75))
            {
                double postFix = t -= (2.25f / 2.75f);
                return (c * (7.5625f * (postFix) * t + 0.9375f) + b);
            }
            else
            {
                double postFix = t -= (2.625f / 2.75f);
                return (c * (7.5625f * (postFix) * t + 0.984375f) + b);
            }
        }

        public static double EaseBounceIn(double t, double b, double c, double d)
        {
            return (c - EaseBounceOut(d - t, 0, c, d) + b);
        }

        public static double EaseBounceInOut(double t, double b, double c, double d)
        {
            if (t < d / 2)
            {
                return (EaseBounceIn(t * 2, 0, c, d) * 0.5f + b);
            }
            else
            {
                return (EaseBounceOut(t * 2 - d, 0, c, d) * 0.5f + c * 0.5f + b);
            }
        }

        // Elastic Easing functions
        public static double EaseElasticIn(double t, double b, double c, double d)
        {
            if (t == 0)
            {
                return b;
            }
            if ((t /= d) == 1)
            {
                return (b + c);
            }

            double p = d * 0.3f;
            double a = c;
            double s = p / 4;
            double postFix = a * Math.Pow(2, 10 * (t -= 1));

            return (-(postFix * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b);
        }

        public static double EaseElasticOut(double t, double b, double c, double d)
        {
            if (t == 0)
            {
                return b;
            }
            if ((t /= d) == 1)
            {
                return (b + c);
            }

            double p = d * 0.3f;
            double a = c;
            double s = p / 4;

            return (a * Math.Pow(2, -10 * t) * Math.Sin((t * d - s) * (2 * Math.PI) / p) + c + b);
        }

        public static double EaseElasticInOut(double t, double b, double c, double d)
        {
            if (t == 0)
            {
                return b;
            }
            if ((t /= d / 2) == 2)
            {
                return (b + c);
            }

            double p = d * (0.3f * 1.5f);
            double a = c;
            double s = p / 4;

            double postFix = 0f;
            if (t < 1)
            {
                postFix = a * Math.Pow(2, 10 * (t -= 1));
                return -0.5f * (postFix * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
            }

            postFix = a * Math.Pow(2, -10 * (t -= 1));

            return (postFix * Math.Sin((t * d - s) * (2 * Math.PI) / p) * 0.5f + c + b);
        }
    }
}
