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
using System.Threading;

namespace CarManager
{
    public partial class CarEdit : Form
    {
        private CarsList _listForm;
        private Car _car;

        public CarEdit(CarsList listForm)
        {
            InitializeComponent();
            _listForm = listForm;
            lblTitle.Text = "Toevoegen";
        }

        public CarEdit(CarsList listForm, Car car) : this(listForm)
        {
            lblTitle.Text = "Wijzigen";
            _car = car;
            txtID.Text = car.ID.ToString();
            txtBrand.Text = car.Brand;
            txtModel.Text = car.Model;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_car == null) {
                _car = new Car()
                {
                    Brand = txtBrand.Text,
                    Model = txtModel.Text,
                    SUV = false
                };
                _listForm.SaveNewCar(_car);
            }
            else
            {
                _car.Brand = txtBrand.Text;
                _car.Model = txtModel.Text;
                _listForm.SaveEditCar(_car);
            }

            _listForm.Show();
            Dispose();
        }
    }
}
