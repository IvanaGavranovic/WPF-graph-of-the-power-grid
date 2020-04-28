using PredmetniZadatak2.Classes;
using PredmetniZadatak2.Handlers;
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

namespace PredmetniZadatak2
{
    public partial class MainWindow : Window
    {
        public UInt64[,] grid = new UInt64[1000, 1000];

        private List<Tuple<UInt64, UInt64>> drawnLines = new List<Tuple<ulong, ulong>>();

        private NetworkModel networkModel = new NetworkModel();
       
        private DrawingImage drawingImage;
        private DrawingGroup drawingGroup;

        public MainWindow()
        {
            InitializeComponent();

            drawingImage = new DrawingImage();
            mainScreen.Source = drawingImage;

            drawingGroup = new DrawingGroup();
            drawingImage.Drawing = drawingGroup;

            grid = GridHandler.MakeGrid();
            networkModel = GridHandler.LoadNetworkModel(networkModel, grid, drawingGroup, myCanvas, out grid);

            DrawLines();
        }

        public void DrawLines()
        {
            for (int i = 0; i < networkModel.Lines.Count; i++)
            {
                UInt64 idStart = networkModel.Lines[i].FirstEnd;
                UInt64 idStop = networkModel.Lines[i].SecondEnd;

                // provera da li oba ID-a uopste postoje
                if (!GridHandler.Entities.ContainsKey(idStart) || !GridHandler.Entities.ContainsKey(idStop))
                {
                    continue;
                }

                // provera da li je vec nacrtana ta linija -> putanja A-B je isto sta i B-A, tako da je ne treba crtati
                if (i == 0)
                {
                    drawnLines.Add(new Tuple<ulong, ulong>(idStart, idStop));
                }
                else if (drawnLines.Contains(new Tuple<ulong, ulong>(idStart, idStop)) || drawnLines.Contains(new Tuple<ulong, ulong>(idStop, idStart)))
                {
                    continue;
                }
                else
                {
                    drawnLines.Add(new Tuple<ulong, ulong>(idStart, idStop));
                }

                // indeks prvog cvora
                int rowStart = GridHandler.Entities[idStart].Row;
                int columnStart = GridHandler.Entities[idStart].Column;

                // indeks drugog cvora
                int rowStop = GridHandler.Entities[idStop].Row;
                int columnStop = GridHandler.Entities[idStop].Column;

                double x1 = (columnStart / 1000.0) * myCanvas.Width * 4.7;
                double y1 = (rowStart / 1000.0) * myCanvas.Height * 7;
                double x2 = (columnStop / 1000.0) * myCanvas.Width * 4.7;
                double y2 = (rowStop / 1000.0) * myCanvas.Height * 7;

                // HORIZONTALA
                if (rowStart == rowStop)
                {
                    // u desno
                    if (columnStart < columnStop)
                    {
                        ImageDrawing line = ScreenHandler.DrawRightHorizontalLine(x1, y1, x2, y2);
                        drawingGroup.Children.Add(line);
                    }
                    // u levo
                    else
                    {
                        ImageDrawing line = ScreenHandler.DrawLeftHorizontalLine(x1, y1, x2, y2);
                        drawingGroup.Children.Add(line);
                    }
                }

                // VERTIKALA
                if (columnStart == columnStop)
                {
                    // na dole
                    if (rowStart < rowStop)
                    {
                        ImageDrawing line = ScreenHandler.DrawDownVerticalLine(x1, y1, x2, y2);
                        drawingGroup.Children.Add(line);
                    }
                    // na gore
                    else
                    {
                        ImageDrawing line = ScreenHandler.DrawUpVerticalLine(x1, y1, x2, y2);
                        drawingGroup.Children.Add(line);
                    }
                }

                // GORE LEVO
                if (rowStart > rowStop && columnStart > columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na gore, ne menja se x1 koordinata
                        ImageDrawing lineUp = ScreenHandler.DrawUpVerticalLine(x1, y1, x1, y2);
                        drawingGroup.Children.Add(lineUp);

                        // idem na levo, ne menja se y2 koordinata
                        ImageDrawing lineLeft = ScreenHandler.DrawLeftHorizontalLine(x1, y2, x2, y2);
                        drawingGroup.Children.Add(lineLeft);
                    }
                    else
                    {
                        // idem prvo na levo, ne menja se y1 koordinata
                        ImageDrawing lineLeft = ScreenHandler.DrawLeftHorizontalLine(x1, y1, x2, y1);
                        drawingGroup.Children.Add(lineLeft);

                        // idem na gore, ne menja se x2 koordinata
                        ImageDrawing lineUp = ScreenHandler.DrawUpVerticalLine(x2, y1, x2, y2);
                        drawingGroup.Children.Add(lineUp);
                    }
                }

                // GORE DESNO
                else if (rowStart > rowStop && columnStart < columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na gore, ne menja se x1 koordinata
                        ImageDrawing lineUp = ScreenHandler.DrawUpVerticalLine(x1, y1, x1, y2);
                        drawingGroup.Children.Add(lineUp);

                        // idem u desno, ne menja se y2 koordinata
                        ImageDrawing lineRight = ScreenHandler.DrawRightHorizontalLine(x1, y2, x2, y2);
                        drawingGroup.Children.Add(lineRight);
                    }
                    else
                    {
                        // idem na desno, ne menja se y1 koordinata
                        ImageDrawing lineRight = ScreenHandler.DrawRightHorizontalLine(x1, y1, x2, y1);
                        drawingGroup.Children.Add(lineRight);

                        // idem na gore, ne menja se x2 koordinata
                        ImageDrawing lineUp = ScreenHandler.DrawUpVerticalLine(x2, y1, x2, y2);
                        drawingGroup.Children.Add(lineUp);
                    }
                }

