namespace IBSLFormsApp
{
    partial class FormStorePolicy
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
            groupBoxPolicy = new GroupBox();
            label2 = new Label();
            fileStoreProc = new TextBox();
            richTextPolicy = new RichTextBox();
            label1 = new Label();
            buttonRunPolicy = new Button();
            groupBoxPolicy.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxPolicy
            // 
            groupBoxPolicy.Controls.Add(label2);
            groupBoxPolicy.Controls.Add(fileStoreProc);
            groupBoxPolicy.Controls.Add(richTextPolicy);
            groupBoxPolicy.Controls.Add(label1);
            groupBoxPolicy.Controls.Add(buttonRunPolicy);
            groupBoxPolicy.Location = new Point(34, 12);
            groupBoxPolicy.Name = "groupBoxPolicy";
            groupBoxPolicy.Size = new Size(1015, 578);
            groupBoxPolicy.TabIndex = 0;
            groupBoxPolicy.TabStop = false;
            groupBoxPolicy.Text = "Policy Store Procedure";
            groupBoxPolicy.Enter += groupBoxPolicy_Enter_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 99);
            label2.Name = "label2";
            label2.Size = new Size(120, 20);
            label2.TabIndex = 8;
            label2.Text = "Query Statement";
            // 
            // fileStoreProc
            // 
            fileStoreProc.Location = new Point(109, 30);
            fileStoreProc.Name = "fileStoreProc";
            fileStoreProc.Size = new Size(227, 27);
            fileStoreProc.TabIndex = 7;
            fileStoreProc.Text = "ibsl_";
            fileStoreProc.TextChanged += fileStoreProc_TextChanged;
            // 
            // richTextPolicy
            // 
            richTextPolicy.Location = new Point(100, 134);
            richTextPolicy.Name = "richTextPolicy";
            richTextPolicy.Size = new Size(831, 370);
            richTextPolicy.TabIndex = 6;
            richTextPolicy.Text = pol_Store_query_stmt;
            richTextPolicy.TextChanged += richTextPolicy_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 37);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 3;
            label1.Text = "File Name";
            label1.Click += label1_Click;
            // 
            // buttonRunPolicy
            // 
            buttonRunPolicy.Location = new Point(837, 529);
            buttonRunPolicy.Name = "buttonRunPolicy";
            buttonRunPolicy.Size = new Size(94, 29);
            buttonRunPolicy.TabIndex = 2;
            buttonRunPolicy.Text = "RUN";
            buttonRunPolicy.UseVisualStyleBackColor = true;
            buttonRunPolicy.Click += button_Click_Run;
            // 
            // FormStorePolicy
            // 
            ClientSize = new Size(1085, 602);
            Controls.Add(groupBoxPolicy);
            Name = "FormStorePolicy";
            Text = "FormStorePolicy";
            Load += FormStorePolicy_Load;
            groupBoxPolicy.ResumeLayout(false);
            groupBoxPolicy.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupPolicy;
        private Button buttonRunPolicy;
        private GroupBox groupBoxPolicy;
        private Label label1;
        private RichTextBox richTextPolicy;
        private TextBox fileStoreProc;
        private Label label2;
    }
}