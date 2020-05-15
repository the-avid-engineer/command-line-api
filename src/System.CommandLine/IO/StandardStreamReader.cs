// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace System.CommandLine.IO
{
    public static class StandardStreamReader
    {
        public static IStandardStreamReader Create(TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return new AnonymousStandardStreamReader(reader.Read);
        }

        private class AnonymousStandardStreamReader : IStandardStreamReader
        {
            private readonly Func<int> _read;

            public AnonymousStandardStreamReader(Func<int> read)
            {
                _read = read;
            }

            public int Read()
            {
                return _read();
            }
        }
    }
}
