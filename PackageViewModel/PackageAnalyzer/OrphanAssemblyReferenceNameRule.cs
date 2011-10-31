﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using NuGet;
using NuGetPackageExplorer.Types;

namespace PackageExplorerViewModel.Rules
{
    [Export(typeof(IPackageRule))]
    internal class OrphanAssemblyReferenceNameRule : IPackageRule
    {
        #region IPackageRule Members

        public IEnumerable<PackageIssue> Validate(IPackage package, string packagePath)
        {
            if (package.References.Any())
            {
                IEnumerable<string> allLibFiles = package.GetFilesInFolder("lib").Select(Path.GetFileName);
                var libFilesSet = new HashSet<string>(allLibFiles, StringComparer.OrdinalIgnoreCase);

                return from reference in package.References
                       where !libFilesSet.Contains(reference.File)
                       select CreateIssue(reference.File);
            }
            return new PackageIssue[0];
        }

        #endregion

        private static PackageIssue CreateIssue(string reference)
        {
            return new PackageIssue(
                PackageIssueLevel.Error,
                "Assembly reference name not found.",
                "The name '" + reference + "' in the Filtered Assembly References is not found under the 'lib' folder.",
                "Either remove this assembly reference name or add a file with this name to the 'lib' folder.");
        }
    }
}