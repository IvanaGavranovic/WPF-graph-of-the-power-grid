using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PredmetniZadatak2.Handlers
{
    public class ScreenHandler
    { 
        public static ImageDrawing DrawSubstationImage(int indexI, int indexJ, Canvas myCanvas)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\circle4.png", UriKind.Relative));

            image.Rect = new Rect((indexJ / 1000.0) * myCanvas.Width * 4.7, (indexI / 1000.0) * myCanvas.Height * 7, 3, 3);

            return image;
        }

        public static ImageDrawing DrawNodeImage(int indexI, int indexJ, Canvas myCanvas)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\circle5.png", UriKind.Relative));

            image.Rect = new Rect((indexJ / 1000.0) * myCanvas.Width * 4.7, (indexI / 1000.0) * myCanvas.Height * 7, 3, 3);

            return image;
        }

        public static ImageDrawing DrawSwitchImage(int indexI, int indexJ, Canvas myCanvas)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\circle1.png", UriKind.Relative));

            image.Rect = new Rect((indexJ / 1000.0) * myCanvas.Width * 4.7, (indexI / 1000.0) * myCanvas.Height * 7, 3, 3);

            return image;
        }

        public static ImageDrawing DrawRightHorizontalLine(double x1, double y1, double x2, double y2)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\horizontalLine.png", UriKind.Relative));

            image.Rect = new Rect(x1 + 1.5, y1 + 0.5, x2 - x1, 2);

            return image;
        }

        public static ImageDrawing DrawLeftHorizontalLine(double x1, double y1, double x2, double y2)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\horizontalLine.png", UriKind.Relative));

            image.Rect = new Rect(x2 + 1.5, y1 + 0.5, x1 - x2, 2);

            return image;
        }

        public static ImageDrawing DrawDownVerticalLine(double x1, double y1, double x2, double y2)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\verticalLine.png", UriKind.Relative));

            image.Rect = new Rect(x1 + 0.5, y1 + 1.5, 2, y2 - y1);

            return image;
        }

        public static ImageDrawing DrawUpVerticalLine(double x1, double y1, double x2, double y2)
        {
            ImageDrawing image = new ImageDrawing();
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\verticalLine.png", UriKind.Relative));
            image.Rect = new Rect(x1 + 0.5, y2 + 1.5, 2, y1 - y2);

            return image;
        }
    }
}
