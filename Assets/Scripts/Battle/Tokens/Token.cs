using System;
using Random = UnityEngine.Random;

namespace Battle.Tokens
{
    public class Token
    {
        private Side _sideA;
        private Side _sideB;
        
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
            ActiveSide = InactiveSide;
            InactiveSide = ActiveSide;
            SideChanged?.Invoke();
        }
        
        public void Cast()
        {
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

        public override string ToString()
        {
            return $"({_sideA} | {_sideB})";
        }
    }

    public class Side
    {
        public int Value { get; }
        public Symbol Symbol { get; }

        public Side(int value, Symbol symbol)
        {
            Value = value;
            Symbol = symbol;
        }

        public override string ToString()
        {
            return $"{Value} {Symbol}";
        }
    }

    public enum Symbol
    {
        None,
        Attack,
        Defense,
        Energy,
        Agility,
    }
}