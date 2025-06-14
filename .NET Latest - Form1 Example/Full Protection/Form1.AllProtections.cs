// This example contains the full protection setup for Agni Sentinel in a .NET Windows Forms application.
using System;
using System.Windows.Forms;
using AgniSentinel;
using AgniSentinel.Core;
using AgniSentinel.Core.Configuration;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        private Sentinel? _sentinel;

        public Form1()
        {
            InitializeComponent();
            InitializeAgniSentinel();
        }

        private void InitializeAgniSentinel()
        {
            var options = new SentinelOptions
            {
                UseAntiTampering = true,
                UseDebuggerDetection = true,
                UseProcessMonitoring = true,
                UseMemoryProtection = true,
                UseEnvironmentValidation = true,
                UseAntiVirtualization = true,
                UseAssemblyMonitoring = true,
                UseFileSystemMonitoring = true,
                UseNetworkMonitoring = true,
                CheckIntervalMs = 2000,
                TamperingDetectedAction = SecurityAction.Terminate,
                DebuggerDetectedAction = SecurityAction.Terminate,
                SuspiciousProcessDetectedAction = SecurityAction.Log,
                MemoryTamperingDetectedAction = SecurityAction.Terminate,
                InvalidEnvironmentDetectedAction = SecurityAction.Terminate,
                VirtualizationDetectedAction = SecurityAction.Log,
                AssemblyModificationDetectedAction = SecurityAction.Terminate,
                FileTamperingDetectedAction = SecurityAction.Terminate,
                NetworkThreatDetectedAction = SecurityAction.Terminate,
                CustomThreatHandler = HandleSecurityThreat,                
            };

            _sentinel = new Sentinel(options);
            _sentinel.SecurityThreatDetected += OnSecurityThreatDetected;
            _sentinel.Initialize().StartMonitoring();
        }

        private void HandleSecurityThreat(SecurityThreatDetectedEventArgs e)
        {
            // Show message, disable, and close
            MessageBox.Show($"Application security compromised! The application will now close.", "Agni Guardian 🛡️", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Enabled = false;
            Application.Exit();
        }

        private void OnSecurityThreatDetected(object? sender, SecurityThreatDetectedEventArgs e)
        {
            // Show message, disable, and close
            MessageBox.Show($"Application security compromised! The application will now close.", "Agni Guardian 🛡️", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Enabled = false;
            Application.Exit();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _sentinel?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
