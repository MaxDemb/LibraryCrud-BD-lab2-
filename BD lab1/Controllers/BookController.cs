using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class BookController : BaseController
    {
        public BookController(int actionNumber) : base(actionNumber) { }

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
                context.books.Add(book);
                context.SaveChanges();
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
                var entity = context.books.Find(deleteId);
                context.books.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public override void Read()
        {
            foreach(var i in context.books)
            {
                Console.WriteLine($"id: {i.Id}");
                Console.WriteLine($"name: {i.Name}");
                Console.WriteLine($"penalty sum: {i.CreationYear}");
                Console.WriteLine($"reader id: {i.GenreId}");
            }
            Console.ReadLine();
        }

        public override void Update()
        {
            base.Update();
            var entity = context.books.Find(updateId);
            context.books.Update(entity);
            context.SaveChanges();
        }
        public override void Find()
        {
            base.Find();

            var i = context.books.Find(findId);

            Console.WriteLine($"id: {i.Id}");
            Console.WriteLine($"name: {i.Name}");
            Console.WriteLine($"penalty sum: {i.CreationYear}");
            Console.WriteLine($"reader id: {i.GenreId}");
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
