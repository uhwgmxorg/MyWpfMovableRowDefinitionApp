using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace MyWpfMovableRowDefinitionApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _mouseDown = false;
        private int _counter = 0;

        #region INotify Changed Properties  
        private string mousePos;
        public string MousePos
        {
            get { return mousePos; }
            set { SetField(ref mousePos, value, nameof(MousePos)); }
        }

        // Template for a new INotify Changed Property
        // for using CTRL-R-R
        private bool xxx;
        public bool Xxx
        {
            get { return xxx; }
            set { SetField(ref xxx, value, nameof(Xxx)); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Window_MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            double titleHeight = SystemParameters.WindowCaptionHeight + SystemParameters.ResizeFrameHorizontalBorderHeight;
            double verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
            double clientAreaHeight = Height - titleHeight - verticalBorderWidth;

            // For Debuging
            MousePos = String.Format("MousePos X={0} Y={1} ClientAreaHeight={2}", e.GetPosition(this).X, e.GetPosition(this).Y, clientAreaHeight);

            if (_mouseDown)
            {
                double d;

                d = e.GetPosition(this).Y / clientAreaHeight;

                Debug.WriteLine(String.Format("{0} d={1} Height={2} Y={3}", ++_counter, d, Height, e.GetPosition(this).Y));

                gridMain.RowDefinitions[0].Height = new GridLength(d, GridUnitType.Star);
                gridMain.RowDefinitions[1].Height = new GridLength(1-d, GridUnitType.Star);
            }
        }

        /// <summary>
        /// Window_MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _mouseDown = false;
            Debug.WriteLine(String.Format("Window_MouseUp {0}", _mouseDown));
        }

        /// <summary>
        /// Window_MouseLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _mouseDown = false;
            Debug.WriteLine(String.Format("Window_MouseLeave {0}", _mouseDown));
        }

        /// <summary>
        /// gridMarker_MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridMarker_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _mouseDown = true;
            Debug.WriteLine(String.Format("{0}", _mouseDown));
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }
}
