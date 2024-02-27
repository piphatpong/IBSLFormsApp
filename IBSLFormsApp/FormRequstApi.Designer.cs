using System.Data.SqlClient;
using System.Text;

namespace IBSLFormsApp
{
    partial class FormRequstApi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelAPIRequest = new Label();
            buttonPolicy = new Button();
            buttonPayment = new Button();
            buttonClaim = new Button();
            groupBoxStorePoLists = new GroupBox();
            panel1 = new Panel();
            Servers = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            pwd = new TextBox();
            usrName = new TextBox();
            radioButtonUAT = new RadioButton();
            radioButtonPRD = new RadioButton();
            buttonRefresh = new Button();
            statusStrip1 = new StatusStrip();
            groupBox2 = new GroupBox();
            BStoreClaim = new Button();
            BStorePayment = new Button();
            btStorePolicy = new Button();
            label1 = new Label();
            PivKeyFile = new TextBox();
            pivkeySaha = new Button();
            groupBox3 = new GroupBox();
            sslStatus = new GroupBox();
            StatusBox = new RichTextBox();
            groupBox4 = new GroupBox();
            GenPolicyButton = new Button();
            groupBoxStorePoLists.SuspendLayout();
            Servers.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // labelAPIRequest
            // 
            labelAPIRequest.AutoSize = true;
            labelAPIRequest.Font = new Font("Bernard MT Condensed", 26.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelAPIRequest.ForeColor = SystemColors.ControlDarkDark;
            labelAPIRequest.Location = new Point(14, 9);
            labelAPIRequest.Name = "labelAPIRequest";
            labelAPIRequest.Size = new Size(353, 52);
            labelAPIRequest.TabIndex = 0;
            labelAPIRequest.Text = "IBS Life Dash board";
            // 
            // buttonPolicy
            // 
            buttonPolicy.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPolicy.ForeColor = Color.FromArgb(0, 192, 0);
            buttonPolicy.Location = new Point(465, 202);
            buttonPolicy.Name = "buttonPolicy";
            buttonPolicy.Size = new Size(209, 49);
            buttonPolicy.TabIndex = 1;
            buttonPolicy.Text = "Policy Request Api";
            buttonPolicy.UseVisualStyleBackColor = true;
            buttonPolicy.Click += button_Click_policy;
            // 
            // buttonPayment
            // 
            buttonPayment.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPayment.ForeColor = Color.Fuchsia;
            buttonPayment.Location = new Point(465, 257);
            buttonPayment.Name = "buttonPayment";
            buttonPayment.Size = new Size(209, 49);
            buttonPayment.TabIndex = 2;
            buttonPayment.Text = "Payment Request Api";
            buttonPayment.UseVisualStyleBackColor = true;
            buttonPayment.Click += button_Click_payment;
            // 
            // buttonClaim
            // 
            buttonClaim.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            buttonClaim.ForeColor = Color.FromArgb(0, 192, 192);
            buttonClaim.Location = new Point(465, 312);
            buttonClaim.Name = "buttonClaim";
            buttonClaim.Size = new Size(209, 49);
            buttonClaim.TabIndex = 3;
            buttonClaim.Text = "Claim Request Api";
            buttonClaim.UseVisualStyleBackColor = true;
            buttonClaim.Click += button_Click_claim;
            // 
            // groupBoxStorePoLists
            // 
            groupBoxStorePoLists.Controls.Add(panel1);
            groupBoxStorePoLists.Controls.Add(Servers);
            groupBoxStorePoLists.Controls.Add(buttonPolicy);
            groupBoxStorePoLists.Controls.Add(buttonPayment);
            groupBoxStorePoLists.Controls.Add(buttonClaim);
            groupBoxStorePoLists.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxStorePoLists.Location = new Point(11, 60);
            groupBoxStorePoLists.Name = "groupBoxStorePoLists";
            groupBoxStorePoLists.Size = new Size(698, 372);
            groupBoxStorePoLists.TabIndex = 0;
            groupBoxStorePoLists.TabStop = false;
            groupBoxStorePoLists.Text = "Work space";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Location = new Point(24, 38);
            panel1.Margin = new Padding(1, 1, 1, 3);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(1);
            panel1.Size = new Size(332, 323);
            panel1.TabIndex = 7;
            // 
            // Servers
            // 
            Servers.Controls.Add(label3);
            Servers.Controls.Add(label2);
            Servers.Controls.Add(pwd);
            Servers.Controls.Add(usrName);
            Servers.Controls.Add(radioButtonUAT);
            Servers.Controls.Add(radioButtonPRD);
            Servers.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Servers.Location = new Point(384, 28);
            Servers.Margin = new Padding(3, 4, 3, 4);
            Servers.Name = "Servers";
            Servers.Padding = new Padding(3, 4, 3, 4);
            Servers.Size = new Size(283, 167);
            Servers.TabIndex = 6;
            Servers.TabStop = false;
            Servers.Text = "Servers";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 120);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 9;
            label3.Text = "Password";
            label3.Click += label3_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 68);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 8;
            label2.Text = "UserName";
            // 
            // pwd
            // 
            pwd.Location = new Point(95, 117);
            pwd.Name = "pwd";
            pwd.Size = new Size(174, 27);
            pwd.TabIndex = 7;
            pwd.Text = "Sahalife@123";
            // 
            // usrName
            // 
            usrName.Location = new Point(95, 65);
            usrName.Name = "usrName";
            usrName.Size = new Size(174, 27);
            usrName.TabIndex = 6;
            usrName.Text = "piphatphong@sahalife.co.th";
            // 
            // radioButtonUAT
            // 
            radioButtonUAT.AutoSize = true;
            radioButtonUAT.Location = new Point(32, 22);
            radioButtonUAT.Margin = new Padding(3, 4, 3, 4);
            radioButtonUAT.Name = "radioButtonUAT";
            radioButtonUAT.Size = new Size(58, 24);
            radioButtonUAT.TabIndex = 4;
            radioButtonUAT.TabStop = true;
            radioButtonUAT.Text = "UAT";
            radioButtonUAT.UseVisualStyleBackColor = true;
            radioButtonUAT.CheckedChanged += radioButtonUAT_CheckedChanged;
            // 
            // radioButtonPRD
            // 
            radioButtonPRD.AutoSize = true;
            radioButtonPRD.Location = new Point(129, 22);
            radioButtonPRD.Margin = new Padding(3, 4, 3, 4);
            radioButtonPRD.Name = "radioButtonPRD";
            radioButtonPRD.Size = new Size(106, 24);
            radioButtonPRD.TabIndex = 5;
            radioButtonPRD.TabStop = true;
            radioButtonPRD.Text = "Production";
            radioButtonPRD.UseVisualStyleBackColor = true;
            radioButtonPRD.CheckedChanged += radioButtonPRD_CheckedChanged;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Image = Properties.Resources.icons8_refresh_arrow_24;
            buttonRefresh.Location = new Point(1115, 17);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(66, 37);
            buttonRefresh.TabIndex = 5;
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 786);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1232, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(BStoreClaim);
            groupBox2.Controls.Add(BStorePayment);
            groupBox2.Controls.Add(btStorePolicy);
            groupBox2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(715, 60);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(503, 83);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Create Store Procedure";
            // 
            // BStoreClaim
            // 
            BStoreClaim.Location = new Point(361, 27);
            BStoreClaim.Margin = new Padding(3, 4, 3, 4);
            BStoreClaim.Name = "BStoreClaim";
            BStoreClaim.Size = new Size(105, 37);
            BStoreClaim.TabIndex = 2;
            BStoreClaim.Text = "Claim";
            BStoreClaim.UseVisualStyleBackColor = true;
            BStoreClaim.Click += BStoreClaim_Click;
            // 
            // BStorePayment
            // 
            BStorePayment.Location = new Point(199, 27);
            BStorePayment.Margin = new Padding(3, 4, 3, 4);
            BStorePayment.Name = "BStorePayment";
            BStorePayment.Size = new Size(109, 37);
            BStorePayment.TabIndex = 1;
            BStorePayment.Text = "Payment";
            BStorePayment.UseVisualStyleBackColor = true;
            BStorePayment.Click += BTStorePayment;
            // 
            // btStorePolicy
            // 
            btStorePolicy.Location = new Point(25, 28);
            btStorePolicy.Name = "btStorePolicy";
            btStorePolicy.Size = new Size(107, 36);
            btStorePolicy.TabIndex = 0;
            btStorePolicy.Text = "Policy";
            btStorePolicy.UseVisualStyleBackColor = true;
            btStorePolicy.Click += BTStorePolicy;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(7, 37);
            label1.Name = "label1";
            label1.Size = new Size(139, 20);
            label1.TabIndex = 7;
            label1.Text = "Upload file SSL key";
            // 
            // PivKeyFile
            // 
            PivKeyFile.Location = new Point(158, 29);
            PivKeyFile.Margin = new Padding(3, 4, 3, 4);
            PivKeyFile.Name = "PivKeyFile";
            PivKeyFile.Size = new Size(183, 27);
            PivKeyFile.TabIndex = 6;
            PivKeyFile.TextChanged += PivKeyFile_TextChanged;
            // 
            // pivkeySaha
            // 
            pivkeySaha.Location = new Point(360, 31);
            pivkeySaha.Name = "pivkeySaha";
            pivkeySaha.Size = new Size(117, 29);
            pivkeySaha.TabIndex = 3;
            pivkeySaha.Text = "Brows";
            pivkeySaha.UseVisualStyleBackColor = true;
            pivkeySaha.Click += btnPrivetKeySaha_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(sslStatus);
            groupBox3.Controls.Add(PivKeyFile);
            groupBox3.Controls.Add(pivkeySaha);
            groupBox3.Controls.Add(label1);
            groupBox3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(717, 149);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(502, 283);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "SSL key";
            groupBox3.Enter += groupBox3_Enter_1;
            // 
            // sslStatus
            // 
            sslStatus.BackColor = SystemColors.ActiveCaptionText;
            sslStatus.ForeColor = Color.Chartreuse;
            sslStatus.Location = new Point(7, 83);
            sslStatus.Margin = new Padding(3, 4, 3, 4);
            sslStatus.Name = "sslStatus";
            sslStatus.Padding = new Padding(3, 4, 3, 4);
            sslStatus.Size = new Size(489, 181);
            sslStatus.TabIndex = 15;
            sslStatus.TabStop = false;
            sslStatus.Text = "SSL key files";
            sslStatus.Enter += sslStatus_Enter;
            // 
            // StatusBox
            // 
            StatusBox.BackColor = SystemColors.InactiveCaptionText;
            StatusBox.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            StatusBox.ForeColor = Color.Aquamarine;
            StatusBox.Location = new Point(14, 453);
            StatusBox.Name = "StatusBox";
            StatusBox.ReadOnly = true;
            StatusBox.Size = new Size(695, 326);
            StatusBox.TabIndex = 10;
            StatusBox.Text = "Status";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(GenPolicyButton);
            groupBox4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.Location = new Point(718, 440);
            groupBox4.Margin = new Padding(3, 4, 3, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 4, 3, 4);
            groupBox4.Size = new Size(502, 339);
            groupBox4.TabIndex = 16;
            groupBox4.TabStop = false;
            groupBox4.Text = "Generate Tables from U-Smart";
            // 
            // GenPolicyButton
            // 
            GenPolicyButton.Location = new Point(28, 43);
            GenPolicyButton.Name = "GenPolicyButton";
            GenPolicyButton.Size = new Size(150, 29);
            GenPolicyButton.TabIndex = 3;
            GenPolicyButton.Text = "Gen Policy Table";
            GenPolicyButton.UseVisualStyleBackColor = true;
            GenPolicyButton.Click += GenPolicyButton_Click;
            // 
            // FormRequstApi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1232, 808);
            Controls.Add(groupBox4);
            Controls.Add(StatusBox);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(buttonRefresh);
            Controls.Add(statusStrip1);
            Controls.Add(groupBoxStorePoLists);
            Controls.Add(labelAPIRequest);
            Name = "FormRequstApi";
            Text = "FormRequestApi";
            Load += Form1_Load;
            groupBoxStorePoLists.ResumeLayout(false);
            Servers.ResumeLayout(false);
            Servers.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label labelAPIRequest;
        private Button buttonPolicy;
        private Button buttonPayment;
        private Button buttonClaim;
        private GroupBox groupBoxStorePoLists;
        private RadioButton radioButtonUAT;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private Button buttonRefresh;
        private StatusStrip statusStrip1;
        private GroupBox groupBox2;
        private Button btStorePolicy;
        private RadioButton radioButtonPRD;
        private GroupBox Servers;
        private Button BStoreClaim;
        private Button BStorePayment;
        private Button pivkeySaha;
        private Label label1;
        private TextBox PivKeyFile;
        private GroupBox groupBox3;
        private GroupBox sslStatus;
        private Label label3;
        private Label label2;
        private TextBox pwd;
        private TextBox usrName;
        protected RichTextBox StatusBox;
        private GroupBox groupBox4;
        private Button GenPolicyButton;
        private Panel panel1;
    }
}