using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class ReaderController : BaseController
    {
        public ReaderController(int actionNumber) : base(actionNumber) { }
        public override void Delete()
        {
            base.Delete();

            try
            {
                var entity = context.abonnements.Find(deleteId);
                context.abonnements.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public override void Create()
        {
            string name = null;
            string home_adress = null;
            bool problematic = false;

            bool success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Reader properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    success = false;
                    Console.WriteLine("Length of name > 50. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Home adress:");
                home_adress = Console.ReadLine();
                if (name.Length > 100)
                {
                    success = false;
                    Console.WriteLine("Length of name > 100. It is wrong.");
                    Console.ReadLine();
                    continue;
                }


                Console.WriteLine("Problematic:");
                success = Boolean.TryParse(Console.ReadLine(), out problematic);
                if(success == false)
                {
                    Console.WriteLine("Problematic must be boolean.");
                    Console.ReadLine();
                    continue;
                }
                success = true;
            } while (success == false);




            try
            {
                context.readers.Add(new Reader
                {
                    Name = name,
                    HomeAdress = home_adress,
                    Problematic = problematic
                });
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
            foreach(var i in context.readers)
            {
                Console.WriteLine($"id: {i.Id}");
                Console.WriteLine($"name: {i.Name}");
                Console.WriteLine($"penalty sum: {i.Problematic}");
            }
            Console.ReadLine();
        }
        public override void Update()
        {
            base.Update();
            var entity = context.readers.Find(updateId);
            context.readers.Update(entity);
            context.SaveChanges();
        }
        public override void Find()
        {
            base.Find();
            var i = context.readers.Find(findId);


            Console.WriteLine($"id: {i.Id}");
            Console.WriteLine($"name: {i.Name}");
            Console.WriteLine($"penalty sum: {i.Problematic}");
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
                Console.WriteLine("3. Home Adress");
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
                case 3:
                    base.fieldString = "home_adress";
                    break;
            }

        }

    }
}
