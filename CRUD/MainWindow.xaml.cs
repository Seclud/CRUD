using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CRUD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _choosenid; //Хранит ID выбранного в таблице объекта, нужно для корректного обновления и удаления
        private Product _selectedProduct = new Product();
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ViewModel _viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            GetProducts();
            DataContext = _viewModel;
        }
        private async void GetProducts() //Чтение таблицы из базы данных и запись ее в окно нашей программы
        {
            try
            {
                _products = await _viewModel.Read();
                ProductsGridView.ItemsSource = _products;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void ProductsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e) //Метод для обработки выбранного объекта из таблицы приложения
        {   
            if (ProductsGridView.SelectedItem != null)
            {
                // Преобразуем выбранный объект в продукт
                var selectedProduct = (Product)ProductsGridView.SelectedItem;

                // Меняем наши значения на основе выбранного объекта
                NameTextBox.Text = selectedProduct.Name;
                PriceNumeric.Text = selectedProduct.Cost.ToString();
                _choosenid = selectedProduct.Id;
            }
        }
        //Методы для кнопок
        private void ReadProductsButton_Click(object sender, RoutedEventArgs e) //Кнопка для чтения базы данных
        {
            GetProducts();
        }
        private void UpdateSelectedProduct() //Обновление свойств выбранного продукта (над которым будут производится дальнейшие действия) на основе введеных данных
        {
            _selectedProduct.Name = NameTextBox.Text;
            _selectedProduct.Cost = double.Parse(PriceNumeric.Text);
            _selectedProduct.Id = _choosenid;
        }
        private async void CreateProductButton_Click(object sender, RoutedEventArgs e) //Кнопка для добавления продукта
        {
            try
            {
                UpdateSelectedProduct();
                await _viewModel.Create(_selectedProduct);
                GetProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void UpdateProductButton_Click(object sender, RoutedEventArgs e) //Кнопка для изменения продукта
        {
            try
            {
                UpdateSelectedProduct();
                await _viewModel.Update(_selectedProduct);
                GetProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e) //Кнопка для удаления продукта
        {
            try
            {
                UpdateSelectedProduct();
                await _viewModel.Delete(_selectedProduct);
                GetProducts();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
