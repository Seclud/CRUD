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
        private int choosenid;
        private Product selectedProduct = new Product();
        //обсерваблколлекшн наследует INotificationChanged
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        private ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            GetProducts();
            DataContext = viewModel;
        }
        private void GetProducts() //Возвращает все продукты
        {
            products = viewModel.Select();
            ProductsGridView.ItemsSource = products;
        }
        private void ProductsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGridView.SelectedItem != null)
            {
                // Преобразуем выбранный объект в продукт
                Product selectedProduct = (Product)ProductsGridView.SelectedItem;

                // Меняем наши значения на основе выбранного объекта
                NameTextBox.Text = selectedProduct.Name;
                PriceNumeric.Text = selectedProduct.Cost.ToString();
                choosenid = selectedProduct.Id;
            }
        }
        //Методы для кнопок
        private void ReadProductsButton_Click(object sender, RoutedEventArgs e)
        {
            GetProducts();
        }
        private void UpdateSelectedProduct()
        {
            selectedProduct.Name = NameTextBox.Text;
            selectedProduct.Cost = double.Parse(PriceNumeric.Text);
            selectedProduct.Id = choosenid;
        }
        private void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedProduct();
            viewModel.Create(selectedProduct);
            GetProducts();
        }
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedProduct();
            viewModel.Update(selectedProduct);
            GetProducts();
        }
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedProduct();
            viewModel.Delete(selectedProduct);
            GetProducts();
        }
    }
}
