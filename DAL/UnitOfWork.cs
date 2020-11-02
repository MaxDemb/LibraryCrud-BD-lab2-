using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DAL
{
    public class UnitOfWork
    {
        private AbonnementRepository abonnement; 
        private AuthorRepository author;
        private BookRepository book;
        private GenreRepository genre;
        private ReaderCardRepository readerCard;
        private ReaderRepository reader;

        private readonly NpgsqlConnection sqlConnection;
        public string connectionString { get; set; }



        public AbonnementRepository AbonnementRepository { get
            {
                if(abonnement == null)
                {
                    abonnement = new AbonnementRepository(sqlConnection);
                }
                return abonnement;
            }
        }
        public AuthorRepository AuthorRepository
        {
            get
            {
                if (author == null)
                {
                    author = new AuthorRepository(sqlConnection);
                }
                return author;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (book == null)
                {
                    book = new BookRepository(sqlConnection);
                }
                return book;
            }
        }
        public GenreRepository GenreRepository
        {
            get
            {
                if (genre == null)
                {
                    genre = new GenreRepository(sqlConnection);
                }
                return genre;
            }
        }
        public ReaderCardRepository ReaderCardRepository
        {
            get
            {
                if (readerCard == null)
                {
                    readerCard = new ReaderCardRepository(sqlConnection);
                }
                return readerCard;
            }
        }
        public ReaderRepository ReaderRepository
        {
            get
            {
                if (reader == null)
                {
                    reader = new ReaderRepository(sqlConnection);
                }
                return reader;
            }
        }


        public UnitOfWork(string connectionString)
        {
            this.connectionString = connectionString;

            sqlConnection = new NpgsqlConnection(this.connectionString);


        }

        //public void Mock()
        //{
        //    sqlConnection.Open();
        //    var sql = "SELECT version()";

        //    using var cmd = new NpgsqlCommand(sql, sqlConnection);

        //    var version = cmd.ExecuteScalar().ToString();


        //    Console.WriteLine($"PostgreSQL version: {version}");
        //    sqlConnection.Close();
        //}


        
    }
}
