namespace Nerdosity_V002.Models
{
    public class Operation
    {
        public static bool IsAdd(string action) => action.EqualsNoCase("add");
        public static bool IsDelete(string action) => action.EqualsNoCase("delete");
    }
}
