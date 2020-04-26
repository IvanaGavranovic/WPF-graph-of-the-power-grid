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
            image.ImageSource = new BitmapImage(new Uri(@"..\..\Images\circle3.png", UriKind.Relative));

            image.Rect = new Rect((indexJ / 1000.0) * myCanvas.Width * 4.7, (indexI / 1000.0) * myCanvas.Height * 7, 3, 3);

            return image;
        }
    }
}
