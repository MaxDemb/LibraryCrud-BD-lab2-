using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ReaderRepository : BaseRepository, IRepository
    {
        public readonly string sqlInsert = "Insert into reader(name, home_adress, problematic) VALUES(@name,@home_adress, @problematic)";
        public readonly string sqlDelete = "Delete from reader where @id = id";
        public readonly string sqlSelect = "Select id, name, home_adress, problematic from reader";
        
        public ReaderRepository(NpgsqlConnection sqlConnection) : base(sqlConnection) { }

      
        public void Insert(Reader reader)
        {
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", reader.Name);
            cmd.Parameters.AddWithValue("home_adress", reader.HomeAdress);
            cmd.Parameters.AddWithValue("problematic", reader.Problematic);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
        public void Select(string whereExpression)
        {

            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlSelect + whereExpression, sqlConnection);

            Console.Clear();

            try
            {

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetValue(0));
                    Console.WriteLine("Name: {0}", rdr.GetValue(1));
                    Console.WriteLine("Home adress: {0}", rdr.GetValue(2));
                    Console.WriteLine("Problematic: {0}", rdr.GetValue(3));
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
            Console.ReadLine();
        }
        public void Delete(int id)
        {
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlDelete, sqlConnection);

            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        public void Generate(int recordsAmount)
        {
            string sqlGenerate = "insert into reader (name, problematic) (select " + base.sqlRandomString + "," + base.sqlRandomBoolean + " from generate_series(1,1000000) limit(" + recordsAmount + "))";

            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlGenerate, sqlConnection);


            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
