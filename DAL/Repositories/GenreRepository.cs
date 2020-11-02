using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DAL.Repositories
{
    public class GenreRepository : BaseRepository, IRepository
    {

        public readonly string sqlInsert = "Insert into genre(name) VALUES(@name)";
        public readonly string sqlDelete = "Delete from genre where @id = id";
        public readonly string sqlSelect = "Select Id, Name from genre";
       
        public GenreRepository(NpgsqlConnection sqlConnection) : base(sqlConnection) { }

        public void Insert(Genre genre)
        {
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", genre.Name);
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
                    Console.WriteLine("Id: {0}", rdr.GetInt32(0));
                    Console.WriteLine("Name: {0}", rdr.GetString(1));
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
            string sqlGenerate = "Insert into genre(name) select chr(trunc(65 + random()*50)::int)||chr(trunc(65 + random() * 25)::int)||chr(trunc(65 + random() * 25)::int)||chr(trunc(65 + random() * 25)::int) from generate_series(1," + recordsAmount + ")";
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
