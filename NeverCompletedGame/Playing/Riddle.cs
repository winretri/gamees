using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Playing
{
    public class Riddle
    {
        public string Question { get; set; }
        
        public string Solution { get; set; }
        
        public int Level { get; set; }
    }
}
