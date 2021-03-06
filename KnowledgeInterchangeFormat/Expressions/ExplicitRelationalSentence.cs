// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace KnowledgeInterchangeFormat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A relation applied via the <c>holds</c> operator.
    /// </summary>
    public class ExplicitRelationalSentence : RelationalSentence, IEquatable<ExplicitRelationalSentence>
    {
        private const int HashCodeSeed = unchecked((int)0xf4d86bef);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplicitRelationalSentence"/> class.
        /// </summary>
        /// <param name="relation">The term identifying the relation.</param>
        /// <param name="arguments">The arguments to the relation.</param>
        /// <param name="sequenceVariable">An optional <see cref="SequenceVariable"/> containing additional arguments.</param>
        public ExplicitRelationalSentence(Term relation, IEnumerable<Term> arguments, SequenceVariable sequenceVariable)
            : base(relation, arguments, sequenceVariable)
        {
        }

        /// <inheritdoc/>
        public override bool Equals(Expression other) => other is ExplicitRelationalSentence explicitRelationalSentence && this.Equals(explicitRelationalSentence);

        /// <inheritdoc/>
        public bool Equals(ExplicitRelationalSentence other) => !(other is null) &&
            this.Arguments.Count == other.Arguments.Count &&
            this.Relation == other.Relation &&
            this.SequenceVariable == other.SequenceVariable &&
            EquatableUtilities.ListsEqual(this.Arguments, other.Arguments);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = HashCodeSeed;
            EquatableUtilities.Combine(ref hash, this.Relation);
            EquatableUtilities.Combine(ref hash, EquatableUtilities.HashList(this.Arguments));
            EquatableUtilities.Combine(ref hash, this.SequenceVariable);
            return hash;
        }

        /// <inheritdoc/>
        public override void ToString(StringBuilder sb)
        {
            sb.Append("(holds ");

            this.Relation.ToString(sb);

            foreach (var arg in this.Arguments)
            {
                sb.Append(' ');
                arg.ToString(sb);
            }

            if (this.SequenceVariable != null)
            {
                sb.Append(' ');
                this.SequenceVariable.ToString(sb);
            }

            sb.Append(')');
        }
    }
}
