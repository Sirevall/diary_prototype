using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary_prototype
{
    public interface IFile
    {
        List<Note> ReadFile(List<Note> notes);

        void WriteFile(List<Note> notes);
    }
}
