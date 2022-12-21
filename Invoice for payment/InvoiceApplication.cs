using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

// Заблокувати попередження о використанні бінарної сериалізації
#pragma warning disable SYSLIB0011

namespace Invoice_for_payment
{
    public class InvoiceApplication
    {
        private List<Invoice> invPayed = new();
        private List<Invoice> invNotPayed = new();

        //Налаштування файлової системи
        private readonly string dirPath = "../../../../SaveFile";
        private readonly string fileNamePayed = "/Payed.bin";
        private readonly string fileNameNotPayed = "/NotPayed.bin";

        //Редагування штрафів
        public void Mod()
        {
            Console.Clear();
            Console.WriteLine("Введіть номер штрафу");
            string searchInput = Console.ReadLine();

            Invoice invoice;
            invoice = (invNotPayed.Find(p => p.numInvoice == searchInput));

            if (invoice == null)
                Console.WriteLine("\tЗа вказаним номером штрафу - нічого не знайдено");
            else
            {
                Console.WriteLine("\tЗнайдено:");
                invoice.Print();
                invoice.Edit();
                Console.Clear();
                Console.WriteLine("Відредагований штраф має вид: ");
                invoice.Print();

                if(invoice.isPayed == true)
                {
                    if (invNotPayed.Remove(invoice))
                    {
                        invPayed.Add(invoice);
                        Save();
                    }
                }
            }
            Console.ReadKey();
        }

        //Пошук штрафів
        public void Search()
        {
            Console.Clear();
            Console.WriteLine("Введіть номер штрафу");
            string searchInput = Console.ReadLine();

            List<Invoice> findResult =new();

            findResult.AddRange(invPayed.FindAll(p => p.numInvoice == searchInput));
            findResult.AddRange(invNotPayed.FindAll(p => p.numInvoice == searchInput));

            if(findResult.Count == 0)
                Console.WriteLine("\tЗа вказаним номером штрафу - нічого не знайдено");
            else
            {
                Console.WriteLine("\tЗнайдено:");
                Show(findResult);
            }
            Console.ReadKey();
        }

        //Додавання штрафу
        public void Add()
        {
            Console.Clear();
            Invoice inv = new();
            inv.SetInvoice();
            if (inv.isPayed)
            {
                invPayed.Add(inv);
                invPayed.Sort();
                Save();
            }
            else
            {
                invNotPayed.Add(inv);
                invNotPayed.Sort();
                Save();
            }
            Console.Clear();
        }
        
        //Збереження штрафів у файл
        public void Save()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                BinaryFormatter bf = new();

                using (Stream stream = File.Create(dirPath + fileNamePayed))
                {
                    bf.Serialize(stream, invPayed);
                }
                using (Stream stream = File.Create(dirPath + fileNameNotPayed))
                {
                    bf.Serialize(stream, invNotPayed);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Завантаження штрафів із файлу
        private void Load()
        {
            try
            {
                BinaryFormatter bf = new();
                using (Stream stream = File.OpenRead(dirPath + fileNamePayed))
                {
                    invPayed = (List<Invoice>)bf.Deserialize(stream);
                }
                using (Stream stream = File.OpenRead(dirPath + fileNameNotPayed))
                {
                    invNotPayed = (List<Invoice>)bf.Deserialize(stream);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        //Переглянути штрафи
        public void Print() 
        {
            Console.Clear(); 
 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tCплачені штрафи");
            foreach(Invoice inv in invPayed) 
            { 
                inv.Print();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tНе сплачені штрафи");
            foreach (Invoice inv in invNotPayed)
            {
                inv.Print();
            }
            Console.ReadKey();
        }
        private static void Show(List<Invoice> invoice)
        {
            foreach (Invoice i in invoice)
            {
                i.Print();
            }
        }
        
        //Меню
        public void Menu()
        {
            Load();
            List<string> list = new()
            {
                "Додати штраф",
                "Роздрукувати штрафи",
                "Пошук штрафу",
                "Редагувати штраф",
                "Вихід"
            };

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                int a = ConsoleMenu.SelectVertical(HPosition.Center,
                                    VPosition.Top,
                                    HorizontalAlignment.Center,
                                    list);

                switch (a)
                {
                    case 0:
                        Add();
                        break;
                    case 1:
                        Print();
                        break;
                    case 2:
                        Search();
                        break;
                    case 3: 
                        Mod(); 
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
            Console.Clear();
            Save();
        }

    }
}
