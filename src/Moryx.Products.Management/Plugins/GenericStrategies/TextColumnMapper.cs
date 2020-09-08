// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Reflection;
using Moryx.Container;
using Moryx.Serialization;
using Moryx.Tools;
using Newtonsoft.Json;

namespace Moryx.Products.Management
{
    /// <summary>
    /// Mapper for columns of type <see cref="string"/>
    /// </summary>
    [TextStrategyConfiguration]
    [Component(LifeCycle.Transient, typeof(IPropertyMapper), Name = nameof(TextColumnMapper))]
    internal class TextColumnMapper : ColumnMapper<string>
    {
        public TextColumnMapper(Type targetType) : base(targetType)
        {

        }

        protected override IPropertyAccessor<object, string> CreatePropertyAccessor(PropertyInfo objectProp)
        {
            // Convert return value to string
            if (objectProp.PropertyType == typeof(Guid))
                return new GuidAccessor(objectProp);

            if (objectProp.PropertyType.IsClass && objectProp.PropertyType != typeof(string))
                return new JsonAccessor(objectProp);

            return base.CreatePropertyAccessor(objectProp);
        }

        /// <summary>
        /// Accessor decorator to convert GUID to string and back
        /// </summary>
        private class GuidAccessor : ConversionAccessor<string, Guid>
        {
            public GuidAccessor(PropertyInfo property) : base(property)
            {
            }

            public override string ReadProperty(object instance)
            {
                var value = Target.ReadProperty(instance);
                return value.ToString();
            }

            public override void WriteProperty(object instance, string value)
            {
                var guid = Guid.Parse(value);
                Target.WriteProperty(instance, guid);
            }
        }

        /// <summary>
        /// Accessor decorator to convert objects to JSON and back
        /// </summary>
        private class JsonAccessor : ConversionAccessor<string, object>
        {
            public JsonAccessor(PropertyInfo property) : base(property)
            {
            }

            public override string ReadProperty(object instance)
            {
                var value = Target.ReadProperty(instance);
                return JsonConvert.SerializeObject(value, Target.Property.PropertyType, JsonSettings.Minimal);
            }

            public override void WriteProperty(object instance, string value)
            {
                var deserialized = JsonConvert.DeserializeObject(value, Target.Property.PropertyType);
                Target.WriteProperty(instance, deserialized);
            }
        }
    }
}