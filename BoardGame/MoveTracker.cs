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
        protected List<Move> history;
        protected Move[,] moves;
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
            history = new List<Move>();
            moves = new Move[3,3];
        }

        public bool addMove(Move move)
        {
            history.Add(move);
            moves[move.row, move.col] = move;
            MoveRecord record = new MoveRecord(move, moves);

            Console.WriteLine($"Move added {move.row} {move.col}");
            foreach (var observer in observers)
            {
                //if (!loc.HasValue)
                //    observer.OnError(new LocationUnknownException());
                //else
                    observer.OnNext(record);
            }
            return true;
        }

        private bool isValidMove(Move move)
        {
            return true;
        }

        //execute()
        //{
        //    //undo
        //    Board.grid[3][2] = 'X'
        //    //redo
        //    Board.grid[3][2] = ''
        //}
    }
}

