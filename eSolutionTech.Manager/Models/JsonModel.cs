using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSolutionTech.Manager.Models
{
  public class JsonModel
  {
    public class Response
    {
      [JsonProperty("Code")]
      public string Code { get; set; }
      [JsonProperty("Value")]
      public string Value { get; set; }
    }
  }
}
