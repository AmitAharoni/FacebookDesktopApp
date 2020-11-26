using System;
using System.Windows.Forms;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
    public partial class FormLogIn : Form
    {
        private AppManager m_AppManager;

        public FormLogIn()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            m_AppManager = AppManager.Instance;
            try
            {
                m_AppManager.Login();
                this.Hide();
                m_AppManager.MainForm.StartPosition = FormStartPosition.Manual;
                m_AppManager.MainForm.Location = this.Location;
                m_AppManager.MainForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
        }
    }
}