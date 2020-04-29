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

        private NetworkModel networkModel = new NetworkModel();
        private List<Tuple<UInt64, UInt64>> drawnLines = new List<Tuple<ulong, ulong>>();

        public MainWindow()
        {
            InitializeComponent();

            networkModel = GridHandler.LoadNetworkModel(networkModel, grid, myCanvas);

            DrawLines();
        }


        public void DrawLines()
        {
            for (int i = 0; i < networkModel.Lines.Count; i++)
            {
                UInt64 idStart = networkModel.Lines[i].FirstEnd;    // 41990 (switch id)
                UInt64 idStop = networkModel.Lines[i].SecondEnd;    // 41992

                if (!GridHandler.Entities.ContainsKey(idStart) || !GridHandler.Entities.ContainsKey(idStop))
                {
                    continue;
                }

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

                int rowStart = GridHandler.Entities[idStart].Row;       // 729
                int columnStart = GridHandler.Entities[idStart].Column; // 824

                int rowStop = GridHandler.Entities[idStop].Row;         // 722
                int columnStop = GridHandler.Entities[idStop].Column;   // 821

                double x1 = (columnStart - ScreenHandler.MinJ) / (ScreenHandler.MaxJ - ScreenHandler.MinJ) * myCanvas.Width + 1.5;  //480,61
                double y1 = (rowStart - ScreenHandler.MinI) / (ScreenHandler.MaxI - ScreenHandler.MinI) * myCanvas.Height + 1.5;    //288,55
                double x2 = (columnStop - ScreenHandler.MinJ) / (ScreenHandler.MaxJ - ScreenHandler.MinJ) * myCanvas.Width + 1.5;   //465,94
                double y2 = (rowStop - ScreenHandler.MinI) / (ScreenHandler.MaxI - ScreenHandler.MinI) * myCanvas.Height + 1.5;     //253,29

                // HORIZONTALA
                if (rowStart == rowStop)
                {
                    // u desno
                    if (columnStart < columnStop)
                    {
                        Line line = ScreenHandler.DrawLine(x1, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(line);
                    }
                    // u lijevo
                    else
                    {
                        Line line = ScreenHandler.DrawLine(x1, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(line);
                    }
                }

                // VERTIKALA
                if (columnStart == columnStop)
                {
                    // na dolje
                    if (rowStart < rowStop)
                    {
                        Line line = ScreenHandler.DrawLine(x1, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(line);
                    }
                    // na gore
                    else
                    {
                        Line line = ScreenHandler.DrawLine(x1, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(line);
                    }
                }

                // GORE LIJEVO
                if (rowStart > rowStop && columnStart > columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na gore, ne mijenja se x1 koordinata
                        Line lineUp = ScreenHandler.DrawLine(x1, y1, x1, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineUp);

                        // idem na lijevo, ne mijenja se y2 koordinata
                        Line lineLeft = ScreenHandler.DrawLine(x1, y2, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineLeft);
                    }
                    else
                    {
                        // idem prvo na lijevo, ne mijenja se y1 koordinata
                        Line lineLeft = ScreenHandler.DrawLine(x1, y1, x2, y1, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineLeft);

                        // idem na gore, ne mijenja se x2 koordinata
                        Line lineUp = ScreenHandler.DrawLine(x2, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineUp);
                    }
                }

                // GORE DESNO
                else if (rowStart > rowStop && columnStart < columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na gore, ne mijenja se x1 koordinata
                        Line lineUp = ScreenHandler.DrawLine(x1, y1, x1, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineUp);

                        // idem u desno, ne mijenja se y2 koordinata
                        Line lineRight = ScreenHandler.DrawLine(x1, y2, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineRight);
                    }
                    else
                    {
                        // idem na desno, ne mijenja se y1 koordinata
                        Line lineRight = ScreenHandler.DrawLine(x1, y1, x2, y1, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineRight);

                        // idem na gore, ne mijenja se x2 koordinata
                        Line lineUp = ScreenHandler.DrawLine(x2, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineUp);
                    }
                }

                // DOLE LIJEVO
                else if (rowStart < rowStop && columnStart > columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na dolje, ne mijenja se x1 koordinata
                        Line lineDown = ScreenHandler.DrawLine(x1, y1, x1, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineDown);

                        // idem na levo, ne mijenja se y2 koordinata
                        Line lineLeft = ScreenHandler.DrawLine(x1, y2, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineLeft);
                    }
                    else
                    {
                        // idem prvo na levo, ne mijenja se y1 koordinata
                        Line lineLeft = ScreenHandler.DrawLine(x1, y1, x2, y1, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineLeft);

                        // idem na dole, ne mijenja se x2 koordinata
                        Line lineDown = ScreenHandler.DrawLine(x2, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineDown);
                    }
                }

                // DOLJE DESNO
                else if (rowStart < rowStop && columnStart < columnStop)
                {
                    if (i % 2 == 0)
                    {
                        // idem na dolje, ne mijenja se x1 koordinata
                        Line lineDown = ScreenHandler.DrawLine(x1, y1, x1, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineDown);

                        // idem u desno, ne mijenja se y2 koordinata
                        Line lineRight = ScreenHandler.DrawLine(x1, y2, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineRight);
                    }
                    else
                    {
                        // idem na desno, ne mijenja se y1 koordinata
                        Line lineRight = ScreenHandler.DrawLine(x1, y1, x2, y1, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineRight);

                        // idem na dolje, ne mijenja se x2 koordinata
                        Line lineDown = ScreenHandler.DrawLine(x2, y1, x2, y2, myCanvas, networkModel.Lines[i]);
                        myCanvas.Children.Add(lineDown);
                    }
                }
            }
        }
    }
}
