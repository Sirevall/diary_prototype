using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Diary_prototype
{
    class Diary
    {
        public void Add(List<Note> Notes)
        {
            DateTime time = new DateTime();

            time = DateTime.Now;

            int ID;
            if (Notes.Count != 0) ID = Notes.Max(e => e.NoteNumber) + 1;
            else ID = 1;
            

            Console.Write("\nВведите текст заметки: ");
            string Text = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Введите место: ");
            string Place = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Сделана ли задача?\n1 - да  2 - нет\nОтвет - ");
            string Status = Console.ReadKey().KeyChar.ToString() == "1"? "Выполнено" : "Не выполнено";
            Console.WriteLine();

            Notes.Add(new Note(ID, time, Text, Place, Status));
            Console.WriteLine();
        }

        public void RemoveNote(List<Note> Notes)
        {
            Console.Write("По какому столбцу хотите выполнить удаление?" +
                "\n1 - номер заметки" +
                "\n2 - время записи" +
                "\n3 - статус" +
                "\n4 - место создания" +
                "\n5 - содержание" +
                "\nОтвет - ");
            string DeleteMode = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine();

            string AnswerUser;
            switch (DeleteMode)
            {
                case "1":
                    int number = 0;
                    bool flag = false;
                    do
                    {
                        Console.Write("\nВведите номер заметки, который следует удалить: ");
                        AnswerUser = Console.ReadLine();
                        Console.WriteLine();
                        flag = int.TryParse(AnswerUser, out number);

                    } while (!flag);
                    Notes.RemoveAll(e => e.NoteNumber == number);
                    break;
                case "2":
                    Console.Write("\nВведите дату и время, которые следует удалить в формате дд.мм.гггг чч:мм:сс: ");
                    AnswerUser = Console.ReadLine();
                    Console.WriteLine();
                    Notes.RemoveAll(e => e.RecordTime.ToString().Contains(AnswerUser) == true);
                    break;
                case "3":
                    Console.WriteLine("\nКакие записи необходимо удалить?\n1 - выполненные\n2 - невыполненные");
                    Console.Write("Ответ - ");
                    AnswerUser = Console.ReadKey().KeyChar.ToString() == "1" ? "Выполнено" : "Не выполнено";
                    Notes.RemoveAll(e => e.Status == AnswerUser);
                    break;
                case "4":
                    Console.Write("\nЗаписи из каких мест следует удалить?\nОтвет - ");
                    AnswerUser = Console.ReadLine();
                    Console.WriteLine();
                    Notes.RemoveAll(e => e.Place == AnswerUser);
                    break;
                case "5":
                    Console.Write("\nЗаметки с каким содержанием хотите удалить?\nОтвет - ");
                    AnswerUser = Console.ReadLine();
                    Console.WriteLine();
                    Notes.RemoveAll(e => e.Text == AnswerUser);
                    break;
                default: Console.WriteLine("\nНичего не было сделано, так как не был введен корректный ответ"); break;
            }
        }

        public void Print(List<Note> Notes)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{"№",-5} {"Время записи:",-20} {"Статус:",-14} {"Место создания:",-18} {"Содержание заметки:"}\n");
            Console.ResetColor();

            foreach (var note in Notes)
            {
                Console.WriteLine($"{note.NoteNumber,-5} {note.RecordTime,-20} {note.Status,-14} {note.Place,-18} {note.Text}");
            }
            Console.WriteLine();
        }

        public void Edit(List<Note> Notes)
        {
            int number = 0;
            bool flag = false;
            do
            {
                Console.Write("\nВведите номер заметки, которую следует редактировать: ");
                string AnswerUser = Console.ReadLine();
                Console.WriteLine();
                flag = int.TryParse(AnswerUser, out number);

            } while (!flag);

            bool Exist = false;

            foreach (var note in Notes)
            {
                if (note.NoteNumber == number)
                {
                    Exist = true;
                    break;
                }
            }

            if (Exist)
            {

                Console.Write("\nВведите текст заметки: ");
                string Text = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Введите место: ");
                string Place = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Сделана ли задача?\n1 - да  2 - нет\nОтвет - ");
                string Status = Console.ReadKey().KeyChar.ToString() == "1" ? "Выполнено" : "Не выполнено";
                Console.WriteLine();

                Notes.FindAll(e => e.NoteNumber == number).ForEach(x => { x.Text = Text; x.Place = Place; x.Status = Status; });
            }
            else Console.WriteLine("\nЗаметки под таким номером не существует\n");
        }

        public void Sort(List<Note> Notes, string SortMode = "1")
        {
            Console.Write("По какому столбцу хотите выполнить сортировку?" +
                "\n1 - номер заметки" +
                "\n2 - время записи" +
                "\n3 - статус" +
                "\n4 - место создания" +
                "\n5 - содержание" +
                "\nОтвет - ");
            SortMode = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("\n");
            switch (SortMode)
            {
                case "1":
                    Notes.Sort((a, b) => a.NoteNumber.CompareTo(b.NoteNumber));
                    break;
                case "2":
                    Notes.Sort((a, b) => a.RecordTime.CompareTo(b.RecordTime));
                    break;
                case "3":
                    Notes.Sort((a, b) => a.Status.CompareTo(b.Status));
                    break;
                case "4":
                    Notes.Sort((a, b) => a.Place.CompareTo(b.Place));
                    break;
                case "5":
                    Notes.Sort((a, b) => a.Text.CompareTo(b.Text));
                    break;
                default:
                    Console.WriteLine("Я бездельник ооооо мама мама.\n");
                    break;
            }

        }

        public void PrintToSpecialDate(List<Note> Notes)
        {
            Notes.Sort((a, b) => a.RecordTime.CompareTo(b.RecordTime));

            Console.WriteLine("\nВведите диапазон дат, который хотите увидеть в формате: дд.мм.гггг");
                Console.Write("Дата начала: ");
                string StartTime = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Дата окончания: ");
                string EndTime = Console.ReadLine();
                Console.WriteLine();

            List<Note> CutNotes = new List<Note>();

            int StartTimeIndex = Notes.FindIndex(e => e.RecordTime.ToString().Contains(StartTime));
            int EndTimeIndex = Notes.FindLastIndex(e => e.RecordTime.ToString().Contains(EndTime));

            if (StartTimeIndex == -1 && EndTimeIndex == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nТакой диапазон не найден, показаны все записи ежедневника\n");
                Console.ResetColor();
                Print(Notes);
            }
            else if (StartTimeIndex == -1 )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nДата начала не найдена в ежедневнике, поэтому показаны записи с начала\n");
                Console.ResetColor();
                CutNotes = Notes.GetRange(0, EndTimeIndex);
                Print(CutNotes);
            }
            else if (EndTimeIndex == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nДата окончания не найдена в ежедневнике, поэтому показаны записи до конца\n");
                Console.ResetColor();
                CutNotes = Notes.GetRange(StartTimeIndex, EndTimeIndex = Notes.Count - StartTimeIndex);
                Print(CutNotes);
            }
            else
            {
                CutNotes = Notes.GetRange(StartTimeIndex, EndTimeIndex - StartTimeIndex + 1);
                Print(CutNotes);
            }     
        }
    }
}
