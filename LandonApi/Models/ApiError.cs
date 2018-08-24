using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Models // this represents an exception as a json object. they can be stacked together to make a json document
                           // then all of it dumped out to the client including stacktraces if it is in developer mode.
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }

        // get inherited property. maps a json property to a .net member or constructor parameter
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)] // not sure what this does
        [DefaultValue("")] // if no stacktrace it wont be added to the response
        public string StackTrace { get; set; }
    }
}
