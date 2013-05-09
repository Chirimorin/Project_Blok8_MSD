using MSD.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MSD
{
	/// <summary>
	/// Interaction logic for LogInView.xaml
	/// </summary>
	public partial class LogInView : Window
	{
		public LogInView()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        private void WachtwoordVergeten(object sender, RoutedEventArgs e)
        {
            //open de wachtwoordvergetenusercontrol in de loginview
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();

        }
	}
}