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
        }
    }
}
