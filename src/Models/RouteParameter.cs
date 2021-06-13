// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace DynamicRouteParameterParser.Models
{
    public class RouteParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public RouteParameterType Type { get; set; }
    }
}