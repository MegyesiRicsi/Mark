using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cbradio
{
    class feladat
    {
        public int ora;
        public int perc;
        public int hivas;
        public string nev;

        public feladat(string egysor)
        {
            var darabok = egysor.Split(';');
            ora = int.Parse(darabok[0]);
            perc = int.Parse(darabok[1]);
            hivas = int.Parse(darabok[2]);
            nev = darabok[3];


        }

    }

    class Program
    {
        static int AtszamolPercre(int ora, int perc)
        {
            return perc + ora * 60;
        }
        static void Main(string[] args)
        {
            var sr = new StreamReader("cb.txt");
            var lista = new List<feladat>();
            string elsosor = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                lista.Add(new feladat(sr.ReadLine()));
            }
            sr.Close();
            Console.WriteLine($"3. feladat: bejegyzések száma: {lista.Count} db");

            bool asd = false;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].hivas == 4)
                {
                    asd = true;
                    break;
                }
            }
            if (asd)
            {
                Console.WriteLine($"4. feladat: Volt négy adást indító sofőr.");
            }
            else
            {
                Console.WriteLine($"4. feladat: Nem volt négy adást indító sofőr.");
            }
            Console.Write("5. feladat: Kérek egy nevet: ");
            string ember = Console.ReadLine();
            bool asdasd = false;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].nev == ember)
                {
                    asdasd = true;
                }
            }

            if (asdasd)
            {
                var hasznalt = (
                from sor in lista
                where sor.nev == ember
                select sor.hivas
                ).Sum();
                Console.WriteLine($"\t{ember} {hasznalt}x használta a CB-rádiót");
            }
            else
            {
                Console.WriteLine("\tNincs ilyen nevű sofőr!");
            }
            var sw = new StreamWriter("cb2.txt");
            string fejlec = "Kezdes;nev;AdasDb";
            sw.WriteLine(fejlec);
            foreach (var item in lista)
            {
                sw.WriteLine(AtszamolPercre(item.ora, item.perc) + ";" + item.nev + ";" + item.hivas);
            }
            sw.Close();
            var osszes = (
                from sor in lista
                group sor by sor.nev
                );
            Console.WriteLine($"8. feladat: soförők száma: { osszes.Count()} fő");
            //var stat = new SortedDictionary<string, int>();
            //foreach (var item in lista)
            //{
            //    if (stat.Keys.Contains(item.nev))
            //    {
            //        stat[item.nev] += item.hivas;
            //    }
            //    else
            //    {
            //        stat[item.nev] = item.hivas;
            //    }
            //}
            //foreach (var item in stat.Reverse())
            //{
            //    Console.WriteLine(item.Key +"   "+item.Value);
            //}

            var asdf = (
                from sor in lista
                select sor.nev).Distinct();
            string maxn = "";
            int maxh = 0;
            foreach (var item in asdf)
            {
                int temp = (
                    from sor in lista
                    where sor.nev == item
                    select sor.hivas
                    ).Sum();// lista.Where(c => c.nev == item).Select(b => b.hivas).Sum();
                if (temp > maxh)
                {
                    maxh = temp;
                    maxn = item;
                }
            }
            Console.WriteLine($"{maxn}, {maxh}");

            Console.ReadLine();
        }
    }
}
