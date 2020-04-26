using PredmetniZadatak2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PredmetniZadatak2.Handlers
{
    public class GridHandler
    {
        public static Dictionary<UInt64, Entity> Entities = new Dictionary<UInt64, Entity>();

        public static UInt64[,] MakeGrid()
        {
            UInt64[,] grid = new UInt64[1000, 1000];

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    grid[i, j] = 0;
                }
            }

            return grid;
        }
        public static NetworkModel LoadNetworkModel(NetworkModel networkModel, UInt64[,] grid, DrawingGroup drawingGroup, Canvas myCanvas, out UInt64[,] networkGrid)
        {
            networkGrid = new UInt64[1000, 1000];
            int counter = 0;

            networkModel = XmlHandler.Load<NetworkModel>(@"..\..\Geographic.xml");

            // SUBSTATIONS
            for (int i = 0; i < networkModel.Substations.Count; i++)
            {
                double decimalX, decimalY;
                CoordinatesHandler.ToLatLon(networkModel.Substations[i].X, networkModel.Substations[i].Y, 34, out decimalX, out decimalY);
                networkModel.Substations[i].X = decimalX;
                networkModel.Substations[i].Y = decimalY;

                int indexI, indexJ;
                CoordinatesHandler.FromCoordsToIndex(decimalX, decimalY, out indexI, out indexJ);

                if (grid[indexI, indexJ] == 0)
                {
                    networkGrid[indexI, indexJ] = networkModel.Substations[i].Id;
                    grid[indexI, indexJ] = networkModel.Substations[i].Id;

                    networkModel.Substations[i].Row = indexI;
                    networkModel.Substations[i].Column = indexJ;

                    Entities.Add(networkModel.Substations[i].Id, networkModel.Substations[i]);
                }
                ImageDrawing image = ScreenHandler.DrawSubstationImage(indexI, indexJ, myCanvas);
                drawingGroup.Children.Add(image);
            }
        
            return networkModel;

        }
    }
}
