/* Copyright (C) since 2009 UBIC, Inc. All Rights Reserved */

using System;
using System.IO;
using System.Linq;

namespace ByteOrderMarkRemover
{
    class Program
    {
        private static readonly byte[] BOM = new byte[] { 0xEF, 0xBB, 0xBF };

        static void Main(string[] args)
        {
            using (Stream input = Console.OpenStandardInput())
            using (Stream output = Console.OpenStandardOutput())
            {
                byte[] buf = new byte[BOM.Length];
                input.Read(buf, 0, buf.Length);

                if (!buf.SequenceEqual(BOM))
                {
                    output.Write(buf, 0, buf.Length);
                }

                input.CopyTo(output);
            }
        }
    }
}
