// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace KnowledgeInterchangeFormat.Expressions
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// An unrestricted logical definition.
    /// </summary>
    public class UnrestrictedLogicalDefinition : UnrestrictedDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnrestrictedLogicalDefinition"/> class.
        /// </summary>
        /// <param name="constant">The constant being defined.</param>
        /// <param name="description">An optional description of the definition.</param>
        /// <param name="sentences">The sentences in the definition.</param>
        public UnrestrictedLogicalDefinition(Constant constant, CharacterString description, IEnumerable<Sentence> sentences)
            : base(constant, description, sentences)
        {
        }

        /// <inheritdoc/>
        public override void ToString(StringBuilder sb)
        {
            sb.Append("(deflogical ");

            this.Constant.ToString(sb);

            if (this.Description != null)
            {
                sb.Append(' ');
                this.Description.ToString(sb);
            }

            foreach (var sentence in this.Sentences)
            {
                sb.Append(' ');
                sentence.ToString(sb);
            }

            sb.Append(')');
        }
    }
}
