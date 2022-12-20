using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;


namespace Invoice_for_payment
{
    public class InvoiceApplication
    {
        private List<Invoice> invPayed = new();
        private List<Invoice> invNotPayed = new();

        //Налаштування файлової системи
        private string dirPath = "../../../../SaveFile";
        private string fileNamePayed = "/Payed.bin";
        private string fileNameNotPayed = "/NotPayed.bin";

        /// <summary>
        /// TO DO
        /// </summary>
        
        //Редагування штрафів
        public void Mod()
        {

        }
        //Пошук штрафів
        public void Search()
        {

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
            }
            else
            {
                invNotPayed.Add(inv);
                invNotPayed.Sort();
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
                BinaryFormatter bf = new BinaryFormatter();

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
                BinaryFormatter bf = new BinaryFormatter();
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

        private void Show(List<Invoice> invoice)
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
            List<string> list = new List<string>();
            list.Add("Додати штраф");
            list.Add("Роздрукувати штрафи");
            list.Add("Пошук штрафу");
            list.Add("Редагувати штраф");
            list.Add("Вихід");

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
                    case 5:
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
