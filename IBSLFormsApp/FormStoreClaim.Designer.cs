namespace IBSLFormsApp
{
    partial class FormStoreClaim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStoreClaim));
            groupBox1 = new GroupBox();
            richBoxClaim = new RichTextBox();
            ClaimStoreProc = new TextBox();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richBoxClaim);
            groupBox1.Controls.Add(ClaimStoreProc);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new Point(29, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1060, 603);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Form Claim Store Procedure";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // richBoxClaim
            // 
            richBoxClaim.Location = new Point(132, 123);
            richBoxClaim.Name = "richBoxClaim";
            richBoxClaim.Size = new Size(863, 414);
            richBoxClaim.TabIndex = 4;
            //richBoxClaim.Text = resources.GetString("richBoxClaim.Text");
            richBoxClaim.Text = Claim_Store_query_stmt;
            richBoxClaim.TextChanged += richBoxClaim_TextChanged_1;
            // 
            // ClaimStoreProc
            // 
            ClaimStoreProc.Location = new Point(143, 54);
            ClaimStoreProc.Name = "ClaimStoreProc";
            ClaimStoreProc.Size = new Size(203, 27);
            ClaimStoreProc.TabIndex = 3;
            ClaimStoreProc.Text = "ibsl_";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 123);
            label2.Name = "label2";
            label2.Size = new Size(120, 20);
            label2.TabIndex = 2;
            label2.Text = "Query Statement";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 54);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 1;
            label1.Text = "File Name";
            // 
            // button1
            // 
            button1.Location = new Point(890, 556);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "RUN";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormStoreClaim
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 681);
            Controls.Add(groupBox1);
            Name = "FormStoreClaim";
            Text = "Form Store Claim";
            Load += FormStoreClaim_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RichTextBox richBoxClaim;
        private TextBox ClaimStoreProc;
        private Label label2;
        private Label label1;
        private Button button1;
    }
}