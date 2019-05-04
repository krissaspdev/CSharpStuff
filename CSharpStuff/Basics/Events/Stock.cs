using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CSharpStuff.Basics.Events
{

    public class PriceChangedEventArgs : EventArgs
    {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;
        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
    }

    public class Stock
    {
        public string Symbol;
        decimal price;

        public Stock(string symbol)
        {
            Symbol = symbol;
        }

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }
    }

    public static class Runner
    {
        public static void Run()
        {
            var stock = new Stock("THPW");
            stock.Price = 27.10m;
            stock.PriceChanged += (sender, e) =>
            {
                if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                    Console.WriteLine("Alert, 10% stock price increase !");
            };
            stock.Price = 31.59m;


            var stockSimpleEvent = new StockSimpleEvent("PLN");
            stockSimpleEvent.Price = 27.10m;
            stockSimpleEvent.PriceChanged += StockSimpleEvent_PriceChanged;
            stockSimpleEvent.Price = 31.59m;

        }

        private static void StockSimpleEvent_PriceChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Price changed!");
        }
    }

}
