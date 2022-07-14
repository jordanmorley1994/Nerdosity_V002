using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdosity_V002.Models
{
    public class Nav
    {
        public static string Active(string value, string current) =>
            (value == current) ? "active" : "";
        public static string Active(int value, int current) =>
            (value == current) ? "active" : "";
    }
}
