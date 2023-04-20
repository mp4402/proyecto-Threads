using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace HelloWorldApp {

    class Geeks {

        static void Main(string[] args)
        {
            string path = @"C:\GitHub\proyecto-Threads\Docker_proyecto\so_data\index_data_1.csv";
            readFile(path);
        }

      
        static void readFile(string path){
            using(var reader = new StreamReader(@path))
            {
                List<int> Open = new List<int>();
                List<int> High = new List<int>();
                List<int> Low = new List<int>();
                List<int> Close = new List<int>();
                int countLines = 0;
                while (!reader.EndOfStream)
                {
                    countLines = countLines + 1; 
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (countLines > 1){
                        int openValue = Convert.ToInt32(values[1]);
                        int highValue = Convert.ToInt32(values[2]);
                        int lowValue = Convert.ToInt32(values[3]);
                        int closeValue = Convert.ToInt32(values[4]);
                        Open.Add(openValue);
                        High.Add(highValue);
                        Low.Add(lowValue);
                        Close.Add(closeValue);
                    }
                }
                  calculateStatistic(Open, High, Low, Close);
            }
        }
         
        static void calculateStatistic(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            double avgOpen = Math.Round(listOpen.Average(),2);
            double avgHigh = Math.Round(listHigh.Average(),2);
            double avgLow = Math.Round(listLow.Average(),2);
            double avgClose = Math.Round(listClose.Average(),2);
            double stdOpen = Math.Round(Math.Sqrt(listOpen.Average(v=>Math.Pow(v-avgOpen,2))),2);
            double stdHigh = Math.Round(Math.Sqrt(listHigh.Average(v=>Math.Pow(v-avgHigh,2))),2);
            double stdLow = Math.Round(Math.Sqrt(listLow.Average(v=>Math.Pow(v-avgLow,2))),2);
            double stdClose = Math.Round(Math.Sqrt(listClose.Average(v=>Math.Pow(v-avgClose,2))),2);
            int minOpen = listOpen.Min();
            int minHigh = listHigh.Min();
            int minLow = listLow.Min();
            int minClose = listClose.Min();
            int maxOpen = listOpen.Max();
            int maxHigh = listHigh.Max();
            int maxLow = listLow.Max();
            int maxClose = listClose.Max();
            int sizeOpen = listOpen.Count();
            int sizeHigh = listHigh.Count();
            int sizeLow = listLow.Count();
            int sizeClose = listClose.Count();
            
            var csv = new StringBuilder();
            var newLine = string.Format("{0},{1},{2},{3}", "Open", "High", "Low", "Close");
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3}", sizeOpen.ToString(), sizeHigh.ToString(), sizeLow.ToString(), sizeClose.ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3}", avgOpen.ToString(), avgHigh.ToString(), avgLow.ToString(), avgClose.ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3}", stdOpen.ToString(), stdHigh.ToString(), stdLow.ToString(), stdClose.ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3}", minOpen.ToString(), minHigh.ToString(), minLow.ToString(), minClose.ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3}", maxOpen.ToString(), maxHigh.ToString(), maxLow.ToString(), maxClose.ToString());
            csv.AppendLine(newLine);  

            File.WriteAllText(@"C:\GitHub\proyecto-Threads\Docker_proyecto\so_respuesta\index_data_out_1.csv", csv.ToString());


        }
    }

}
