// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace KnowledgeInterchangeFormat.Expressions
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// An unrestricted relation definition.
    /// </summary>
    public class UnrestrictedRelationDefinition : UnrestrictedDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnrestrictedRelationDefinition"/> class.
        /// </summary>
        /// <param name="constant">The constant being defined.</param>
        /// <param name="description">An optional description of the definition.</param>
        /// <param name="sentences">The sentences in the definition.</param>
        public UnrestrictedRelationDefinition(Constant constant, CharacterString description, IEnumerable<Sentence> sentences)
            : base(constant, description, sentences)
        {
        }

        /// <inheritdoc/>
        public override void ToString(StringBuilder sb)
        {
            sb.Append("(defrelation ");

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
