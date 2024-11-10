using ScottPlot.Plottables;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot.Helper
{
    public delegate void ShowValueAtX(bool isShow, int x);

    public  static class PlotInteract
    {
        public static (SignalXY signalXY, DataPoint point) GetSignalXYUnderMouse(ScottPlot.Plot plot, double x, double y)
        {
            Pixel mousePixel = new Pixel(x, y);

            Coordinates mouseLocation = plot.GetCoordinates(mousePixel);

            var plot_limit = plot.Axes.GetLimits();

            if (plot_limit.Left > mouseLocation.X)
            {
                return (null, DataPoint.None);
            }

            if (plot_limit.Right < mouseLocation.X)
            {
                return (null, DataPoint.None);
            }

            if (plot_limit.Bottom > mouseLocation.Y)
            {
                return (null, DataPoint.None);
            }

            if (plot_limit.Top < mouseLocation.Y)
            {
                return (null, DataPoint.None);
            }


            foreach (var signal in plot.GetPlottables<SignalXY>().Reverse())
            {
                if (!signal.IsVisible) continue;
                DataPoint nearest = signal.Data.GetNearest(mouseLocation, plot.LastRender);
                if (nearest.IsReal)
                {
                    return (signal, nearest);
                }
            }


            return (null, DataPoint.None);
        }

        public class DataLogerUnderMouse
        {
            public DataLogger signalXY = null;
            public DataPoint point = DataPoint.None;

            public DataLogerUnderMouse() { }


            public DataLogerUnderMouse(DataLogger signalXY, DataPoint point)
            {
                this.signalXY = signalXY;
                this.point = point;
            }
        };

        public static DataLogerUnderMouse GetDataLoggerUnderMouse(ScottPlot.Plot plot, double x, double y)
        {
            Pixel mousePixel = new Pixel(x, y);

            Coordinates mouseLocation = plot.GetCoordinates(mousePixel);

            var plot_limit = plot.Axes.GetLimits();

            if (plot_limit.Left > mouseLocation.X)
            {
                return new DataLogerUnderMouse();
            }

            if (plot_limit.Right < mouseLocation.X)
            {
                return new DataLogerUnderMouse();
            }

            if (plot_limit.Bottom > mouseLocation.Y)
            {
                return new DataLogerUnderMouse();
            }

            if (plot_limit.Top < mouseLocation.Y)
            {
                return new DataLogerUnderMouse();
            }

            var searchZoneSize = (plot_limit.Top - plot_limit.Bottom) / 64;



            List<DataLogerUnderMouse> nearests = new List<DataLogerUnderMouse>();

            foreach (var signal in plot.GetPlottables<DataLogger>().Reverse())
            {
                if (!signal.IsVisible) continue;

                if (signal.Data.Coordinates.Count == 0) continue;

                var rangeX = signal.Data.GetRangeX().Rectified();
                var px = (int)(mouseLocation.X + 1.5);


                if (rangeX.Value1 > px)
                {
                    continue;
                }

                if (rangeX.Value2 < px)
                {
                    continue;
                }

                try
                {
                    var py = signal.Data.Coordinates[px].Y;

                    DataPoint nearest = new DataPoint(px - 1, py, px - 1);
                    nearests.Add(new DataLogerUnderMouse(signal, nearest));

                    //DataPoint nearest = signal.GetNearestX(mouseLocation, new PixelRect(
                    //    (float)plot_limit.Left, (float)plot_limit.Right, (float)plot_limit.Bottom, (float)plot_limit.Top),
                    //    1);
                    //if (nearest.IsReal)
                    //{

                    //    nearests.Add(new DataLogerUnderMouse(signal, nearest));
                    //}
                }
                catch (Exception)
                {


                }


            }

            double min = double.MaxValue;
            DataLogerUnderMouse itemMatch = null;

            foreach (var item in nearests)
            {
                var delta = Math.Abs(mouseLocation.Y - item.point.Y);

                if (delta > searchZoneSize) continue;
                if (delta <= min)
                {
                    min = delta;
                    itemMatch = item;
                }
            }

            if (itemMatch != null)
            {
                return itemMatch;
            }


            return new DataLogerUnderMouse();
        }

        public static DataLogerUnderMouse GetDataLoggerStraightUnderMouse(ScottPlot.Plot plot, double x, double y)
        {
            Pixel mousePixel = new Pixel(x, y);

            Coordinates mouseLocation = plot.GetCoordinates(mousePixel);

            var plot_limit = plot.Axes.GetLimits();

            if (plot_limit.Left > mouseLocation.X)
            {
                return new DataLogerUnderMouse();
            }

            if (plot_limit.Right < mouseLocation.X)
            {
                return new DataLogerUnderMouse();
            }

            if (plot_limit.Bottom > mouseLocation.Y)
            {
                return new DataLogerUnderMouse();
            }

            if (plot_limit.Top < mouseLocation.Y)
            {
                return new DataLogerUnderMouse();
            }

            var searchZoneSize = (plot_limit.Top - plot_limit.Bottom) / 64;



            List<DataLogerUnderMouse> nearests = new List<DataLogerUnderMouse>();

            foreach (var signal in plot.GetPlottables<DataLogger>().Reverse())
            {
                if (!signal.IsVisible) continue;

                if (signal.Data.Coordinates.Count == 0) continue;

                var rangeX = signal.Data.GetRangeX().Rectified();
                //var px = (int)(mouseLocation.X + 1.5);
                var px = (int)(mouseLocation.X + 0.5);


                if (rangeX.Value1 > px)
                {
                    continue;
                }

                if (rangeX.Value2 < px)
                {
                    continue;
                }

                try
                {
                    var py = signal.Data.Coordinates[px].Y;

                    DataPoint nearest = new DataPoint(px, py, px);
                    //DataPoint nearest = new DataPoint(px - 1, py, px - 1);
                    nearests.Add(new DataLogerUnderMouse(signal, nearest));

                    //DataPoint nearest = signal.GetNearestX(mouseLocation, new PixelRect(
                    //    (float)plot_limit.Left, (float)plot_limit.Right, (float)plot_limit.Bottom, (float)plot_limit.Top),
                    //    1);
                    //if (nearest.IsReal)
                    //{

                    //    nearests.Add(new DataLogerUnderMouse(signal, nearest));
                    //}
                }
                catch (Exception)
                {


                }


            }

            double min = double.MaxValue;
            DataLogerUnderMouse itemMatch = null;

            foreach (var item in nearests)
            {
                var delta = Math.Abs(mouseLocation.Y - item.point.Y);

                if (delta > searchZoneSize) continue;
                if (delta <= min)
                {
                    min = delta;
                    itemMatch = item;
                }
            }

            if (itemMatch != null)
            {
                return itemMatch;
            }


            return new DataLogerUnderMouse();
        }



        static void MouseMouse(ScottPlot.WPF.WpfPlot plot, System.Windows.Input.MouseEventArgs e, ShowValueAtX handler)
        {
            var pos = e.GetPosition(plot);

            Pixel mousePixel = new Pixel(pos.X, pos.Y);

            Coordinates mouseLocation = plot.Plot.GetCoordinates(mousePixel);


            var plot_limit = plot.Plot.Axes.GetLimits();

            if (plot_limit.Left > pos.X)
            {
                handler?.Invoke(false, -1);
                return;
            }

            if (plot_limit.Right < mouseLocation.X)
            {
                handler?.Invoke(false, -1);
                return;
            }

            if (plot_limit.Bottom > mouseLocation.Y)
            {
                handler?.Invoke(false, -1);
                return;
            }

            if (plot_limit.Top < mouseLocation.Y)
            {
                handler?.Invoke(false, -1);
                return;
            }

            handler?.Invoke(true, (int)(mouseLocation.X + 0.5));


        }


        public static void InitChart(ScottPlot.WPF.WpfPlot plot, ShowValueAtX showValueAtX)
        {
            //ScottPlot.Plottables.Crosshair crosshair;

            //crosshair = plot.Plot.Add.Crosshair(0, 0);
            //crosshair.IsVisible = false;
            //crosshair.MarkerShape = MarkerShape.OpenCircle;
            //crosshair.MarkerSize = 15;
            //crosshair.TextColor = ScottPlot.Colors.White;
            //crosshair.TextBackgroundColor = crosshair.HorizontalLine.Color;


            plot.Plot.Legend.IsVisible = false;
            plot.Plot.FigureBackground.Color = ScottPlot.Colors.Transparent;
            plot.Plot.Axes.Color(ScottPlot.Colors.Gray);
            plot.Plot.Grid.MajorLineColor = ScottPlot.Colors.Gray.WithOpacity(0.25);
            plot.Plot.Grid.MinorLineColor = ScottPlot.Colors.Gray;

            plot.Plot.Axes.Title.Label.Bold = true;
            plot.Plot.Axes.Title.Label.FontName = ScottPlot.Fonts.System;
            plot.Plot.Axes.Title.Label.FontSize = 16;

            plot.MouseMove += (s, e) =>
            {
                var pos = e.GetPosition(plot);


                MouseMouse(plot, e, showValueAtX);

                //var xy = Helper.Plot.GetSignalXYUnderMouse(plot.Plot, pos.X, pos.Y);
                //var xy = GetDataLoggerUnderMouse(plot.Plot, pos.X, pos.Y);

                //if (xy.point.IsReal)
                //{
                //    crosshair.IsVisible = true;
                //    crosshair.Position = xy.point.Coordinates;

                //    crosshair.VerticalLine.Text = $"{xy.point.Index}, {xy.signalXY.LegendText}:{xy.point.Y}";
                //    plot.Refresh();
                //}
                //else
                //{
                //    if (crosshair.IsVisible)
                //    {
                //        crosshair.IsVisible = false;
                //        plot.Refresh();

                //    }
                //}

            };
        }

        public static void Zoom_XY(
    ScottPlot.WPF.WpfPlot[] plot)
        {
            foreach (var item in plot)
            {
                item.Plot.Axes.Rules.Clear();
                item.Refresh();
            }

            
        }

        public static void Zoom_X(
            ScottPlot.WPF.WpfPlot[] plot)
        {
            foreach (var item in plot)
            {
                ScottPlot.AxisLimits limits = item.Plot.Axes.GetLimits();
                var rule = new ScottPlot.AxisRules.LockedVertical(
                    item.Plot.Axes.Left,
                    limits.Bottom,
                    limits.Top);


                item.Plot.Axes.Rules.Clear();
                item.Plot.Axes.Rules.Add(rule);
                item.Refresh();
            }

            
        }

        public static void Zoom_Y(
            ScottPlot.WPF.WpfPlot[] plot)
        {
            foreach (var item in plot)
            {
                ScottPlot.AxisLimits limits = item.Plot.Axes.GetLimits();
                var rule = new ScottPlot.AxisRules.LockedHorizontal(
                    item.Plot.Axes.Bottom,
                    limits.Left,
                    limits.Right);


                item.Plot.Axes.Rules.Clear();
                item.Plot.Axes.Rules.Add(rule);
                item.Refresh();
            }

            
        }
    }
}
