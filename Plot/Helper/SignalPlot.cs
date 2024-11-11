using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot.Helper
{
    public class SignalPlot : SignalPlotBase
    {

        double[] dataY;

        public double GetY(int index)
        {
            return this.dataY[index];
        }

        public void Render(double[] data)
        {
            this.dataY = data;
            var sig = this.Plot.Plot.Add.Signal(data);
            this.Signal = sig;

            sig.LegendText = this.Name;
            this.Tag = sig.LineColor.ToHex();

        }

        public void Render(List<double> data)
        {
            this.Render(data.ToArray());

            //var sig = this.Plot.Plot.Add.Signal(data);
            //this.Signal = sig;

            //sig.LegendText = this.Name;
            //this.Tag = sig.LineColor.ToHex();

        }

    }
}
