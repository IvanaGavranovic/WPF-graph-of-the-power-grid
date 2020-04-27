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
            // NODES
            for (int i = 0; i < networkModel.Nodes.Count; i++)
            {
                double decimalX, decimalY;
                CoordinatesHandler.ToLatLon(networkModel.Nodes[i].X, networkModel.Nodes[i].Y, 34, out decimalX, out decimalY);
                networkModel.Nodes[i].X = decimalX;
                networkModel.Nodes[i].Y = decimalY;

                int indexI, indexJ;
                CoordinatesHandler.FromCoordsToIndex(decimalX, decimalY, out indexI, out indexJ);

                if (grid[indexI, indexJ] == 0)
                {
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    networkGrid[indexI, indexJ] = networkModel.Nodes[i].Id;
                }
                else
                {
                    bool spaceFound = FindFreeSpaceForNode(networkModel, grid, indexI, indexJ, i, out indexI, out indexJ);

                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    networkGrid[indexI, indexJ] = networkModel.Nodes[i].Id;

                    if (!spaceFound)
                    {
                        counter++;
                    }
                }

                networkModel.Nodes[i].Row = indexI;
                networkModel.Nodes[i].Column = indexJ;

                Entities.Add(networkModel.Nodes[i].Id, networkModel.Nodes[i]);

                ImageDrawing image = ScreenHandler.DrawNodeImage(indexI, indexJ, myCanvas);
                drawingGroup.Children.Add(image);
            }

            // SWITCHES
            for (int i = 0; i < networkModel.Switches.Count; i++)
            {
                double decimalX, decimalY;
                CoordinatesHandler.ToLatLon(networkModel.Switches[i].X, networkModel.Switches[i].Y, 34, out decimalX, out decimalY);
                networkModel.Switches[i].X = decimalX;
                networkModel.Switches[i].Y = decimalY;

                int indexI, indexJ;
                CoordinatesHandler.FromCoordsToIndex(decimalX, decimalY, out indexI, out indexJ);

                if (grid[indexI, indexJ] == 0)
                {
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    networkGrid[indexI, indexJ] = networkModel.Switches[i].Id;
                }
                else
                {
                    bool spaceFound = FindFreeSpaceForSwitch(networkModel, grid, indexI, indexJ, i, out indexI, out indexJ);

                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    networkGrid[indexI, indexJ] = networkModel.Switches[i].Id;

                    if (!spaceFound)
                    {
                        counter++;
                    }
                }

                networkModel.Switches[i].Row = indexI;
                networkModel.Switches[i].Column = indexJ;

                Entities.Add(networkModel.Switches[i].Id, networkModel.Switches[i]);

                ImageDrawing image = ScreenHandler.DrawSwitchImage(indexI, indexJ, myCanvas);
                drawingGroup.Children.Add(image);
            }

            return networkModel;
        }

        public static bool FindFreeSpaceForNode(NetworkModel networkModel, UInt64[,] grid, int indexI, int indexJ, int i, out int freeI, out int freeJ)
        {
            bool spaceFound = false;
            int step = 1;

            while (!spaceFound)
            {
                if (grid[indexI, (indexJ - step) % 1000] == 0)
                {
                    indexJ = (indexJ - step) % 1000;
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI - step) % 1000, (indexJ - step) % 1000] == 0)
                {
                    indexI = (indexI - step) % 1000;
                    indexJ = (indexJ - step) % 1000;
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI - step) % 1000, indexJ] == 0)
                {
                    indexI = (indexI - step) % 1000;
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    spaceFound = true;
                }
                else if (grid[indexI, (indexJ + step) % 1000] == 0)
                {
                    indexJ = (indexJ + step) % 1000;
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI + step) % 1000, (indexJ + step) % 1000] == 0)
                {
                    indexI = (indexI + step) % 1000;
                    indexJ = (indexJ + step) % 1000;
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI + step) % 1000, indexJ] == 0)
                {
                    indexI = (indexI + step) % 1000; ;
                    grid[indexI, indexJ] = networkModel.Nodes[i].Id;
                    spaceFound = true;
                }

                if (++step == 16)
                {
                    break;
                }
            }

            freeI = indexI;
            freeJ = indexJ;

            return spaceFound;
        }

        public static bool FindFreeSpaceForSwitch(NetworkModel networkModel, UInt64[,] grid, int indexI, int indexJ, int i, out int freeI, out int freeJ)
        {
            bool spaceFound = false;
            int step = 1;

            while (!spaceFound)
            {
                if (grid[indexI, (indexJ - step) % 1000] == 0)
                {
                    indexJ = (indexJ - step) % 1000;
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI - step) % 1000, (indexJ - step) % 1000] == 0)
                {
                    indexI = (indexI - step) % 1000;
                    indexJ = (indexJ - step) % 1000;
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI - step) % 1000, indexJ] == 0)
                {
                    indexI = (indexI - step) % 1000;
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    spaceFound = true;
                }
                else if (grid[indexI, (indexJ + step) % 1000] == 0)
                {
                    indexJ = (indexJ + step) % 1000;
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI + step) % 1000, (indexJ + step) % 1000] == 0)
                {
                    indexI = (indexI + step) % 1000;
                    indexJ = (indexJ + step) % 1000;
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    spaceFound = true;
                }
                else if (grid[(indexI + step) % 1000, indexJ] == 0)
                {
                    indexI = (indexI + step) % 1000; ;
                    grid[indexI, indexJ] = networkModel.Switches[i].Id;
                    spaceFound = true;
                }

                if (++step == 16)
                {
                    break;
                }
            }

            freeI = indexI;
            freeJ = indexJ;

            return spaceFound;
        }

    }
}
