namespace CustomTaskPractice
{
    using System;
    using System.Runtime.CompilerServices;

    [AsyncMethodBuilderAttribute(typeof(TimerTaskMethodBuilder<>))]
    public class TimerTask<T> : INotifyCompletion
    {
        private T result;
        private Action continuation;

        public bool IsCompleted { get; private set; }

        public TimerTask<T> GetAwaiter()
        {
            return this;
        }

        internal void SetResult(
            T result)
        {
            this.result = result;
            this.IsCompleted = true;
            this.continuation?.Invoke();
        }

        public void OnCompleted(
            Action continuation)
        {
            this.continuation = continuation;

            if (this.IsCompleted)
            {
                continuation();
            }
        }

        public T GetResult()
        {
            if (this.IsCompleted)
            {
                return result;
            }

            throw new Exception("not completed");
        }

        public void SetException(Exception ex)
        {
            this.IsCompleted = true;
            Console.WriteLine(ex.Message);
        }
    }
}