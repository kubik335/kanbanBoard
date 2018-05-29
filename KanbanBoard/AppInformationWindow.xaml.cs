using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace KanbanBoard
{
    /// <summary>
    /// Interaction logic for ApplicationInformation.xaml
    /// </summary>
    public partial class AppInformationWindow : Window
    {
        public AppInformationWindow()
        {
            InitializeComponent();
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(System.IO.Path.GetFullPath(@"..\..\Resources\AboutApp.txt")))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    appInfoTextBlock.Text = line;
                }
            }
            catch (Exception ex)
            {
                // Throw new exception if file was not found
                throw new FileLoadException("Could not read the file", ex.Message);
            }
        }
    }
}
