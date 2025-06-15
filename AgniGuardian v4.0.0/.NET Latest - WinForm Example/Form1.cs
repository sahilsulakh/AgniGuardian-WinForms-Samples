using AgniGuardian;          // <<<--- MUST ADD THIS NAMESPACE FOR AGNIGUARDIAN
using System;
using System.Configuration;
using System.Windows.Forms;

namespace TestApplication          // <<<--- REPLACE WITH YOUR WORKSPACE NAME
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var options = new GuardianOptions
            {
                EnableAntiTamper = true,
                EnableAntiDebugger = false,          // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                EnableAntiPatch = true,
                EnableAntiInjector = true,
                EnableAntiDump = true,
                EnableAntiDecompile = true,
                EnableIntegrityChecker = false,          // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                EnableProcessMonitor = true,
                EnableRuntimeGuard = false,          // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                EnableNetworkGuard = false,          // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                DiscordWebhookUrl = "your_discord_webhook_url"          // <<<--- REPLACE IT WITH YOUR DISCORD WEBHOOK URL
            };

            Guardian.Initialize(options);

        }
    }
}
