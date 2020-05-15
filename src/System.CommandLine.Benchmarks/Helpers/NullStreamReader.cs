// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.CommandLine.IO;

namespace System.CommandLine.Benchmarks.Helpers
{
    internal class NullStreamReader : IStandardStreamReader
    {
        public int Read()
        {
            // Only return end of stream
            return -1;
        }
    }
}
