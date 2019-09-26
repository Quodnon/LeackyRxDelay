using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using akarnokd.reactive_extensions;

namespace ConsoleApp3
{
    class TestEntity
    {
        public TimeSpan DelayTime { get; set; }
        public string Id { get; set; }
        public string UpdateData { get; set; }
    }

    class Program
    {
        static void TestSyncObservable()
        {
            var subj = new Subject<TestEntity>();
            var disposePreDelayCount = 0;

            var testSubj = subj.GroupByUntil(x => x.Id, x => x.Select(y => y.UpdateData == "removed"))
                .SelectMany(g => g.Select(x =>
                    {
                        return Observable.Return(x)
                            .DoOnDispose(() => { disposePreDelayCount++; }) //works well
                            .Delay(TimeSpan.FromMinutes(20)); //creates tons of timers.
                    })
                    .Switch())
                .Where(x => x.UpdateData == "filter")
                .Select(x => x);

            var subscription = testSubj.Subscribe(y =>
            {
                Console.WriteLine($"{DateTime.UtcNow} id = {y.Id} data = {y.UpdateData}");
            });

            Console.WriteLine($"{DateTime.UtcNow} start sending updates to subject");
            Helper.ProduceData(subj);
            
            Console.WriteLine($"{DateTime.UtcNow} Switch() called dispose {disposePreDelayCount} times");
            Console.WriteLine("take your time and check the amount of unmanaged memory");
            
            var t2 = Task.Delay(TimeSpan.FromMinutes(25));
                

            Task.WaitAll(t2);

            Console.WriteLine("Ended");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start app");
            TestSyncObservable();
        }
    }
}