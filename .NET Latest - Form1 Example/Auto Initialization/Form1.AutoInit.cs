// This example contains the default auto-initialization of Agni Sentinel.

using System;
using System.Windows.Forms;
using AgniSentinel;
using AgniSentinel.Core;

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
            // Use auto-initialization for default protection
            _sentinel = SentinelFactory.GetOrCreate();
            if (_sentinel != null)
                _sentinel.SecurityThreatDetected += OnSecurityThreatDetected;
        }

        private void OnSecurityThreatDetected(object? sender, SecurityThreatDetectedEventArgs e)
        {
            MessageBox.Show($"Application security compromised! The application will now close.", "Agni Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
