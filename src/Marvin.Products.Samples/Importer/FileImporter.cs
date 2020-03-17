// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.IO;
using Marvin.AbstractionLayer;
using Marvin.Container;
using Marvin.Modules;
using Marvin.Products.Management;
using Marvin.Products.Management.Importers;

namespace Marvin.Products.Samples
{
    [ExpectedConfig(typeof(FileImporterConfig))]
    [Plugin(LifeCycle.Singleton, typeof(IProductImporter), Name = nameof(FileImporter))]
    public class FileImporter : ProductImporterBase<FileImporterConfig, FileImportParameters>
    {
        /// <summary>
        /// Method to generate an instance of the parameter array
        /// </summary>
        /// <returns></returns>
        protected override IImportParameters GenerateParameters()
        {
            return new FileImportParameters { FileExtension = ".mjb" };
        }

        /// <summary>
        /// Import a product using given parameters
        /// </summary>
        protected override IProductType[] Import(FileImportParameters parameters)
        {
            using (var stream = parameters.ReadFile())
            {
                var textReader = new StreamReader(stream);
                var identifier = textReader.ReadLine();
                var revision = short.Parse(textReader.ReadLine() ?? "0");
                var name = textReader.ReadLine();

                return new IProductType[]
                {
                    new NeedleType
                    {
                        Name = name,
                        Identity = new ProductIdentity(identifier, revision)
                    }
                };
            }
        }
    }
}
