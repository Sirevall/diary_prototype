using System;

namespace Diary_prototype
{
    public struct Note
    {
        /// <summary>
        /// Порядковый номер заметки
        /// </summary>
        public int NoteNumber { get; set; }
        /// <summary>
        /// Время создания заметки
        /// </summary>
        public DateTime RecordTime { get; set; }
        /// <summary>
        /// Содержание заметки
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Местоположение при создании заметки
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// Выполнена задача или нет
        /// </summary>
        public string Status { get; set; }

        public Note(int noteNumber, DateTime recordTime, string text, string place, string status)
        {
            (NoteNumber, RecordTime, Text, Place, Status) = (noteNumber, recordTime, text, place, status);
        }
    }
}
