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
    public class BookRepository : BaseRepository, IRepository
    {


        public readonly string sqlInsert = "Insert into book(name, creation_year, genre_id) VALUES(@name, @creation_year, @genre_id)";
        public readonly string sqlSelect = "Select Id, Name, creation_year, genre_id from book";
        public readonly string sqlDelete = "Delete from book where @id = id";
        public BookRepository(NpgsqlConnection sqlConnection) : base(sqlConnection) { }

        public void Insert(Book book)
        {
            sqlConnection.Open();
            
            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", book.Name);
            cmd.Parameters.AddWithValue("creation_year", book.CreationYear);
            cmd.Parameters.AddWithValue("genre_id", book.GenreId);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
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
            string sqlSelectWhere = sqlSelect + whereExpression;
            using var cmd = new NpgsqlCommand(sqlSelectWhere, sqlConnection);

            Console.Clear();

            try
            {

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetInt32(0));
                    Console.WriteLine("Name: {0}", rdr.GetString(1));
                    Console.WriteLine("Creation Year: {0}", rdr.GetDateTime(2));
                    Console.WriteLine("Genre Id: {0}", rdr.GetInt32(3));
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
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
            string sqlGenerate = "insert into book (name, creation_year, genre_id) (select " + base.sqlRandomString + "," + base.sqlRandomDate + ", genre.id from generate_series(1,1000), genre limit(" + recordsAmount + "))";

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
