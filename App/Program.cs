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
        //public static string pathBase = @"C:\GitHub\proyecto-Threads\Docker_proyecto\so_data\index_data_";
        public static string pathBase =  Environment.CurrentDirectory + @"/so_data/index_data_";
    }
    class un_hilo {

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            if (args.Length > 1)
            {
                var value = Convert.ToInt32(args[1]);
                switch(args[0].ToLower()){
                    case "files":
                        Thread thread1;
                        Thread thread2;
                        Thread thread3;
                        Thread thread4;
                        Thread thread5;
                        Thread thread6;
                        Thread thread7;
                        switch(value) 
                        {
                            case 1:
                                ciclo(1,1000);
                                break;
                            case 2:
                                thread1 = new Thread(() => func_thr(501,1000));
                                thread1.Start();
                                ciclo(1,501);
                                thread1.Join();
                                break;
                            case 4:
                                thread1 = new Thread(() => func_thr(251,501));
                                thread2 = new Thread(() => func_thr(501,751));
                                thread3 = new Thread(() => func_thr(751,1000));
                                thread1.Start();
                                thread2.Start();
                                thread3.Start();
                                ciclo(1,251);
                                thread1.Join();
                                thread2.Join();
                                thread3.Join();
                                break;
                            case 8:
                                thread1 = new Thread(() => func_thr(125,251));
                                thread2 = new Thread(() => func_thr(251,376));
                                thread3 = new Thread(() => func_thr(376,501));
                                thread4 = new Thread(() => func_thr(501,626));
                                thread5 = new Thread(() => func_thr(626,751));
                                thread6 = new Thread(() => func_thr(751,876));
                                thread7 = new Thread(() => func_thr(876,1000));
                                thread1.Start();
                                thread2.Start();
                                thread3.Start();
                                thread4.Start();
                                thread5.Start();
                                thread6.Start();
                                thread7.Start();
                                ciclo(1,126);
                                thread1.Join();
                                thread2.Join();
                                thread3.Join();
                                thread4.Join();
                                thread5.Join();
                                thread6.Join();
                                thread7.Join();
                                break;
                            default:
                                Console.WriteLine("Los unicos parametros validos para el numero de hilos son: [1, 2, 4, 8]");
                                break;
                        }
                        break;
                    case "functions":
                        switch (value){
                            case 1:
                                ciclo(1,1000);
                                break;
                            case 2: case 4: case 8:
                                ciclo_function(1,1000, value);
                                break;
                            default:
                                Console.WriteLine("Los unicos parametros validos para el numero de hilos son: [1, 2, 4, 8]");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Los unicos parametros validos para el tipo de paralelizacion son: [Files, Functions]");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Deben ser 2 parámetros: [Files, Functions] [No_hilos (1,2,4,8)]");
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

        
        static void ciclo_function(int inicio, int final, int no_hilos){
            string path = "";
            for (int i = inicio; i < final; i++){
                path = Globals.pathBase + i.ToString() + ".csv";
                readFile_function(path, no_hilos);
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

        static void readFile_function(string path, int no_hilos){
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
            calculateStatistic_function(Open, High, Low, Close, no_document, no_hilos);
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

        static void calculateStatistic_function(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose, string no_document, int no_hilos){
            List<List<int>> resultThread = new List<List<int>>();
            
            List<int> sizes = new List<int>();
            List<double> averages = new List<double>();
            List<double> stds = new List<double>();
            List<int> mins = new List<int>();
            List<int> maxs = new List<int>();

            Thread thread1;
            Thread thread2;
            Thread thread3;
            Thread thread4;
            // Thread thread5;
            // Thread thread6;
            // Thread thread7;

            switch (no_hilos){
                case 2:
                    //thread1 = new Thread(() => func_thr(501,1000));
                    
                    thread1 = new Thread(() => { resultThread = thread_2(listOpen, listHigh, listLow, listClose); });
                    
                    thread1.Start();
                    
                    averages = calculateAverage(listOpen, listHigh, listLow, listClose);
                    stds = calculateStd(listOpen, listHigh, listLow, listClose);
                    
                    thread1.Join();

                    sizes = resultThread[0];
                    mins = resultThread[1];
                    maxs = resultThread[2];
                   
                    break;
                case 4:
                    
                    thread1 = new Thread(() => { resultThread = thread_4(listOpen, listHigh, listLow, listClose); });
                    thread2 = new Thread(() => { averages = calculateAverage(listOpen, listHigh, listLow, listClose); });
                    thread3 = new Thread(() => { maxs = calculateMax(listOpen, listHigh, listLow, listClose); });
                    
                    thread1.Start();
                    thread2.Start();
                    thread3.Start();
                    
                    stds = calculateStd(listOpen, listHigh, listLow, listClose);
                    
                    thread1.Join();
                    thread2.Join();
                    thread3.Join();

                    sizes = resultThread[0];
                    mins = resultThread[1];
                    
                    break;
                case 8:
                    thread1 = new Thread(() => { averages = calculateAverage(listOpen, listHigh, listLow, listClose); });
                    thread2 = new Thread(() => { stds = calculateStd(listOpen, listHigh, listLow, listClose); });
                    thread3 = new Thread(() => { mins = calculateMin(listOpen, listHigh, listLow, listClose); });
                    thread4 = new Thread(() => { maxs = calculateMax(listOpen, listHigh, listLow, listClose); });
                    //thread5 = new Thread(() => { averages = calculateAverage(listOpen, listHigh, listLow, listClose); });
                    //thread6 = new Thread(() => func_thr(751,876));
                    //thread7 = new Thread(() => func_thr(876,1000));
                    thread1.Start();
                    thread2.Start();
                    thread3.Start();
                    thread4.Start();
                    // thread5.Start();
                    // thread6.Start();
                    // thread7.Start();
                    
                    sizes = calculateCount(listOpen, listHigh, listLow, listClose);

                    thread1.Join();
                    thread2.Join();
                    thread3.Join();
                    thread4.Join();
                    // thread5.Join();
                    // thread6.Join();
                    // thread7.Join();
                    break;
            }

            // sizes = calculateCount(listOpen, listHigh, listLow, listClose);
            // averages = calculateAverage(listOpen, listHigh, listLow, listClose);
            // stds = calculateStd(listOpen, listHigh, listLow, listClose);
            // mins = calculateMin(listOpen, listHigh, listLow, listClose);
            // maxs = calculateMax(listOpen, listHigh, listLow, listClose);
            generateCsvFile(sizes, averages, stds, mins, maxs, no_document);
        }

        static List<List<int>> thread_2(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<List<int>> results = new List<List<int>>();
            List<int> sizes = new List<int>();
            List<int> mins = new List<int>();
            List<int> maxs = new List<int>();

            sizes = calculateCount(listOpen, listHigh, listLow, listClose);
            mins = calculateMin(listOpen, listHigh, listLow, listClose);
            maxs = calculateMax(listOpen, listHigh, listLow, listClose);

            results.Add(sizes);
            results.Add(mins);
            results.Add(maxs);

            return results;
        }

        static List<List<int>> thread_4(IEnumerable<int> listOpen, IEnumerable<int> listHigh, IEnumerable<int> listLow, IEnumerable<int> listClose){
            List<List<int>> results = new List<List<int>>();
            List<int> sizes = new List<int>();
            List<int> mins = new List<int>();

            sizes = calculateCount(listOpen, listHigh, listLow, listClose);
            mins = calculateMin(listOpen, listHigh, listLow, listClose);

            results.Add(sizes);
            results.Add(mins);

            return results;
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
            //var path = @"C:\GitHub\proyecto-Threads\Docker_proyecto\so_respuesta\index_data_out_" + no_document + ".csv";
            var path = Environment.CurrentDirectory + @"/so_respuesta/archivo_out_" + no_document + ".csv";
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
