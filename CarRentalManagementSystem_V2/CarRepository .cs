using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagementSystem_V2
{
    internal class CarRepository
    {

        private string carDbConnectionString = "Server=(localdb)\\MSSQLLocalDB; Database=CarRentalManagement; Trusted_Connection=True; TrustServerCertificate=True;";

        public void CreateCar(string brand, string model, decimal rentalPrice)
        {
            try
            {
                string capitalizeBrand = CapitalizeBrand(brand);
                string insertQuery = @"INSERT INTO Cars (Brand, Model, RentalPrice)
                                   VALUES(@brand, @model, @rentalPrice);";
                using (SqlConnection conn = new SqlConnection(carDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@brand", capitalizeBrand);
                        cmd.Parameters.AddWithValue("@model", model);
                        cmd.Parameters.AddWithValue("@rentalPrice", rentalPrice);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Car added successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void UpdateCar(int carId, string brand, string model, decimal rentalPrice)
        {
            try
            {
                string capitalizeBrand = CapitalizeBrand(brand);
                string updateQuery = @"UPDATE Cars SET Brand=@brand, Model=@model, RentalPrice=@rentalPrice WHERE CarId=@carId;";
                using (SqlConnection conn = new SqlConnection(carDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
                        cmd.Parameters.AddWithValue("@brand", capitalizeBrand);
                        cmd.Parameters.AddWithValue("@model", model);
                        cmd.Parameters.AddWithValue("@rentalPrice", rentalPrice);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Car updated successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void DeleteCar(int carId)
        {
            try
            {
                string deleteQuery = @"DELETE FROM Cars WHERE CarId=@carId;";
                using (SqlConnection conn = new SqlConnection(carDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Car deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public Car ReadCarById(int carId)
        {
            Car car = null;
            try
            {
                string getQuery = @"SELECT * FROM Cars WHERE CarId=@carId;";
                using (SqlConnection conn = new SqlConnection(carDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@carId", carId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string brand = reader.GetString(1);
                                string model = reader.GetString(2);
                                decimal rentalPrice = reader.GetDecimal(3);
                                car = new Car(id, brand, model, rentalPrice);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return car;
        }

        public List<Car> ReadAllCars()
        {
            var carList = new List<Car>();
            try
            {
                string getQuery = @"SELECT * FROM Cars;";
                using (SqlConnection conn = new SqlConnection(carDbConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Car List: ");
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string brand = reader.GetString(1);
                                string model = reader.GetString(2);
                                decimal rentalPrice = reader.GetDecimal(3);
                                carList.Add(new Car(id, brand, model, rentalPrice));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return carList;
        }
        public string CapitalizeBrand(string value)
        {
            string[] words = value.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words);
        }

    }
}
