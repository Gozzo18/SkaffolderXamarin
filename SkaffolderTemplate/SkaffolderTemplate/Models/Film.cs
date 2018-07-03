using System;
using System.Collections.Generic;
using System.Text;

namespace SkaffolderTemplate.Models
{
     public class Film
    {
        public string _id { get; set; }
        public string genre { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        
        public string filmMaker { get; set; }

        public string[] actors { get; set; }
        
    }
}
