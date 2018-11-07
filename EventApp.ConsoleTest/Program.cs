using System;
using EventApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EventApp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EventAppContext();
            foreach (var ev in context.Events.Include(ev => ev.Location))
            {
                Console.WriteLine($"{ev.Name} - {ev.Location.Name}");
            }
        }
    }
}
