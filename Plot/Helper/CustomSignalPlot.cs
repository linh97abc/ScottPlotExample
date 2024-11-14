//using ScottPlot;
//using SkiaSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Plot.Helper
//{
//    public class CustomSignalPlot : ScottPlot.Plottables.Signal
//    {
//        public CustomSignalPlot(ISignalSource data) : base(data)
//        {
//        }

//        public override void Render(RenderPack rp)
//        {
//            if (!Data.GetYs().Any())
//            {
//                return;
//            }

          
//            RenderHighDensity(rp);
            
//        }

//        private void RenderHighDensity(RenderPack rp)
//        {
//            using (SKPaint paint = new SKPaint())
//            {
//                LineStyle.ApplyToPaint(paint);

//                IEnumerable<PixelColumn> cols = Enumerable.Range(0, (int)Axes.DataRect.Width)
//                    .Select(x => Data.GetPixelColumn(Axes, x))
//                    .Where(x => x.HasData);

//                if (!cols.Any())
//                    return;

//                using (SKPath path = new SKPath())
//                {
//                    path.MoveTo(cols.First().X, cols.First().Enter);

//                    var numPoint = cols.Count();

//                    System.Diagnostics.Debug.WriteLine($"numPoint: {numPoint}");


//                    //int point_index = 0;
//                    //int point_draw = -1;

//                    int num_point_draw = 0;

//                    foreach (PixelColumn col in cols)
//                    {
//                        //var pos = (int)((2048.0f * point_index / numPoint) + 0.5);
//                        //point_index++;

//                        //if (pos == point_draw) continue;

//                        path.LineTo(col.X, col.Enter);
//                        path.MoveTo(col.X, col.Bottom);
//                        path.LineTo(col.X, col.Top);
//                        path.MoveTo(col.X, col.Exit);

//                        num_point_draw++;
//                    }

//                    rp.Canvas.DrawPath(path, paint);

//                    System.Diagnostics.Debug.WriteLine($"num_point_draw: {num_point_draw}");
//                }
//            }

           
//        }
//    }
//}
