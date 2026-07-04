//using ApiaryEngine.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ApiaryEngine.Domain.Bees.WorkerBee.States
//{
//    public class CollectingHoneyState : IState
//    {
//        public DateTime _nextTimeAct;
//        public WorkerBee Context { get; set; }
//        Flower flower;

//        public CollectingHoneyState(WorkerBee context, Flower flower)
//        {
//            Context = context;
//            this.flower = flower;
//            _nextTimeAct = DateTime.Now.AddSeconds(10);
//        }

//        public bool IsCompleted { get; set; } = false;

//        public void Act()
//        {
//            if (flower.HoneyAmount < 10)
//            {
//                IsCompleted = true;
//                return;
//            }

//            Context.AddHoney(flower.GetHoney(10));

//            if (Context.CollectedHoney)
//        }

//        public IState NextState()
//        {
//            return new DeliveringToHoneyState(Context);  либо лететь на другой цветок если тут пусто
//        }
//    }
//}
