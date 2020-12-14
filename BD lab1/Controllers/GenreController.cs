using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace UI.Controllers
{
    public class GenreController : BaseController
    {
        public GenreController(int actionNumber) : base(actionNumber) { }

        public override void Delete()
        {
            base.Delete();

            try
            {
                var entity = context.genres.Find(deleteId);
                context.genres.Remove(entity);
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
            foreach(var i in context.genres)
            {
                Console.WriteLine($"id: {i.Id}");
                Console.WriteLine($"name: {i.Name}");
            }
            Console.ReadLine();
        }
        public override void Create()
        {
            string name;

            bool success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Genre properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    success = false;
                    Console.WriteLine("Length of name > 50. It is wrong.");
                    Console.ReadLine();
                    continue;
                }
                success = true;
            } while (success == false);




            try
            {
                context.genres.Add(new Genre { Name = name });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public override void Find()
        {
            base.Find();

            var i = context.genres.Find(findId);


            Console.WriteLine($"id: {i.Id}");
            Console.WriteLine($"name: {i.Name}");
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
