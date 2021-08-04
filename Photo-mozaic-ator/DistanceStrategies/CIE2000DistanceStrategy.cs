using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Photo_mozaic_ator.DistanceStrategies
{

    /// <summary>
    /// CIE 1976 distance implementation. Translates colros to XYZ and then Lab reprezentation. Takes square distance of Lab colors.
    /// </summary>
    class CIE2000DistanceStrategy : IDistanceStrategy
    {
        // CIE76 color distance
        // rgb -> XYZ -> Lab
        // compare 2 Lab colors with eucleideian norm

        public int Distance(Color a, Color b)
        {
            var AinLAB = rgb2lab(a.R, a.G,a.B);
            var BinLAB = rgb2lab(b.R, b.G,b.B);

            // x1000 is for keeping accuracy
            return (int)(DE00(AinLAB[0], AinLAB[1], AinLAB[2], BinLAB[0], BinLAB[1], BinLAB[2])*1000);
        }

        static float Gamma(float x)
        {
            return x > 0.04045f ? MathF.Pow((x + 0.055f) / 1.055f, 2.4f) : x / 12.92f;
        }

        /// <summary>
        /// Translates first to XYZ and then to Lab representation.
        /// </summary>
        /// <param name="var_R"></param>
        /// <param name="var_G"></param>
        /// <param name="var_B"></param>
        /// <returns></returns>
        public static float[] rgb2lab(float var_R, float var_G, float var_B)
        {
            var_R /= 255.0f;
            var_G /= 255.0f;
            var_B /= 255.0f;

            float[] arr = new float[3];
            float B = Gamma(var_B);
            float G = Gamma(var_G);
            float R = Gamma(var_R);
            float X = 0.412453f * R + 0.357580f * G + 0.180423f * B;
            float Y = 0.212671f * R + 0.715160f * G + 0.072169f * B;
            float Z = 0.019334f * R + 0.119193f * G + 0.950227f * B;

            X /= 0.95047f;
            Y /= 1.0f;
            Z /= 1.08883f;

            float FX = X > 0.008856f ? MathF.Pow(X, 1.0f / 3.0f) : (7.787f * X + 0.137931f);
            float FY = Y > 0.008856f ? MathF.Pow(Y, 1.0f / 3.0f) : (7.787f * Y + 0.137931f);
            float FZ = Z > 0.008856f ? MathF.Pow(Z, 1.0f / 3.0f) : (7.787f * Z + 0.137931f);
            arr[0] = Y > 0.008856f ? (116.0f * FY - 16.0f) : (903.3f * Y);
            arr[1] = 500f * (FX - FY);
            arr[2] = 200f * (FY - FZ);
            return arr;

        }

        public override string ToString()
        {
            return "CIE2000 Distance Strategy";
        }

        ///----------------------------------------------------------NEW

        private readonly double _l1S;
        private readonly double _a1S;
        private readonly double _b1S;

        private const double Pi = Math.PI;
        private const double Pi2 = 2.0 * Math.PI;

        private const double kL = 1.0, kC = 1.0, kH = 1.0;

                
        /**
         * Implementation of 
         * "The CIEDE2000 Color-Difference Formula: Implementation Notes, Supplementary Test Data, and Mathematical Observations".
         */
        public double DE00(double _l1S, double _a1S, double _b1S, double l2s, double a2s, double b2s)
        {
            var mCs = (Math.Sqrt(_a1S * _a1S + _b1S * _b1S) + Math.Sqrt(a2s * a2s + b2s * b2s)) / 2.0;
            var G = 0.5 * (1.0 - Math.Sqrt(Math.Pow(mCs, 7) / (Math.Pow(mCs, 7) + Math.Pow(25.0, 7))));
            var a1p = (1.0 + G) * _a1S;
            var a2p = (1.0 + G) * a2s;
            var C1p = Math.Sqrt(a1p * a1p + _b1S * _b1S);
            var C2p = Math.Sqrt(a2p * a2p + b2s * b2s);

            var h1p = Math.Abs(a1p) + Math.Abs(_b1S) > double.Epsilon ? Math.Atan2(_b1S, a1p) : 0.0;
            if (h1p < 0.0) h1p += Pi2;
            var h2p = Math.Abs(a2p) + Math.Abs(b2s) > double.Epsilon ? Math.Atan2(b2s, a2p) : 0.0;
            if (h2p < 0.0) h2p += Pi2;

            var dLp = l2s - _l1S;
            var dCp = C2p - C1p;

            var dhp = 0.0;
            var cProdAbs = Math.Abs(C1p * C2p);
            if (cProdAbs > double.Epsilon && Math.Abs(h1p - h2p) <= Pi)
            {
                dhp = h2p - h1p;
            }
            else if (cProdAbs > double.Epsilon && h2p - h1p > Pi)
            {
                dhp = h2p - h1p - Pi2;
            }
            else if (cProdAbs > Double.Epsilon && h2p - h1p < -Pi)
            {
                dhp = h2p - h1p + Pi2;
            }

            var dHp = 2.0 * Math.Sqrt(C1p * C2p) * Math.Sin(dhp / 2.0);

            var mLp = (_l1S + l2s) / 2.0;
            var mCp = (C1p + C2p) / 2.0;

            var mhp = 0.0;
            if (cProdAbs > double.Epsilon && Math.Abs(h1p - h2p) <= Pi)
            {
                mhp = (h1p + h2p) / 2.0;
            }
            else if (cProdAbs > double.Epsilon && Math.Abs(h1p - h2p) > Pi && h1p + h2p < Pi2)
            {
                mhp = (h1p + h2p + Pi2) / 2.0;
            }
            else if (cProdAbs > double.Epsilon && Math.Abs(h1p - h2p) > Pi && h1p + h2p >= Pi2)
            {
                mhp = (h1p + h2p - Pi2) / 2.0;
            }
            else if (cProdAbs <= double.Epsilon)
            {
                mhp = h1p + h2p;
            }

            var T = 1.0 - 0.17 * Math.Cos(mhp - Pi / 6.0) + .24 * Math.Cos(2.0 * mhp) +
                0.32 * Math.Cos(3.0 * mhp + Pi / 30.0) - 0.2 * Math.Cos(4.0 * mhp - 7.0 * Pi / 20.0);
            var dTheta = Pi / 6.0 * Math.Exp(-Math.Pow((mhp / (2.0 * Pi) * 360.0 - 275.0) / 25.0, 2));
            var RC = 2.0 * Math.Sqrt(Math.Pow(mCp, 7) / (Math.Pow(mCp, 7) + Math.Pow(25.0, 7)));
            var mlpSqr = (mLp - 50.0) * (mLp - 50.0);
            var SL = 1.0 + 0.015 * mlpSqr / Math.Sqrt(20.0 + mlpSqr);
            var SC = 1.0 + 0.045 * mCp;
            var SH = 1.0 + 0.015 * mCp * T;
            var RT = -Math.Sin(2.0 * dTheta) * RC;

            var de00 = Math.Sqrt(
                Math.Pow(dLp / (kL * SL), 2) + Math.Pow(dCp / (kC * SC), 2) + Math.Pow(dHp / (kH * SH), 2) +
                RT * dCp / (kC * SC) * dHp / (kH * SH)
            );
            return de00;
        }
    }
}
