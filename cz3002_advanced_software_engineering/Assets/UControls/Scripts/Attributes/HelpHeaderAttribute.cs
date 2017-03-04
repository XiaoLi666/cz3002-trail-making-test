using System;
using UnityEngine;

namespace Ultralpha
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class HelpHeaderAttribute : PropertyAttribute
    {
        public readonly string content;

        public readonly string tooltip;


        public HelpHeaderAttribute(string content)
        {
            this.content = content;
        }

        public HelpHeaderAttribute(string content, string tooltip)
        {
            this.content = content;
            this.tooltip = tooltip;
        }
    }
}