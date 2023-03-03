using System;
namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.Name);    // to get the current thread
            Thread.CurrentThread.Name = "Main Thread";  //to name the thread 

            //Console.WriteLine($"Background Thread: {Thread.CurrentThread.IsBackground}");
            ///
            /// Foreground thread: they keep the application alive as long as any of them is running
            /// Background thread: They don't keep the app alive in their own - terminating immediately once all forground ended
            ///
            var wallet = new Wallet("Hosny", 80);

            //These thread run in parallel
            Thread t1 = new Thread(wallet.RunRandomTransactions);
            t1.Name = "T1";
            Console.WriteLine($"after declaration {t1.Name} state is: {t1.ThreadState}");
            t1.Start();
            t1.Join();  // this allows one thread to wait for the completion of another
            Console.WriteLine($"after start {t1.Name} state is: {t1.ThreadState}");

            Thread t2 = new Thread(new ThreadStart(wallet.RunRandomTransactions));
            t2.Name = "T2";
            Console.WriteLine($"after declaration {t2.Name} state is: {t2.ThreadState}");
            t2.Start();
            Console.WriteLine($"after start {t2.Name} state is: {t2.ThreadState}");



            Console.ReadLine();
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
                Thread.Sleep(1000);
                BitCoins -= amount;
                Console.WriteLine($"[Thread: {Thread.CurrentThread.ManagedThreadId}-{Thread.CurrentThread.Name}" +
                                  $",Processor: {Thread.GetCurrentProcessorId()}] -{amount}");
            }

            public void Credit(int amount)
            {
                Thread.Sleep(1000);
                BitCoins += amount;
                Console.WriteLine($"[Thread: {Thread.CurrentThread.ManagedThreadId}-{Thread.CurrentThread.Name} " +
                                  $",Processor: {Thread.GetCurrentProcessorId()}] +{amount}");
            }

            public void RunRandomTransactions()
            {
                int[] amounts = { 10, 20, 30, -20, 10, -10, 30, -10, 40, -20 };

                foreach (var amount in amounts)
                {
                    var abValue = Math.Abs(amount);
                    if (amount < 0) Debit(abValue);
                    else Credit(abValue);
                    
                }
            }

            public override string ToString()
            {
                return $"[{Name} -> {BitCoins} Bitcoins]";
            }
        }

    }
}