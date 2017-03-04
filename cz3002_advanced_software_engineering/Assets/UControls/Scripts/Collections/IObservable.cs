using System;
using System.Collections;

namespace Ultralpha
{
    public interface IObservable : IEnumerable
    {
        event Action StateChangedHandler;
    }
}