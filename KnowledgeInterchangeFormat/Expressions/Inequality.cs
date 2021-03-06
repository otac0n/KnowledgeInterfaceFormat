// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace KnowledgeInterchangeFormat.Expressions
{
    using System;
    using System.Text;

    /// <summary>
    /// A <see cref="Sentence"/> invloving the <c>/=</c> operator.
    /// </summary>
    public class Inequality : Sentence, IEquatable<Inequality>
    {
        private const int HashCodeSeed = 0x34f09f28;

        /// <summary>
        /// Initializes a new instance of the <see cref="Inequality"/> class.
        /// </summary>
        /// <param name="left">The left side of the inequality.</param>
        /// <param name="right">The right side of the inequality.</param>
        public Inequality(Term left, Term right)
        {
            this.Left = left ?? throw new ArgumentNullException(nameof(left));
            this.Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        /// <summary>
        /// Gets the left side of the inequality.
        /// </summary>
        public Term Left { get; }

        /// <summary>
        /// Gets the right side of the inequality.
        /// </summary>
        public Term Right { get; }

        /// <inheritdoc/>
        public override bool Equals(Expression other) => other is Inequality inequality && this.Equals(inequality);

        /// <inheritdoc/>
        public bool Equals(Inequality other) => !(other is null) &&
            this.Left == other.Left &&
            this.Right == other.Right;

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = HashCodeSeed;
            EquatableUtilities.Combine(ref hash, this.Left);
            EquatableUtilities.Combine(ref hash, this.Right);
            return hash;
        }

        /// <inheritdoc />
        public override void ToString(StringBuilder sb)
        {
            sb.Append("(/= ");
            this.Left.ToString(sb);
            sb.Append(' ');
            this.Right.ToString(sb);
            sb.Append(')');
        }
    }
}
