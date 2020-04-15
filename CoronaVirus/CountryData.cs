﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaVirus
{
    class CountryData
    {       
        public string country { get; set; }
        public int updated { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int casesPerOneMillion { get; set; }
        public int deathsPerOneMillion { get; set; }
    }

    class CountryDataList
    {
        public List<CountryData> countries { get; set; }
    }
}
