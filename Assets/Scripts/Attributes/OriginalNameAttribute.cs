using System;

namespace TarkovServerU19
{
    internal class OriginalNameAttribute : Attribute
    {
        public OriginalNameAttribute(string name)
        {
            this.Name = name;
        }
        public readonly string Name;
    }
}
