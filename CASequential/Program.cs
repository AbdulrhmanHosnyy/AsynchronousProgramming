using System;


namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var wallet = new Wallet("Hosny", 80);

            wallet.RunRandomTransactions();
            Console.WriteLine("-----------------------");
            Console.WriteLine(wallet + "\n");

            wallet.RunRandomTransactions();
            Console.WriteLine("-----------------------");
            Console.WriteLine(wallet + "\n");

            Console.ReadLine();
        }
    }
    class Wallet
    {
        public string Name { get; private set; }
        public int BitCoins { get; private set; }

        public Wallet(string name, int bitcoins)
        {
            Name = name;
            BitCoins = bitcoins;    
        }

        public void Debit(int amount)
        {
            BitCoins -= amount;
        }

        public void Credit(int amount)
        {
            BitCoins += amount;
        }

        public void RunRandomTransactions()
        {
            int[] amounts = { 10, 20, 30, -20, 10, -10, 30, -10, 40, -20 };

            foreach (var amount in amounts)
            {
                var abValue = Math.Abs(amount);
                if(amount < 0) Debit(abValue);
                else Credit(abValue);
                Console.WriteLine($"[Thread: {Thread.CurrentThread.ManagedThreadId}" + 
                                  $",Processor: {Thread.GetCurrentProcessorId()}] {amount}");
            }
        }

        public override string ToString()
        {
            return $"[{Name} -> {BitCoins} Bitcoins]";
        }
    }
}