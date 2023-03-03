namespace CAThreadPool
{
    class Program
    {

        static void Main(string[] args)
        {

            ///
            ///Thread has overhead in time and memory
            ///pool of pre-created recyclable threads
            ///helps metigate the issue of performance by reducing the number of threads
            ///With pool thread:
            /// You cannot name a thread
            /// always background
            /// Ideal for short running process


            //1
            Console.WriteLine("Using ThreadPool:");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Print));

            //2
            Console.WriteLine("Using Task:");
            Task.Run(Print);

            //Example
            var employee = new Employee { Rate = 10, TotalHours = 40 };
            ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateSalary), employee);
            Console.WriteLine(employee.TotalSalary);
            Console.ReadLine();

        }
        public static void CalculateSalary (object employee)
        {
            var emp = employee as Employee;

            if (emp is null) return;
            emp.TotalSalary = emp.TotalHours * emp.Rate;
            Console.WriteLine(emp.TotalSalary.ToString("C"));
        }

        private static void Print()
        {
            Console.WriteLine($"Thred Id: {Thread.CurrentThread.ManagedThreadId}, Thread Name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is Pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Background: {Thread.CurrentThread.IsBackground}");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Cycle {i + 1}");
            }
        }

        private static void Print(object state)
        {
            Console.WriteLine($"Thred Id: {Thread.CurrentThread.ManagedThreadId}, Thread Name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is Pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Background: {Thread.CurrentThread.IsBackground}");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Cycle {i + 1}");
            }
        }
    }

    class Employee
    {
        public decimal TotalHours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalSalary { get; set; }
    }
}