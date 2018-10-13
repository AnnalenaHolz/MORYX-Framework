﻿using System.Runtime.Serialization;
using Marvin.AbstractionLayer;
using Marvin.Serialization;

namespace Marvin.Products.Management.Modification
{
    /// <summary>
    /// DTO representation of a recipe
    /// </summary>
    [DataContract(IsReference = true)]
    internal class RecipeModel
    {
        /// <summary>
        /// Id of the recipe
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Name of the recipe
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// This recipe's state
        /// </summary>
        [DataMember]
        public RecipeState State { get; set; }

        /// <summary>
        /// Revision of the recipe
        /// </summary>
        [DataMember]
        public int Revision { get; set; }

        /// <summary>
        /// Flag if this recipe is the current recipe
        /// </summary>
        [DataMember]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Type of the recipe
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Ingredients of the recipe
        /// </summary>
        [DataMember]
        public Entry Ingredients { get; set; }

        /// <summary>
        /// The id of the current referenced workplan
        /// </summary>
        [DataMember]
        public long WorkplanId { get; set; }
    }
}