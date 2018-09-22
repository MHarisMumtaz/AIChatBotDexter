using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace Dexter
{
     class User
     {
          public static string name;

          public static void getinfo(ListBox lstbx)
          {
              StreamReader sr = new StreamReader(@"about.txt", true);
              while (sr.EndOfStream==false)
              {
                  lstbx.Items.Add(sr.ReadLine());
              }
              sr.Close();
          }//end of geting info method
     }
}
