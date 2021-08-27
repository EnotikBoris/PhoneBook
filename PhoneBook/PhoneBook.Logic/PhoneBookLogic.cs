using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Common.Contracts;
using PhoneBook.Model.Common;

namespace PhoneBook.Logic
{

    public class PhoneBookLogic : IPhoneBookOperations
    {
        private IPhoneBookOperations _pbo;

        public PhoneBookLogic(IPhoneBookOperations pbo)
        {
            _pbo = pbo;
        }
        public OperationResult Create(Note note)
        {
            if (!IsNumberValid(note.TelephoneNumber))
            {
                return GetInvalidResult(nameof(Create));
            }
            else
            {
                return _pbo.Create(note);
            }
        }

        

        public OperationResult Delete(string number)
        {
            if (!IsNumberValid(number))
            {
                return GetInvalidResult(nameof(Delete));
            }
            else
            {
                return _pbo.Delete(number);
            }
        }

        public Note[] ReadAll(string firstName, string secondName)
        {

            return _pbo.ReadAll(firstName, secondName);
        }

        public Note[] ReadAll()
        {
            return _pbo.ReadAll();
        }

        public OperationResult Update(Note note)
        {
            if (!IsNumberValid(note.TelephoneNumber))
            {
                return GetInvalidResult(nameof(Update));
            }
            else
            {
                return _pbo.Update(note);
            }
        }

        private OperationResult GetInvalidResult(string operationName)
        {
            return new OperationResult
            {
                Name = operationName,
                Status = "Invalid operation",
                Description = "Номер телефона не соответствует требуемому формату"
            };
        }

        private bool IsNumberValid(string number)
        {
            var items = number.Split(' ', '(', ')', '+').Where(s => !string.IsNullOrEmpty(s)).ToArray();

            return IsNumberValid(items) && IsNumberLengthCorrect(number);
        }

        private bool IsNumberValid(string[] items )
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (!int.TryParse(items[i], out int num))
                    return false;
            }

            return true;
        }

        private bool IsNumberLengthCorrect(string number)
        {
            number = number.Replace(" ", "");
            number = number.Replace("(", "");
            number = number.Replace(")", "");

            if (number.Length == 12 || number.Length == 11)
            {
                return true;
            }

            return false;
        }
    }

}
