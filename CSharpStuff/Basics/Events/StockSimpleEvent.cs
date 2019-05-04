using System;

namespace CSharpStuff.Basics.Events
{
    public class StockSimpleEvent
    {
        public string Symbol;
        decimal price;

        public StockSimpleEvent(string symbol)
        {
            Symbol = symbol;
        }

        public event EventHandler PriceChanged;

        protected virtual void OnPriceChanged(EventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                price = value;
                OnPriceChanged(EventArgs.Empty);
            }
        }
    }
}