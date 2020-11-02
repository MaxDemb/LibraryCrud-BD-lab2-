using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class ReaderController : BaseController
    {
        public ReaderController(int actionNumber, string connectionString) : base(actionNumber, connectionString) { }
        public override void Delete()
        {
            base.Delete();

            try
            {
                uow.ReaderRepository.Delete(base.deleteId);
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
                uow.ReaderRepository.Insert(new Reader { 
                    Name = name,
                    HomeAdress = home_adress,
                    Problematic = problematic
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        public override void Read()
        {
            uow.ReaderRepository.Select("");
        }
        public override void Update()
        {
            base.Update();
            uow.ReaderRepository.Update("reader", base.fieldToUpdate, newValue, fieldToFind[0], oldValue[0]);
        }
        public override void Find()
        {
            base.Find();

            uow.ReaderRepository.Select(base.whereExpression);
        }


        public override void Generate()
        {
            base.Generate();
            uow.ReaderRepository.Generate(recordsAmount);
            Console.WriteLine("Success. Press any key to continue...");
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
