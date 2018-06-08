﻿using Marvin.Model;

namespace Marvin.Products.Model
{
    public class OutputDescriptionEntity : EntityBase
    {
        public virtual int Index { get; set; }

        public virtual bool Success { get; set; }

        public virtual string Name { get; set; }

        public virtual long MappingValue { get; set; }

        public virtual long StepEntityId { get; set; }

        public virtual StepEntity Step { get; set; }
    }
}
