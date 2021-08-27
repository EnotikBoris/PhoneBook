using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Logic;
using PhoneBook.Dal;
using PhoneBook.Common.Contracts;

namespace PhoneBook.DI
{
    public static class DI
    {
        private static IPhoneBookOperations _logic;
        private static IPhoneBookOperations _dao;

        public static IPhoneBookOperations GetLogic()
        {
            if (_logic is null)
            {
                _logic = new PhoneBookLogic(_dao);
            }
             
            return _logic;
        } 

        public static IPhoneBookOperations GetDao(string filePath)
        {
            if (_dao is null)
            {
                _dao = new PhoneBookDao(filePath);
            }

            return _dao;
        }
    }
}
