using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetniZadatak2.Classes
{
    public class Entity
    {
        private UInt64 id;
        private string name;
        private double x;
        private double y;

        private int row;
        private int column;

        public UInt64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        public int Row { get => row; set => row = value; }
        public int Column { get => column; set => column = value; }

        public Entity() { }
    }
}
