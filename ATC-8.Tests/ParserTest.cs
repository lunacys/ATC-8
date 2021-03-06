﻿using System;
using ATC8.VirtualMachine;
using ATC8.VirtualMachine.Lexer.Tokens;
using NUnit.Framework;

namespace ATC_8.Tests
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void Parser_ParseFileTest()
        {
            Parser parser = new Parser();
            var bytecode = parser.ParseFile("ParserTest.txt");

            string testOutput = "4 4 98 97 110 107 2 2 4 3 111 114 103 2 0 4 6 105 110 99 " +
                                "98 105 110 3 8 109 97 105 110 46 115 112 114 4 4 98 97 110 107 2 " +
                                "0 4 3 111 114 103 2 128 0 0 1 5 109 121 115 112 114 5 44 5 " +
                                "91 2 0 5 93 0 0 1 3 98 107 103 5 44 2 255 0 0 1 1 " +
                                "120 5 44 2 0 0 0 1 1 121 5 44 2 0 0 0 1 7 112 114 " +
                                "111 99 101 101 100 5 44 2 1 0 0 1 5 114 101 115 101 116 5 44 " +
                                "2 0 0 1 1 7 112 114 111 99 101 101 100 5 44 8 0 0 1 1 " +
                                "5 114 101 115 101 116 5 44 8 1 6 5 98 101 103 105 110 0 54 1 " +
                                "2 107 112 5 44 1 3 101 110 100 4 4 109 111 118 101 1 5 109 121 " +
                                "115 112 114 5 44 1 1 120 5 44 1 1 121 0 48 1 7 100 114 97 " +
                                "119 98 107 103 4 4 100 114 97 119 1 5 109 121 115 112 114 5 44 1 " +
                                "1 120 5 44 1 1 121 0 48 1 5 98 101 103 105 110 6 7 100 114 " +
                                "97 119 98 107 103 4 4 100 114 97 119 0 54 1 7 100 114 97 119 98 " +
                                "107 103 6 3 101 110 100 4 4 100 98 117 103 3 7 101 120 105 116 105 " +
                                "110 103 ";

            string output = "";
            foreach (var word in bytecode)
            {
                output += word;
                output += " ";
            }

            Assert.That(output.Equals(testOutput));
        }
    }
}