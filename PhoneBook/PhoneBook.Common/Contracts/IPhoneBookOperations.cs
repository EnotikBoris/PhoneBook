using PhoneBook.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Common.Contracts
{
    public interface IPhoneBookOperations
    {
        OperationResult Create(Note note);
        OperationResult Update(Note note);
        OperationResult Delete(string namder);
        Note[] ReadAll(string firstName, string secondName);
        Note[] ReadAll();
    }
}
