using System;
#pragma warning disable 219

namespace DynamicRouteParameterParser.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var routeTemplate = "http://www.turhanyildirim.com/{year}/{month}/{day}/{pageName}/";
            var sampleUrl = "http://www.turhanyildirim.com/2018/02/10/coklu-ortama-loglama-icin-basit-bir-yaklamis-autofac/?section=code";
            var parser = new Parser.DynamicRouteParameterParser(routeTemplate);
            var parseResult = parser.ParseRoute(sampleUrl);
            
            Console.WriteLine( $"Route Template > {routeTemplate}");   
            Console.WriteLine( $"Sample Url > {sampleUrl}");
            Console.WriteLine();
            Console.WriteLine("Parse Result");
            Console.WriteLine("--------------");
            Console.WriteLine("Key - Value - Type");
            Console.WriteLine("--------------");
            foreach (var item in parseResult)
            {
                Console.WriteLine($"{item.Key} - {item.Value} - {item.Type}");
            }

            Console.ReadLine();
        }
    }
}