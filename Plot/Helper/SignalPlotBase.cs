using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot.Helper
{
    public class SignalPlotBase : BindableBase
    {
        public string Tag { get; set; }
        public string Name { get; set; }

        public ScottPlot.WPF.WpfPlot Plot { get; set; }


        public IPlottable Signal { get; protected set; }

        bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible == value) return;
                if (this.Signal != null)
                {
                    this.Signal.IsVisible = value;
                    Plot?.Refresh();
                }

                Set(ref isVisible, value);
            }
        }

        public SignalPlotBase() { }
        public SignalPlotBase(ScottPlot.WPF.WpfPlot plot, string name)
        {
            this.Plot = plot;
            this.Name = name;
        }

    }
}
