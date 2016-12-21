using System.Collections.Generic;
using OxyPlot;
using DiagnosticsGrapher;
using System;
using System.Windows;

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
        private static GraphViewModel<DateValue> graphViewModel;

        public static void StartUp()
        {
            graphViewModel = new GraphViewModel<DateValue>();
            var graphWindow = new MainWindow();

            graphWindow.DataContext = graphViewModel;
            graphWindow.SelectionChanged += graphViewModel.OnSelectionChanged;
            graphWindow.DragDrop += OnDragDrop;
            graphWindow.ComparisonChanged += graphViewModel.OnComparisonChanged;

            graphWindow.ShowDialog();
        }

        public static void OnDragDrop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                try
                {
                    var data = DataParser.ParseXml(files[0]);

                    List<Graph<DateValue>> graphs = new List<Graph<DateValue>>();

                    foreach (var coordinates in data)
                    {
                        Graph<DateValue> graph = new Graph<DateValue>();

                        graph.Title = coordinates.Title;

                        foreach (var coordinate in coordinates)
                        {
                            graph.Add(new DateValue() { Date = coordinate.X, Value = coordinate.Y });
                        }

                        graphs.Add(graph);
                    }

                    graphViewModel.Populate(graphs);
                }
                catch
                {
                    System.Console.WriteLine("Unable to parse file \"{0}\".", files[0]);
                }

            }
        }
    }
}
