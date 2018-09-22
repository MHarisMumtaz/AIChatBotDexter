using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Dexter
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
        bool textchk = false;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter the Name", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                User.name = textBox1.Text;
                Window1 start = new Window1();
                start.Show();
                this.Hide();
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation dim = new DoubleAnimation();
            dim.To = 0;
            dim.From = 0.7;
            dim.RepeatBehavior = RepeatBehavior.Forever;
            dim.Duration = new Duration(TimeSpan.FromSeconds(4));
            canvas1.BeginAnimation(Canvas.OpacityProperty, dim);
            User.getinfo(listBox1);
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string url = "https://www.facebook.com/haris.mumtaz.984";
            System.Diagnostics.Process.Start(url); 
        }

        private void textBox1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (textchk == false)
            {
                textBox1.Clear();
                textchk = true;
            }
        }

        private void textBox1_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
           
           
        }

    
    }
}
