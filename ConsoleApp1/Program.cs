using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection connect = CreateConnection("testdb1");
            ReadData(connect,"regions");

            Console.ReadLine();
        }

        public static SQLiteConnection CreateConnection(string file)
        {
            /// подключаемся к заданному файлу
            SQLiteConnection connect = new SQLiteConnection($"DataSource={file}");
            try
            {
                /// пробуем его открыть
                connect.Open();
            }
            catch (Exception ex)
            {
                /// если не открывается - получаем исключение
                Console.WriteLine(ex);
                throw;
            }
            /// возвращаем соединение
            return connect;
        }

        public static void ReadData(SQLiteConnection conn, string table)
        {
            /// класс для чтения данных
            SQLiteDataReader sqlite_datareader;
            /// команда sqlite
            SQLiteCommand sqlite_cmd;
            /// создаем команду для конкретного соединения
            sqlite_cmd = conn.CreateCommand();
            /// команда читает все данные из таблицы table
            sqlite_cmd.CommandText = $"SELECT * FROM {table}";

            /// команда чтения выполняет то, что записано в ее тексте (читать можно и более сложные данные)
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            /// теперь мы можем читать данные по строкам
            while (sqlite_datareader.Read())
            {
                /// считанная строка
                string myreader = sqlite_datareader.GetString(1);
                /// выводит данные на консоль
                Console.WriteLine(myreader);
            }
            conn.Close();
        }

    }
}
