using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class AuthorController : BaseController
    {
        public AuthorController(int actionNumber) : base(actionNumber) { }

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
                context.authors.Add(author);
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
                var entity = context.authors.Find(deleteId);
                context.authors.Remove(entity);
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
            foreach(var i in context.authors)
            {
                Console.WriteLine($"id: {i.Id}");
                Console.WriteLine($"name: {i.Name}");
                Console.WriteLine($"is woman: {i.Is_Woman}");
                Console.WriteLine($"birthdate: {i.Birthdate}");
            }
            Console.ReadLine();
        }

        public override void Update()
        {
            base.Update();
            var entity = context.authors.Find(updateId);
            context.authors.Update(entity);
            context.SaveChanges();
        }
        public override void Find()
        {
            base.Find();

            var i = context.authors.Find(findId);

            Console.WriteLine($"id: {i.Id}");
            Console.WriteLine($"name: {i.Name}");
            Console.WriteLine($"is woman: {i.Is_Woman}");
            Console.WriteLine($"birthdate: {i.Birthdate}");
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
