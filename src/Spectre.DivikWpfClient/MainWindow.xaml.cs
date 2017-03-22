using Spectre.Algorithms.Parameterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Spectre.DivikWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            MaxKNumberTextBox.Text = 10.ToString();
            LevelNumberTextBox.Text = 3.ToString();
            UsingLevelsCheckbox.IsChecked = true;
            UsingAmplitudeFiltrationCheckbox.IsChecked = true;
            UsingVarianceFiltrationCheckbox.IsChecked = true;
            PercentSizeLimitTextBox.Text = 0.001.ToString();
            FeaturePreservationLimitTextBox.Text = 0.05.ToString();
            MetricComboBox.ItemsSource = Enum.GetValues(typeof(Metric)).Cast<Metric>();
            MetricComboBox.SelectedValue = Metric.Pearson;
            PlottingPartitionsCheckbox.IsChecked = false;
            PlottingRecursivelyCheckbox.IsChecked = false;
            PlottingDecompositionCheckbox.IsChecked = false;
            PlottingDecompositionRecursivelyCheckbox.IsChecked = false;
            MaxComponentsForDecompositionNumberTextBox.Text = 3.ToString();
            OutputPathTextBox.Text = ".";
            CachePathTextBox.Text = ".";
            CachingCheckbox.IsChecked = false;
            VerboseCheckbox.IsChecked = false;
            KmeansMaxItersNumberTextBox.Text = 100.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DivikProgress.IsIndeterminate = !DivikProgress.IsIndeterminate;
            DivikProgressLabel.Text = DivikProgressLabel.Text == "" ? "Divik running..." : "";
        }
    }
}
