using System.Collections;
using System.Diagnostics;
using OtonomHazineAvcisi;

namespace OtonomHazineAvcisi
{
     class Program
    {
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            
            ApplicationConfiguration.Initialize();
                Application.Run(new otonomHazineAvcisi());

            
        }

    }
}