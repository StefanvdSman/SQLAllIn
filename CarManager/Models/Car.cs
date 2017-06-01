using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool SUV { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} - {2}", Brand, Model, SUV ? "BIG" : "small");
        }
    }
}
