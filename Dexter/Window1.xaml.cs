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
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;
using DexterLab;
using System.Threading;

namespace Dexter
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
       
        datacollection DexterCollection;
        Alternate_Q_A alternative;
        bool textchk = false;
        int countnewquestion = 0;
        string ans = "", ques = "";
       
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Thread th = new System.Threading.Thread(new ThreadStart(DexterCollection.SaveAll));
            th.Start();
            //DexterCollection.SaveAll();
            MainWindow home = new MainWindow();
            home.Show();
            this.Hide();   
        
        }

        private void Window_KeyDown_2(object sender, KeyEventArgs e)
        {

             if (e.Key == Key.Enter)
             {
                 if (listBox2.Items.Count>20)
                 {
                     listBox2.Items.Clear();
                     listBox1.Items.Clear();
                 }
                 if (textBox1.Text != "")
                 {
                     listBox2.Items.Add("Typing....");
                     listBox1.Items.Add("");
                     listBox1.Items.Add("     " + textBox1.Text);
                     listBox1.ScrollIntoView("     " + textBox1.Text);

                     if (countnewquestion < 2)
                     {
                         ans = DexterCollection.Search_Answer(textBox1.Text);
                     }
                     if (countnewquestion == 2)
                     {

                         countnewquestion = 0;
                         ans = textBox1.Text;
                         DexterCollection.Add(ques, ans);
                         ans = alternative.Random_Alt_Answer();

                         listBox2.Items[listBox2.Items.Count - 1] = "";
                         listBox2.Items.Add(ans + "     ");
                         listBox2.ScrollIntoView(ans + "     ");
                         ans = "";
                     }
                     else
                     {

                         if (ans == textBox1.Text)
                         {
                            ans = DexterCollection.Search_In_Answer(ans);
                            if (ans==textBox1.Text)
                            {

                                ques = ans;
                                countnewquestion += 2;
                                string que = alternative.Random_Alt_Question();

                                listBox2.Items[listBox2.Items.Count - 1] = "";
                                listBox2.Items.Add(que + "? " + ans + "     ");
                                listBox2.ScrollIntoView(que + "? " + ans + "     ");
                                que = "";

                            }
                            else
                            {
                                listBox2.Items[listBox2.Items.Count - 1] = "";
                                listBox2.Items.Add(ans + "     ");
                                listBox2.ScrollIntoView(ans + "     ");
                            }
                             
                         }
                         else
                         {

                             listBox2.Items[listBox2.Items.Count - 1] = "";
                             listBox2.Items.Add(ans + "     ");
                             listBox2.ScrollIntoView(ans + "     ");

                         }
                     }//end of return back question

                     textBox1.Clear();
                 }//end of if user input is not empty
             }//end of enter key press
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
           
            DexterCollection = new datacollection();
            alternative = new Alternate_Q_A();
            DoubleAnimation da = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(3)));
            da.RepeatBehavior = RepeatBehavior.Forever;
            RotateTransform rt = new RotateTransform();
            image1.RenderTransform = rt;
            image1.RenderTransformOrigin=new Point(0.5,0.5);
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
            DoubleAnimation dim = new DoubleAnimation();
            dim.To = 0;
            dim.From = 0.5;
            dim.RepeatBehavior = RepeatBehavior.Forever;
            dim.Duration = new Duration(TimeSpan.FromSeconds(6));
            image1.BeginAnimation(Canvas.OpacityProperty, dim);
            DexterCollection.RetrieveDataFromFile();
            
            if (User.name.Length>6)
             {
                 textBlock1.Width = textBlock1.Width + textBlock1.Width;
             }
             else
             {
                 textBlock1.Width = 65;
             }
             textBlock1.Text = User.name;
             listBox2.Items.Add("Whats in your mind?    ");
        }

        private void textBox1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (textchk == false)
            {
                textBox1.Clear();
                textchk = true;
            }
        }

        private void textBox1_QueryCursor(object sender, QueryCursorEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            char k = (char)e.Key;
          
            int asci = Convert.ToInt16(k);
          //  textBlock1.Text = asci.ToString();
            if (asci >= 43 && asci <= 69 || asci == 140 || asci==6 || asci==145 || asci==144 || asci==34)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
