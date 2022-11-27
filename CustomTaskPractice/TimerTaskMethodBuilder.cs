namespace CustomTaskPractice
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public struct TimerTaskMethodBuilder<T>
    {
        private Stopwatch stopwatch;

        private TimerTask<T> task;
        public TimerTask<T> Task
        {
            get
            {
                return task ??= new TimerTask<T>();
            }
        }

        public void Start<TStateMachine>(
            ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("Start timer");
            this.stopwatch = Stopwatch.StartNew();
            stateMachine.MoveNext();
        }

        public void SetException(Exception ex)
        {
            this.Task.SetException(ex);
        }

        public void SetResult(
            T result)
        {
            this.stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            this.Task.SetResult(result);
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
                where TAwaiter : INotifyCompletion 
                where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public static TimerTaskMethodBuilder<T> Create()
        {
            return new TimerTaskMethodBuilder<T>();
        }

        public void AwaitUnsafeOnCompleted<TAwaiter,TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.UnsafeOnCompleted(stateMachine.MoveNext);
        }

        public void SetStateMachine(
            IAsyncStateMachine stateMachine)
        {
        }
    }
}