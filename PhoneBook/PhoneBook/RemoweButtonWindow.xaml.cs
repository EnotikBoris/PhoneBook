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
    /// Логика взаимодействия для RemoweButtonWindow.xaml
    /// </summary>
    public partial class RemoweButtonWindow : Window
    {
        public event Action Action;

        public RemoweButtonWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string number = Number.Text;

            var logic = DI.DI.GetLogic();
            var result = logic.Delete(number);

            if (result.Status == "Invalid operation")
            {
                MessageBox.Show($"{result.Status} - {result.Name}: {result.Description}");
            }

            Action();
        }

    }
}
