using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComMgtSystem.Presenters
{
    class presenter
    {
        Views.IView view;
        Models.model model;

        public presenter(Views.IView v, Models.model m)
        {
            this.view = v;
            this.model = m;
        }

        public void calculate()
        {
            view.result = model.add(view.numberA, view.numberB);
        }

    }
}
