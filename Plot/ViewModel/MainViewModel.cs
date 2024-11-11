﻿using ScottPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot.ViewModel
{
    public class MainViewModel : Helper.BindableBase
    {
        public ObservableCollection<Helper.SignalPlot> Plots { get; } = new ObservableCollection<Helper.SignalPlot>();

        Helper.CSVReader csvReader = new Helper.CSVReader();

        string dataFilePath;
        public string DataFilePath
        {
            get => dataFilePath;
            set
            {
                bool isNewData = dataFilePath != value;
                Set(ref dataFilePath, value);

                if (isNewData)
                {
                    this.Render();
                }

            }

        }

        int indexView;
        public int IndexView
        {
            get => indexView;
            set => Set(ref indexView, value);
        }

        public MainViewModel(ScottPlot.WPF.WpfPlot plot)
        {
            this.plot = plot;
            //Plots.Add(new Helper.SignalPlot() { Plot = plot });
            //Plots.Add(new Helper.SignalPlot() { Plot = plot });
            //Plots[0].Name = "x1";
            //Plots[1].Name = "ash asdh ashd";

            //Plots[0].Render(ScottPlot.Generate.Sin(1000));
            //Plots[1].Render(ScottPlot.Generate.Sin(1000, 2));

            Helper.PlotInteract.InitChart(plot, ShowChartVal);
        }

        void ShowChartVal(bool isShow, int x)
        {
            this.IndexView = x;

            foreach (var item in this.Plots)
            {
               

                if (!isShow)
                {
                    item.SetViewValue(false, 0);
                    continue;
                }

                try
                {
                    var y = item.GetY(x);
                    item.SetViewValue(isShow, y);
                }
                catch (Exception)
                {
                    item.SetViewValue(false, 0);
                }
            }
        }


        ScottPlot.WPF.WpfPlot plot;
        void Render()
        {
            this.plot.Plot.Clear();
            Plots.Clear();

            csvReader.LoadData(DataFilePath);

            for (int i = 0; i < csvReader.NSignal; i++)
            {
                var sig = new Helper.SignalPlot() { Plot = plot, Name = csvReader.Labels[i] };
                sig.Render(csvReader.Datas[i]);
                Plots.Add(sig);

            }



            this.plot.Refresh();
        }
    }
}
