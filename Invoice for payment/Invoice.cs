using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Invoice_for_payment
{
    [Serializable]
    public class Invoice : ISerializable, IComparable<Invoice>
    {
        //Булевий покажчик стану рахунку
        public bool isPayed = false;
        //Номер рахунку
        public string numInvoice;
        //Оплата за день
        public double PayByDay { get; set; }
        //Кількість днів
        public int DayCount { get; set; }
        //Штраф за один день затримки оплати
        public double PenaltyByDay { get; set; }
        //Кількість днів затримки сплати
        public int DayPenaltyCount { get; set; }

        ////Поля які розраховуються
        //Cумма до сплати без штрафу
        public double payWithoutPenalty;
        //Штраф
        public double penalty;
        //Загальна сумма до сплати
        public double ivoiceForPayment;

        ////Конструктори
        public Invoice()
        {
            numInvoice = SetnumInvoice();
        }

        ///Серіалізація
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Номер рахунку", numInvoice);
            info.AddValue("Стан рахунку", isPayed);
            info.AddValue("Оплата за день", PayByDay);
            info.AddValue("Кількість днів на сплату штрафу", DayCount);
            info.AddValue("Сумма штрафу за один день затримки", PenaltyByDay);
            info.AddValue("Кількість днів затримки сплати штрафу", DayPenaltyCount);
            
            if (isPayed == true)
            {
                info.AddValue("Сумма до сплати без штрафу", payWithoutPenalty);
                info.AddValue("Сумма штрафу", penalty);
                info.AddValue("Загальная сумма до сплати", ivoiceForPayment);
            }

        }
        private Invoice(SerializationInfo info, StreamingContext context)
        {
            numInvoice = info.GetString("Номер рахунку");
            isPayed = info.GetBoolean("Стан рахунку");
            PayByDay = info.GetDouble("Оплата за день");
            DayCount = info.GetInt32("Кількість днів на сплату штрафу");
            PenaltyByDay = info.GetDouble("Сумма штрафу за один день затримки");
            DayPenaltyCount = info.GetInt32("Кількість днів затримки сплати штрафу");

            if (isPayed == true)
            {
                payWithoutPenalty = info.GetDouble("Сумма до сплати без штрафу");
                penalty = info.GetDouble("Сумма штрафу");
                ivoiceForPayment = info.GetDouble("Загальная сумма до сплати");
            }

        }

        ////Методи
        ///Метод друку рахунку
        public void Print()
        {
            string payedInfo = isPayed == true ? ("Оплачено") : ("Не оплачено");
            
            if (isPayed) 
                Console.ForegroundColor = ConsoleColor.Green;
            else 
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(
                $"■ Номер рахунку: {numInvoice};\n" +
                $"■ Статус рахунку: {payedInfo};\n" +
                $"■ Оплата за день: {PayByDay};\n" +
                $"■ Кількість днів: {DayCount};\n" +
                $"■ Штраф за один день затримки оплати: {PenaltyByDay};\n" +
                $"■ Кількість днів затримки оплати: {DayPenaltyCount};");
            Console.WriteLine(
                $"■ Сумма до сплати без врахування штрафу: {payWithoutPenalty};\n" +
                $"■ Штраф: {penalty};\n" +
                $"■ Загальна сумма до сплати: {ivoiceForPayment}.\n");
            Console.ResetColor();
        }


        ///Приватний метод встановлення номера рахунку
        static private string SetnumInvoice()
        {
            DateTime dT = DateTime.Now;
            return $"{dT.Year}{dT.Month}{dT.Day}{dT.Hour}{dT.Minute}{dT.Millisecond}";
        }
        ////Метод перевірки вводу числа від користувача
        static private double TakeDoubleInput(string message)
        {
            string pattern = @"^(\d*)+[,]?(\d){1,2}$";
            double rezult;
            Regex regex = new(pattern);
            while (true)
            {
                Console.WriteLine(message);
                string temp = Console.ReadLine();
                if (regex.IsMatch(temp))
                {
                    rezult = Convert.ToDouble(temp);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний ввод данних");
                    Console.ResetColor();
                }
            }
            return rezult;
        }
        static private int TakeIntInput(string message)
        {
            string pattern = @"^(\d)+$";
            int rezult;
            Regex regex = new(pattern);
            while (true)
            {
                Console.WriteLine(message);
                string temp = Console.ReadLine();
                if (regex.IsMatch(temp))
                {
                    rezult = Int32.Parse(temp);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний ввод данних");
                    Console.ResetColor();
                }
            }
            return rezult;
        }
        static private bool TakeBoolInput(string message) 
        {
            List<string> list = new List<string>();
            list.Add("Так");
            list.Add("Ні");

            bool exit = false;
            bool rezult = false;

            while (!exit)
            {
                Console.Clear();    
                Console.WriteLine(message.PadLeft(55));
                int a = ConsoleMenu.SelectVertical(HPosition.Center,
                                    VPosition.Top,
                                    HorizontalAlignment.Center,
                                    list);
                switch (a)
                {
                    case 0:
                        { rezult = true; exit = true; break; }
                    case 1:
                        { rezult = false; exit = true; break;}
                }
            }
            Console.Clear();
            return rezult;
        }

        ////Методи обчислення розрахункових полів
        ////Метод розрахунку суми до сплати без штрафу
        private double CalculetePayWithoutPenalty()
        {
            return PayByDay * DayCount;
        }
        ////Метод розрахунку суми штрафу
        private double CalculetePenalty()
        {
            return PenaltyByDay * DayPenaltyCount;
        }
        ////Метод розрахунку загальної сумми до сплати
        private double CalculeteIvoiceForPayment()
        {
            return payWithoutPenalty + penalty;
        }

        ////Перевантаження ту стрінг
        public override string ToString()
        {
            string payedInfo = isPayed == true ? ("Оплачено") : ("Не оплачено");
            return
                $"■ Номер рахунку: {numInvoice};\n" +
                $"■ Статус рахунку: {payedInfo};\n" +
                $"■ Оплата за день: {PayByDay};\n" +
                $"■ Кількість днів: {DayCount};\n" +
                $"■ Штраф за один день затримки оплати: {PenaltyByDay};\n" +
                $"■ Кількість днів затримки оплати: {DayPenaltyCount};\n" +
                $"■ Сумма до сплати без врахування штрафу: {payWithoutPenalty};\n" +
                $"■ Штраф: {penalty};\n" +
                $"■ Загальна сумма до сплати: {ivoiceForPayment}.";

        }

        ////Сеттер
        public void SetInvoice()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Створено рахунок № {numInvoice}".PadLeft(60));
            Console.ResetColor();
            PayByDay = TakeDoubleInput("Введіть сумму оплати за день");
            DayCount = TakeIntInput("Введіть кількість днів на сплату штрафу");
            PenaltyByDay = TakeDoubleInput("Введіть сумму штрафу за один день затримки");
            DayPenaltyCount = TakeIntInput("Введіть кількість днів затримки сплати штрафу");
            payWithoutPenalty = CalculetePayWithoutPenalty();
            penalty = CalculetePenalty();
            ivoiceForPayment = CalculeteIvoiceForPayment();
            isPayed = TakeBoolInput($"За рахунком №{numInvoice}\tШтраф сплачено?");
        }

        ///Метод редактування штрафу
        public void Edit()
        {
            PenaltyByDay = TakeDoubleInput("Введіть сумму штрафу за один день затримки");
            DayPenaltyCount = TakeIntInput("Введіть кількість днів затримки сплати штрафу");
            payWithoutPenalty = CalculetePayWithoutPenalty();
            penalty = CalculetePenalty();
            ivoiceForPayment = CalculeteIvoiceForPayment();
            isPayed = TakeBoolInput($"За рахунком №{numInvoice}\tШтраф сплачено?");
        }

        public int CompareTo(Invoice obj)
        {
            return numInvoice.CompareTo(obj.numInvoice);
        }
    }
}
