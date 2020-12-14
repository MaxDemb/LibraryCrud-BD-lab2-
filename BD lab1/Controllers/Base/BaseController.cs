using DAL;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UI.Controllers
{
    public abstract class BaseController
    {

        protected ApplicationContext context = new ApplicationContext();
        protected int findId;
        protected int updateId;
        protected int deleteId { get; set; }
        protected int recordsAmount;

        protected string fieldString;
        private int actionNumber { get; set; }

        public BaseController(int actionNumber)
        {
            this.actionNumber = actionNumber;
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
            Console.WriteLine("Enter id of entity you want to update:");
            updateId = Int32.Parse(Console.ReadLine());
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

        public virtual void Find()
        {
            Console.WriteLine("Enter id of record you want to find");
            findId = Int32.Parse(Console.ReadLine());
        }

        protected virtual void ChooseField(string appendString)
        {
            throw new NotImplementedException();
        }


    }
}
