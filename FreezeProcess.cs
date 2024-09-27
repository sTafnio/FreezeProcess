using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using ExileCore;

namespace FreezeProcess;

public class FreezeProcess : BaseSettingsPlugin<FreezeProcessSettings>
{
    [DllImport("ntdll.dll")]
    private static extern uint NtSuspendProcess(IntPtr processHandle);

    public override bool Initialise()
    {
        return true;
    }

    public override Job Tick()
    {
        if (Settings.Freeze.PressedOnce())
        {
            FreezeGameProcess();
        }

        return null;
    }

    private static void FreezeGameProcess()
    {
        try
        {
            Process gameProcess = Process.GetProcessesByName("PathOfExile").FirstOrDefault();

            if (gameProcess != null)
            {
                uint result = NtSuspendProcess(gameProcess.Handle);
                if (result == 0)
                {
                    DebugWindow.LogMsg("Game process frozen.");
                }
                else
                {
                    DebugWindow.LogError($"Error freezing game process: {result}");
                }
            }
            else
            {
                DebugWindow.LogError("Game process not found.");
            }
        }
        catch (Exception ex)
        {
            DebugWindow.LogError($"Error freezing game process: {ex.Message}");
        }
    }

    public override void Render()
    {
    }
}