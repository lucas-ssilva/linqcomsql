using System;
using likar.Entities;
using System.Linq;
using System.Collections.Generic;

namespace likar
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
        }

        static void Main(string[] args)
        {
            Category cat1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category cat2 = new Category() { Id = 2, Name = "IT", Tier = 1 };
            Category cat3 = new Category() { Id = 3, Name = "Games", Tier = 3 };

            List<Product> products = new List<Product>() // data source 
            {
                new Product() {Id = 2,Name = "Computer",Price = 1500.00,Category = cat2},
                new Product() {Id = 1,Name = "Toy",Price = 110.00,Category = cat1},
                new Product() {Id = 3,Name = "Persona 5",Price = 40.00,Category = cat3},
                new Product() {Id = 4,Name = "Hammer",Price = 90.00,Category = cat1},
                new Product() {Id = 5,Name = "TV",Price = 1700.00,Category = cat3},
                new Product() {Id = 6,Name = "Notebook",Price = 1300.00,Category = cat2},
                new Product() {Id = 7,Name = "Saw",Price = 80.00,Category = cat1},
                new Product() {Id = 8,Name = "Tablet",Price = 700.00,Category = cat2},
                new Product() {Id = 9,Name = "Camera",Price = 700.00,Category = cat2},
                new Product() {Id = 10,Name = "Printer",Price = 350.00,Category = cat2},
                new Product() {Id = 11,Name = "Macbook",Price = 1800.00,Category = cat2},
                new Product() {Id = 12,Name = "SoundBar",Price = 700.00,Category = cat3},
            };

            //var query = products.Where(x => x.Category.Tier == 1 && x.Price < 900.00); // logica da consulta 
            var r1 = from p in products // alternativa a lambda de cima 
                     where p.Category.Tier == 1 && p.Price < 900.0
                     select p;

            /*foreach (Product p in query) // executar a consulta
            {
                Console.WriteLine(p);
            }*/

            // metodo alternativo usando função auxiliar para imprimir 
            Print("Tier 1 e valor até 900.00", r1);

            Console.WriteLine();
            //var query2 = products.Where(x => x.Category.Name == "Tools").Select(x => x.Name);
            var r2 = from p in products where p.Category.Name == "Tools" select p.Name;

            Print("name of the products from tools", r2);

            Console.WriteLine();
            //var query3 = products.Where(x => x.Name.StartsWith('C') || x.Name.StartsWith('c')).Select(x => new { x.Name, CategoryName = x.Category.Name, x.Price });
            var r3 = from p in products where p.Name.StartsWith('C') select new { p.Name, CategoryName = p.Category.Name, p.Price };
            Print("Começa com C e objeto anonimo ", r3);

            Console.WriteLine();
            //var query4 = products.Where(x => x.Category.Tier == 1).OrderBy(x => x.Price).ThenBy(x => x.Name); // ordena por preço e em caso de empate ordena por nome 
            var r4 = from p in products where p.Category.Tier == 1 orderby p.Name orderby p.Price select p;
            Print("ordenar tier 1, por preço e por nome para desempate", r4);

            Console.WriteLine();
            // var query5 = r4.Skip(2).Take(4);
            var r5 = (from p in r4 select p).Skip(2).Take(4);
            Print("ordenar tier 1, por preço e por nome para desempate pula 2 pega 4 ", r5); // skip = pula os 2 primeiros, take = pega os proximos ... numeros 

            Console.WriteLine();
            //var r16 = products.GroupBy(p => p.Category); //seleciona uma categoria ou grupo, precisa de forech especial, no exemplo ele traz todas as categorias de produtos e depois com o segundo forech ele filtra nos produros dentro das categorias 
            var r16 = from p in products group p by p.Category;
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine(group.Key.Name + ": ");
                foreach (Product p in group)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
