using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace StringSearcher
{
    public partial class StringSearcher : Form
    {
        public StringSearcher()
        {
            InitializeComponent();
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {   // folder selection, setting the selected path
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //initialising folder browsing dialog 
            if (fbd.ShowDialog(this) == DialogResult.OK) // if OK is selected, the selected path will be added to text box
            {
                searchFoldersTextBox.Text = fbd.SelectedPath;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchFoldersTextBox.Text))
            {
                
                MessageBox.Show("Please choose a folder.");

            }
            else if (string.IsNullOrWhiteSpace(searchStringTextBox.Text))
            {

                MessageBox.Show("Please enter a search string.");
            }
            else
            {
                resultListView.Items.Clear(); // clearing the list before every search
                SearchDir(searchFoldersTextBox.Text); //calling the function 1st 
            }
            
        }

        void SearchDir(string dir) // main search function
        {   
            // run through all files in current directory in loop
            foreach (string file in Directory.GetFiles(dir, "*.txt"))
            {
                // reading from the file again
                using (var streamReader = new StreamReader(file))
                {
                    var contents = streamReader.ReadToEnd().ToLower();       
                    int wordcount = WordCount(searchStringTextBox.Text.ToLower(), contents);
                    if (wordcount > 0)
                    {
                        ListViewItem item = new ListViewItem(file); // initialising listviewitem       
                        item.SubItems.Add(wordcount.ToString());
                        resultListView.Items.Add(item);
                    }
                }
            }
           
            // run through all directories in current directory
            foreach (string directory in Directory.GetDirectories(dir))
            {
                // recursively call SearchDir() function with current directory in loop
                SearchDir(directory);
            }
        }

        // WordCount() function taking two args, searched word and text from file
        private static int WordCount(string word, string text)
        {
            // initialising new regex 
            var regex = new Regex(string.Format(word), RegexOptions.IgnoreCase);
            return regex.Matches(text).Count;
        }

        // double click on viewlist row to open the selected file
        private void resultListView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = (ListViewItem)resultListView.SelectedItems[0];
            Process.Start(item.Text.ToString());
        }

        // view the selected file in textBox inside the application
        private void viewButton_Click(object sender, EventArgs e)
        {
            if (resultListView.SelectedItems.Count > 0)
            {
                String file = resultListView.SelectedItems[0].Text;
                TextReader reading = new TextReader(file);
                reading.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an item.");
            }
            
        }
    }
}
