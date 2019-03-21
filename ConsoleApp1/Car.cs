using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Car
    {
        public int ID { get; private set; }
        public int Year { get; set; }
        public int DrivedKM { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public Energy Energy { get; set; }
        public decimal Price { get; set; }
        public String City { get; set; }
        public int DoorCount { get; set; }
        public bool Crashed { get; set; }

        public Car(int id)
        {
            ID = id;
        }



        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID);
            sb.Append("\t");
            sb.Append(Year);
            sb.Append("\t");
            sb.Append(DrivedKM);
            sb.Append("\t");
            sb.Append(Brand);
            sb.Append("\t");
            sb.Append(Model);
            sb.Append("\t");
            sb.Append(Energy);
            sb.Append("\t");
            sb.Append(Price);
            sb.Append("\t");
            sb.Append(City);
            sb.Append("\t");
            sb.Append(DoorCount);
            sb.Append("\t");
            sb.Append(Crashed);
            sb.Append("\n");
            return sb.ToString();
        }
    }
}
