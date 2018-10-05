using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Helpers
{
    public class Helper
    {
        public static string ConcatenateParameters(object[] sqlParameters)
        {
            var result = sqlParameters.Aggregate(string.Empty, (current, param) => current + (param + "=" + param + ","));

            //Remove the Last Parameter
            result = result.Substring(0, result.Length - 1);

            return result;
        }
    }
}