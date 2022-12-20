using System;
using System.Drawing;
using System.IO;
using System.Text;

// Заблокувати попередження о використанні бінарної сериалізації
#pragma warning disable SYSLIB0011

namespace Invoice_for_payment
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Console.OutputEncoding = Encoding.Unicode;

            InvoiceApplication app = new();
            app.Menu();

            //app.Load();
            //app.Add();
            //app.Print();
            //app.Save();


        }
    }
}
