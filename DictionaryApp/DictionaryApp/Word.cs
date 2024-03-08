using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class Word
    {
        public string Syntax { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string DefaultCategory { get; set; }

        public Word(string syntax, string category, string description)
        {
            Syntax = syntax;
            Category = category;
            Description = description;
            DefaultCategory = "Choose category";
        }
    }
}
