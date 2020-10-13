﻿using System;
using System.Linq;

namespace AtCoderLibraryCSharp
{
    public class TwoSatisfiability
    {
        public bool[] Answer { get; }

        private readonly int _length;
        private readonly StronglyConnectedComponent _scc;

        public TwoSatisfiability(int length = 0)
        {
            _length = length;
            Answer = new bool[length];
            _scc = new StronglyConnectedComponent(length * 2);
        }

        public void AddClause(int i, bool f, int j, bool g)
        {
            if (i < 0 || _length < i) throw new IndexOutOfRangeException(nameof(i));
            if (j < 0 || _length < j) throw new IndexOutOfRangeException(nameof(j));
            _scc.AddEdge(i * 2 + (f ? 0 : 1), j * 2 + (g ? 1 : 0));
            _scc.AddEdge(j * 2 + (g ? 0 : 1), i * 2 + (f ? 1 : 0));
        }

        public bool IsSatisfiable()
        {
            var (_, identities) = _scc.GetIds();
            var ids = identities.ToArray();
            for (var i = 0; i < _length; i++)
            {
                if (ids[i * 2] == ids[i * 2 + 1]) return false;
                Answer[i] = ids[i * 2] < ids[i * 2 + 1];
            }

            return true;
        }
    }
}