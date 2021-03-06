﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.Equasion_methods;

namespace library
{
    class GaussIntegralMethod : IntegralMethod
    {
        private readonly double epsilan = Math.Pow(10, -4);

        private int power;
        private int powerForCoefCalculation = 10;
        private int powerForIntegralCalculation = 50;

        public GaussIntegralMethod(int power = 2)
        {
            this.power = power;
        }

        public double Solve(MathFunction func, double a, double b, int n)
        {
            double result = 0,
                difference = (b - a) / n,
                left = a,
                right = left + difference;
            double[] solutions = SolutionsForLezandrPolynomial(powerForCoefCalculation),
                     coefs = QuadratureCoefs(solutions, powerForCoefCalculation);
            
            for (int i = 0; i < n; i ++)
            {
                result += SumForIntegralApproximation(func, left, right, solutions, coefs);
                left = right;
                right += difference;
            }

            return result;
        }

        private double SumForIntegralApproximation(MathFunction func, double a, double b, double[] solutions, double[] coefs)
        {
            double result = 0;

            for (int i = 0; i <= powerForCoefCalculation; i++)
            {
                double x = (a + b) / 2 + (b - a) / 2 * solutions[i];
                result += coefs[i] * func.Calculate(x);
            }

            result *= (b - a) / 2;

            return result;
        }

        private double[] QuadratureCoefs(double[] solutions, int n)
        {
            double[] result = new double[n + 1];
            for (int i = 0; i <= n; i++)
                result[i] = CalculateQuadratureCoef(i, solutions, n);

            return result;
        }

        private double CalculateQuadratureCoef(int index, double[] solutions, int n)
        {
            double Ai = 0;

            MathFunction func = new ConstFunction(1.0d);

            for (int i = 0; i <= n; i++)
            {
                if (i != index)
                    func *= (new XFunction(1.0d) - solutions[i]) / (solutions[index] - solutions[i]);
            }

            Ai = func.CalculateDeterminedIntegral(new SimpsonMethod(), -1, 1, powerForIntegralCalculation);

            return Ai;
        }

        private MathFunction LezandrPolynomial(int n)
        {
            if (n == 0)
                return new ConstFunction(1.0d);
            if (n == 1)
                return new XFunction(1.0d);

            return (double)(2 * n - 1) / n * new XFunction(1.0d) * LezandrPolynomial(n - 1) -
                (double)(n - 1) / n * LezandrPolynomial(n - 2);
        }

        private double[] SolutionsForLezandrPolynomial(int n)
        {
            MathFunction poly = LezandrPolynomial(n + 1);
            Equasion eq = new Equasion(poly, new ConstFunction(0));
            EquasionMethod method = new HalfDivision();

            double left = -1,
                   right = -1 + 2 * epsilan;

            double[] solutions = new double[n + 1];
            int bound = n % 2 == 0 ? n / 2 : (n + 1) / 2;
            int index = 0;

            while (index < bound &&
                   left < 0)
            {
                try
                {
                    solutions[index] = method.Solve(eq, left, right);
                    left = solutions[index] + epsilan;
                    right = left + 2 * epsilan;
                    index++;
                } catch
                {
                    left += 2 * epsilan;
                    right += 2 * epsilan;
                }
                
            }

            for (int i = 0; i < bound; i++)
                solutions[n - i] = -solutions[i];

            return solutions;
        }
    }
}
