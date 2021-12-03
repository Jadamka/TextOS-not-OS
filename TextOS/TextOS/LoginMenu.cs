﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextOS
{
    public class LoginMenu
    {
        public string Name { get; }
        public Action Selected { get; }

        public LoginMenu(string Name, Action Selected)
        {
            this.Name = Name;
            this.Selected = Selected;
        }
    }
}
