using System;

namespace FlightSimulator
{
    class Dispatcher
    {
        public string Name { get; set; } // имя диспетчера
        public int Adjustment { get; set; } // корректировка погодных условий
        public int PenaltyPoints { get; set; } // штрафные очки, которые начислил диспетчер
        public bool OnAir { get; set; } // флаг, который указывает на то, в каком состоянии в данный момент самолет(на земле/в воздухе)
        public string Message { get; set; } // переменная, в которой хранится рекомендация
        public Dispatcher(String name) // конструктор диспетчера, обязательно надо указать имя
        {
            var rand = new Random(name.GetHashCode());
            Name = name.Trim().Replace(" ", "");
            Adjustment = rand.Next(-200, 200); // генерируется корректировка погодных условий
        }

        public void Recomendation(int speed, int height)
        {
            if (!OnAir && speed > 0 && height > 0) // если показатели высоты и скорости выше нуля, флаг OnAir активируется, это нужно для проверки дальнейшего состояния самолета
            {
                OnAir = true;
            }
            if (speed > Pilot.MaxSpeed) // если превышена максимальная скорость, начисляются штрафные очки(100) до тех пор, пока она не войдет в диапазон нормы
            {
                PenaltyPoints += 100;
                Console.Write("Немедленно снизьте скорость полета! Вам начисленно 100 штрафных очков!");
                Message = "Немедленно снизьте скорость полета! Вам начисленно 100 штрафных очков!";
            }
            else if (OnAir && (speed <= 0 || height <= 0)) // если во время полета(OnAir == true) один из показателей станет 0 или меньше, генерируется исключение о крушении
            {
                //throw new PlaneCrashedException("Ваш самолет потерпел крушение! Высота или скорость были недопустимо малы!");

            }
            else // выполняется код высчитывания рекомендуемой высоты
            {
                int recomendedHeight = 7 * speed - Adjustment;
                Console.Write("Рекомендованная высота полета : {0} ", recomendedHeight);
                Message = ("Рекомендованная высота полета : " + recomendedHeight);
                var diapason = (height > recomendedHeight) ? height - recomendedHeight : recomendedHeight - height;
                if (diapason > 300 && diapason <= 600) // если разница между текущей и рекомендуемой высоты в этом диапазоне, то начисляются штрафные очки(25)
                {
                    PenaltyPoints += 25;
                    Console.Write("Превышен рекомендуемый диапазон высоты! Вам начислено 25 штрафных очков!");
                    Message += "Превышен рекомендуемый диапазон высоты! Вам начислено 25 штрафных очков!";
                }
                else if (diapason > 600 && diapason <= 1000) // если разница между текущей и рекомендуемой высоты в этом диапазоне, то начисляются штрафные очки(50) 
                {
                    PenaltyPoints += 50;
                    Console.Write("Превышен рекомендуемый диапазон высоты! Вам начислено 50 штрафных очков!");
                    Message += "Превышен рекомендуемый диапазон высоты! Вам начислено 50 штрафных очков!";
                }
                else if (diapason > 1000) // // если разница между текущей и рекомендуемой высоты выше этой точки, генерируется исключение о крушении
                {
                    throw new PlaneCrashedException("Превышен допустимый диапазон высоты! Ваш самолет потерпел крушение!");
                }
            }
            Check();
        }

        private void Check() // метод, проверяющий кол-во штрафных очков, если превышена норма(1000), то генерируется исключение о непригодности пилота
        {
            if (PenaltyPoints > 1000)
            {
                throw new UnfitForFlightException("Вы набрали более 1000 штрафных очков! Вы непригодны к полетам!");
            }
        }
    }
}
