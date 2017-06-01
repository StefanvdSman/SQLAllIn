using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarManager.Models;
using MySql.Data.MySqlClient;

namespace CarManager.DatabaseControllers
{
    public class CarDBController : DatabaseController
    {
        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM cars;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                while(dataReader.Read())
                {
                    Car car = new Car()
                    {
                        ID = dataReader.GetInt32("id"),
                        Brand = dataReader.GetString("brand"),
                        Model = dataReader.GetString("model"),
                        SUV = dataReader.GetBoolean("suv")
                    };

                    cars.Add(car);
                }
            }
            catch (Exception e)
            {
                Console.Write("Er ging iets fout bij het ophalen van een car: " + e);
            }
            finally
            {
                conn.Close();
            }

            return cars;
        }

        public void InsertCar(Car car)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string query = @"INSERT INTO cars (brand, model, suv) VALUES (@brand, @model, @suv);";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlParameter brandParam = new MySqlParameter("@brand", MySqlDbType.VarChar);
                MySqlParameter modelParam = new MySqlParameter("@model", MySqlDbType.VarChar);
                MySqlParameter suvParam = new MySqlParameter("@suv", MySqlDbType.Bit);

                brandParam.Value = car.Brand;
                modelParam.Value = car.Model;
                suvParam.Value = car.SUV;

                cmd.Parameters.Add(brandParam);
                cmd.Parameters.Add(modelParam);
                cmd.Parameters.Add(suvParam);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch(Exception e)
            {
                trans.Rollback();
                Console.Write("Er ging iets fout bij het inserten van een car: " + e);
            }
            finally
            {
                conn.Close();
            }
        }
        public void DeleteCar(Car car)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string query = @"DELETE FROM cars WHERE id=(@id);";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int16);
                              
                idParam.Value = car.ID;

                cmd.Parameters.Add(idParam);
               
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Er ging iets fout bij het deleten van een car: " + e);
            }
            finally
            {
                conn.Close();
            }
        }
        public void EditCar(Car car)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string query = @"UPDATE cars
    SET brand = (@brand),
        model = (@model),
        suv   = (@suv) WHERE id =(@id);
    ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlParameter brandParam = new MySqlParameter("@brand", MySqlDbType.VarChar);
                MySqlParameter modelParam = new MySqlParameter("@model", MySqlDbType.VarChar);
                MySqlParameter suvParam = new MySqlParameter("@suv", MySqlDbType.Bit);
                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int16);

                idParam.Value = car.ID;
                brandParam.Value = car.Brand;
                modelParam.Value = car.Model;
                suvParam.Value = car.SUV;

                cmd.Parameters.Add(brandParam);
                cmd.Parameters.Add(modelParam);
                cmd.Parameters.Add(suvParam);
                cmd.Parameters.Add(idParam);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                Console.Write("Er ging iets fout bij het inserten van een car: " + e);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
