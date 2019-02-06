using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using MMLib.RapidPrototyping.Generators;

namespace LoremIpsum
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .MapResult(RunService, ThrowArgumentException);
        }
        private static async Task RunService(Options args)
        {            
            Console.WriteLine(Generate(args));
        }
        private static Task ThrowArgumentException(IEnumerable<Error> arg)
        {
            Environment.Exit((int)SystemErrorCode.ERROR_INVALID_PARAMETER);
            return null;
        }
        public static string Generate(Options args)
        {
            WordGenerator generator = new WordGenerator();
            LoremIpsumGenerator loremIpsumGenerator = new LoremIpsumGenerator();
            return loremIpsumGenerator.Next(args.ParagraphCount, args.MaxSentenceInParagraph);
        } 
    }

    class Options
    {
        [Option('p', "paragraph-count", Required=false, Default=1, HelpText="Number of paragraphs to generate")]
        public int ParagraphCount { get; set; }
        [Option('s', "max-sentence-count", Required=false, Default=4, HelpText="Maximum number of sentence in each paragraph")]
        public int MaxSentenceInParagraph { get; set; }
    }

    internal enum SystemErrorCode
    {
        ERROR_INVALID_PARAMETER = 87
    }
}
