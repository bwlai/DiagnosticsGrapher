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
    public class GraphViewModel<T> : INotifyPropertyChanged
    {
        #region Properties

        private List<Graph<T>> m_Graphs;
        public List<Graph<T>> Graphs 
        { 
            get
            {
                return m_Graphs;
            }
            set
            {
                if (m_Graphs != value)
                {
                    m_Graphs = value;
                    OnPropertyChanged("Graphs");
                }
            }
        }

        private PlotModel m_CurrentPlotModel = null;
        public PlotModel CurrentPlotModel 
        { 
            get
            {
                return m_CurrentPlotModel;
            }
            set
            {
                if (m_CurrentPlotModel != value)
                {
                    m_CurrentPlotModel = value;
                    OnPropertyChanged("CurrentPlotModel");
                }
            }
        }

        private PlotModel m_ComparisonPlotModel = null;
        public PlotModel ComparisonPlotModel
        {
            get
            {
                return m_ComparisonPlotModel;
            }
            set
            {
                if (m_ComparisonPlotModel != value)
                {
                    m_ComparisonPlotModel = value;
                    OnPropertyChanged("ComparisonPlotModel");
                }
            }
        }

        private Graph<T> m_SelectedGraph;
        public Graph<T> SelectedGraph
        {
            get
            {
                return m_SelectedGraph;
            }
            set
            {
                if (m_SelectedGraph != value)
                {
                    m_SelectedGraph = value;
                    OnPropertyChanged("SelectedGraph");
                }
            }
        }

        private Graph<T> m_ComparisonGraph;
        public Graph<T> ComparisonGraph
        {
            get
            {
                return m_ComparisonGraph;
            }
            set 
            { 
                if (m_ComparisonGraph != value)
                {
                    m_ComparisonGraph = value;
                    OnPropertyChanged("ComparisonGraph");
                }
            }
        }

        #endregion

        public GraphViewModel() { }

        public void Populate(List<Graph<T>> graphs)
        {
            if (graphs.Count <= 0) return;

            SelectedGraph = graphs[0];
            ComparisonGraph = graphs[0];

            CurrentPlotModel = CreatePlotModel(SelectedGraph);
            ComparisonPlotModel = CreatePlotModel(ComparisonGraph);
            Graphs = graphs;
        }


        public void OnSelectionChanged(object sender, EventArgs args)
        {
             CurrentPlotModel = CreatePlotModel(SelectedGraph);
        }
        
        public void OnComparisonChanged(object sender, EventArgs args)
        {
            ComparisonPlotModel = CreatePlotModel(ComparisonGraph);
        }

        public PlotModel CreatePlotModel(params Graph<T>[] graphs)
        {
            var model = new PlotModel();

            // Axes initialization
            var dateTimeAxis = new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Dash,
                Title = "Time"
            };
            var linearAxis = new LinearAxis() 
            { 
                LabelFormatter = LabelFormatter,
                MajorGridlineStyle = LineStyle.Dash,
            };

            model.Axes.Add(dateTimeAxis);
            model.Axes.Add(linearAxis);

            try
            {
                foreach(var graph in graphs)
                {
                    var lineSeries = new LineSeries()
                    {
                        DataFieldX = "Date",
                        DataFieldY = "Value",
                        ItemsSource = graph
                    };
                    model.Series.Add(lineSeries);
                }
            }
            catch
            {
                throw new ArgumentException();
            }

            model.IsLegendVisible = true;
            
            return model;
        }


        // ------------------------------

        private string LabelFormatter(double value)
        {
            if (value < 1E3)
            {
                return String.Format("{0}", value);
            }
            else if (value >= 1E3 && value < 1E6)
            {
                return String.Format("{0}K", value / 1E3);
            }
            else if (value >= 1E6 && value < 1E9)
            {
                return String.Format("{0}M", value / 1E6);
            }
            else if (value >= 1E9)
            {
                return String.Format("{0}B", value / 1E9);
            }
            else
            {
                return String.Format("{0}", value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
