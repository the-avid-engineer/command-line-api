// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine.IO;
using System.IO;
using System.Linq;

namespace System.CommandLine.Rendering
{
    internal class RecordingReader : TextReader, IStandardStreamReader
    {
        private readonly Stack<char> _characterStream;

        public RecordingReader(string inputStream)
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
