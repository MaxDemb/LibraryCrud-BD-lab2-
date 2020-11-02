using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ReaderCardRepository : BaseRepository, IRepository
    {

        public readonly string sqlInsert = "Insert into reader_card(registration_date, bonus_count, reader_id) VALUES(@registration_date, @bonus_count, @reader_id)";
        public readonly string sqlDelete = "Delete from reader_card where @id = id";
        public readonly string sqlSelect = "Select id, registration_date, bonus_count, reader_id from reader_card";
        public ReaderCardRepository(NpgsqlConnection sqlConnection) : base(sqlConnection) { }

        public void Insert(ReaderCard readerCard)
        {
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("registration_date", readerCard.RegistrationDate);
            cmd.Parameters.AddWithValue("bonus_count", readerCard.BonusCount);
            cmd.Parameters.AddWithValue("reader_id", readerCard.ReaderId);
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
                    Console.WriteLine("Registration Date: {0}", rdr.GetValue(1));
                    Console.WriteLine("Bonus Count: {0}", rdr.GetValue(2));
                    Console.WriteLine("Reader id: {0}", rdr.GetValue(3));
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
    }
}
