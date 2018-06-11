﻿using Marvin.Model;
using System.Collections.Generic;

namespace Marvin.Products.Model
{
    public class WorkplanEntity : ModificationTrackedEntityBase
    {
        public virtual string Name { get; set; }

        public virtual int Version { get; set; }

        public virtual int State { get; set; }

        public virtual int MaxElementId { get; set; }

        public virtual ICollection<ProductRecipeEntity> Recipes { get; set; }

        public virtual ICollection<WorkplanReference> SourceReferences { get; set; }

        public virtual ICollection<WorkplanReference> TargetReferences { get; set; }

        public virtual ICollection<ConnectorEntity> Connectors { get; set; }

        public virtual ICollection<StepEntity> Steps { get; set; }

        public virtual ICollection<StepEntity> Parents { get; set; }
    }
}
