using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;





namespace Lab2
{
    public class UI
    {
       static Dictionary<String, Matrix> all = new Dictionary<String, Matrix>(); //Matrix m;

      public static void menu()
        {
            Console.WriteLine(

@"
 Работа с матрицами
------------------------------
1 – Ввод матрицы
2 - Операции
3 – Вывод результатов
0 - Выход
------------------------------");
            char c = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (c)
                {
                    case '1': newMatrix(); break;
                    case '2': operations(); break;
                    case '3': print(); menu(); break;
                case '0': Environment.Exit(0); break;

                default: {
                        Console.WriteLine("Неправильная комманда. Выберите другую: ");
                            break; }
                }

            menu();
        }

       static void newMatrix()

        {
            Console.WriteLine(

 @"
Создайте новую матрицу 
------------------------------
1 – Создать квадратную матрицу автоматически
2 - Создать прямоугольную матрицу автоматически
3 – Ввести с клавиатуры
0 - Выход
------------------------------");
            char c = char.ToLower(Console.ReadKey(true).KeyChar);

            switch (c){
           
                case '1': matrixSquare(); break;
                case '2': matrixRectangle(); break;
                case '3': matrixInput(); break;
                case '0': return;
                default: { Console.WriteLine("Неправильная комманда."); newMatrix(); break; }
            }
           return;
        }

      static void matrixSquare()
        {
            Console.WriteLine(

@"
Выберите команду
------------------------------
1 – Создать единичную матрицу
2 - Создать пустую матрицу
3 – Сгенерировать данные автоматически
0 (или другая клавиша) - Выход в главное меню
------------------------------");
            char choice1 = char.ToLower(Console.ReadKey(true).KeyChar);
            if (choice1 == '1'|| choice1 == '2'||choice1 == '3')
            {
                Console.WriteLine("Введите название матрицы");
                String choice3 = Console.ReadLine();



                if (!all.ContainsKey(choice3))
                {
                    try
                    {
                        Console.WriteLine("Введите размер");
                        int choice2 = int.Parse(Console.ReadLine());
                        if (choice2 < 1) { Console.WriteLine("Неправильный размер. Выберите новую комманду: "); matrixSquare(); }
                        else
                        {
                            switch (choice1)
                            {
                                case '1': all.Add(choice3, Matrix.GetUnity(choice2)); break;
                                case '2': all.Add(choice3, Matrix.GetEmpty(choice2)); break;
                                case '3': all.Add(choice3, new Matrix(choice2, false)); break;
                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Формат данных неверен. Повторите операцию: "); matrixSquare(); }


                }
                else { Console.WriteLine("Имя существует. Повторите операциюю "); matrixSquare(); }
            }
            return;
        }

      static void matrixRectangle()
        {
            Console.WriteLine("Введите название матрицы");
            String choice3 = Console.ReadLine();
            if (!all.ContainsKey(choice3))
            { try
                {
                    Console.WriteLine("Введите количество строк");
                    int n = int.Parse(Console.ReadLine());
                    if (n < 1) { Console.WriteLine("Неправильный размер. Повторите операцию: "); matrixRectangle(); }
                    else
                    {
                        Console.WriteLine("Введите количество столбцов");
                        int m = int.Parse(Console.ReadLine());
                        if (m < 1) { Console.WriteLine("Неправильный размер. Повторите операцию: "); matrixRectangle(); }
                        else all.Add(choice3, new Matrix(n, m, false));
                    }
                }
                catch (FormatException){ Console.WriteLine("Формат данных неверен. Повторите операцию: "); matrixRectangle(); }
            }
            else { Console.WriteLine("Имя существует. Повторите операцию "); matrixRectangle(); }
            return;
        }

      static void matrixInput()
        { Console.WriteLine("Введите название матрицы");
            String choice3 = Console.ReadLine();
            if (!all.ContainsKey(choice3))
            {
                Console.WriteLine("Введите элементы матрицы (разделяйте строчки через \";\", а столбцы через пробел: ");
            string choice = Console.ReadLine();
                Matrix m;
            bool res = Matrix.TryParse(choice, out m);
                if (res) all.Add(choice3, m);
            else
            {
                Console.WriteLine("Формат неверен. Введите снова или нажмите 0 для выхода в главное меню");
                string ch = Console.ReadLine();
                if (ch == "0") return;
                else matrixInput();
            }
            }
            else { Console.WriteLine("Имя существует. Повторите операцию "); matrixInput(); }
            return;


        }

      static void operations(){
            Console.WriteLine(
    @"
    Выберете из списка:
    1 – свойства матрицы
    2 – сложить матрицы
    3 – вычесть из матрицы другую матрицу
    4 – умножить матрицу на число
    5 – умножить матрицу на другую матрицу
    6 – транспонировать матрицу
    7 - вычислить след матрицы
    0 – Выход в главное меню "
        );
            char c = char.ToLower(Console.ReadKey(true).KeyChar);
            switch (c)
            {
                case '1': properties(); break;
                case '2': sum(); break;
                case '3': substract(); break;
                case '4': multiplyNum(); break;
                case '5': multiply(); break;
                case '6': 
                    {
                        Console.WriteLine(@"Выберите матрицу");
                        Matrix m = null;
                        getMatrix(out m);
                        m.Transpose();
                        Console.WriteLine(m);
                        break;
                    }
                case '7': {
                        Console.WriteLine(@"Выберите матрицу");
                        Matrix m = null;
                        getMatrix(out m);
                        Console.WriteLine(m.Trace()); break;
                          }
                case '0': return;
                default: { Console.WriteLine("Неправильная комманда. "); break; }
            }
            return;
        }

      static void properties()
        {
  Console.WriteLine(@"Выберите матрицу");
  Matrix m = null;
  getMatrix(out m);

            Console.WriteLine(
    @"
    Выберете свойство (можно выбрать несколько подряд):
    1 – Количество столбцов и строк
    2 – Размер матрицы (только для квадратной)
    3 - Является ли матрица квадратной
    4 – Является ли матрица нулевой
    5 – Является ли матрица единичной
    6 – Является ли матрица диагональной
    7 - Является ли матрица симметричной
    0 – Выход в операции "
        );
            char c = char.ToLower(Console.ReadKey(true).KeyChar);
            while (c!='0'){
                switch (c)
                {
                    case '1': Console.WriteLine("Строки: "+ m.Rows + " Cтолбцы: " + m.Columns); break;
                    case '2': { if (m.Size!=null) 
                            Console.WriteLine("Размер: " + m.Size); 
                            else Console.WriteLine("Матрица не квадратная"); 
                    break; }
                    case '3': Console.WriteLine("Является ли матрица квадратной: " + m.IsSquared); break;
                    case '4': Console.WriteLine("Является ли матрица нулевой: " + m.IsEmpty); break;
                    case '5': Console.WriteLine("Является ли матрица единичной: " + m.IsUnity); break;
                    case '6': Console.WriteLine("Является ли матрица диагональной: " + m.IsDiagonal); break;
                    case '7': Console.WriteLine("Является ли матрица симметричной: " + m.IsUnity); break;
                    case '0': operations(); break;
                    default: { Console.WriteLine("Неправильная комманда. ");  break; }
                }
                Console.WriteLine("Выбирете другую команду: "); 
                c = char.ToLower(Console.ReadKey(true).KeyChar);
            }
            operations();
        }

      public static void getMatrix(out Matrix m){
            Console.WriteLine(
    @"
    1 – Ввести имя
    2 - Ввести новую
    3 - Посмотреть существующие матрицы
    0 – Выход в операции "
        );
            switch (char.ToLower(Console.ReadKey(true).KeyChar))
            {
                case '1':
                    {
                        Console.WriteLine("Введите название матрицы");
                        String choice3 = Console.ReadLine();
                        if (all.ContainsKey(choice3)) {
                            all.TryGetValue(choice3, out m);
                            break; }
                        else { Console.WriteLine("Имя не существует. Повторите операцию "); getMatrix(out m); }
                        break;
                    }
                    
                case '2': newMatrix(); getMatrix(out m); break;
                case '3': {
                        foreach (KeyValuePair<string, Matrix> j in all)
                        Console.WriteLine(j);
                        getMatrix(out m);
                         break;
                           }
                case '0': m = null; operations(); break;
                default: { Console.WriteLine("Неправильная комманда. Выбирете другую: "); getMatrix(out m); break; }
            }
            return;

        }

      static void sum(){
            Console.WriteLine("Введите название новой матрицы");
            String choice3 = Console.ReadLine();
            if (!all.ContainsKey(choice3))
            {
                Console.WriteLine( @"Выберите матрицу А для сложения:");
            Matrix m=null;
            getMatrix(out m);
            Console.WriteLine(@"Выберите матрицу B для сложения:");
            Matrix n = null;
            getMatrix(out n);

                Matrix tmp = m + n;
                if(tmp!=null) all.Add(choice3, tmp);
                Console.WriteLine(tmp);
            }
            else { Console.WriteLine("Имя существует. Повторите операцию "); sum(); }
            return;
        }

      static void substract()
        {
            Console.WriteLine("Введите название новой матрицы");
            String choice3 = Console.ReadLine();
            if (!all.ContainsKey(choice3))
            {
                Console.WriteLine(@"Выберите матрицу А - уменьшаемое:");
            Matrix m = null;
            getMatrix(out m);
            Console.WriteLine(@"Выберите матрицу B - вычитаемое:");
            Matrix n = null;
            getMatrix(out n);
           
                Matrix tmp = m - n;
                if (tmp != null) all.Add(choice3, tmp);
                Console.WriteLine(tmp);
            }
            else { Console.WriteLine("Имя существует. Повторите операцию "); substract(); }
            return;
        }

      static void multiplyNum()
        {
            Console.WriteLine("Введите название новой матрицы");
            String choice3 = Console.ReadLine();
            if (!all.ContainsKey(choice3))
            {
                Console.WriteLine(@"Выберите матрицу");
                Matrix m = null;
                getMatrix(out m);
                try
                {
                    Console.WriteLine(@"Введите мультипликатор");
                    int n = int.Parse(Console.ReadLine());

                    Matrix tmp = m * n;
                    if (tmp != null) all.Add(choice3, tmp);
                    Console.WriteLine(tmp);
                }
                catch (FormatException) { Console.WriteLine("Формат данных неверен. Повторите операцию: "); multiplyNum(); }
            }
            else { Console.WriteLine("Имя существует. Повторите операцию "); multiplyNum(); }
            return;
        }

        static void multiply()
        {
            Console.WriteLine("Введите название новой матрицы");
            String choice3 = Console.ReadLine();
            if (!all.ContainsKey(choice3))
            {
                Console.WriteLine(@"Выберите матрицу А");
                Matrix m = null;
                getMatrix(out m);
                Console.WriteLine(@"Выберите матрицу B");
                Matrix n = null;
                getMatrix(out n);

                Matrix tmp = m * n;
                if (tmp != null) all.Add(choice3, tmp);
                Console.WriteLine(tmp);
            }
            else { Console.WriteLine("Имя существует. Повторите операцию "); multiply(); }
            return;
        }

        static void print(){
            Console.WriteLine(
    @"
    Выберите операцию:
    1 – Вывсти одну матрицу
    2 – Вывести все
    0 – Выход в главное меню "
        );

            switch (char.ToLower(Console.ReadKey(true).KeyChar))
            {
                case '1': { 
                        Console.WriteLine("Введите имя матрицы: ");
                        String choice3 = Console.ReadLine();
                        Matrix tmp;
                        if(all.TryGetValue(choice3, out tmp))
                        Console.WriteLine(tmp);
                        else Console.WriteLine("Неудалось найти матрицу."); print();
                        break; }
                case '2':
                    {
                        foreach (KeyValuePair<string, Matrix> j in all)
                            Console.WriteLine(j);
                        break;
                    }
                case '0': return;
                default: { Console.WriteLine("Неправильная комманда. Выбирете другую: "); print(); break; }
            }

            print();
        }
    
    }
}