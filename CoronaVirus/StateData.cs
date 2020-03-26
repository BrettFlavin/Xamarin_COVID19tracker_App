﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaVirus
{
    public class StateData
    {
        public string state { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int active { get; set; }
    }

    public class StateDataList
    {
        public List<StateData> states { get; set; }
    }
}
