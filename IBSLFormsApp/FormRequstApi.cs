using static IBSLFormsApp.Model.Payment.PaymentProgram;
using IBSLFormsApp.Model.Payment;
using System.Data.SqlClient;
using System.Text;
using static IBSLFormsApp.Model.Libraries.LoadingProgress;
using IBSLFormsApp.Model.Libraries;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IBSLFormsApp
{
    public partial class FormRequstApi : Form
    {
        DebugLog debl = new DebugLog();

        String selectStorefile = "";
        public FormRequstApi()
        {
            InitializeComponent();
            ReadFileStore();
            GetFileSSL();
        }

        private async void button_Click_policy(object sender, EventArgs e)
        {
            /* debl.debuglog("*****************"); */

            LoadingProgress loadingprogress = new LoadingProgress();

            loadingprogress.Show();

            string ServerName = "";

            if (radioButtonUAT.Checked)
            {
                ServerName = "uat";
            }
            else
            {
                ServerName = "prd";
            }

            selectStorefile = panel1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;


            debl.debuglog("------" + selectStorefile + "-------" + ServerName + "---------" + usrName + "----------" + pwd);


            PolicyProgram polprog = new PolicyProgram();

            string restxt = await polprog.policyprogram(selectStorefile, ServerName, usrName.Text, pwd.Text);

            StatusBox.Text = restxt;

            loadingprogress.Close();

        }
        private async void button_Click_payment(object sender, EventArgs e)
        {
            LoadingProgress loadingprogress = new LoadingProgress();

            loadingprogress.Show();

            string ServerName = "";

            if (radioButtonUAT.Checked)
            {
                ServerName = "uat";
            }
            else
            {
                ServerName = "prd";
            }
            selectStorefile = panel1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            PaymentProgram paymentProgram = new PaymentProgram();

            var restxt = await paymentProgram.Paymentprogram(selectStorefile, ServerName, usrName.Text, pwd.Text);

            StatusBox.Text = restxt;
            loadingprogress.Close();

        }
        private async void button_Click_claim(object sender, EventArgs e)
        {
            LoadingProgress loadingprogress = new LoadingProgress();

            loadingprogress.Show();

            string ServerName = "";

            if (radioButtonUAT.Checked)
            {
                ServerName = "uat";
            }
            else
            {
                ServerName = "prd";
            }

            //selectStorefile = groupBoxStorePoLists.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            selectStorefile = panel1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

            debl.debuglog("------" + selectStorefile + "-------" + ServerName + "---------" + usrName + "----------" + pwd);

            ClaimProgram claimProgram = new ClaimProgram();

            var restxt = await claimProgram.claimprogram(selectStorefile, ServerName, usrName.Text, pwd.Text);

            StatusBox.Text = restxt.ToString();
            loadingprogress.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ReadFileStore()
        {
            string result = "";
            int col = 20;
            //String connectionString = @"Data Source=10.20.25.101;Initial Catalog=IBS_Life;User ID=devconnect;Password=P@ssw0rd1234";

            String connectionString = "Data Source=VELA\\SQLEXPRESS;Initial Catalog=IBS_Life;Integrated Security=True";
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetfileStoreProc", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        col = col + 30;
                        result = String.Format("{0}", reader["SPECIFIC_NAME"]);
                        RadiFileStore(result.ToString(), col);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error GetfileStoreProc");
            }

        }

        private void RadiFileStore(string indy, int col)
        {
            string responsValue = "";
            RadioButton radioButtonStoreFile = new RadioButton();
            radioButtonStoreFile.Name = indy;
            radioButtonStoreFile.Text = indy;
            radioButtonStoreFile.AutoSize = true;
            radioButtonStoreFile.Location = new Point(36, col);
            radioButtonStoreFile.Size = new Size(117, 24);
            radioButtonStoreFile.TabIndex = 2;
            radioButtonStoreFile.TabStop = true;
            //this.Controls.Add(radioButtonStoreFile);
            panel1.Controls.Add(radioButtonStoreFile);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Reset Form");
            this.Controls.Clear();
            this.InitializeComponent();
            ReadFileStore();
            GetFileSSL();
        }

        private void GetFileSSL()
        {
            String fileListTxt = "";
            string ShortcutDirectory = Application.StartupPath + "/SSLKey/";
            //string ShortcutDirectory = @"C:\Users\user\Desktop\";
            string[] Shortcuts = Directory.GetFiles(ShortcutDirectory, "*.*");
            foreach (string name in Shortcuts)
            {
                fileListTxt = fileListTxt + Path.GetFileName(name) + "\n";
            }
            sslStatus.Text = fileListTxt;
        }



        private void BTStorePolicy(object sender, EventArgs e)
        {
            var PolicyForm = new FormStorePolicy();
            PolicyForm.Show();
        }

        private void BTStorePayment(object sender, EventArgs e)
        {
            var PaymentForm = new FormStorePayment();
            PaymentForm.Show();
        }
        private void BStoreClaim_Click(object sender, EventArgs e)
        {
            var claimForm = new FormStoreClaim();
            claimForm.Show();
        }

        private void btnPrivetKeySaha_Click(object sender, EventArgs e)
        {
            string strFolderPath = Application.StartupPath + "/SSLKey/";
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //*** Create Folder
                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }

                //*** Save File
                string filePath = dlg.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);
                File.Copy(filePath, strFolderPath + fileName, true);

                PivKeyFile.Text = fileName;

                MessageBox.Show("Õ—æ‚À≈¥‰ø≈Ï‡√’¬∫√ÈÕ¬·≈È«");
            }
        }



        private void PivKeyFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter_1(object sender, EventArgs e)
        {

        }

        private void radioButtonUAT_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonPRD_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void sslStatus_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void GenPolicyButton_Click(object sender, EventArgs e)
        {
            var GenPolicyForm = new FormGenPolicyTable();
            GenPolicyForm.Show();
        }

        private void listViewStorePoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}