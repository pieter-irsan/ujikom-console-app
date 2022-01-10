using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
<<<<<<< HEAD
using System.Linq;

namespace Praktek
=======

namespace Praktek // Initialize repository, then try the draw table functions while implementing git.
>>>>>>> d73549fa871ae891a65b426fc182037223ac6e3e
{
    class Method
    {
        private string path = @"Data Source=(local); Initial Catalog=Praktek; Integrated Security=true";
        Model m = new Model();
        Menu menu = new Menu();

        public void Result()
        {
<<<<<<< HEAD
            try
            {
                Console.Write("\nFixed Cost    : ");
                m.FixedCost = Convert.ToInt32(Console.ReadLine());
                Console.Write("Quantity      : ");
                m.Quantity = Convert.ToInt32(Console.ReadLine());
                Console.Write("Variable Cost : ");
                m.VariableCost = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\nCalculation Results");
                Console.WriteLine("=============================");
                CalculateCosts(m.Quantity, m.VariableCost, m.FixedCost);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n[" + e.Message + "]");
            }
        }

        public void CalculateCosts(int quantity, int variableCost, int fixedCost)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("GetPrevQuantityAndTotalCost", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            m.PrevQuantity = reader.GetInt32(0);
                            m.PrevTotalCost = reader.GetInt32(1);
                        }
                    } reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n[" + e.Message + "]");
                }
            }

            m.TotalCost           = fixedCost + variableCost;
            m.AverageFixedCost    = fixedCost/quantity;
            m.AverageVariableCost = variableCost/quantity;
            m.AverageTotalCost    = m.TotalCost/quantity;
            if (m.PrevQuantity == 0 && m.PrevTotalCost == 0)
                m.MarginalCost = 0;
            else
                m.MarginalCost    = (m.TotalCost - m.PrevTotalCost) / (quantity - m.PrevQuantity);
            
            Console.WriteLine("Marginal Cost         : {0}", m.MarginalCost);
            Console.WriteLine("Average Fixed Cost    : {0}", m.AverageFixedCost);
            Console.WriteLine("Average Variable Cost : {0}", m.AverageVariableCost);
            Console.WriteLine("Average Total Cost    : {0}", m.AverageTotalCost);

            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("InsertCosts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@VariableCost", variableCost);
                command.Parameters.AddWithValue("@FixedCost", fixedCost);
                command.Parameters.AddWithValue("@TotalCost", m.TotalCost);
                command.Parameters.AddWithValue("@MarginalCost", m.MarginalCost);
                command.Parameters.AddWithValue("@AverageFixedCost", m.AverageFixedCost);
                command.Parameters.AddWithValue("@AverageVariableCost", m.AverageVariableCost);
                command.Parameters.AddWithValue("@AverageTotalCost", m.AverageTotalCost);

                try 
                {
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i == 1) 
                    {
                        Console.WriteLine("[Successfully saved to database]");
                        return;
                    }
                }
                catch (Exception e) 
                {
                    Console.WriteLine("\n[" + e.Message + "]");
                }
            }
