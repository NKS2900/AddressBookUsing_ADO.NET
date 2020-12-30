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
            Console.ReadKey();

        }
    }
}
