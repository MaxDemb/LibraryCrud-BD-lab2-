using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UI.Controllers
{
    public class AbonnementController : BaseController
    {
        public AbonnementController(int actionNumber) : base(actionNumber)
        {
        }

        public override void Create()
        {
            string name;
            int deadline = 0;
            int penaltySum = 0;
            int readerId = 0;
            int bookId = 0;

            bool success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Abonnement properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    success = false;
                    Console.WriteLine("Length of name > 50. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Deadline year:");
                success = Int32.TryParse(Console.ReadLine(), out deadline);
                if (success == false)
                {
                    Console.WriteLine("Deadline year  must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Penalty Sum:");
                success = Int32.TryParse(Console.ReadLine(), out penaltySum);
                if (success == false)
                {
                    Console.WriteLine("Penalty Sum must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("ReaderId:");
                success = Int32.TryParse(Console.ReadLine(), out readerId);
                if (success == false)
                {
                    Console.WriteLine("ReaderId must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("BookId:");
                success = Int32.TryParse(Console.ReadLine(), out bookId);
                if (success == false)
                {
                    Console.WriteLine("BookId must be a number...");
                    Console.ReadLine();
                    continue;
                }


                success = true;
            } while (success == false);

            Abonnement abonnement = new Abonnement
            {
                Name = name,
                Deadline = new DateTime(deadline, 1, 1),
                PenaltySum = penaltySum,
                ReaderId = readerId,
                BookId = bookId
            };



            try
            {
                context.abonnements.Add(abonnement);
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
            foreach(var i in context.abonnements)
            {
                Console.WriteLine($"id: {i.Id}");
                Console.WriteLine($"name: {i.Name}");
                Console.WriteLine($"penalty sum: {i.PenaltySum}");
                Console.WriteLine($"reader id: {i.ReaderId}");
                Console.WriteLine($"book id: {i.BookId}");
                Console.WriteLine($"deadline: {i.Deadline}");
            }
            Console.ReadLine();
        }
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

        public override void Update()
        {
            base.Update();
            var entity = context.abonnements.Find(findId);

            ChooseField("You want to update");
            Console.WriteLine("Enter new value");
            if(fieldString == "id")
            {
                entity.Id = Int32.Parse(Console.ReadLine()); 
            }
            else if (fieldString == "name")
            {
                entity.Name = Console.ReadLine();
            }
            else if(fieldString == "Penalty Sum")
            {
                entity.PenaltySum = Int32.Parse(Console.ReadLine());
            }
            context.abonnements.Update(entity);
            context.SaveChanges();
        }

        public override void Find()
        {
            base.Find();

            var i = context.abonnements.Find(findId);

            Console.WriteLine($"id: {i.Id}");
            Console.WriteLine($"name: {i.Name}");
            Console.WriteLine($"penalty sum: {i.PenaltySum}");
            Console.WriteLine($"reader id: {i.ReaderId}");
            Console.WriteLine($"book id: {i.BookId}");
            Console.WriteLine($"deadline: {i.Deadline}");

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
                Console.WriteLine("3. Penalty Sum");
                success = Int32.TryParse(Console.ReadLine(), out fieldNum);
            } while (success = false || fieldNum < 1 || fieldNum > 3);


            switch(fieldNum)
            {
                case 1:
                    base.fieldString = "id";
                    break;
                case 2:
                    base.fieldString = "name";
                    break;
                case 3:
                    base.fieldString = "penalty_sum";
                    break;
            }
        }
    }
}
