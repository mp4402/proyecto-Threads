using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App{
    static class Globals
    {
        public static string pathBase = @"C:\GitHub\proyecto-Threads\Docker_proyecto\so_data\index_data_";
        //public static string pathBase =  @"/Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_data/index_data_";
    }
    class un_hilo {

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (args.Length > 0)
            {
                int value = Convert.ToInt32(args[0]);
                switch(value) 
                {
                    case 1:
                        break;
                    case 2:
                        Thread thread1 = new Thread(() => func_thr(501,1000));
                        thread1.Start();
                        ciclo(1,501);
                        thread1.Join();
                        break;
                    case 4:
                        break;
                    case 8:
                        break;
                    default:
                        Console.WriteLine("Los unicos parametros validos para el numero de hilos son: [1, 2, 4, 8]");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No hay parametros");
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        }

        static void ciclo(int inicio, int final){
            string path = "";
            for (int i = inicio; i < final; i++){
                path = Globals.pathBase + i.ToString() + ".csv";
                readFile(path);
            }
        }

        static void func_thr(object inicio, object final){
            string path = "";
            for (int i = Convert.ToInt32(inicio); i < Convert.ToInt32(final); i++){
                path = Globals.pathBase + i.ToString() + ".csv";
                readFile(path);
            }
        }

        static void readFile(string path){
            List<int> Open = new List<int>();
            List<int> High = new List<int>();
            List<int> Low = new List<int>();
            List<int> Close = new List<int>();
            using(var reader = new StreamReader(@path))
            {
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
            }
            var no_document = Regex.Match(path, @"\d+").Value;
            calculateStatistic(Open, High, Low, Close, no_document);
        }
         
        static void calculateStatistic(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose, string no_document){
            List<int> sizes = new List<int>();
            List<double> averages = new List<double>();
            List<double> stds = new List<double>();
            List<int> mins = new List<int>();
            List<int> maxs = new List<int>();
            sizes = calculateCount(listOpen, listHigh, listLow, listClose);
            averages = calculateAverage(listOpen, listHigh, listLow, listClose);
            stds = calculateStd(listOpen, listHigh, listLow, listClose);
            mins = calculateMin(listOpen, listHigh, listLow, listClose);
            maxs = calculateMax(listOpen, listHigh, listLow, listClose);
            generateCsvFile(sizes, averages, stds, mins, maxs, no_document);
        }

        static List<int> calculateCount(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<int> sizes = new List<int>();
            sizes.Add(listOpen.Count());
            sizes.Add(listHigh.Count());
            sizes.Add(listLow.Count());
            sizes.Add(listClose.Count());
            return sizes;
        }

        static List<double> calculateAverage(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<double> averages = new List<double>();
            averages.Add(Math.Round(listOpen.Average(),2));
            averages.Add(Math.Round(listHigh.Average(),2));
            averages.Add(Math.Round(listLow.Average(),2));
            averages.Add(Math.Round(listClose.Average(),2));
            return averages;
        }

        static List<double> calculateStd(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<double> stds = new List<double>();
            double avgOpen = Math.Round(listOpen.Average(),2);
            double avgHigh = Math.Round(listHigh.Average(),2);
            double avgLow = Math.Round(listLow.Average(),2);
            double avgClose = Math.Round(listClose.Average(),2);
            stds.Add(Math.Round(Math.Sqrt(listOpen.Average(v=>Math.Pow(v-avgOpen,2))),2));
            stds.Add(Math.Round(Math.Sqrt(listHigh.Average(v=>Math.Pow(v-avgHigh,2))),2));
            stds.Add(Math.Round(Math.Sqrt(listLow.Average(v=>Math.Pow(v-avgLow,2))),2));
            stds.Add(Math.Round(Math.Sqrt(listClose.Average(v=>Math.Pow(v-avgClose,2))),2));
            return stds;
        }

        static List<int> calculateMin(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<int> mins = new List<int>();
            mins.Add(listOpen.Min());
            mins.Add(listHigh.Min());
            mins.Add(listLow.Min());
            mins.Add(listClose.Min());
            return mins;
        }

        static List<int> calculateMax(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<int> maxs = new List<int>();
            maxs.Add(listOpen.Max());
            maxs.Add(listHigh.Max());
            maxs.Add(listLow.Max());
            maxs.Add(listClose.Max());
            return maxs;
        }

        static void generateCsvFile(List<int> sizes, List<double> averages, List<double> stds, List<int> mins, List<int> maxs, string no_document){
            var path = @"C:\GitHub\proyecto-Threads\Docker_proyecto\so_respuesta\index_data_out_" + no_document + ".csv";
            //var path = @"/Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_respuesta/archivo_out_" + no_document + ".csv";
            var csv = new StringBuilder();
            var newLine = string.Format("{0},{1},{2},{3},{4}","","Open", "High", "Low", "Close");
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3},{4}","count", sizes[0].ToString(), sizes[1].ToString(), sizes[2].ToString(), sizes[3].ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3},{4}","mean", averages[0].ToString(), averages[1].ToString(), averages[2].ToString(), averages[3].ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3},{4}","std", stds[0].ToString(), stds[1].ToString(), stds[2].ToString(), stds[3].ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3},{4}","min", mins[0].ToString(), mins[1].ToString(), mins[2].ToString(), mins[3].ToString());
            csv.AppendLine(newLine);
            newLine = string.Format("{0},{1},{2},{3},{4}","max", maxs[0].ToString(), maxs[1].ToString(), maxs[2].ToString(), maxs[3].ToString());
            csv.AppendLine(newLine);  

            File.WriteAllText(path, csv.ToString());
        }
    }
}