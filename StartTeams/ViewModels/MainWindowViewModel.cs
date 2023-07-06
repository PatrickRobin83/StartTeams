/*
*----------------------------------------------------------------------------------
*          Filename:	MainWindowViewModel.cs
*          Date:        2023.07.05
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace StartTeams.ViewModels;
[ObservableObject]
public partial class MainWindowViewModel 
{
    #region Fields
    public string ApplicationPath { get; set; }
    #endregion

    #region Properties

    #endregion

    #region Constructor

    #endregion

    #region Methods

    private string getApplicationpath()
    {

        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Teams"))
        {
            var microsoftTeamsExecuteablePath = registryKey?.GetValue("InstallLocation")?.ToString();
            if (microsoftTeamsExecuteablePath != null)
            {
                var microsoftTeamsExecutable = Path.Combine(microsoftTeamsExecuteablePath, "current", "Teams.exe");
                if (File.Exists(microsoftTeamsExecutable))
                {
                    return microsoftTeamsExecutable;
                }
            }
        }

        return null;
    }
    #endregion

    #region Commands

    [RelayCommand]
    private void StartTeams()
    {
        //MessageBox.Show("Button funktioniert");
        Process newProcess = null;
        if (!string.IsNullOrEmpty(getApplicationpath()))
        {
            newProcess = new Process
            {
                StartInfo = new ProcessStartInfo(getApplicationpath())
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    CreateNoWindow = false
                }
            };
            newProcess.Start();
        }
    }
    [RelayCommand]
    private void ExitApp()
    {
        Environment.Exit(0);
    }
    #endregion
}