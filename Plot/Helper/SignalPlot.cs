using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot.Helper
{
    public class SignalPlot : SignalPlotBase
    {

        public void Render(double[] data)
        {
            var sig = this.Plot.Plot.Add.Signal(data);
            this.Signal = sig;

            sig.LegendText = this.Name;
            this.Tag = sig.LineColor.ToHex();

        }

        public void Render(List<double> data)
        {
            var sig = this.Plot.Plot.Add.Signal(data);
            this.Signal = sig;

            sig.LegendText = this.Name;
            this.Tag = sig.LineColor.ToHex();

        }

    }
}
