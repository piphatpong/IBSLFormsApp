using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Diagnostics.Eventing.Reader;

namespace IBSLFormsApp
{
    partial class FormStorePayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStorePayment));
            groupBox1 = new GroupBox();
            richTextPayment = new RichTextBox();
            fileStoreProc = new TextBox();
            label2 = new Label();
            fileNamePayment = new Label();
            buttonRunPayment = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richTextPayment);
            groupBox1.Controls.Add(fileStoreProc);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(fileNamePayment);
            groupBox1.Controls.Add(buttonRunPayment);
            groupBox1.Location = new Point(48, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1117, 621);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Payment Store Procedure";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // richTextPayment
            // 
            richTextPayment.Location = new Point(81, 176);
            richTextPayment.Name = "richTextPayment";
            richTextPayment.Size = new Size(951, 369);
            richTextPayment.TabIndex = 4;
            //richTextPayment.Text = resources.GetString("richTextPayment.Text");
            richTextPayment.Text = Payment_Store_query_stmt;
            richTextPayment.TextChanged += richTextPayment_TextChanged;
            // 
            // fileStoreProc
            // 
            fileStoreProc.Location = new Point(190, 53);
            fileStoreProc.Name = "fileStoreProc";
            fileStoreProc.Size = new Size(250, 27);
            fileStoreProc.TabIndex = 3;
            fileStoreProc.Text = "ibsl_";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(70, 139);
            label2.Name = "label2";
            label2.Size = new Size(120, 20);
            label2.TabIndex = 2;
            label2.Text = "Query Statement";
            label2.Click += label2_Click;
            // 
            // fileNamePayment
            // 
            fileNamePayment.AutoSize = true;
            fileNamePayment.Location = new Point(70, 60);
            fileNamePayment.Name = "fileNamePayment";
            fileNamePayment.Size = new Size(72, 20);
            fileNamePayment.TabIndex = 1;
            fileNamePayment.Text = "FileName";
            // 
            // buttonRunPayment
            // 
            buttonRunPayment.Location = new Point(898, 568);
            buttonRunPayment.Name = "buttonRunPayment";
            buttonRunPayment.Size = new Size(134, 47);
            buttonRunPayment.TabIndex = 0;
            buttonRunPayment.Text = "RUN";
            buttonRunPayment.UseVisualStyleBackColor = true;
            buttonRunPayment.Click += buttonRunPayment_Click;
            // 
            // FormStorePayment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 719);
            Controls.Add(groupBox1);
            Name = "FormStorePayment";
            Text = "FormStorePayment";
            Load += FormStorePayment_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RichTextBox richTextPayment;
        private TextBox fileStoreProc;
        private Label label2;
        private Label fileNamePayment;
        private Button buttonRunPayment;
    }
}