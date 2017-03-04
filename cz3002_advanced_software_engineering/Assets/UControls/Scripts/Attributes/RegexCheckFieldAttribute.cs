using System;
using UnityEngine;

namespace Ultralpha
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class RegexCheckFieldAttribute : PropertyAttribute
    {
        public readonly string pattern;
        public bool forceClear;
        public string warning;

        public RegexCheckFieldAttribute(string pattern)
        {
            this.pattern = pattern;
        }

        public RegexCheckFieldAttribute(Type type) : this(GetPattern(type))
        {
        }

        private static string GetPattern(Type type)
        {
            switch (type)
            {
                case Type.Integer:
                    return @"^[-+]?\d+$";
                case Type.FloatingPoint:
                    return @"^[-+]?[0-9]*\.?[0-9]+\b$";
                case Type.IPV4Address:
                    return
                        @"^(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.){3}(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])$";
                case Type.EmailAddress:
                    return @"^[A-Z0-9._%+-]+@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$";
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public enum Type
        {
            Integer,
            FloatingPoint,
            IPV4Address,
            EmailAddress,
        }
    }
}