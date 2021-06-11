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
        public void Add(List<Note> notes)
        {
            DateTime time = new DateTime();

            time = DateTime.Now;

            int id;
            if (notes.Count != 0)
                id = notes.Max(e => e.NoteNumber) + 1;
            else id = 1;


            Console.Write("\nВведите текст заметки: ");
            string text = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Введите место: ");
            string place = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Сделана ли задача?\n1 - да  2 - нет\nОтвет - ");
            string status = Console.ReadKey().KeyChar.ToString() == "1" ? "Выполнено" : "Не выполнено";
            Console.WriteLine();

            notes.Add(new Note(id, time, text, place, status));
            Console.WriteLine();
        }
        public void RemoveNote(List<Note> notes)
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

            string answerUser;

            switch (DeleteMode)
            {
                case "1":
                    int noteNumber = 0;
                    bool flag = false;
                    do
                    {
                        Console.Write("\nВведите номер заметки, который следует удалить: ");
                        answerUser = Console.ReadLine();
                        Console.WriteLine();
                        flag = int.TryParse(answerUser, out noteNumber);
                    } while (!flag);
                    notes.RemoveAll(e => e.NoteNumber == noteNumber);
                    break;
                case "2":
                    Console.Write("\nВведите дату и время, которые следует удалить в формате дд.мм.гггг чч:мм:сс: ");
                    answerUser = Console.ReadLine();
                    Console.WriteLine();
                    notes.RemoveAll(e => e.RecordTime.ToString().Contains(answerUser) == true);
                    break;
                case "3":
                    Console.WriteLine("\nКакие записи необходимо удалить?\n1 - выполненные\n2 - невыполненные");
                    Console.Write("Ответ - ");
                    answerUser = Console.ReadKey().KeyChar.ToString() == "1" ? "Выполнено" : "Не выполнено";
                    notes.RemoveAll(e => e.Status == answerUser);
                    break;
                case "4":
                    Console.Write("\nЗаписи из каких мест следует удалить?\nОтвет - ");
                    answerUser = Console.ReadLine();
                    Console.WriteLine();
                    notes.RemoveAll(e => e.Place == answerUser);
                    break;
                case "5":
                    Console.Write("\nЗаметки с каким содержанием хотите удалить?\nОтвет - ");
                    answerUser = Console.ReadLine();
                    Console.WriteLine();
                    notes.RemoveAll(e => e.Text == answerUser);
                    break;
                default:
                    Console.WriteLine("\nНичего не было сделано, так как не был введен корректный ответ");
                    break;
            }
        }
        public void Print(List<Note> notes)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{"№",-5} {"Время записи:",-20} {"Статус:",-14} {"Место создания:",-18} {"Содержание заметки:"}\n");
            Console.ResetColor();

            foreach (var note in notes)
            {
                Console.WriteLine($"{note.NoteNumber,-5} {note.RecordTime,-20} {note.Status,-14} {note.Place,-18} {note.Text}");
            }
            Console.WriteLine();
        }
        public void Edit(List<Note> notes)
        {
            int noteNumber = 0;
            bool flag = false;
            do
            {
                Console.Write("\nВведите номер заметки, которую следует редактировать: ");
                string answerUser = Console.ReadLine();
                Console.WriteLine();
                flag = int.TryParse(answerUser, out noteNumber);

            } while (!flag);

            bool noteExistInList = false;

            foreach (var note in notes)
            {
                if (note.NoteNumber == noteNumber)
                {
                    noteExistInList = true;
                    break;
                }
            }
            if (noteExistInList)
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

                notes.FindAll(e => e.NoteNumber == noteNumber).ForEach(x => { x.Text = Text; x.Place = Place; x.Status = Status; });
            }
            else Console.WriteLine("\nЗаметки под таким номером не существует\n");
        }
        public void Sort(List<Note> notes, string sortMode = "1")
        {
            Console.Write("По какому столбцу хотите выполнить сортировку?" +
                "\n1 - номер заметки" +
                "\n2 - время записи" +
                "\n3 - статус" +
                "\n4 - место создания" +
                "\n5 - содержание" +
                "\nОтвет - ");
            sortMode = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("\n");

            switch (sortMode)
            {
                case "1":
                    notes.Sort((a, b) => a.NoteNumber.CompareTo(b.NoteNumber));
                    break;
                case "2":
                    notes.Sort((a, b) => a.RecordTime.CompareTo(b.RecordTime));
                    break;
                case "3":
                    notes.Sort((a, b) => a.Status.CompareTo(b.Status));
                    break;
                case "4":
                    notes.Sort((a, b) => a.Place.CompareTo(b.Place));
                    break;
                case "5":
                    notes.Sort((a, b) => a.Text.CompareTo(b.Text));
                    break;
                default:
                    Console.WriteLine("Я бездельник ооооо мама-мама.\n");
                    break;
            }
        }
        public void PrintToSpecialDate(List<Note> notes)
        {
            notes.Sort((a, b) => a.RecordTime.CompareTo(b.RecordTime));

            Console.WriteLine("\nВведите диапазон дат, который хотите увидеть в формате: дд.мм.гггг");

            Console.Write("Дата начала: ");
            string startTime = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Дата окончания: ");
            string endTime = Console.ReadLine();
            Console.WriteLine();

            List<Note> cutNotes = new List<Note>();

            int startTimeIndex = notes.FindIndex(e => e.RecordTime.ToString().Contains(startTime));
            int endTimeIndex = notes.FindLastIndex(e => e.RecordTime.ToString().Contains(endTime));

            if (startTimeIndex == -1 && endTimeIndex == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nТакой диапазон не найден, показаны все записи ежедневника\n");
                Console.ResetColor();
                Print(notes);
            }
            else if (startTimeIndex == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nДата начала не найдена в ежедневнике, поэтому показаны записи с начала\n");
                Console.ResetColor();
                cutNotes = notes.GetRange(0, endTimeIndex);
                Print(cutNotes);
            }
            else if (endTimeIndex == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nДата окончания не найдена в ежедневнике, поэтому показаны записи до конца\n");
                Console.ResetColor();
                cutNotes = notes.GetRange(startTimeIndex, endTimeIndex = notes.Count - startTimeIndex);
                Print(cutNotes);
            }
            else
            {
                cutNotes = notes.GetRange(startTimeIndex, endTimeIndex - startTimeIndex + 1);
                Print(cutNotes);
            }
        }
    }
}
