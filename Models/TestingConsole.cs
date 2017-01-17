using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TestingConsole : IConsole
    {
        public Stack<string> OutputStack { get; set; }
        public Stack<string> InputStack { get; set; }
        public ConsoleKeyInfo ReadKey(bool intercept = false)
        {
            return new ConsoleKeyInfo(this.InputStack.Pop()[0], ConsoleKey.Backspace, false, false, false);
        }

        public void Write(string value)
        {
            this.OutputStack.Push(value);
        }

        public void WriteLine(string value)
        {
            this.OutputStack.Push(value);
        }

        public string ReadLine()
        {
            return this.InputStack.Pop();
        }
    }
}
