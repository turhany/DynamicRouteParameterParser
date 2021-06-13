using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using DynamicRouteParameterParser.Models;

namespace DynamicRouteParameterParser.Parser
{
    public class DynamicRouteParameterParser
    {
        private HashSet<string> Variables { get; set; }
        private string RouteFormat { get; set; }

        public DynamicRouteParameterParser(string baseRouteTemplate)
        {
            if (string.IsNullOrWhiteSpace(baseRouteTemplate))
            {
                throw new ArgumentNullException(nameof(baseRouteTemplate));
            }

            if (baseRouteTemplate.Contains(AppConstants.QueryStringDivider))
            {
                baseRouteTemplate = baseRouteTemplate.Split(AppConstants.QueryStringDivider).First();
            }

            RouteFormat = baseRouteTemplate;
            ParseRouteFormat();
        }
 
        /// <summary>
        /// Extract variable values from a given instance of the route you're trying to parse.
        /// </summary> 
        /// <param name="route">Route for parse</param>
        /// <returns>A dictionary of Variable names mapped to values.</returns>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        public List<RouteParameter> ParseRoute(string route)
        {
            var splitedList = route.Split(AppConstants.QueryStringDivider);

            if (splitedList.Length > 2)
            {
                throw new ArgumentException(
                    "route format is invalid, there are multiple url, querystring split char '?'.");
            }

            var baseRoute = splitedList.First();
            var queryString = splitedList.Length > 1 ? splitedList.Last() : string.Empty;

            var response = new List<RouteParameter>();
            ParseRouteParams(baseRoute, response);
            ParseQueryStringParams(queryString, response);

            return response;
        }

        #region Private Helper Methods

        private void ParseRouteFormat()
        {
            var variableList = new List<string>();
            var matchCollection = Regex.Matches
            (
                this.RouteFormat
                , string.Format(AppConstants.RouteTokenPattern, AppConstants.VariableStartChar, AppConstants.VariableEndChar)
                , RegexOptions.IgnoreCase
            );

            foreach (var match in matchCollection)
            {
                variableList.Add(RemoteVariableChars(match.ToString()));
            }

            Variables = new HashSet<string>(variableList);
        }
        
        private static string RemoteVariableChars(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return input;

            string result = new String(input.ToArray());
            result = result.Replace(AppConstants.VariableStartChar.ToString(), String.Empty)
                .Replace(AppConstants.VariableEndChar.ToString(), String.Empty);
            return result;
        }

        private static string WrapWithVariableChars(string input)
        {
            return string.Format("{0}{1}{2}",AppConstants.VariableStartChar, input, AppConstants.VariableEndChar);
        }

        private void ParseRouteParams(string baseRoute, List<RouteParameter> parseResponse)
        { 
            string formatUrl = new string(RouteFormat.ToArray());
            foreach (string variable in Variables)
            {
                formatUrl = formatUrl.Replace(WrapWithVariableChars(variable),
                    string.Format(AppConstants.VariableTokenPattern, variable));
            }

            var regex = new Regex(formatUrl, RegexOptions.IgnoreCase);
            var matchCollection = regex.Match(baseRoute);

            foreach (var variable in Variables)
            {
                var value = matchCollection.Groups[variable].Value;
                parseResponse.Add(new RouteParameter
                {
                    Key = variable,
                    Value = value,
                    Type = RouteParameterType.Route
                });
            }
        }

        private static void ParseQueryStringParams(string queryString, List<RouteParameter> parseResponse)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return;
            }

            var parsedQueryString = HttpUtility.ParseQueryString(queryString);

            foreach (string key in parsedQueryString)
            {
                parseResponse.Add(new RouteParameter
                {
                    Key = key,
                    Value = parsedQueryString[key],
                    Type = RouteParameterType.QueryString
                });
            }
        }

        #endregion
    }
}