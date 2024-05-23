using Arx.Sys.Stdio;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Arx.Sys.Language
{
    public class ArxAssembly
    {
        byte[] stack = null!;
        int ptr = 0;

        public void interpretCode(string[] code)
        {
            int line = 1;

            foreach(var i in code)
            {
                string[] splitString = i.Split(' ');

                if (splitString[0] == "initstack")
                {
                    try
                    {
                        stack = new byte[int.Parse(splitString[1])];
                    }
                    catch
                    {
                        throwError(line, "cannot init stack");
                    }
                }
                else if (splitString[0] == "ptr")
                {
                    try
                    {
                        if(splitString.Length != 3)
                        {
                            if (splitString[1] == "next")
                            {
                                if (ptr + 1 > stack.Length)
                                {
                                    throwError(line, "pointer out of bound");
                                }
                            }
                            else if (splitString[1] == "prev")
                            {
                                if (ptr - 1 < 0)
                                {
                                    throwError(line, "pointer out of bound");
                                }
                            }
                        }
                        else if (splitString.Length == 3)
                        {
                            if (splitString[1] == "next")
                            {
                                if (ptr + int.Parse(splitString[2]) > stack.Length)
                                {
                                    throwError(line, "pointer out of bound");
                                }
                            }
                            else if (splitString[1] == "prev")
                            {
                                if (ptr - int.Parse(splitString[2]) < 0)
                                {
                                    throwError(line, "pointer out of bound");
                                }
                            }
                        }
                    }
                    catch
                    {
                        throwError(line, "exception occured");
                    }
                }

                line++;
            }
        }

        private void throwError(int line, string reason)
        {
            Write.Println("ERROR OCCURED!", ConsoleColor.Red);
            Write.Println($"Line {line}: {reason}\n", ConsoleColor.Red);
        }
    }
}
