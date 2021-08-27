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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public event Action Action;
        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
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

            var result = logic.Create(note);
            
            
            if (result.Status == "Invalid operation")
            {
                MessageBox.Show($"{result.Status} - {result.Name}: {result.Description}");
            }

            Action();
        }

    }
}
