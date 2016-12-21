using System.Windows;
using OxyPlot;
using System.Collections.Generic;
using System;

namespace DiagnosticsGrapher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public EventHandler SelectionChanged;
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, null);
            }
        }

        public EventHandler ComparisonChanged;
        private void ComboBox_ComparisonChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComparisonChanged != null)
            {
                ComparisonChanged(this, null);
            }
        }

        public DragEventHandler DragDrop;
        private void PlotView_Drop(object sender, DragEventArgs e)
        {
            if (DragDrop != null)
            {
                DragDrop(this, e);
            }
        }
    }
}
