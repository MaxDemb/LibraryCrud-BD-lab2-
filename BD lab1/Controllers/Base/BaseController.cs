using DAL;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UI.Controllers
{
    public abstract class BaseController
    {
        protected string fieldString;

        protected string fieldToUpdate;
        protected string newValue;
        protected List<string> fieldToFind = new List<string>();
        protected List<string> oldValue = new List<string>();


        protected int recordsAmount;

        protected string whereExpression = " where";

        private int actionNumber { get; set; }
        protected UnitOfWork uow { get; set; }
        protected string connectionString { get; set; }
        protected int deleteId { get; set; }

        public BaseController() { }

        public BaseController(int actionNumber, string connectionString)
        {
            this.actionNumber = actionNumber;
            this.connectionString = connectionString;
            this.uow = new UnitOfWork(connectionString);
        }




        public void doLogic()
        {
            switch(actionNumber)
            {
                case 1:
                    Create();
                    break;
                case 2:
                    Read();
                    break;
                case 3:
                    Update();
                    break;
                case 4:
                    Delete();
                    break;
                case 5:
                    Generate();
                    break;
                case 6:
                    Find();
                    break;
            }
        }

        public virtual void Create()
        {
            throw new NotImplementedException();
        }

        public virtual void Read()
        {
            throw new NotImplementedException();
        }

        public virtual void Update()
        {

            ChooseField(" to update");
            this.fieldToUpdate = fieldString;

            Console.WriteLine("Enter new value:");
            this.newValue = Console.ReadLine();

            ChooseField(" to find");
            this.fieldToFind[0] = fieldString;

            Console.WriteLine("Enter value in this field");
            this.oldValue[0] = Console.ReadLine();
        }

        public virtual void Delete()
        {

            bool success = false;
            int id = 0;
            do
            {
                Console.WriteLine("Enter number of record you want to delete (or 0 to step back):");
                success = Int32.TryParse(Console.ReadLine(), out id);
                if (success == false)
                {
                    Console.WriteLine("Id must be a number...");
                    Console.ReadLine();
                    continue;
                }
            } while (success == false || id < 0);
            this.deleteId = id;
        }

        public virtual void Generate()
        {
            bool success = true;
            int recordsAmount;
            do
            {

                Console.Clear();
                Console.WriteLine("How many records you want to generate?");
                success = Int32.TryParse(Console.ReadLine(), out recordsAmount);
            } while (success == false);
            this.recordsAmount = recordsAmount;
        }

        public virtual void Find()
        {
            int choose = 0;
            do
            {
                ChooseField(" to find");
                this.fieldToFind.Add(fieldString);

                Console.WriteLine("Enter value in this field");
                this.oldValue.Add(Console.ReadLine());

                Console.WriteLine("To add another field to find enter 1 and any other key to continue");
                Int32.TryParse(Console.ReadLine(), out choose);
            } while (choose == 1);

           
            
           
            for(var i = 0; i < oldValue.Count; i++)
            {

                int new_int;
                if (!Int32.TryParse(oldValue[i], out new_int))
                {
                    oldValue[i] = "'" + oldValue[i] + "'";
                }
                if(i >= 1)
                {

                    this.whereExpression += " and " + fieldToFind[i] + " = " + oldValue[i];
                }
                else
                {

                    this.whereExpression += " " + fieldToFind[i] + " = " + oldValue[i];
                }
            }
        }

        protected virtual void ChooseField(string appendString)
        {
            throw new NotImplementedException();
        }


    }
}
