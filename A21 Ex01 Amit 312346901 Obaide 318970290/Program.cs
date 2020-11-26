using System;
using System.Windows.Forms;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
     public static class Program
     {
          [STAThread]
          public static void Main()
          {
               Application.EnableVisualStyles();
               Application.SetCompatibleTextRenderingDefault(false);
               Application.Run(new FormLogIn());
          }
     }
}
