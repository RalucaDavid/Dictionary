using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class WordDefinition
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public WordDefinition(string name, string category, string description)
        {
            Name = name;
            Category = category;
            Description = description;
        }
    }
}
