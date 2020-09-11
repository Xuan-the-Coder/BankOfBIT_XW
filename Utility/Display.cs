using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public static class Display
    {
        public static string DisplayHeading(string heading, int length)
        {
            string str = heading.Substring(0, length);

            return str;
        }
    }
}

