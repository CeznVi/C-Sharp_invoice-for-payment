using System;
using System.Drawing;
using System.IO;
using System.Text;


namespace Invoice_for_payment
{
    internal class Program
    {
        static void Main()
        { 
            Console.OutputEncoding = Encoding.Unicode;

            InvoiceApplication app = new();
            app.Menu();
        }
    }
}
