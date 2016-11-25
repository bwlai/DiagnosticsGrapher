using System.Collections.Generic;
using OxyPlot;
using DiagnosticsGrapher;
using System;

namespace DiagnosticsGrapher.Data
{
    public class DateValue
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }

    public class Graph<T> : List<T>
    {
        public string Title { get; set; }
        public string XAxisLabel { get; set; }
        public string YAxisLabel { get; set; }
    }

    public class Grapher
    {
        public static void StartUp()
        {
            var data = DataParser.ParseXml(@"C:\Users\blai\AppData\Local\Colligo\Engage\Diagnostics\meow.txt");

            List<Graph<DateValue>> graphs = new List<Graph<DateValue>>();

            double smallest = double.MaxValue;

            foreach (var coordinates in data)
            {
                Graph<DateValue> graph = new Graph<DateValue>();

                graph.Title = coordinates.Title;
                
                foreach (var coordinate in coordinates)
                {
                    graph.Add(new DateValue() { Date = coordinate.X, Value = coordinate.Y });
                    var y = coordinate.Y;
                    if (y < smallest && y != 0.0)
                    {
                        smallest = y;
                    }
                    //graph.Add(new DataPoint(coordinate.Y, coordinate.Y));
                }

                graphs.Add(graph);
            }

            var graphViewModel = new GraphViewModel<DateValue>(graphs, smallest);
            var graphWindow = new MainWindow();
            graphWindow.DataContext = graphViewModel;
            graphWindow.ShowDialog();
        }
    }
}
