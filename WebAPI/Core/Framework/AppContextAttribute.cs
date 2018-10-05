using System;

namespace Core.Framework
{
    public class AppContextAttribute : Attribute
    {
        public AppContextType ContextType { set; get; }
        public AppContextAttribute(AppContextType type)
        {
            ContextType = type;
        }
    }
}
