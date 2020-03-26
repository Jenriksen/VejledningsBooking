using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vejledningsbooking.Framework
{
    public abstract class Entity<TId> where TId : IEquatable<TId>
    {
        private readonly List<object> _events;
        protected Entity() => _events = new List<object>();
        protected void Raise(object @event) => _events.Add(@event);
        public IEnumerable<object> GetChanges() =>
            _events.AsEnumerable();
        public void ClearChanges() => _events.Clear();

        public void Apply(object @event){
            When(@event);
            EnsureValidState();
            _events.Add(@event);
        }

        protected abstract void EnsureValidState();

        protected abstract void When(object @event);
    }
}
