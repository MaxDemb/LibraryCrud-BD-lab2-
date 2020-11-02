using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class BookController : BaseController
    {
        public BookController(int actionNumber, string connectionString) : base(actionNumber, connectionString) { }

        public override void Create()
        {
            string name;
            int creationYear = 0;
            int genreId = 0;

            bool success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Book properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    success = false;
                    Console.WriteLine("Length of name > 50. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Creation Year:");
                success = Int32.TryParse(Console.ReadLine(), out creationYear);
                if (success == false)
                {
                    Console.WriteLine("Creation year must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("GenreId:");
                success = Int32.TryParse(Console.ReadLine(), out genreId);
                if (success == false)
                {
                    Console.WriteLine("Genre Id must be a number...");
                    Console.ReadLine();
                    continue;
                }

                success = true;
            } while (success == false);

            Book book = new Book
            {
                Name = name,
                GenreId = genreId,
                CreationYear = new DateTime(creationYear, 1, 1)
            };



            try
            {
                uow.BookRepository.Insert(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public override void Delete()
        {
            base.Delete();

            try
            {
                uow.BookRepository.Delete(base.deleteId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public override void Read()
        {
            uow.BookRepository.Select("");
        }

        public override void Update()
        {
            base.Update();
            uow.BookRepository.Update("Book", base.fieldToUpdate, base.newValue, base.fieldToFind[0], base.oldValue[0]);
        }
        public override void Find()
        {
            base.Find();

            uow.BookRepository.Select(base.whereExpression);
        }
        public override void Generate()
        {
            base.Generate();
            uow.BookRepository.Generate(base.recordsAmount);
            Console.WriteLine("Success. Press any key to continue");
            Console.ReadLine();
        }
        protected override void ChooseField(string appendString)
        {
            bool success = false;
            int fieldNum = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose field" + appendString + ":");
                Console.WriteLine("1. Id");
                Console.WriteLine("2. Name");
                success = Int32.TryParse(Console.ReadLine(), out fieldNum);
            } while (success = false || fieldNum < 1 || fieldNum > 2);


            switch (fieldNum)
            {
                case 1:
                    base.fieldString = "id";
                    break;
                case 2:
                    base.fieldString = "name";
                    break;
            }


        }
    }
}
