using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BetweenAsynMethodAndAsyncOperation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ThrowExceptionsAsync();
            ThrowAllExceptionsAsync();
            Console.ReadLine();
        }

        private static async Task ThrowAllExceptionsAsync()
        {
            var t1 = Task.Run(() => throw new Exception("Exception from Task 1"));
            var t2 = Task.Run(() => throw new Exception("Exception from Task 2"));
            var t3 = Task.Run(() => throw new Exception("Exception from Task 3"));
            try
            {
                await Task.WhenAll(t1, t2, t3).WithAggregatedExceptions();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("All exceptions will be thrown here by GetResult() due to Task.Wait()");
                Console.WriteLine(string.Join(",", ae.InnerExceptions.Select(e => e.Message)));
            }

        }

        private static async Task ThrowExceptionsAsync()
        {
            var t1 = Task.Run(() => throw new Exception("Exception from Task 1"));
            var t2 = Task.Run(() => throw new Exception("Exception from Task 2"));
            var t3 = Task.Run(() => throw new Exception("Exception from Task 3"));
            try
            {
                await Task.WhenAll(t1, t2, t3);
            }
            catch (Exception e)
            {
                Console.WriteLine("Only the first exception will be thrown here by GetResult()");
                Console.WriteLine($"Message = {e.Message} (Should be Exception from Task 1)");
            }
        }
    }

    public class AggregatedExceptionAwaitable
    {
        private readonly Task _task;

        public AggregatedExceptionAwaitable(Task task)
        {
            _task = task;
        }

        public AggregatedExceptionAwaiter GetAwaiter()
        {
            return new AggregatedExceptionAwaiter(_task);
        }
    }

    public static class TaskExtensions
    {
        public static AggregatedExceptionAwaitable WithAggregatedExceptions(this Task task)
        {
            return new AggregatedExceptionAwaitable(task);
        }
    }

    public class AggregatedExceptionAwaiter : INotifyCompletion
    {
        private readonly Task _task;

        public AggregatedExceptionAwaiter(Task task)
        {
            _task = task;
        }

        public bool IsCompleted => _task.GetAwaiter().IsCompleted;

        public void OnCompleted(Action continuation)
        {
            _task.GetAwaiter().OnCompleted(continuation);
        }

        public void GetResult()
        {
            _task.Wait();
        }
    }
}