using System;

namespace FlightSimulator
{
    class Simulator
    {
        private Pilot plane = new Pilot();
        private Dispatcher[] Dispatchers { get; set; }
        public void AddDispatcher(String name)
        {
            Dispatcher[] ndis;
            if (Dispatchers != null)
            {
                ndis = new Dispatcher[Dispatchers.Length + 1];
                for (int i = 0; i < Dispatchers.Length; i++)
                {
                    ndis[i] = Dispatchers[i];
                }
            }
            else
            {
                ndis = new Dispatcher[1];
            }
            ndis[ndis.Length - 1] = new Dispatcher(name);
            Dispatchers = ndis;
            plane.Recomendations += Dispatchers[Dispatchers.Length - 1].Recomendation;
        }
        public void DelDispatcher(String name)
        {
            if (Dispatchers == null || Dispatchers.Length <= 2) throw new InvalidOperationException("Вы не можете удалить диспетчера, т.к. у вас минимальное количество диспетчеров для полета или меньше!");
            int index = -1;
            for (int i = 0; i < Dispatchers.Length; i++)
            {
                if (Dispatchers[i].Name.Contains(name)) index = i;
            }
            if (index == -1) throw new ArgumentException("Диспетчера с таким именем не существует!");
            Dispatcher[] ndis = new Dispatcher[Dispatchers.Length - 1];
            plane.Recomendations -= Dispatchers[Dispatchers.Length - 1].Recomendation;
            for (int i = 0, space = 0; i < Dispatchers.Length; i++, space++)
            {
                if (i == index) { plane.BufferPoints += Dispatchers[space].PenaltyPoints; space++; }
                if (space != Dispatchers.Length) ndis[i] = Dispatchers[space];
            }
            Dispatchers = ndis;
        }
        public void SpeedIncrease(ConsoleKeyInfo key) // увеличение скорости
        {
            if (Dispatchers == null || Dispatchers.Length < 2) throw new InsufficientNumberOfDispatchersException("У вас слишком мало диспетчеров для полета(<2)!");
            plane.SpeedIncrease(key);
        }

        public void SpeedReduction(ConsoleKeyInfo key) // уменьшение скорости
        {
            if (Dispatchers.Length < 2) throw new InsufficientNumberOfDispatchersException("У вас слишком мало диспетчеров для полета(<2)!");
            plane.SpeedReduction(key);
        }
        public void PrintDispatchers()
        {
            if (Dispatchers != null && (Dispatchers[0].OnAir && plane.CurrentSpeed == 0 && plane.CurrentHeight == 0))
            {
                for (int i = 0; i < Dispatchers.Length; i++)
                {
                    plane.BufferPoints += Dispatchers[i].PenaltyPoints;
                }
                Console.WriteLine("Ура! Вы прилетели! Итого штрафных очков вы заработали : {0}", plane.BufferPoints);
                return;
            }
            if (Dispatchers != null)
            {
                for (int i = 0; i < Dispatchers.Length; i++)
                {
                    Console.Write(Dispatchers[i].Name + "(штрафные очки : {0}) ", Dispatchers[i].PenaltyPoints);
                    Console.WriteLine("Корректировка погодных условий : {0} ", Dispatchers[i].Adjustment);
                    Console.WriteLine(Dispatchers[i].Message);
                }
            }
        }
        void Text(Pilot curPlane) // вывод клавиш управления и их значений
        {
            Console.WriteLine("Клавиши управления : " +
                              "\nУвеличить скорость(UpArrow = +50км/ч, Shift + UpArrow = +150км/ч), " +
                              "\nУменьшить скорость(DownArrow = -50км/ч, Shift + DownArrow = -150км/ч), " +
                              "\nУвеличить высоту(PageUp = +250м, Shift + PageUp = +500м), " +
                              "\nУменьшить высоту(PageDown = -250м, Shift + PageDown = -500м), " +
                              "\nДобавить диспетчера(LeftArrow), Удалить диспетчера(RightArrow)");
            Console.WriteLine("==================================================");
            Console.WriteLine("Текущая скорость : {0} \nТекущая высота : {1}", curPlane.CurrentSpeed, curPlane.CurrentHeight);
            PrintDispatchers();
        }

        public void MainFunction()
        {
            String name;
            Text(plane);
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            try
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        SpeedIncrease(keyInfo);
                        break;
                    case ConsoleKey.DownArrow:
                        SpeedReduction(keyInfo);
                        break;
                    case ConsoleKey.PageUp:
                        plane.HeightIncrease(keyInfo);
                        break;
                    case ConsoleKey.PageDown:
                        plane.HeightReduction(keyInfo);
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Write("Введите имя нового диспетчера : ");
                        name = Console.ReadLine();
                        AddDispatcher(name);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Write("Введите имя диспетчера для удаления : ");
                        name = Console.ReadLine();
                        DelDispatcher(name);
                        break;
                }
            }
            catch (InsufficientNumberOfDispatchersException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Нажмите Enter");
                Console.ReadLine();
            }
            catch (PlaneCrashedException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Нажмите Enter");
                Console.ReadLine();
            }
            catch (UnfitForFlightException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Нажмите Enter");
                Console.ReadLine();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Нажмите Enter");
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Нажмите Enter");
                Console.ReadLine();
            }
            Console.Clear();
        }
        // ReSharper disable once FunctionNeverReturns
    }
}
