using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverse_ebay
{
    class GUI:IOberflaeche
    {
        private IFachkonzept fachkonzept;

        public GUI(IFachkonzept _fachkonzept)
        {
            this.fachkonzept = _fachkonzept;
        }

        public void start()
        {
            GUI_View view = new GUI_View();
            view.ShowDialog();
            //mainmenue();
        }
    }
}
