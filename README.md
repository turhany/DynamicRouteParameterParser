#   **Dynamic Route Parameter Parser**

![alt tag](/img/dynamicrouteparameterparser.png)  

Little package for parse route and querystring parameters.

[![NuGet version](https://badge.fury.io/nu/DynamicRouteParameterParser.svg)](https://badge.fury.io/nu/DynamicRouteParameterParser)  ![Nuget](https://img.shields.io/nuget/dt/DynamicRouteParameterParser)

#### Features:
- Parse url query string parameters   
- Parse url route section parameters

#### Usages:
-----

```cs
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
```

#### Output:

![alt tag](/img/output.jpg) 

### Release Notes

#### 1.0.2
* When url contains "." on route section it broke parse and it fixed

#### 1.0.1
* Description updates

#### 1.0.0
* Base release
