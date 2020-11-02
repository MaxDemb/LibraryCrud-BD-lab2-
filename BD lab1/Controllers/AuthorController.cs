using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class AuthorController : BaseController
    {
        public AuthorController(int actionNumber, string connectionString) : base(actionNumber, connectionString) { }

        public override void Create()
        {
            string name;
            bool is_woman = false;

            bool success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Author properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    success = false;
                    Console.WriteLine("Length of name > 50. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Is woman? True or false:");
                success = Boolean.TryParse(Console.ReadLine(), out is_woman);
                if (success == false)
                {
                    Console.WriteLine("is_woman must be a boolean (true or false)...");
                    Console.ReadLine();
                    continue;
                }

                success = true;
            } while (success == false);

            Author author = new Author
            {
                Name = name,
                Is_Woman = is_woman
            };



            try
            {
                uow.AuthorRepository.Insert(author);
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
                uow.AuthorRepository.Delete(base.deleteId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        public override void Read()
        {
            uow.AuthorRepository.Select("");
        }

        public override void Update()
        {
            base.Update();
            uow.AuthorRepository.Update("author", base.fieldToUpdate, base.newValue, base.fieldToFind[0], base.oldValue[0]);
        }
        public override void Generate()
        {
            base.Generate();
            uow.AuthorRepository.Generate(recordsAmount);
            Console.WriteLine("Success. Press any key to continue...");
            Console.ReadLine();

        }
        public override void Find()
        {
            base.Find();

            uow.AuthorRepository.Select(base.whereExpression);
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
            } while (success = false || fieldNum < 1 || fieldNum > 3);



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
