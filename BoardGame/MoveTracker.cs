using System.Collections.Generic;
using System;

namespace BoardGame
{
    public struct MoveRecord
    {
        public Move latest;
        public Move[,] moves; // QQ accessibility

        public MoveRecord(Move move, Move[,] moves)
        {
            this.latest = move;
            this.moves = moves;
        }
    }

    public class MoveTracker: IObservable<MoveRecord>
    {
        private Stack<PlaceCommand> _Undoables = new Stack<PlaceCommand>();
        private Stack<PlaceCommand> _Redoables = new Stack<PlaceCommand>();

        protected Move[,] moves; // rename to state
        private List<IObserver<MoveRecord>> observers;

        public IDisposable Subscribe(IObserver<MoveRecord> observer)
        {
            Console.WriteLine($"Before: {observers.Count} observers found");
            if (!observers.Contains(observer))
                observers.Add(observer);
            Console.WriteLine($"After: {observers.Count} observers found");
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber: IDisposable // QQ class inside class
        {
            private List<IObserver<MoveRecord>> _observers;
            private IObserver<MoveRecord> _observer;

            public Unsubscriber(List<IObserver<MoveRecord>> observers, IObserver<MoveRecord> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public MoveTracker()
        {
            observers = new List<IObserver<MoveRecord>>();
            moves = new Move[3,3];
        }

        public void Undo()
        {
            if (_Undoables.Count > 0)
            {
                PlaceCommand place = _Undoables.Pop();
                place.UnExecute();
                _Redoables.Push(place);
            }
        }

        public void Redo()
        {
            if (_Redoables.Count > 0)
            {
                PlaceCommand place = _Redoables.Pop();
                place.Execute();
                _Undoables.Push(place);
            }
        }

        public void InsertMove(PlaceCommand place)
        {
            _Undoables.Push(place);
            _Redoables.Clear();
        }

    }
}