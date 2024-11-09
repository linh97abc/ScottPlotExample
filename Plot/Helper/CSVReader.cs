using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot.Helper
{


    public class CSVReader
    {
        List<double>[] values = new List<double>[0];
        string[] labels = new string[0];
        int nSignal = 0;


        public List<double>[] Datas { get => values; }
        public string[] Labels { get => labels; }
        public int NSignal { get => nSignal; }
        public int Length { get; private set; }

        public void LoadData(string filePath)
        {


            using (TextReader reader = new StreamReader(filePath))
            {
                string line;


                string[] cells;
                int dataRow = 0;

                do
                {

                    line = reader.ReadLine();
                    if (line is null)
                    {
                        throw new Exception("File data invalid");
                    }

                    if (line.Contains(','))
                    {
                        cells = line.Split(',');



                        nSignal = cells.Length;
                        values = new List<double>[nSignal];
                        labels = new string[nSignal];



                        for (int i = 0; i < cells.Length; i++)
                        {
                            labels[i] = cells[i].Trim();
                            values[i] = new List<double>();
                        }

                        break;

                    }

                }
                while (true);





                while ((line = reader.ReadLine()) != null)
                {
                    cells = line.Split(',');

                    for (int i = 0; i < cells.Length; i++)
                    {
                        //cell.Trim();
                        values[i].Add(double.Parse(cells[i]));

                    }

                    dataRow++;

                }
                this.Length = dataRow;
            }




        }


    }
}
