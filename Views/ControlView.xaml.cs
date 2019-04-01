using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class ControlView : UserControl
    {

        public ControlView()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            VM.IsModify = false;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            VM.IsModify = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.FilterIndex = FilterDropdown.SelectedIndex;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            VM.filterBool = true;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            VM.filterBool = false;
        }

        private void MethodsDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAll();
            switch (MethodsDropdown.SelectedIndex)
            {
                case 0:
                    SaveButton.Visibility = Visibility.Visible;
                    LoadButton.Visibility = Visibility.Visible;
                break;
                case 1:
                    DeleteButton.Visibility = Visibility.Visible;
                    break;
                case 2:
                    AddEditGrid.Visibility = Visibility.Visible;
                    AddEditRadio1.Visibility = Visibility.Visible;
                    AddEditRadio2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    SortButton.Visibility = Visibility.Visible;
                    FilterDropdown.Visibility = Visibility.Visible;
                    break;
                case 4:
                    FilterButton.Visibility = Visibility.Visible;
                    FilterGrid.Visibility = Visibility.Visible;
                    FilterDropdown.Visibility = Visibility.Visible;
                    FilterField.Visibility = Visibility.Visible;
                    FilterTextBlock.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void HideAll()
        {
            SaveButton.Visibility = Visibility.Collapsed;
            LoadButton.Visibility = Visibility.Collapsed;
            AddEditRadio1.Visibility = Visibility.Collapsed;
            AddEditRadio2.Visibility = Visibility.Collapsed;
            AddEditGrid.Visibility = Visibility.Collapsed;
            FilterDropdown.Visibility = Visibility.Collapsed;
            FilterGrid.Visibility = Visibility.Collapsed;
            FilterButton.Visibility = Visibility.Collapsed;
            FilterField.Visibility = Visibility.Collapsed;
            FilterTextBlock.Visibility = Visibility.Collapsed;
            SortButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
        }
    }
}
