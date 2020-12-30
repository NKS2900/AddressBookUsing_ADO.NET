using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookUsing_ADO.NET
{
    public class AddressBookRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=addressbook_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void CheckConnection()
        {
            try
            {
                using (this.connection)
                {
                    connection.Open();
                    Console.WriteLine("Database_Connected_Successfully....");
                    connection.Close();
                }
            }
            catch
            {
                Console.WriteLine("Database_NOT_Connected!!!");
            }
        }

        public bool AddContact(AddressBookModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("addressProcedure", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", model.Last_Name);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Zip", model.Zip);
                    command.Parameters.AddWithValue("@Phone_Number", model.Phone_Number);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@BookName", model.BookName);
                    command.Parameters.AddWithValue("@AddressbookType", model.AddressbookType);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void EditContactUsingFirstName(AddressBookModel model)
        {
            try
            {
                using (this.connection)
                {
                    string updateQuery = @"UPDATE address_book SET last_name = @Last_Name, city = @City, state = @State, email = @Email, bookname = @BookName, addressbooktype = @AddressbookType WHERE first_name = @First_Name;";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", model.Last_Name);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@BookName", model.BookName);
                    command.Parameters.AddWithValue("@AddressbookType", model.AddressbookType);
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Updated successfully...");
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteContactUsingName(AddressBookModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("deletcontactProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Deleted successfully...");
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RetrieveContactFromPerticularCityOrState()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (this.connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM address_book WHERE city = 'Latur' OR state = 'Maharashtra';
                        SELECT * FROM address_book WHERE city = 'Beed' OR state = 'Goa'; ", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.First_Name = reader.GetString(0);
                                model.Last_Name = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zip = reader.GetString(5);
                                model.Phone_Number = reader.GetString(6);
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                    model.State, model.Zip, model.Phone_Number, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    model.First_Name = reader.GetString(0);
                                    model.Last_Name = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zip = reader.GetString(5);
                                    model.Phone_Number = reader.GetString(6);
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddressBookSizeByCityANDState()
        {
            try
            {
                using (this.connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"select count(first_name) from address_book WHERE city = 'Latur' AND state = 'Maharashtra'; 
                        select count(first_name) from address_book WHERE city = 'Pune' AND state = 'Maharashtra';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int count = reader.GetInt32(0);
                                Console.WriteLine("Total Contacts From City Latur And State Maharashtra : ", +count);                                
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    int count = reader.GetInt32(0);
                                    Console.WriteLine("Total Contacts From City Pune And State Maharashtra : ",+count);
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SortPersonNameByCity()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (this.connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM address_book WHERE city = 'Pune' order by first_name; 
                        SELECT * FROM address_book WHERE city = 'Latur' order by first_name, last_name;", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Sorted Contact Using first name from Pune");
                            Console.WriteLine("===========================================");
                            while (reader.Read())
                            {
                                model.First_Name = reader.GetString(0);
                                model.Last_Name = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zip = reader.GetString(5);
                                model.Phone_Number = reader.GetString(6);
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                Console.WriteLine("Sorted Contact Using First_Name from Latur");
                                Console.WriteLine("==========================================="); ;
                                while (reader.Read())
                                {
                                    model.First_Name = reader.GetString(0);
                                    model.Last_Name = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zip = reader.GetString(5);
                                    model.Phone_Number = reader.GetString(6);
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
