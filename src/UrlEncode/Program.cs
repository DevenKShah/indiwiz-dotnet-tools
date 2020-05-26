using CommandLine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UrlEncode
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .MapResult(RunService, ThrowArgumentException);
        }
        private static Task RunService(Options args)
        {
            Console.WriteLine(Generate(args));
            return Task.CompletedTask;
        }

        private static Task ThrowArgumentException(IEnumerable<Error> arg)
        {
            Environment.Exit((int)SystemErrorCode.ERROR_INVALID_PARAMETER);
            return null;
        }

        public static string Generate(Options args)
        {
            return WebUtility.UrlEncode(args.Url.ToString());
        }
    }

    class Options
    {
        [Option('u', "url", Required = true, HelpText = "Url to encode")]
        public Uri Url { get; set; }
    }

    internal enum SystemErrorCode
    {
        ERROR_INVALID_PARAMETER = 87
    }
}
