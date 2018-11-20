using System;
using EventApp.Data;
using EventApp.Services;
using System.Collections.Generic;
using EventApp.Data.DTOS;

namespace EventApp.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Context = new EventAppContext();
            TestServices test = new TestServices();
            foreach (var y in test.Query12())
            {
                Console.WriteLine(y.Email);
            }

        }
    }
}
