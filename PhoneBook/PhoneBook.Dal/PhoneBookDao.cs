using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PhoneBook.Common.Contracts;
using PhoneBook.Model.Common;
using System.IO;

namespace PhoneBook.Dal
{
    public class PhoneBookDao : IPhoneBookOperations
    {
        private string _filePath;

        public PhoneBookDao(string filePath)
        {
            _filePath = filePath;
        }
        public OperationResult Create(Note note)
        {
            if (!ReadAll().Any(n => n.TelephoneNumber == note.TelephoneNumber))
            {
                var notes = ReadAll().ToList();
                notes.Add(note);

                string json = JsonSerializer.Serialize(notes);

                using (var fs = new StreamWriter(_filePath))
                {
                    fs.Write(json);
                }

                return new OperationResult
                {
                    Name = "Запись была добавлена телефоный справочник",
                    Status = "Ok"
                };
            }

            return new OperationResult
            {
                Name = nameof(Create),
                Status = "Invalid operation",
                Description = "Запись с таким номером уже существует",
            };
            
        }

        public OperationResult Delete(string number)
        {
            if (ReadAll().Any(n => n.TelephoneNumber == number))
            {
                var notes = ReadAll().ToList();
                notes.RemoveAll(n => n.TelephoneNumber == number);

                var json = JsonSerializer.Serialize(notes);

                using (var fs = new StreamWriter(_filePath))
                {
                    fs.Write(json);
                }

                return new OperationResult
                {
                    Name = "Удалена запись из справочника",
                    Status = "Ok",
                };
            }
            return GetInvalidResult(nameof(Delete));

        }

        public Note[] ReadAll(string firstName, string secondName)
        {
            var notes = ReadAll();

            notes = notes
                .Where(n => n.FirstName == firstName && n.SecondName == secondName).ToArray();

            return notes;
        }

        public Note[] ReadAll()
        {
            string text = null;

            using (var fs = new StreamReader(_filePath))
            {
                text = fs.ReadToEnd();
            }

            Note[] notes = JsonSerializer.Deserialize<Note[]>(text);

            return notes;
        }

        public OperationResult Update(Note note)
        {
            if (ReadAll().Any(n => n.TelephoneNumber == note.TelephoneNumber))
            {
                Delete(note.TelephoneNumber);
                Create(note);

                return new OperationResult
                {
                    Name = "Запись в справочнике обновлена",
                    Status = "Ok",
                };
            }

            return GetInvalidResult(nameof(Update));
        }

        private OperationResult GetInvalidResult(string operationName)
        {
            return new OperationResult
            {
                Name = operationName,
                Status = "Invalid operation",
                Description = "Не существует такой записи"
            };
        }
    }
}
