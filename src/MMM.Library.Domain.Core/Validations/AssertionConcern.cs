using MMM.Library.Domain.Core.Messages;
using System.Text.RegularExpressions;

namespace MMM.Library.Domain.Core.Validations
{
    public class AssertionConcern
    {
        public static void ValidateIfEqual(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfNotEqual(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfNotEqual(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateSize(string value, int maximo, string message)
        {
            var length = value.Trim().Length;
            if (length > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateSize(string value, int minimo, int maximo, string message)
        {
            var length = value.Trim().Length;
            if (length < minimo || length > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMaxMin(double value, double minimo, double maximo, string message)
        {
            if (value < minimo || value > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinimoMaximo(float value, float minimo, float maximo, string message)
        {
            if (value < minimo || value > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(int value, int minimo, int maximo, string message)
        {
            if (value < minimo || value > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(long value, long minimo, long maximo, string message)
        {
            if (value < minimo || value > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateMinMax(decimal value, decimal minimo, decimal maximo, string message)
        {
            if (value < minimo || value > maximo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(decimal value, decimal minimo, string message)
        {
            if (value < minimo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(double value, double minimo, string message)
        {
            if (value < minimo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfLessThan(int value, int minimo, string message)
        {
            if (value < minimo)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfFalse(bool boolvalue, string message)
        {
            if (!boolvalue)
            {
                throw new DomainException(message);
            }
        }

        public static void ValidateIfTrue(bool boolvalue, string message)
        {
            if (boolvalue)
            {
                throw new DomainException(message);
            }
        }
    }
}
