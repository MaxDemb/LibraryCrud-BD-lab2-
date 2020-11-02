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
        public AbonnementController(int actionNumber, string connectionString) : base(actionNumber, connectionString) { }



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
                uow.AbonnementRepository.Insert(abonnement);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        public override void Read()
        {
            uow.AbonnementRepository.Select("");
        }
        public override void Delete()
        {
            base.Delete();

            try
            {
                uow.AbonnementRepository.Delete(base.deleteId);
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
            uow.AbonnementRepository.Update("abonnement",base.fieldToUpdate, base.newValue, base.fieldToFind[0], base.oldValue[0]);
        }

        public override void Find()
        {
            base.Find();

            uow.AbonnementRepository.Select(base.whereExpression);
        }


        public override void Generate()
        {
            base.Generate();
            uow.AbonnementRepository.Generate(recordsAmount);
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
                Console.WriteLine("3. Penalty Sum");
                success = Int32.TryParse(Console.ReadLine(), out fieldNum);
            } while (success = false || fieldNum < 1 || fieldNum > 2);


            switch(fieldNum)
            {
                case 1:
                    base.fieldString = "id";
                    break;
                case 2:
                    base.fieldString = "name";
                    break;
                case 3:
                    base.fieldString = "Penalty Sum";
                    break;
            }
        }
    }
}
