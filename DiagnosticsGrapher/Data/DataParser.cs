using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;

namespace DiagnosticsGrapher.Data
{
    public struct Point<Tx, Ty>
    {
        private KeyValuePair<Tx, Ty> point;

        public Point(Tx x, Ty y) 
        {
            this.point = new KeyValuePair<Tx, Ty>(x, y);
        }

        public Tx X
        {
            get { return point.Key; }
        }

        public Ty Y
        {
            get { return point.Value; }
        }
    }

    public class Points<Tx, Ty> : List<Point<Tx, Ty>> 
    {
        public string Title { get; set; }

        public Points(string title)
        {
            this.Title = title;
        }
    }

    public class DataParser
    {
        public static List<Points<DateTime, double>> ParseXml(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            string rawXml = string.Empty;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                rawXml = streamReader.ReadToEnd();
            }

            if (rawXml.Equals(string.Empty)) return null;

            try
            {

                XDocument xmlDocument = XDocument.Parse(rawXml);

                List<Points<DateTime, double>> data = new List<Points<DateTime, double>>();

                var rootNode = xmlDocument.Root;

                foreach (var sample in rootNode.Elements())
                {
                    DateTime timestamp = DateTime.Parse(sample.Attribute("Timestamp").Value);

                    foreach (var category in sample.Elements())
                    {
                        foreach (var statistic in category.Elements())
                        {
                            string key = statistic.Name.LocalName.Replace('_', ' ').Trim();
                            double value = double.Parse(statistic.Value);

                            Points<DateTime, double> points = null;
                            // Create list to hold values for this header of statistics
                            if (!data.Exists(e => e.Title == key))
                            {
                                points = new Points<DateTime, double>(key);
                                data.Add(points);
                            }
                            else
                            {
                                points = data.Find(e => e.Title == key);
                            }

                            Point<DateTime, double> dataPair = new Point<DateTime, double>(timestamp, value);

                            points.Add(dataPair);
                        }
                    }
                }

                return data;
            }
            catch
            {
                throw new ArgumentException();
            }
        }
    }
}
