using System;
namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var wallet = new Wallet("Hosny", 50);

            //Synchronous code:
            //wallet.Debit(40);
            //wallet.Debit(30); // 10

            // Parallel code:
            // Race condition: scenario where the outcome of the program is affected because of timing
            var t1 = new Thread(() => wallet.Debit(40));
            var t2 = new Thread(() => wallet.Debit(30));
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
            Console.WriteLine(wallet);
            Console.ReadLine();
        }
    }
    class Wallet
    {
        private readonly object bitcoinslock = new object();
        public string Name { get; private set; }
        public int BitCoins { get; private set; }

        public Wallet(string name, int bitcoins)
        { 
            Name = name;
            BitCoins = bitcoins;
        }

        public void Debit(int amount)
        {
            lock(bitcoinslock)  // To avoid race condition
            {
                if (BitCoins >= amount)
                {
                    Thread.Sleep(1000);
                    BitCoins -= amount;
                }
            }
           
        }

        public void Credit(int amount)
        {
            Thread.Sleep(1000);
            BitCoins += amount;
        }

        public override string ToString()
        {
            return $"[{Name} -> {BitCoins} Bitcoins]";
        }
    }
}
