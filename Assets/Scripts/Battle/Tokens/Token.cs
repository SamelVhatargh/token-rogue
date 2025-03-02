﻿using Random = UnityEngine.Random;

namespace Battle.Tokens
{
    public class Token
    {
        private Side _sideA;
        private Side _sideB;
        
        public Side ActiveSide { get; private set; }
        public Side InactiveSide { get; private set; }

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
        }
        
        public void Cast()
        {
            if (Random.Range(0, 1) == 0)
            {
                ActiveSide = _sideA;
                InactiveSide = _sideB;
            }
            else
            {
                ActiveSide = _sideB;
                InactiveSide = _sideA;
            }
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