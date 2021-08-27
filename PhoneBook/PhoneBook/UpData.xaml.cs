using PhoneBook.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhoneBook
{
    /// <summary>
    /// Логика взаимодействия для UpData.xaml
    /// </summary>
    public partial class UpData : Window
    {
        public event Action Action;
        public UpData()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var logic = DI.DI.GetLogic();

            var note = new Note
            {
                FirstName = InputName.Text,
                SecondName = InputSecondName.Text,
                SureName = InputSureName.Text,
                TelephoneNumber = InputNumber.Text,
                Description = InputDescription.Text,
            };

            var result = logic.Update(note);

            if (result.Status == "Invalid operation")
            {
                MessageBox.Show($"{result.Status} - {result.Name}: {result.Description}");
            }

            Action();
        }
    }
}
