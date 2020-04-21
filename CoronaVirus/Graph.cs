using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoronaVirus
{
    // a model to hold json data returned from API call @ https://corona.lmao.ninja/v2/historical/all?lastdays=7
    // the data in this model will be used to create microchart entries
    public class Graph
    {
       public Dictionary<String, float> cases { get; set; }
       public Dictionary<String, float> deaths { get; set; }
       public Dictionary<String, float> recovered { get; set; }
    }

}
