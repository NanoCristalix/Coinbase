using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using NetTopologySuite.Index.Bintree;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Coinbase
{

    class Program
    {
        static IWebDriver Driver; //chrome driver
        Thread Pari;
        IWebElement test;
        IWebElement element;
        int count = 0;
        float element2 = float.MaxValue;
        float element1 = float.MaxValue;
        string ime_na_najgolem = "";
        string ime_na_vtora_najmala = "";
        string ime_na_posledna_valuta = "";
        bool check = true;
        IDictionary<string, float> valuti_procenti = new Dictionary<string, float>();
        IDictionary<string, float> valuti_procenti_sortirani = new Dictionary<string, float>();
        public void Check()
        {
            // Driver.Navigate().GoToUrl("https://www.coinbase.com/trade");
            Driver.Navigate().GoToUrl("https://www.coinbase.com/price");
            Thread.Sleep(60000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            IList<IWebElement> coini = Driver.FindElements(By.CssSelector("[data-element-handle*='asset-table-row']"));
            String[] ime = new String[coini.Count];
            String[] procent = new String[coini.Count];
            int j = 0;
            foreach (IWebElement sddd in coini)
            {
                ime[j++] = sddd.Text.ToString();
            }
            valuti_procenti.Clear();
            valuti_procenti_sortirani.Clear();
            for (int v = 0; v < 42; v++)
            {
                try
                {
                    string[] lines = ime[v].Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                   // string procenti = lines[3];
                    string procenti = lines[4];

                    float procenti_bez_procent = float.Parse(procenti.Remove(procenti.Length - 1, 1));
                    //string valuta = lines[0];
                    string valuta = lines[1];
                    valuti_procenti.Add(valuta.ToLower().Replace(" ", "-"), procenti_bez_procent);
                    
                }
                catch
                {
                    continue;
                }

            }
            
             
           
            
            foreach (KeyValuePair<string, float> author in valuti_procenti.OrderByDescending(key => key.Value))
            {
                valuti_procenti_sortirani.Add(author.Key, author.Value);
                //Console.WriteLine("Valuta: {0}, Procent: {1}", author.Key, author.Value);
            }

            foreach (KeyValuePair<string, float> author in valuti_procenti_sortirani.OrderByDescending(key => key.Value))
            {
                
                Console.WriteLine("Valuta: {0}, Procent: {1}", author.Key, author.Value);
            }

            ime_na_najgolem = valuti_procenti_sortirani.Keys.First().ToLower().Replace(" ", "-");
            ime_na_vtora_najmala = valuti_procenti_sortirani.Keys.Skip(1).First().ToLower().Replace(" ", "-");

            Console.WriteLine("Ime na prva najgolema Valuta:" + valuti_procenti_sortirani.Keys.First().ToLower().Replace(" ", "-"));
            Console.WriteLine("Najmal procent: " + valuti_procenti_sortirani.Values.First());
            Console.WriteLine("Ime na vtora najgolema Valuta:" + valuti_procenti_sortirani.Keys.Skip(1).First().ToLower().Replace(" ", "-"));
            Console.WriteLine("Procent na vtora najgolema: " + valuti_procenti_sortirani.Values.Skip(1).First());
            
            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/nav[1]/div[3]/div/div[1]/button"));
                
            }
            catch
            {
                Console.WriteLine("Nibiva");
            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/nav[1]/div[3]/div/div[1]/button"));
                
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/nav[1]/div[3]/div/div[1]/button/span"));
                
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/nav[1]/div[3]/div/div[1]"));
                
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            test.Click();


            ime_na_najgolem = ime_na_najgolem.ToLower().Replace(" ", "-");
            ime_na_vtora_najmala = ime_na_vtora_najmala.ToLower().Replace(" ", "-");
            Thread.Sleep(5000);
            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[8]/div/div/div/div/div[1]/div[1]/div[3]"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }
            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div/div[3]/div/div/div/div/div[1]/div[3]"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }
            try
            {
                test = Driver.FindElement(By.CssSelector("[data-element-handle*='folder-tab-convert']"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }
            
            test.Click();
            Thread.Sleep(5000);
            if (check)
            {
                string costum = "uniswap";

                try
                {
                    test = Driver.FindElement(By.XPath("/html/body/div[8]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/div[1]/p"));
                }
                catch
                {
                    Console.WriteLine("Nebiva");
                }

                try
                {
                    test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/div[1]/p"));
                }
                catch
                {
                    Console.WriteLine("Nebiva");
                }
                try
                {
                    
                    test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-from-selector']"));
                }
                catch
                {
                    Console.WriteLine("Nebiva");
                }
                
                test.Click();
                Thread.Sleep(5000);
                test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-from-select-" + costum + "']"));
                test.Click();
                if (ime_na_najgolem.Equals("yearn.finance"))
                {
                    ime_na_najgolem = ime_na_najgolem.ToLower().Replace(".", "-");
                }


                if (ime_na_najgolem.Equals("stellar-lumens"))
                {
                    ime_na_najgolem = "stellar";
                }

                if (ime_na_vtora_najmala.Equals("yearn.finance"))
                {
                    ime_na_vtora_najmala = ime_na_vtora_najmala.ToLower().Replace(".", "-");
                }


                if (ime_na_vtora_najmala.Equals("stellar-lumens"))
                {
                    ime_na_vtora_najmala = "stellar";
                }

                if (costum.Equals(ime_na_najgolem.ToLower().Replace(" ", "-")))
                {
                    ime_na_najgolem = ime_na_vtora_najmala.ToLower().Replace(" ", "-");
                    ime_na_posledna_valuta = ime_na_vtora_najmala.ToLower().Replace(" ", "-");
                }
                else
                {
                    ime_na_posledna_valuta = ime_na_najgolem.ToLower().Replace(" ", "-");
                }
                Thread.Sleep(2000);
            }
            else
            {
                try
                {
                    test = Driver.FindElement(By.XPath("/html/body/div[8]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/div[1]/p"));
                }
                catch
                {
                    Console.WriteLine("Nebiva");
                }

                try
                {
                    test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[1]/div[1]/p"));
                }
                catch
                {
                    Console.WriteLine("Nebiva");
                }
                try
                {

                    test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-from-selector']"));
                }
                catch
                {
                    Console.WriteLine("Nebiva");
                }

                test.Click();
                Thread.Sleep(5000);
                test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-from-select-" + ime_na_posledna_valuta.ToLower().Replace(" ", "-") + "']"));
                test.Click();
                Thread.Sleep(5000);
                if (ime_na_najgolem.Equals("yearn.finance"))
                {
                    ime_na_najgolem = ime_na_najgolem.ToLower().Replace(".", "-");
                }


                if (ime_na_najgolem.Equals("stellar-lumens"))
                {
                    ime_na_najgolem = "stellar";
                }

                if (ime_na_vtora_najmala.Equals("yearn.finance"))
                {
                    ime_na_vtora_najmala = ime_na_vtora_najmala.ToLower().Replace(".", "-");
                }


                if (ime_na_vtora_najmala.Equals("stellar-lumens"))
                {
                    ime_na_vtora_najmala = "stellar";
                }


                if (ime_na_posledna_valuta.ToLower().Replace(" ", "-").Equals(ime_na_najgolem.ToLower().Replace(" ", "-")))
                {
                    ime_na_najgolem = ime_na_vtora_najmala.ToLower().Replace(" ", "-");
                    ime_na_posledna_valuta = ime_na_vtora_najmala.ToLower().Replace(" ", "-");
                }
                else
                {
                    ime_na_posledna_valuta = ime_na_najgolem.ToLower().Replace(" ", "-");
                }

            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[8]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[2]/div[1]/p"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[2]/div[1]/p"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[2]/div[1]"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-to-selector']"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.CssSelector("/html/body/div[6]/div/div/div/div[3]/div/div/div/div/div[2]/div[2]/div/div[2]"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            test.Click();
            Thread.Sleep(5000);
            test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-to-select-"+ime_na_najgolem.ToLower().Replace(" ", "-")+"']"));
            test.Click();
            Thread.Sleep(5000);
            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[8]/div/div/div/div[3]/div/div/div/div/div[2]/div[1]/div[2]/button/span"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }
            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div/div[3]/div/div/div/div/div[2]/div[1]/div[2]/button/span"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }

            try
            {
                test = Driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div[3]/div/div/div/div/div[2]/div[1]/div[2]/button"));
            }
            catch
            {
                Console.WriteLine("Nebiva");
            }
            test.Click();
            Thread.Sleep(3000);
            test = Driver.FindElement(By.CssSelector("[data-element-handle*='convert-confirm-button']"));
            test.Click();
            check = false;     


        }


        public void Coin()
        {
            //Driver = new FirefoxDriver();
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://www.coinbase.com/accounts");
            String cena="";
            Thread.Sleep(60000);
            try
            {
                element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
            }
            catch
            {
                Console.WriteLine("Nibuva");
            }
            try
            {
                element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span/h1"));
            }
            catch
            {
                Console.WriteLine("Nibuva");
            }
            try
            {
                element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span"));
            }
            catch
            {
                Console.WriteLine("Nibuva");
            }
            try
            {
                element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
            }
            catch
            {
                Console.WriteLine("Nibuva");
            }
            cena = element.Text;
            String novacena = cena.Substring(1, cena.Length - 1);
            float cena_decimal = float.Parse(novacena);
            float profit = cena_decimal;
            float ne_profit = cena_decimal-cena_decimal/100*3;
            Console.WriteLine("Prva cena: " + cena_decimal);

            for (int i = 0; i < 999999999; i++){

                try
                {
                    element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                }
                catch
                {
                    Console.WriteLine("");
                }
                try
                {
                    element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span/h1"));
                }
                catch
                {
                    Console.WriteLine("");
                }
                try
                {
                    element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span"));
                }
                catch
                {
                    Console.WriteLine("");
                }
                try
                {
                    element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                }
                catch
                {
                    Console.WriteLine("");
                }
                cena = element.Text;
                String novacena1= cena.Substring(1, cena.Length - 1);
                float cena_decimal1 = float.Parse(novacena1);              
                Console.WriteLine(cena_decimal1);
   
                if (cena_decimal1 > profit)
                {
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span/h1"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    cena = element.Text;
                    novacena = cena.Substring(1, cena.Length - 1);
                    cena_decimal = float.Parse(novacena);
                    profit = cena_decimal;
                    ne_profit = cena_decimal - cena_decimal / 100 * 3;
                    continue;
                    //Check();
                    //Driver.Navigate().GoToUrl("https://www.coinbase.com/dashboard");
                    //Thread.Sleep(30000);
                    //Driver.Navigate().GoToUrl("https://www.coinbase.com/accounts");
                    //Thread.Sleep(10000);
                    //element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                    //cena = element.Text;
                    //novacena = cena.Substring(1, cena.Length - 1);
                   // cena_decimal = float.Parse(novacena);
                    //profit = cena_decimal + cena_decimal / 100 * 5;
                    //ne_profit = cena_decimal - cena_decimal / 100 * 3;
       

                }
                else if(cena_decimal1 < ne_profit){

                    Check();
                    Driver.Navigate().GoToUrl("https://www.coinbase.com/dashboard");
                    Thread.Sleep(10000);
                    Driver.Navigate().GoToUrl("https://www.coinbase.com/accounts");
                    Thread.Sleep(60000);
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span/h1"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div/span"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    try
                    {
                        element = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div[1]/div/div/section/div/div/div/div[1]/div[1]/div"));
                    }
                    catch
                    {
                        Console.WriteLine("Nibuva");
                    }
                    cena = element.Text;
                    novacena = cena.Substring(1, cena.Length - 1);
                    cena_decimal = float.Parse(novacena);
                    profit = cena_decimal;
                    ne_profit = cena_decimal - cena_decimal / 100 * 3;
                }
                
                Thread.Sleep(1000);
                

            }


        }



        static void Main(string[] args)
        {

            Program a = new Program();
            a.Pari = new Thread(a.Coin);
            a.Pari.Start();

        }
    }
}
