using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagementSystem_V2
{
    internal class Car
    {
        private int id;

        public Car(int id, string brand, string model, decimal rentalPrice)
        {
            this.id = id;
            Brand = brand;
            Model = model;
            RentalPrice = rentalPrice;
        }

        public string CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal RentalPrice { get; set; }
    }
}
