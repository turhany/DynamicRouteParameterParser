namespace DynamicRouteParameterParser.Models
{
    internal static class AppConstants
    {
        public const char VariableStartChar = '{';
        public const char VariableEndChar = '}';
        public const char QueryStringDivider = '?';
        public const string RouteTokenPattern = @"[{0}].+?[{1}]"; //the 0 and 1 are used by the string.format function, they are the start and end characters.
        public const string VariableTokenPattern = "(?<{0}>[^,]*)"; //the <>'s denote the group name; this is used for reference for the variables later.
    }
}