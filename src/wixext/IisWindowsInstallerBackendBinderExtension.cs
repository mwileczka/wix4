﻿// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Iis
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using WixToolset.Data;
    using WixToolset.Data.WindowsInstaller;
    using WixToolset.Extensibility;

    public class IisWindowsInstallerBackendBinderExtension : BaseWindowsInstallerBackendBinderExtension
    {
        private static readonly TableDefinition[] Tables = LoadTables();

        public override IEnumerable<TableDefinition> TableDefinitions { get => Tables; }

        public override bool TryAddTupleToOutput(IntermediateTuple tuple, WindowsInstallerData output)
        {
            var columnZeroIsId = tuple.Id != null;
            return this.BackendHelper.TryAddTupleToOutputMatchingTableDefinitions(tuple, output, this.TableDefinitions, columnZeroIsId);
        }

        private static TableDefinition[] LoadTables()
        {
            using (var resourceStream = typeof(IisWindowsInstallerBackendBinderExtension).Assembly.GetManifestResourceStream("WixToolset.Iis.tables.xml"))
            using (var reader = XmlReader.Create(resourceStream))
            {
                var tables = TableDefinitionCollection.Load(reader);
                return tables.ToArray();
            }
        }
    }
}
