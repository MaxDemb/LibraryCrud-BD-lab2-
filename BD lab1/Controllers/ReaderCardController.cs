using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Controllers
{
    public class ReaderCardController : BaseController
    {
        public ReaderCardController(int actionNumber, string connectionString) : base(actionNumber, connectionString) { }
        public override void Delete()
        {
            base.Delete();

            try
            {
                uow.ReaderCardRepository.Delete(base.deleteId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        public override void Read()
        {
            uow.ReaderCardRepository.Select("");
        }
        public override void Create()
        {
            //string name;

            //bool success = false;
            //do
            //{
            //    Console.Clear();
            //    Console.WriteLine("Enter ReaderCard properties:");
            //    Console.WriteLine("Name:");
            //    name = Console.ReadLine();
            //    if (name.Length > 50)
            //    {
            //        success = false;
            //        Console.WriteLine("Length of name > 50. It is wrong.");
            //        Console.ReadLine();
            //        continue;
            //    }
            //    success = true;
            //} while (success == false);




            //try
            //{
            //    uow.GenreRepository.Insert(new Genre { Name = name });
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.ReadLine();
            //}
        }
    }
}
