﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace NuGetPackageExplorer.Types
{
    public enum MessageLevel
    {
        Information,
        Warning,
        Error
    }

    public interface IUIServices
    {
        bool OpenSaveFileDialog(string title, string defaultFileName, string filter, bool overwritePrompt,
                                out string selectedFilePath, out int selectedFilterIndex);

        bool OpenFileDialog(string title, string filter, out string selectedFileName);
        bool OpenMultipleFilesDialog(string title, string filter, out string[] selectedFileNames);

        bool OpenRenameDialog(string currentName, string description, out string newName);

        bool OpenPublishDialog(object viewModel);
        bool OpenFolderDialog(string title, string initialPath, out string selectedPath);

        bool Confirm(string title, string message);
        bool Confirm(string title, string message, bool isWarning);
        bool? ConfirmWithCancel(string message, string title);
        void Show(string message, MessageLevel messageLevel);

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Tuple<bool?, bool> ConfirmMoveFile(string fileName, string targetFolder, int numberOfItemsLeft);

        void BeginInvoke(Action action);
    }
}