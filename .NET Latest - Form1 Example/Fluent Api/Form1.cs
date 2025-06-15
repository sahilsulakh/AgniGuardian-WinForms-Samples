using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgniSentinel;
using AgniSentinel.Core;
using AgniSentinel.Core.Reporting;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        private Sentinel? _sentinel;

        public Form1()
        {
            InitializeComponent();

            if (!InitializeAgniSentinel())
            {
                MessageBox.Show("Security check failed! Application will now close.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
        }

        private bool InitializeAgniSentinel()
        {
            try
            {
                // Configure AgniSentinel using the fluent API
                AgniGuard.Protect()
                    .WithAntiTampering()
                    .WithDebuggerDetection()
                    .WithProcessMonitoring()
                    .WithMemoryProtection()
                    .WithEnvironmentValidation()
                    .WithCheckInterval(500) // Check every half second to be more responsive
                    .OnTampering(SecurityAction.Terminate)
                    .OnDebugger(SecurityAction.Terminate)
                    .OnSuspiciousProcess(SecurityAction.Terminate)
                    .WithCustomThreatHandler(HandleSecurityThreat)
                    .Initialize();

                // Get the sentinel instance after initialization
                _sentinel = AgniGuard.GetSentinel();

                if (_sentinel != null)
                {
                    _sentinel.SecurityThreatDetected += OnSecurityThreatDetected;

                    if (_sentinel.State == SentinelState.Error)
                    {
                        MessageBox.Show("Security system initialization failed!", "Security Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize security: {ex.Message}", "Security Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async void HandleSecurityThreat(SecurityThreatDetectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => HandleSecurityThreat(e)));
                return;
            }

            MessageBox.Show(
                $"Security threat detected!\n\nType: {e.ThreatType}\nAction: {e.ThreatAction}",
                "Security Alert",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            this.Enabled = false;
            Application.Exit();
        }

        private async void OnSecurityThreatDetected(object? sender, SecurityThreatDetectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnSecurityThreatDetected(sender, e)));
                return;
            }

            MessageBox.Show(
                $"Security threat detected!\n\nType: {e.ThreatType}\nAction: {e.ThreatAction}",
                "Security Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            this.Enabled = false;
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _sentinel?.Dispose();
        }
    }
}
