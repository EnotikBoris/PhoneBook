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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PhoneBook.DI;
using PhoneBook.Model.Common;

namespace PhoneBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string filePath = @"C:\PhoneBook\phoneBook.json";
            DI.DI.GetDao(filePath);
            var logic = DI.DI.GetLogic();

            PhoneBook.ItemsSource = logic.ReadAll();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();

            addWindow.Action += () 
                => PhoneBook.ItemsSource = DI.DI.GetLogic().ReadAll();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpData upData = new UpData();
            upData.Show();

            upData.Action += ()
                  => PhoneBook.ItemsSource = DI.DI.GetLogic().ReadAll();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemoweButtonWindow remoweButtonWindow = new RemoweButtonWindow();
            remoweButtonWindow.Show();

            remoweButtonWindow.Action += ()
                  => PhoneBook.ItemsSource = DI.DI.GetLogic().ReadAll();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (int.TryParse(Search.Text.Remove(0, 1).Replace(" ", ""), out int i))
                {
                    SearchByNumber(Search.Text);
                }
                else
                {
                    SearchByName(Search.Text);
                }
            }
            catch
            {
                PhoneBook.ItemsSource = DI.DI.GetLogic().ReadAll();
            }
        }

        private void SearchByName(string pattern)
        {
            PhoneBook.ItemsSource = DI.DI.GetLogic()
                .ReadAll()
                .Where(n => (n.FirstName + n.SecondName + n.SureName).Contains(pattern));
        }

        private void SearchByNumber(string pattern)
        {
            PhoneBook.ItemsSource = DI.DI.GetLogic()
                .ReadAll()
                .Where(n => n.TelephoneNumber.Contains(pattern));
        }
    }
}
