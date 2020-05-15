// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.CommandLine.IO
{
    internal static class ConsoleExtensions
    {
        internal static void SetTerminalForegroundRed(this IConsole console)
        {
            if (console.GetType().GetInterfaces().Any(i => i.Name == "ITerminal"))
            {
                ((dynamic)console).ForegroundColor = ConsoleColor.Red;
            }

            if (Platform.IsConsoleRedirectionCheckSupported && 
                !Console.IsOutputRedirected)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (Platform.IsConsoleRedirectionCheckSupported)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
        }

        internal static void ResetTerminalForegroundColor(this IConsole console)
        {
            if (console.GetType().GetInterfaces().Any(i => i.Name == "ITerminal"))
            {
                ((dynamic)console).ForegroundColor = ConsoleColor.Red;
            }

            if (Platform.IsConsoleRedirectionCheckSupported && 
                !Console.IsOutputRedirected)
            {
                Console.ResetColor();
            }
            else if (Platform.IsConsoleRedirectionCheckSupported)
            {
                Console.ResetColor();
            }
        }

        internal static string? ReadLine(this IConsole console, bool echo, bool mask)
        {
            var lineCharacters = new List<char>();
            var previousIndex = -1;
            var notDone = true;

            while (notDone)
            {
                var input = console.In.Read();

                if (input == -1)
                {
                    // If we reach the end of the stream, return null
                    return null;
                }

                var inputCharacter = Convert.ToChar(input);

                switch (inputCharacter)
                {
                    // We're done once we get a new line character
                    case '\n':
                    case '\r':
                        notDone = false;
                        break;

                    // Pop an input chararacter if we get a backspace character
                    case '\b':
                        if (previousIndex < 0) continue;

                        lineCharacters.RemoveAt(previousIndex--);

                        if (echo)
                        {
                            console.Out.Write("\b \b");
                        }
                        break;

                    // The input character is a line character for all other characters (...?)
                    default:
                        lineCharacters.Insert(++previousIndex, inputCharacter);

                        if (echo)
                        {
                            console.Out.Write(mask ? "*" : inputCharacter.ToString());
                        }
                        break;
                }
            }

            var lineBuilder = new StringBuilder();

            foreach (var lineCharacter in lineCharacters)
            {
                lineBuilder.Append(lineCharacter);
            }

            return new string(lineCharacters.ToArray());
        }
    }
}
