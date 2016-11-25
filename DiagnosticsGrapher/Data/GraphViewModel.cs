using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using System.ComponentModel;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DiagnosticsGrapher.Data
{
    public class GraphViewModel<T> 
    {
        public List<Graph<T>> Graphs;

        public PlotModel PlotModel { get; set; }

        public GraphViewModel(List<Graph<T>> graphs, double smallestYValue)
        {
            Graphs = graphs;
            
            var model = new PlotModel();

            var dateTimeAxis = new DateTimeAxis()
            {
                Position = AxisPosition.Bottom
            };
            var linearAxis = new LinearAxis();

            model.Axes.Add(dateTimeAxis);
            model.Axes.Add(linearAxis);

            foreach (var graph in Graphs)
            {
                var lineSeries = new LineSeries()
                {
                    DataFieldX = "Date",
                    DataFieldY = "Value",
                    ItemsSource = graph
                };
                model.Series.Add(lineSeries);
            }

            PlotModel = model;
        }
    }
}
