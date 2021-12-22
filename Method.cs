using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Praktek
{
    class Method
    {
        private string path = @"Data Source=(local); Initial Catalog=Praktek; Integrated Security=true";
        Model m = new Model();
        Menu menu = new Menu();

        public void Result()
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
                    Console.WriteLine("Average Total Cost    : {0}\n", m.AverageTotalCost);

                    // Try to change and experiment with the width specifiers (10).
                    Console.WriteLine($"{"Quantity",10} {"Variable Cost",10} {"Fixed Cost",10} {"Total Cost",10}");
                    Console.WriteLine($"{m.Quantity,10} {m.VariableCost,10} {m.FixedCost,10} {m.TotalCost,10}");
                    Console.WriteLine($"{"Marginal Cost",10} {"Average Fixed Cost",10} {"Average Variable Cost",10} {"Average Total Cost",10}\n");
                    Console.WriteLine($"{m.MarginalCost,10} {m.AverageFixedCost,10} {m.AverageVariableCost,10} {m.AverageTotalCost,10}");
                }
            }
            catch (Exception e)
            {
                // MODIFY SO THAT IT LOGS ERRORS.
                Console.WriteLine("Failed to read data!");
                Console.WriteLine("===================================================================");
                Console.WriteLine(e);
                Console.WriteLine("===================================================================");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            menu.Option();
        }
    }
}
