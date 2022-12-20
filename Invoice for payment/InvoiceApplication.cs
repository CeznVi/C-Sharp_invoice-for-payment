using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_for_payment
{
    public class InvoiceApplication
    {
        private List<Invoice> invPayed;
        private List<Invoice> invNotPayed;

        //Додавання штрафу
        public void Add()
        {
            Invoice inv = new();
            inv.SetInvoice();
            if (inv.isPayed)
                invPayed.Add(inv);
            else
                invNotPayed.Add(inv);
            Console.Clear();
        }
        
        //Видалення штрафів
        public void Remove() { }

        //Збереження штрафів у файл


        //Завантаження штрафів із файлу
        
        
        //Переглянути штрафи
        public void Print() 
        { 
        
        }

        //Меню


        

    }
}
