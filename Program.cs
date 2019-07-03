using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Item TV = new Item("Samsung", 1500);
            Console.WriteLine("--- John and Casey are subscribed to the TV Product");

            // attach or subscribe customer 1
            Buyer john = new Buyer("John");
            TV.Subscribe(john);

            // attach or subscribe customer 2
            Buyer casey = new Buyer("Casey");
            TV.Subscribe(casey);

            // publish notification to the subscribers
            TV.price = 1000;

            Console.WriteLine("--- Casey is unsubscribed and Alan is subscribed to the TV Product");
            // customer 2 is unsubscribed and customer 3 is subscribed
            TV.Unsubscribe(casey);
            Buyer alan = new Buyer("Alan");
            TV.Subscribe(alan);
            TV.price = 1100;
            Console.ReadLine();
        }
    }

    //Item Interface
    interface ItemI
    {
        // attach observer
        void Subscribe(Buyer buyer);
        // detach observer
        void Unsubscribe(Buyer buyer);
        // publish notification to the subscribers
        void Notify();
    }

    //Concrete Subject Class
    public class Item : ItemI
    {
        string name;
        float basePrice;
        float currentPrice;
        List<Buyer> buyers = new List<Buyer>();

        public Item(string name, float basePrice)
        {
            this.name = name;
            this.basePrice = basePrice;
            this.currentPrice = basePrice;
        }

        public float price
        {
            get
            {
                return currentPrice;
            }
            set
            {
                currentPrice = value;
                if (value <= basePrice)
                    Notify();
            }
        }
        public void Subscribe(Buyer buyer)
        {
            buyers.Add(buyer);
        }

        public void Unsubscribe(Buyer buyer)
        {
            buyers.Remove(buyer);
        }

        public void Notify()
        {
            foreach (Buyer observer in buyers)
            {
                observer.Update(this);
            }
        }

        public string Name
        {
            get { return name; }
        }

        public float discount
        {
            get { return (basePrice - currentPrice) * 100 / basePrice; }
        }
        public float CurrentPrice
        {
            get { return currentPrice; }
        }
    }

    //IObserver Interface
    interface BuyerI
    {
        void Update(Item item);
    }

    //ConcreteObserver Interface
    public class Buyer : BuyerI
    {
        string name;
        public Buyer(string name)
        {
            this.name = name;
        }
        public void Update(Item item)
        {
            Console.WriteLine("{0}: {1} TV is now available at {2}; Discount = {3}%", this.name, item.Name, item.CurrentPrice, item.discount);
        }
    }
}
