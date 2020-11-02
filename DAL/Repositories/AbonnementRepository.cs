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

    public class AbonnementRepository : BaseRepository, IRepository
    {
        public readonly string sqlInsert = "Insert into abonnement(name, deadline, penalty_sum, reader_id, book_id) VALUES(@name, @deadline, @penalty_sum, @reader_id, @book_id)";
        public readonly string sqlSelect = "Select * from abonnement";
        public readonly string sqlDelete = "Delete from abonnement where @id = id";
        public AbonnementRepository(NpgsqlConnection sqlConnection) : base(sqlConnection) { }

        public void Insert(Abonnement abonnement)
        {
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", abonnement.Name);
            cmd.Parameters.AddWithValue("deadline", abonnement.Deadline);
            cmd.Parameters.AddWithValue("penalty_sum", abonnement.PenaltySum);
            cmd.Parameters.AddWithValue("reader_id", abonnement.ReaderId);
            cmd.Parameters.AddWithValue("book_id", abonnement.BookId);
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
            
            Console.Clear();

            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlSelect + whereExpression, sqlConnection);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("Id: {0}", rdr.GetValue(0));
                Console.WriteLine("Name: {0}", rdr.GetValue(1));
                Console.WriteLine("Deadline: {0}", rdr.GetValue(2));
                Console.WriteLine("Penalty Sum: {0}", rdr.GetValue(3));
                Console.WriteLine("Reader Id: {0}", rdr.GetValue(4));
                Console.WriteLine("Book Id: {0}", rdr.GetValue(5));
            }
            sqlConnection.Close();

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
            string sqlGenerate = "insert into abonnement (name, penalty_sum, deadline, reader_id, book_id)(select " + base.sqlRandomString + "," + base.sqlRandomInteger + "," + base.sqlRandomDate + ", reader.id,book.id from generate_series(1,1000000),reader,book limit(" + recordsAmount + "))";

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