                // DOLE LEVO
                else if (rowStart < rowStop && columnStart > columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na dole, ne menja se x1 koordinata
                        ImageDrawing lineDown = ScreenHandler.DrawDownVerticalLine(x1, y1, x1, y2);
                        drawingGroup.Children.Add(lineDown);

                        // idem na levo, ne menja se y2 koordinata
                        ImageDrawing lineLeft = ScreenHandler.DrawLeftHorizontalLine(x1, y2, x2, y2);
                        drawingGroup.Children.Add(lineLeft);
                    }
                    else
                    {
                        // idem prvo na levo, ne menja se y1 koordinata
                        ImageDrawing lineLeft = ScreenHandler.DrawLeftHorizontalLine(x1, y1, x2, y1);
                        drawingGroup.Children.Add(lineLeft);

                        // idem na dole, ne menja se x2 koordinata
                        ImageDrawing lineDown = ScreenHandler.DrawDownVerticalLine(x2, y1, x2, y2);
                        drawingGroup.Children.Add(lineDown);
                    }
                }

                // DOLE DESNO
                else if (rowStart < rowStop && columnStart < columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na dole, ne menja se x1 koordinata
                        ImageDrawing lineDown = ScreenHandler.DrawDownVerticalLine(x1, y1, x1, y2);
                        drawingGroup.Children.Add(lineDown);

                        // idem u desno, ne menja se y2 koordinata
                        ImageDrawing lineRight = ScreenHandler.DrawRightHorizontalLine(x1, y2, x2, y2);
                        drawingGroup.Children.Add(lineRight);
                    }
                    else
                    {
                        // idem na desno, ne menja se y1 koordinata
                        ImageDrawing lineRight = ScreenHandler.DrawRightHorizontalLine(x1, y1, x2, y1);
                        drawingGroup.Children.Add(lineRight);

                        // idem na dole, ne menja se x2 koordinata
                        ImageDrawing lineDown = ScreenHandler.DrawDownVerticalLine(x2, y1, x2, y2);
                        drawingGroup.Children.Add(lineDown);
                    }
                }
            }
        }
    }
}
