using System;
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
        IColourPalette _ColourPalette;

        //IFractal myFractal;
        //public IFractal Fractal
        //{
        //    get
        //    {
        //        return myFractal;
        //    }

        //    set
        //    {
        //        myFractal = value;
        //    }
        //}

        FractalRenderer _Renderer;

        public MainWindow()
        {
            InitializeComponent();

            System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(300,200);
            myImage.Source = BitmapToImageSource(myBitmap);

            _ColourPalette = new ColourPaletteHue();
            //myColourPalette = new ColourPaletteGreyScale();

            IFractal fractal = new FractalMadelbrot(myImage.ActualWidth, myImage.ActualWidth);
            FractalCoordinates.DataContext = fractal.RenderArea;
            Iterations.DataContext = fractal;
            Palette.DataContext = _ColourPalette;

            Area drawingArea = new Area(new Cartesian(0, myImage.ActualWidth), new Cartesian(myImage.ActualWidth, 0));
            _Renderer = new FractalRenderer(fractal, drawingArea, _ColourPalette);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Render(myImage.ActualWidth, myImage.ActualHeight);
        }

        private void Render(double ImageWidth, double ImageHeight)
        {
            Area DrawingArea = new Area(new Cartesian(0, ImageHeight), new Cartesian(ImageWidth, 0));

            DateTime t1 = DateTime.Now;
            System.Drawing.Bitmap myBitmap = _Renderer.render(ImageWidth, ImageHeight);

            myImage.Source = BitmapToImageSource(myBitmap);

            DateTime t2 = DateTime.Now;

            TimeSpan elapsed = t2 - t1;
            MsgLog.AppendText ($"Rendering {ImageWidth}x{ImageHeight} took {elapsed}\n");
            MsgLog.ScrollToEnd();
        }

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

            double zoomFactor = 3;
            zoomFactor = (e.Delta > 0) ? zoomFactor : 1/zoomFactor;

            _Renderer.Zoom(imageCenterCoordinates, zoomFactor);
            Render(myImage.ActualWidth, myImage.ActualHeight);
        }

    }
}
