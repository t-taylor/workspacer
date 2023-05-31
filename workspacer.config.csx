#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using System.Linq;
using workspacer;
using workspacer.Bar;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;

Action<IConfigContext> doConfig = (context) =>
{
    // Uncomment to switch update branch (or to disable updates)
    //context.Branch = Branch.None;

    context.AddBar();
    context.AddFocusIndicator();
    //var actionMenu = context.AddActionMenu();

    //context.WorkspaceContainer.CreateWorkspaces("1", "2", "3", "4", "5");
    context.CanMinimizeWindows = true; // false by default

    var monitors = context.MonitorContainer.GetAllMonitors();

    foreach (var mon in monitors)
    {
        context.WorkspaceContainer.CreateWorkspace(mon.Index.ToString(), new ILayoutEngine[0]);
    }

    var MainModKey = KeyModifiers.LAlt;
    var ShiftMod = KeyModifiers.Shift;
    var CtrlMod = KeyModifiers.Control;

    context.Keybinds.UnsubscribeAll();

    context.Keybinds.Subscribe(MainModKey, Keys.Q,
            () => context.Workspaces.FocusedWorkspace.CloseFocusedWindow(), "close focused window");

    context.Keybinds.Subscribe(MainModKey, Keys.Y,
            () => context.Workspaces.FocusedWorkspace.NextLayoutEngine(), "next layout");

    context.Keybinds.Subscribe(MainModKey, Keys.J,
            () => context.Workspaces.FocusedWorkspace.FocusNextWindow(), "focus next window");

    context.Keybinds.Subscribe(MainModKey, Keys.K,
            () => context.Workspaces.FocusedWorkspace.FocusPreviousWindow(), "focus previous window");

    context.Keybinds.Subscribe(MainModKey | KeyModifiers.LShift, Keys.J,
            () => context.Workspaces.FocusedWorkspace.SwapFocusAndNextWindow(), "swap focus and next window");

    context.Keybinds.Subscribe(MainModKey | KeyModifiers.LShift, Keys.K,
            () => context.Workspaces.FocusedWorkspace.SwapFocusAndPreviousWindow(), "swap focus and previous window");

    context.Keybinds.Subscribe(MainModKey, Keys.Oemcomma,
            () => context.Workspaces.FocusedWorkspace.ShrinkPrimaryArea(), "shrink primary area");

    context.Keybinds.Subscribe(MainModKey, Keys.OemPeriod,
            () => context.Workspaces.FocusedWorkspace.ExpandPrimaryArea(), "expand primary area");

    context.Keybinds.Subscribe(MainModKey, Keys.A,
            () => context.Workspaces.FocusedWorkspace.IncrementNumberOfPrimaryWindows(), "increment # primary windows");

    context.Keybinds.Subscribe(MainModKey, Keys.X,
            () => context.Workspaces.FocusedWorkspace.DecrementNumberOfPrimaryWindows(), "decrement # primary windows");

    context.Keybinds.Subscribe(MainModKey, Keys.P,
            () => context.Windows.ToggleFocusedWindowTiling(), "toggle tiling for focused window");

    context.Keybinds.Subscribe(MainModKey | CtrlMod, Keys.Q,
            context.Quit, "quit workspacer");

    context.Keybinds.Subscribe(MainModKey | CtrlMod, Keys.R,
            context.Restart, "restart workspacer");

    context.Keybinds.Subscribe(MainModKey, Keys.H,
            () => context.Workspaces.SwitchToPreviousWorkspace(), "switch to previous workspace");

    context.Keybinds.Subscribe(MainModKey, Keys.L,
            () => context.Workspaces.SwitchToNextWorkspace(), "switch to next workspace");

    context.Keybinds.Subscribe(MainModKey | ShiftMod, Keys.H,
            () => context.Workspaces.MoveFocusedWindowAndSwitchToPreviousWorkspace(), "move window to previous workspace");

    context.Keybinds.Subscribe(MainModKey | ShiftMod, Keys.L,
            () => context.Workspaces.MoveFocusedWindowAndSwitchToNextWorkspace(), "move window to next workspace");

    // Register Alt+Shift+N to create a new workspacer
    context.Keybinds.Subscribe(MainModKey, Keys.OemSemicolon, () => {
            var num_workspaces = context.WorkspaceContainer.GetAllWorkspaces().Count();
            context.WorkspaceContainer.CreateWorkspace((num_workspaces + 1).ToString());
            }, "Create new workspace");


    context.Keybinds.Subscribe(MainModKey, Keys.N,
            () => context.Workspaces.SwitchFocusToNextMonitor(), "switch to next monitor");

    context.Keybinds.Subscribe(MainModKey | CtrlMod, Keys.N,
            () => context.Workspaces.SwitchFocusToPreviousMonitor(), "switch to previous monitor");

    context.Keybinds.Subscribe(MainModKey | ShiftMod, Keys.N,
            () => context.Workspaces.MoveFocusedWindowToNextMonitor(), "move window to next monitor");

    context.Keybinds.Subscribe(MainModKey | ShiftMod | CtrlMod, Keys.N,
            () => context.Workspaces.MoveFocusedWindowToPreviousMonitor(), "move window to previous monitor");

    //context.Keybinds.Subscribe(MainModKey | KeyModifiers.LShift, Keys.I,
    //        () => context.Windows.DumpWindowDebugOutput(), "dump debug info to console for all windows");

    //context.Keybinds.Subscribe(MainModKey, Keys.I,
    //        () => context.Windows.DumpWindowUnderCursorDebugOutput(), "dump debug info to console for window under cursor");

    //context.Keybinds.Subscribe(MainModKey | MainModKey | KeyModifiers.LShift, Keys.I,
    //        () => context.ToggleConsoleWindow(), "toggle debug console");

    context.Keybinds.Subscribe(MainModKey | KeyModifiers.LShift, Keys.OemQuestion,
            () => context.Keybinds.ShowKeybindDialog(), "open keybind window");

};
return doConfig;
