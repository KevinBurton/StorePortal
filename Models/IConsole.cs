using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IConsole
    {
        ConsoleKeyInfo ReadKey(bool intercept = false);
        void Write(string value);
        void WriteLine(string value);
        string ReadLine();
    }
}
