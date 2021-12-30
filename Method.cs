using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Praktek
{
    class Method
    {
        private string path = @"Data Source=(local); Initial Catalog=Praktek; Integrated Security=true";
        Model m = new Model();
        Menu menu = new Menu();

        public void Result()
        {
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
                // MODIFY SO THAT IT LOGS ERRORS.
                Console.WriteLine("\n" + e.Message);
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            menu.Option();
        }

        // TRY TO SEPARATE CALCULATION AND DATABASE INSERTION.
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
                    // MODIFY SO THAT IT LOGS ERRORS.
                    Console.WriteLine("\n" + e.Message);
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
                    // MODIFY SO THAT IT LOGS ERRORS.
                    Console.WriteLine("\n[" + e.Message + "]");
                }
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

                string length = "-" + (costList.Max(m => m.TotalCost).ToString().Length + 1).ToString();
                string header = String.Format(
                    "|{0,"+length+"}|{1,"+length+"}|{2,"+length+"}|{3,"+length+"}|| {4,"+length+"}|{5,"+length+"}|{6,"+length+"}|{7,"+length+"}|", 
                    "Q", "VC", "FC", "TC", "MC", "AVC", "AFC", "ATC"
                );
                Console.WriteLine(header);


                // Print header and body separator.
                // Not working! Maybe try another loop.


                int i = 0;
                Console.Write("\n|");
                while (i < (Convert.ToInt32(length) - 1))
                {
                    Console.Write("-");
                    i++;
                }
                Console.Write("|");

                // Console.WriteLine($"|{"Q",-15}|{"VC",-15}|{"FC",-15}|{"TC",-15}|{"MC",-15}|{"AVC",-15}|{"AFC",-15}|{"ATC",-15}|");
                // Console.WriteLine("|---------------+---------------+---------------+---------------+---------------+---------------+---------------+---------------|");
                foreach (Model m in costList)
                {
                    string body = String.Format(
                        "|{0,"+length+"}|{1,"+length+"}|{2,"+length+"}|{3,"+length+"}|| {4,"+length+"}|{5,"+length+"}|{6,"+length+"}|{7,"+length+"}|", 
                        m.Quantity, m.VariableCost, m.FixedCost, m.TotalCost, m.MarginalCost, m.AverageVariableCost, m.AverageFixedCost, m.AverageTotalCost
                    );
                    Console.WriteLine(body);
                    // Console.WriteLine (
                    //     $"|{m.Quantity,-15}|{m.VariableCost,-15}|{m.FixedCost,-15}|{m.TotalCost,-15}" +
                    //     $"|{m.MarginalCost,-15}|{m.AverageVariableCost,-15}|{m.AverageFixedCost,-15}|{m.AverageTotalCost,-15}|"
                    // );
                }
            }
            catch (Exception e)
            {
                // MODIFY SO THAT IT LOGS ERRORS.
                Console.WriteLine("\n[" + e.Message + "]");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            menu.Option();
        }

        public void ResetTable()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand("TRUNCATE TABLE ProductionCost", connection);
                try
                {
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i == 1)
                        Console.WriteLine("[Table successfully reset]");
                    else
                        Console.WriteLine("[Table reset failed]");
                }
                catch (Exception e)
                {
                    // MODIFY SO THAT IT LOGS ERRORS.
                    Console.WriteLine("\n[" + e.Message + "]");
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                menu.Option();
            }
        }
    }
}
