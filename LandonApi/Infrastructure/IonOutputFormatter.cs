using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi.Infrastructure // this modifies the GET RESPONSE to include ION+JSON instead of just json so people know we are ION compliant with REST
{                                  // must also be wired up in the startup class now
    public class IonOutputFormatter : TextOutputFormatter // wrap the ION output formatter around the default JSON output formatter....
    {
        private readonly JsonOutputFormatter _jsonOutputFormatter; // request the json output formatter for dependancy injection

        public IonOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
        {
            if (jsonOutputFormatter == null) throw new ArgumentNullException(nameof(jsonOutputFormatter));
            _jsonOutputFormatter = jsonOutputFormatter; // if you were able to request a jsonoutputformatter, assign it with dep injection

            SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/ion+json")); // modify the media type to show ION support
            SupportedEncodings.Add(Encoding.UTF8); // modify the supported encodings 
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
                => _jsonOutputFormatter.WriteResponseBodyAsync(context, selectedEncoding);  // respond with this header info instead of default textoutputformatter
        
    }
}