=======
            Console.Write("\nFixed Cost    : ");
            m.FixedCost = Convert.ToInt32(Console.ReadLine());
            Console.Write("Quantity      : ");
            m.Quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Variable Cost : ");
            m.VariableCost = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nCalculation Results");
            Console.WriteLine("=============================");
            CalculateCosts(m.Quantity, m.VariableCost, m.FixedCost);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            menu.Option();
        }

        // TRY TO SEPARATE CALCULATION AND DATABASE INSERTION.
        public void CalculateCosts(int quantity, int variableCost, int fixedCost)
        {
            m.TotalCost = fixedCost + variableCost;
            m.MarginalCost = m.TotalCost/quantity;
            Console.WriteLine("Marginal Cost         : {0}", m.MarginalCost);
            m.AverageFixedCost = fixedCost/quantity;
            Console.WriteLine("Average Fixed Cost    : {0}", m.AverageFixedCost);
            m.AverageVariableCost = variableCost/quantity;
            Console.WriteLine("Average Variable Cost : {0}", m.AverageVariableCost);
            m.AverageTotalCost = m.TotalCost/quantity;
            Console.WriteLine("Average Total Cost    : {0}", m.AverageTotalCost);

            SqlConnection connection = new SqlConnection(path);
            SqlCommand command = new SqlCommand("InsertCost", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@VariableCost", variableCost);
            command.Parameters.AddWithValue("@FixedCost", fixedCost);
            command.Parameters.AddWithValue("@TotalCost", m.TotalCost);
            command.Parameters.AddWithValue("@MarginalCost", m.MarginalCost);
            command.Parameters.AddWithValue("@AverageFixedCost", m.AverageFixedCost);
            command.Parameters.AddWithValue("@AverageVariableCost", m.AverageVariableCost);
            command.Parameters.AddWithValue("@AverageTotalCost", m.AverageTotalCost);
            
            try 
            {
                connection.Open();
                int i = command.ExecuteNonQuery();
                connection.Close();
                if (i == 1) {
                    Console.WriteLine("[Successfully saved to database]");
                    return;
                }
            }
            catch (Exception e) 
            {
                // MODIFY SO THAT IT LOGS ERRORS.
                Console.WriteLine("\nDatabase insert failed!");
                Console.WriteLine("===================================================================");
                Console.WriteLine(e);
                Console.WriteLine("===================================================================");
            }
>>>>>>> d73549fa871ae891a65b426fc182037223ac6e3e
        }

        public void GetCosts()
        {
            try
            {
                List<Model> costList = new List<Model>();
                SqlConnection connection = new SqlConnection(path);
                SqlCommand command = new SqlCommand("GetCosts", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                connection.Open();
                adapter.Fill(table);
                connection.Close();

                foreach(DataRow row in table.Rows)
                {
                    costList.Add (
                        new Model {
                            Quantity = Convert.ToInt32(row["Quantity"]),
                            VariableCost = Convert.ToInt32(row["VariableCost"]),
                            FixedCost = Convert.ToInt32(row["FixedCost"]),
                            TotalCost = Convert.ToInt32(row["TotalCost"]),
                            MarginalCost = Convert.ToInt32(row["MarginalCost"]),
                            AverageFixedCost = Convert.ToInt32(row["AverageFixedCost"]),
                            AverageVariableCost = Convert.ToInt32(row["AverageVariableCost"]),
                            AverageTotalCost = Convert.ToInt32(row["AverageTotalCost"])
                        }
                    );
                }
<<<<<<< HEAD

                // Get max TotalCost length and add 1. Initial "-" is for left alignment.
                string lengthAlign = "-" + (costList.Max(m => m.TotalCost).ToString().Length + 1).ToString();
                string header = String.Format(
                    "|{0,"+lengthAlign+"}|{1,"+lengthAlign+"}|{2,"+lengthAlign+"}|{3,"+lengthAlign+"}|| {4,"+lengthAlign+"}|{5,"+lengthAlign+"}|{6,"+lengthAlign+"}|{7,"+lengthAlign+"}|", 
                    "Q", "VC", "FC", "TC", "MC", "AVC", "AFC", "ATC"
                );
                Console.WriteLine(header);

                // Get max TotalCost length, times the amount of variables (8), plus the amount of other table characters (17).
                int i = costList.Max(m => m.TotalCost).ToString().Length * 8 + 17;
                // Print table header and body separator.
                Console.Write("|");
                while (i > 0)
                {
                    Console.Write("-");
                    i--;
                }
                Console.Write("|\n");

                foreach (Model m in costList)
                {
                    string body = String.Format(
                        "|{0,"+lengthAlign+"}|{1,"+lengthAlign+"}|{2,"+lengthAlign+"}|{3,"+lengthAlign+"}|| {4,"+lengthAlign+"}|{5,"+lengthAlign+"}|{6,"+lengthAlign+"}|{7,"+lengthAlign+"}|", 
                        m.Quantity, m.VariableCost, m.FixedCost, m.TotalCost, m.MarginalCost, m.AverageVariableCost, m.AverageFixedCost, m.AverageTotalCost
                    );
                    Console.WriteLine(body);
=======
                foreach (Model m in costList)
                {
                    Console.WriteLine("\n=== Costs ==================");
                    Console.WriteLine("Quantity      : {0}", m.Quantity);
                    Console.WriteLine("Variable Cost : {0}", m.VariableCost);
                    Console.WriteLine("Fixed Cost    : {0}", m.FixedCost);
                    Console.WriteLine("Total Cost    : {0}", m.TotalCost);
                    Console.WriteLine("=== Calculations ===========");
                    Console.WriteLine("Marginal Cost         : {0}", m.MarginalCost);
                    Console.WriteLine("Average Fixed Cost    : {0}", m.AverageFixedCost);
                    Console.WriteLine("Average Variable Cost : {0}", m.AverageVariableCost);
                    Console.WriteLine("Average Total Cost    : {0}", m.AverageTotalCost);
>>>>>>> d73549fa871ae891a65b426fc182037223ac6e3e
                }
            }
            catch (Exception e)
            {
<<<<<<< HEAD
                Console.WriteLine("\n[" + e.Message + "]");
            }
        }

        public void DeleteRow(int quantity)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("DeleteRow", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Quantity", quantity);
                try
                {
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    if (i == 1)
                        Console.WriteLine("[Row successfully deleted]");
                    else 
                        Console.WriteLine("[Failed to delete row]");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n[" + e.Message + "]");
                }
            }
        }

        public void ResetTable()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("ResetProductionCost", connection);
                try
                {
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i == -1)
                        Console.WriteLine("[Table successfully reset]");
                    else
                        Console.WriteLine("[Table reset failed]");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n[" + e.Message + "]");
                }
            }
        }

        public void FinishTask()
        {
=======
                // MODIFY SO THAT IT LOGS ERRORS.
                Console.WriteLine("Failed to read data!");
                Console.WriteLine("===================================================================");
                Console.WriteLine(e);
                Console.WriteLine("===================================================================");
            }
>>>>>>> d73549fa871ae891a65b426fc182037223ac6e3e
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            menu.Option();
        }
    }
}
