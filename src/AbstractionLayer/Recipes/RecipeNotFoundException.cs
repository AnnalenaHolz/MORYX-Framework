﻿using System;

namespace Marvin.AbstractionLayer
{
    /// <summary>
    /// Exception thrown when a recipe could not be found by its id
    /// </summary>
    public class RecipeNotFoundException : Exception
    {
        /// <summary>
        /// Initialize exception for non existing id
        /// </summary>
        /// <param name="id">Id that was not found in database</param>
        public RecipeNotFoundException(long id)
            : base($"Recipe with id '{id}' not found")
        {
        }
    }
}