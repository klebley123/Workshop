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
using Workshop.Models;
using Workshop.ViewModel;

namespace Workshop.Views
{
    /// <summary>
    /// Interaction logic for PropertiesCars.xaml
    /// </summary>
    public partial class PropertiesCars : Window
    {
        public PropertiesCars(Car car)
        {
            InitializeComponent();
            DataContext = new PropertiesCarViewModel(car);
        }
    }
}
