using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class Program
{
    static NotifyIcon trayIcon;

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // ساخت منوی راست‌کلیک
        ContextMenuStrip menu = new ContextMenuStrip();
        menu.Items.Add("Monitor 1 Only", null, (s, e) => SetDisplayMode("internal"));
        menu.Items.Add("Monitor 2 Only", null, (s, e) => SetDisplayMode("external"));
        menu.Items.Add("Extended Mode", null, (s, e) => SetDisplayMode("extend"));
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Exit", null, (s, e) => Application.Exit());

        // آیکون در Tray
        trayIcon = new NotifyIcon()
        {
            Icon = SystemIcons.Application,
            ContextMenuStrip = menu,
            Text = "Display Switcher",
            Visible = true
        };

        Application.Run();
    }

    static void SetDisplayMode(string mode)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "DisplaySwitch.exe",
            Arguments = mode switch
            {
                "internal" => "/internal",
                "external" => "/external",
                "extend" => "/extend",
                _ => "/extend"
            },
            CreateNoWindow = true,
            UseShellExecute = false
        });
    }
}
