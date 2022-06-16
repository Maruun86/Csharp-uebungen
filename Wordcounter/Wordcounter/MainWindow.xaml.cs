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

namespace Wordcounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Analyse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string text = System.IO.File.ReadAllText(dialog.FileName);
                string[] parts = text.Split(new char[] { ' ', '.', ',', '\n', '\r', ':', '!', '"', ';', '?' });

                List<Word> words = new List<Word>();

                bool doesExist = false;
                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i] != "")
                    {
                        for (int j = 0; j < words.Count; j++)
                        {
                            if (words[j].Text == parts[i])
                            {
                                words[j].AddCount();
                                doesExist = true;
                                break;
                            }
                        }
                        if (!doesExist)
                        {
                            words.Add(new Word(parts[i]));
                        }

                    }
                }

                var sort = words.OrderBy(x => -x.Count).ToList();
                object viewList = myGrid.FindName("viewList");

                var gridView = new GridView();
                ListView.View = gridView;

                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Word",
                    DisplayMemberBinding = new Binding("Word")
                });
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Anzahl",
                    DisplayMemberBinding = new Binding("Anzahl")
                });

                foreach (var word in sort)
                {
                    this.ListView.Items.Add(new MyItem {  Word = word.Text , Anzahl = word.Count});

                }
                SumWords.Content = sort.Count();               
            }
                                      
        }

    }
}
