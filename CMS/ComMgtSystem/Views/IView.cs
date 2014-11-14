using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComMgtSystem.Views
{
    interface IView
    {
        int numberA {get;}
        int numberB {get;}
        int result { set; }
    }
}
