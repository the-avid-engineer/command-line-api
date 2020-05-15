// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.CommandLine.IO
{
    public class TestConsole : IConsole
    {
        public TestConsole(string inputStream = "")
        {
            Out = new StandardStreamWriter();
            Error = new StandardStreamWriter();
            In = new StandardStreamReader(inputStream);
        }

        public IStandardStreamWriter Error { get; protected set; }

        public IStandardStreamWriter Out { get; protected set; }

        public IStandardStreamReader In { get; protected set; }

        public bool IsOutputRedirected { get; protected set; }

        public bool IsErrorRedirected { get; protected set; }

        public bool IsInputRedirected { get; protected set; }

        internal class StandardStreamWriter : TextWriter, IStandardStreamWriter
        {
            private readonly StringBuilder _stringBuilder = new StringBuilder();

            public override void Write(char value)
            {
                _stringBuilder.Append(value);
            }

            public override Encoding Encoding { get; } = Encoding.Unicode;

            public override string ToString() => _stringBuilder.ToString();
        }

        internal class StandardStreamReader : TextReader, IStandardStreamReader
        {
            private readonly Stack<char> _characterStream;

            public StandardStreamReader(string inputStream)
            {
                _characterStream = new Stack<char>(inputStream.ToCharArray().Reverse());
            }

            public override int Read()
            {
                if (_characterStream.Count == 0)
                {
                    return -1;
                }

                return _characterStream.Pop();
            }
        }
    }
}
