using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Class_Project_2020
{
    public class Pokemon
    {
        //#,Name,Type 1,Type 2,Total,HP,Attack,Defense,Sp. Atk,Sp. Def,Speed,Generation,Legendary
        public int _number { get; set; }
        public string _name { get; set; }
        public string _type1 { get; set; }
        public string _type2 { get; set; }
        public int _total { get; set; }
        public int _hitPoints { get; set; }
        public int _attack { get; set; }
        public int _defense { get; set; }
        public bool _avail { get; set; }
    }
}
