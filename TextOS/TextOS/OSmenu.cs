using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextOS
{
    public class OSmenu
    {
        public string Name { get; }
        public Action Selected { get; }

        public OSmenu(string Name, Action Selected)
        {
            this.Name = Name;
            this.Selected = Selected;
        }
    }
}
