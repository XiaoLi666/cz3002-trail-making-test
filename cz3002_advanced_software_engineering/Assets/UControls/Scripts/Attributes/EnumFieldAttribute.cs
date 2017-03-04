using System;
using UnityEngine;

namespace Ultralpha
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumFieldAttribute : PropertyAttribute
    {
        public string label;

        public EnumFieldAttribute(string label)
        {
            this.label = label;
        }
    }
}