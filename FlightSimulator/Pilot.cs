using System;

namespace FlightSimulator
{
    delegate void GetRecomendations(int speed, int height);
    class Pilot
    {
        public event GetRecomendations Recomendations;
        public static int MaxSpeed { get; } = 1000;
        // ReSharper disable once RedundantDefaultMemberInitializer
        public int CurrentSpeed { get; set; } = 0;
        // ReSharper disable once RedundantDefaultMemberInitializer
        public int CurrentHeight { get; set; } = 0;
        // ReSharper disable once RedundantDefaultMemberInitializer
        public int BufferPoints { get; set; } = 0;
        public void SpeedIncrease(ConsoleKeyInfo key) // увеличение скорости
        {
            int plusSpeed;
            if ((key.Modifiers & ConsoleModifiers.Shift) != 0) // проверка : была ли нажата клавиша Shift
            {
                plusSpeed = 150; // в переменную записывается 150, если была нажата клавиша Shift
            }
            else
            {
                plusSpeed = 50; // в переменную записывается 50, если не была нажата клавиша Shift
            }
            CurrentSpeed += plusSpeed; // к текущей скорости прибавляется скорость, которая хранится в переменной plusSpeed
            Recomendations?.Invoke(CurrentSpeed, CurrentHeight); // вызов события, уведомление всех диспетчеров о изменении параметров полета
        }

        public void SpeedReduction(ConsoleKeyInfo key) // уменьшение скорости
        {
            if (CurrentSpeed == 0) return; // если скорость равно нулю, нет смысла отнимать, назад самолет не станет двигаться, а лишняя нагрузка на компьютер не нужна
            int minusSpeed;
            if ((key.Modifiers & ConsoleModifiers.Shift) != 0) // проверка : была ли нажата клавиша Shift
            {
                minusSpeed = 150; // в переменную записывается 150, если была нажата клавиша Shift
            }
            else
            {
                minusSpeed = 50; // в переменную записывается 50, если не была нажата клавиша Shift
            }
            CurrentSpeed -= minusSpeed; // от текущей скорости отнимается скорость, которая хранится в переменной minusSpeed
            if (CurrentSpeed < 0) CurrentSpeed = 0; // если скорость стала отрицательной, то ей присваевается 0 значение
            Recomendations?.Invoke(CurrentSpeed, CurrentHeight); // вызов события, уведомление всех диспетчеров о изменении параметров полета
        }

        public void HeightIncrease(ConsoleKeyInfo key) // увеличение высоты
        {
            if (CurrentSpeed == 0) return; // если скорость равна нулю, то увеличение высоты невозможно
            int plusHeight;
            if ((key.Modifiers & ConsoleModifiers.Shift) != 0) // проверка : была ли нажата клавиша Shift
            {
                plusHeight = 500; // в переменную записывается 500, если была нажата клавиша Shift
            }
            else
            {
                plusHeight = 250; // в переменную записывается 250, если не была нажата клавиша Shift
            }
            CurrentHeight += plusHeight; // к текущей высоте прибавляется высота, которая хранится в переменной plusHeight
            Recomendations?.Invoke(CurrentSpeed, CurrentHeight); // вызов события, уведомление всех диспетчеров о изменении параметров полета
        }

        public void HeightReduction(ConsoleKeyInfo key) // уменьшение высоты
        {
            if (CurrentSpeed == 0 || CurrentHeight == 0) return; // если скорость или высота равно нулю, нет смысла отнимать высоту, т.к. она и так нулевая
            int minusHeight;
            if ((key.Modifiers & ConsoleModifiers.Shift) != 0) // проверка : была ли нажата клавиша Shift
            {
                minusHeight = 500; // в переменную записывается 500, если была нажата клавиша Shift
            }
            else
            {
                minusHeight = 250; // в переменную записывается 250, если не была нажата клавиша Shift
            }
            CurrentHeight -= minusHeight; // от текущей скорости отнимается скорость, которая хранится в переменной minusHeight
            if (CurrentSpeed < 0) CurrentSpeed = 0; // если высота стала отрицательной, то ей присваевается 0 значение
            Recomendations?.Invoke(CurrentSpeed, CurrentHeight); // вызов события, уведомление всех диспетчеров о изменении параметров полета
        }
    }
}
