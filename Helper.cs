using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace ConsoleApp3
{
    class Helper
    {
        static List<TestEntity> GenerateTestDataPortion1()
        {
            var list = new List<TestEntity>();
            for (var i = 0; i < 5_000; i++)
            {
                list.Add(new TestEntity()
                {
                    Id = i % 2 == 0 ? i.ToString() : "f_" + i.ToString(), DelayTime = TimeSpan.FromMinutes(2)
                });
            }

            return list;
        }

        static List<TestEntity> GenerateTestDataPortion2()
        {
            var list = new List<TestEntity>();
            for (var i = 0; i < 5_000; i++)
            {
                list.Add(new TestEntity()
                {
                    Id = i % 2 == 0 ? i.ToString() : "f_" + i.ToString(),
                    DelayTime = TimeSpan.FromMinutes(2),
                    UpdateData = i % 250 == 0 ? "filter" : "removed"
                });
            }

            return list;
        }

        static List<TestEntity> GenerateTestDataPortion3()
        {
            var list = new List<TestEntity>();
            for (var i = 0; i < 5_000; i++)
            {
                list.Add(new TestEntity()
                {
                    Id = i % 2 == 0 ? i.ToString() : "f_" + i.ToString(),
                    DelayTime = TimeSpan.FromMinutes(2),
                    UpdateData = i % 250 == 0 ? "filter" : "removed"
                });
            }

            return list;
        }

        static List<TestEntity> GenerateTestDataWithModification()
        {
            var list = new List<TestEntity>();
            for (var i = 0; i < 5_000; i++)
            {
                list.Add(new TestEntity()
                {
                    Id = i % 2 == 0 ? i.ToString() : "f_" + i.ToString(),
                    DelayTime = TimeSpan.FromMinutes(10),
                    UpdateData = "dummy update"
                });
            }

            return list;
        }

        public static void ProduceData(Subject<TestEntity> subj)
        {
            for (int i = 0; i < 5; i++) 
            {
                Console.WriteLine($"Produced portion no {i} of 5_000 items");
                GenerateTestDataPortion1().ForEach(x=>subj.OnNext(x));
            }
        }
    }
}