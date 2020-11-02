using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DAL.Repositories
{
    public class AuthorRepository : BaseRepository, IRepository
    {

        public readonly string sqlInsert = "Insert into author(name, is_woman) VALUES(@name, @is_woman)";
        public readonly string sqlDelete = "Delete from author where @id = id";
        public readonly string sqlSelect = "Select name, is_woman from author";
        public AuthorRepository(NpgsqlConnection sqlConnection) : base(sqlConnection) { }

        public void Insert(Author author)
        {
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", author.Name);
            cmd.Parameters.AddWithValue("is_woman", author.Is_Woman);
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
                    try
                    {
                        Console.WriteLine("Name: {0}", rdr.GetValue(0));

                        bool woman = rdr.GetBoolean(1);
                        if (woman)
                        {
                            Console.WriteLine("Sex: female");
                        }
                        else
                        {
                            Console.WriteLine("Sex: male");
                        }

                        Console.WriteLine("Name: {0}", rdr.GetValue(2));
                    }
                    catch { }

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
            string sqlGenerate = "insert into author (name, is_woman)(select " + base.sqlRandomString + "," + base.sqlRandomBoolean + " from generate_series(1,1000000),reader,book limit(" + recordsAmount + "))";

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
