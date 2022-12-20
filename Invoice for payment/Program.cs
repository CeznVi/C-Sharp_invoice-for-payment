using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

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
            app.Add();





            //Invoice inv = new Invoice();
            //inv.SetInvoice();
            //inv.Print();

            //Console.ReadKey();
            //Console.Clear();
            //inv.Edit();
            //inv.Print();

            //BinaryFormatter bf = new BinaryFormatter();
            //using (Stream stream = File.Create("point.bin"))
            //{
            //    bf.Serialize(stream, inv);
            //}

            //Console.ReadKey();
            //Console.Clear();



            //inv = null;
            //BinaryFormatter bf = new BinaryFormatter();
            //using (Stream stream = File.OpenRead("point.bin"))
            //{
            //    inv = (Invoice)bf.Deserialize(stream);
            //}
            //inv.Print();

        }
    }
}
