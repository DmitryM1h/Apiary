using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain
{
    public interface IEvent
    {

    }
    public static class ActorsEvents
    {
        private static List<IEvent> _events = [];

        public static void EmitEvent(IEvent @event)
        {
            _events.Add(@event);
        }

        public static IEnumerable<IEvent> GetEvents()
        {
            var res = _events.ToList();
            _events.Clear();
            return res;
        }
    }


    public record class BeeWasBornEvent(int HiveId) : IEvent;


}
