﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace Fractal1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<IColourPalette> _ColourPalettes;
        IColourPalette _ColourPalette;

        IFractal _Fractal;
        public IFractal Fractal
        {
            get
            {
                return _Fractal;
            }

            set
            {
                _Fractal = value;
            }
        }

        IArea _DrawingArea;
        public IArea DrawingArea
        {
            get
            {
                return _DrawingArea;
            }

            set
            {
                _DrawingArea = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(300,200);
            myImage.Source = BitmapToImageSource(myBitmap);


            Fractal = new FractalMadelbrot(myImage.ActualWidth, myImage.ActualWidth);
            FractalCoordinates.DataContext = Fractal.RenderArea;
            Iterations.DataContext = Fractal;


            _ColourPalettes = new List<IColourPalette>();
            _ColourPalettes.Add(new ColourPaletteGreyScale());
            _ColourPalettes.Add(new ColourPaletteHue());
            PaletteList.ItemsSource = _ColourPalettes;
            PaletteList.SelectedIndex = 0;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            Fractal.Reset();
            Render(myImage.ActualWidth, myImage.ActualHeight);
        }

        private void Render(double ImageWidth, double ImageHeight)
        {
            DrawingArea = new Area(new Cartesian(0, ImageHeight), new Cartesian(ImageWidth, 0));

            DateTime t1 = DateTime.Now;

            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap((int)Math.Floor(ImageWidth), (int)Math.Floor(ImageHeight));

            // Here, we need to get the fractal to generate a 2-d array of values
            int[][] array = Fractal.ArrayValues(myBitmap.Width, myBitmap.Height, DrawingArea);

            // Then we need to convert that array into a bitmap.
            ArrayToBitmap(array, ref myBitmap); // <-- by reference

            myImage.Source = BitmapToImageSource(myBitmap);

            DateTime t2 = DateTime.Now;

            TimeSpan elapsed = t2 - t1;
            MsgLog.Text += $"Rendering {ImageWidth}x{ImageHeight} took {elapsed}\n";
        }

        private void ArrayToBitmap(int[][] array, ref Bitmap myBitmap)
        {
            for (int x = 0; x < myBitmap.Width; x++)
            {
                for (int y = 0; y < myBitmap.Height; y++)
                {
                    System.Drawing.Color color = _ColourPalette.ColourFromValue(array[x][y]);
                    myBitmap.SetPixel(x, y, color);
                }
            }
        }

        //private System.Drawing.Color GetColourForCoordinate(IFractal f, double x, double y)
        //{
        //    ICartesian proportion = DrawingArea.ProportionFromPoint(new Cartesian(x, y));

        //    int v = f.PointValue(proportion) ;

        //    return _ColourPalette.ColourFromValue(v);
        //}

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void myImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MsgLog.Text += $"Left button down at {e.GetPosition(myImage).ToString()}\n";
        }

        private void myImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MsgLog.Text += $"Left button down at {e.GetPosition(myImage).ToString()}\n";
        }

        private void myImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ICartesian imageCenterCoordinates = new Cartesian(e.GetPosition(myImage).X, e.GetPosition(myImage).Y);
            ICartesian imageCenterProportion = DrawingArea.ProportionFromPoint(imageCenterCoordinates);

            double zoomFactor = 3;
            zoomFactor = (e.Delta > 0) ? zoomFactor : 1/zoomFactor;
            Fractal.Zoom(imageCenterProportion, new Cartesian(zoomFactor, zoomFactor));
            Render(myImage.ActualWidth, myImage.ActualHeight);
        }

        private void PaletteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox b = (ComboBox)sender;
            _ColourPalette = (IColourPalette)b.SelectedItem;
        }

        private void redrawButton_Click(object sender, RoutedEventArgs e)
        {
            Render(myImage.ActualWidth, myImage.ActualHeight);
        }
    }
}
