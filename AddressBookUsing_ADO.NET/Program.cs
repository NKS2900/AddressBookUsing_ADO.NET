using System;

namespace AddressBookUsing_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***AddressBook_Using_ADO.NET***");
            AddressBookRepo abrepo = new AddressBookRepo();
            //abrepo.CheckConnection();

            AddressBookModel abmodel = new AddressBookModel();
            abmodel.First_Name = "Asif";
            abmodel.Last_Name = "Shaikh";
            abmodel.Address = "mainRoad";
            abmodel.City = "Beed";
            abmodel.State = "Maharashtra";
            abmodel.Zip = "145263";
            abmodel.Phone_Number = "9876532147";
            abmodel.Email = "asif@gmail.com";
            abmodel.BookName = "address002";
            abmodel.AddressbookType = "friend";
            bool result = abrepo.AddContact(abmodel);

            if (result)
                Console.WriteLine("Record added successfully...");
            else
                Console.WriteLine("Some problem is there...");
            Console.ReadKey();

        }
    }
}
