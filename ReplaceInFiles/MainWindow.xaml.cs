using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace ReplaceInFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<FindReplacePair> replaceList
            = new ObservableCollection<FindReplacePair>();
        ObservableCollection<string> fileList = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = replaceList;
            listBox.ItemsSource = fileList;
            replaceList.Add(new FindReplacePair("1", "2"));
        }

        private void buttonAddRow_Click(object sender, RoutedEventArgs e)
        {
            replaceList.Add(new FindReplacePair());
        }

        private void buttonSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string fileName in openFileDialog.FileNames)
                    fileList.Add(fileName);
            }
        }

        private void buttonReplace_Click(object sender, RoutedEventArgs e)
        {
            foreach (string fileName in fileList)
            {
                string text = File.ReadAllText(fileName);
                foreach (FindReplacePair pair in replaceList)
                {
                    text = text.Replace(pair.Find, pair.Replace);
                }
                File.WriteAllText(fileName, text);
            }
            MessageBox.Show("Done!");
        }
    }

    public class FindReplacePair
    {
        public FindReplacePair()
        {
            Find = string.Empty;
            Replace = string.Empty;
        }
        public FindReplacePair(string find, string replace)
        {
            Find = find;
            Replace = replace;
        }
        public string Find { get; set; }
        public string Replace { get; set; }
    }
}
