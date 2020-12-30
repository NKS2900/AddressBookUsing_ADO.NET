using System;

namespace AddressBookUsing_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***AddressBook_Using_ADO.NET***");
            AddressBookRepo abrepo = new AddressBookRepo();
            abrepo.CheckConnection();

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

            AddressBookModel abmodel1 = new AddressBookModel();
            abmodel1.First_Name = "Asif";
            abmodel1.Last_Name = "Shaikh";
            abmodel1.Address = "mainRoad";
            abmodel1.City = "Beed";
            abmodel1.State = "Maharashtra";
            abmodel1.Zip = "145263";
            abmodel1.Phone_Number = "9876532147";
            abmodel1.Email = "asif@gmail.com";
            abmodel1.BookName = "address002";
            abmodel1.AddressbookType = "family";
            bool result1 = abrepo.AddContact(abmodel1);
            if (result1)
                Console.WriteLine("Record added successfully...");
            else
                Console.WriteLine("Some problem is there...");

            AddressBookModel editModel = new AddressBookModel();
            editModel.First_Name = "Dipak";
            editModel.Last_Name = "Nagar";
            editModel.City = "Latur";
            editModel.State = "Maharashtra";
            editModel.Email = "dpk@gmail.com";
            editModel.BookName = "address002";
            editModel.AddressbookType = "office";
            abrepo.EditContactUsingFirstName(editModel);

            AddressBookModel delmodel = new AddressBookModel();
            delmodel.First_Name = "Asif";
            abrepo.DeleteContactUsingName(delmodel);

            abrepo.RetrieveContactFromPerticularCityOrState();
            abrepo.AddressBookSizeByCityANDState();
            abrepo.SortPersonNameByCity();
            abrepo.GetNumberOfContactsCountByBookType();
            Console.ReadKey();

        }
    }
}
