using AgniGuardian;          // <<<--- MUST USE NAMESPACE FOR AGNIGUARDIAN
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App1Framework          // <<<--- CHANGE IT WITH YOUR WORKSPACE NAME
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

  
        private void Form1_Load(object sender, EventArgs e)
        {
            AgniGuardian.Guardian.Initialize(new GuardianOptions
            {
                EnableAntiTamper = true,
                EnableAntiDebugger = true,
                EnableAntiPatch = true,
                EnableAntiInjector = true,
                EnableAntiDump = false,             // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                EnableAntiDecompile = true,
                EnableIntegrityChecker = false,     // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                EnableProcessMonitor = true,
                EnableRuntimeGuard = true,         // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                EnableNetworkGuard = false,        // <<<--- KEEP IT FALSE DURING DEVELOPMENT
                DiscordWebhookUrl = "your_discord_webhook_url"          // <<<--- REPLACE WITH YOUR DISCORD WEBHOOK URL
            });
        }

    }
}

