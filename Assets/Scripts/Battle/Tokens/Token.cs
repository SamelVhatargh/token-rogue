using System;
using Random = UnityEngine.Random;

namespace Battle.Tokens
{
    public class Token
    {
        private readonly Side _sideA;
        private readonly Side _sideB;
        public bool IsSpent { get; private set; }
        
        public Side ActiveSide { get; private set; }
        public Side InactiveSide { get; private set; }
        
        public event Action SideChanged;

        public Token(Side sideA, Side sideB)
        {
           _sideA = sideA;
           _sideB = sideB;
           ActiveSide = sideA;
           InactiveSide = sideB;
        }
        
        public void Flip()
        {
            (ActiveSide, InactiveSide) = (InactiveSide, ActiveSide);
            SideChanged?.Invoke();
        }
        
        public void Cast()
        {
            IsSpent = false;
            if (Random.Range(0, 2) == 0)
            {
                ActiveSide = _sideA;
                InactiveSide = _sideB;
            }
            else
            {
                ActiveSide = _sideB;
                InactiveSide = _sideA;
            }
            SideChanged?.Invoke();
        }
        
        public void Spend()
        {
            IsSpent = true;
        }

        public override string ToString()
        {
            return $"({_sideA} | {_sideB})";
        }
    }

    public class Side
    {
        public int Value { get; }
        public Symbol Symbol { get; }
        public bool HasInitiative { get; }

        public Side(int value, Symbol symbol, bool initiative = false)
        {
            Value = value;
            Symbol = symbol;
            HasInitiative = initiative;
        }

        public override string ToString()
        {
            return $"{Value} {Symbol}{(HasInitiative ? "*" : "")}";
        }
    }

    [Flags]
    public enum Symbol
    {
        None = 0,
        Empty = 1 << 0,
        Attack = 1 << 1,
        Defense = 1 << 2,
        Energy = 1 << 3,
        Agility = 1 << 4,
        Everything = Empty | Attack | Defense | Energy | Agility,
    }
}