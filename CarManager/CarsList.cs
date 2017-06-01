using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarManager.Models;
using CarManager.DatabaseControllers;

namespace CarManager
{
    public partial class CarsList : Form
    {
        private CarDBController _carDB; 
        public CarsList()
        {
            _carDB = new CarDBController();
            InitializeComponent();
            InitializeList();
           
        }
        List<Car> cars;
        private void InitializeList()
        {
           
            lstCars.Items.Clear();
            cars = _carDB.GetAllCars();
            /*{
                new Car() {ID = 1, Brand = "BMW", Model = "X5", SUV = true },
                new Car() {ID = 2, Brand = "VW", Model = "Golf", SUV = false },
                new Car() {ID = 3, Brand = "Audi", Model = "Q7", SUV = true },
                new Car() {ID = 4, Brand = "Mercedes", Model = "GLS", SUV = true },
                new Car() {ID = 1, Brand = "Mini", Model = "Cooper S", SUV = false }
            };*/
            
            foreach (Car car in cars)
            {
                lstCars.Items.Add(car);
            }
        }

        public void SaveNewCar(Car car)
        {
            car.ID = lstCars.Items.Cast<Car>().Max(x => x.ID) + 1;
            lstCars.Items.Add(car);
            _carDB.InsertCar(car);
        }

        public void SaveEditCar(Car car)
        {
            int index = -1;
            for (int i = 0; i < lstCars.Items.Count; i++)
            {
                Car c = lstCars.Items[i] as Car;
                if(c.ID == car.ID)
                {
                    index = i;
                }
            }
            lstCars.Items[index] = car;
            _carDB.EditCar(car);
        }

        private void RemoveCar(Car car)
        {
            lstCars.Items.Remove(car);
            _carDB.DeleteCar(car);
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            new CarEdit(this).Show();
            this.Hide();
        }

        private void btnRemoveCar_Click(object sender, EventArgs e)
        {
            if(lstCars.SelectedItem != null)
            {
                RemoveCar((Car)lstCars.SelectedItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstCars.SelectedItem != null)
            {
                new CarEdit(this, (Car)lstCars.SelectedItem).Show();
                this.Hide();
            }
        }

        private void CarsList_Load(object sender, EventArgs e)
        {

        }
    }
}
