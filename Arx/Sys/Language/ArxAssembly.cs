using System;
using System.Collections.Generic;
using System.Threading;

namespace Arx.Sys.Language
{
    public class ArxAssembly
    {
        private struct Label
        {
            public string labelName;
            public int line;
        }

        private Stack<int> stack;
        private List<Label> labels;
        private int? poppedData = null!;

        public ArxAssembly()
        {
            stack = new Stack<int>();
            labels = new List<Label>();
        }

        public void Interpret(string[] instruction)
        {
            try
            {
                for (int i = 0; i < instruction.Length; i++)
                {
                    if (string.IsNullOrEmpty(instruction[i])) { continue; }
                    else if (instruction[i].Split(' ')[0].ToLower() == "halt")
                    {
                        Thread.Sleep(-100);
                    }
                    else if (instruction[i].Split(' ')[0].ToLower() == "push")
                    {
                        stack.Push(int.Parse(instruction[i].Split(' ')[1]));
                    }
                    else if (instruction[i].Split(' ')[0].ToLower() == "pop")
                    {
                        if(poppedData == null!)
                        {
                            poppedData = stack.Pop();
                        }
                        else
                        {
                            Stdio.Write.Println("Instruction Error: Popped data has not been cleared!");
                            break;
                        }
                    }
                    else if (instruction[i].Split(' ')[0].ToLower() == "clear")
                    {
                        poppedData = null!;
                    }
                    else if (instruction[i].Split(' ')[0].ToLower() == "jump")
                    {
                        foreach(var e in labels)
                        {
                            if(e.labelName == instruction[i].Split(' ')[1])
                            {
                                i = e.line;
                            }
                        }
                    }
                    else if (instruction[i].Split(' ')[0].ToLower() == "print")
                    {
                        Stdio.Write.Println(poppedData.ToString());
                    }
                    else if (instruction[i].Split(' ')[0].ToLower() == "ascp")
                    {
                        Stdio.Write.Println($"{(char)poppedData}");
                    }
                    else if (instruction[i].EndsWith(':'))
                    {
                        labels.Add(new Label()
                        {
                            line = i,
                            labelName = instruction[i].Replace(":", ""),
                        });
                    }   
                }

                stack = new Stack<int>();
                poppedData = null!;
            }
            catch (Exception ex)
            {
                Stdio.Write.Println("Exception: " + ex.ToString(), ConsoleColor.Red);
            }
        }
    }
}
