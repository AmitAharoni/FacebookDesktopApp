using System;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
    public partial class FormMain : Form
    {
        private AppManager m_AppManager;
        private User m_LoggedInUser;

        public FormMain()
        {
            InitializeComponent();
            m_AppManager = AppManager.Instance;
            m_LoggedInUser = m_AppManager.LoggedInUser;
            fetchUserData();
        }

        private void fetchUserData()
        {
            pictureBoxProfileImage.Load(m_LoggedInUser.PictureLargeURL);
            labelUserName.Text = m_LoggedInUser.Name;
            labelGender.Text = m_LoggedInUser.Gender.ToString();
            labelLocation.Text = m_LoggedInUser.Location.Name;
            labelEmail.Text = m_LoggedInUser.Email.ToString();
            labelBirthDay.Text = m_LoggedInUser.Birthday.ToString();
        }

        private void fetchFriends()
        {
            try
            {
                foreach (User friend in m_LoggedInUser.Friends)
                {
                    if (!listBoxFriends.Items.Contains(friend.Name))
                    {
                        listBoxFriends.Items.Add(friend.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed:   " + ex.Message);
            }
        }

        private void fetchPosts()
        {
            try
            {
                foreach (Post post in m_LoggedInUser.WallPosts)
                {
                    if (post.Message != null)
                    {
                        if (!listBoxPosts.Items.Contains(post.Message))
                        {
                            listBoxPosts.Items.Add(post.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
        }

        private void fetchCheckIns()
        {
            try
            {
                foreach (Checkin checkIn in m_LoggedInUser.Checkins)
                {
                    if (!listBoxCheckIn.Items.Contains(checkIn.Place.Name))
                    {
                        listBoxCheckIn.Items.Add(checkIn.Place.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
        }

        private void labelFriends_Click(object sender, EventArgs e)
        {
            fetchFriends();
        }

        private void labelPosts_Click(object sender, EventArgs e)
        {
            fetchPosts();
        }

        private void labelCheckIns_Click(object sender, EventArgs e)
        {
            fetchCheckIns();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            m_AppManager.Logout();
            this.Hide();
            m_AppManager.LoginForm.StartPosition = FormStartPosition.Manual;
            m_AppManager.LoginForm.Location = this.Location;
            m_AppManager.LoginForm.Show();
        }

        private void buttonCheckinFeature_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_AppManager.CheckInForm.StartPosition = FormStartPosition.Manual;
            m_AppManager.CheckInForm.Location = this.Location;
            m_AppManager.CheckInForm.Show();
        }

        private void buttonFriendRaterFeature_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_AppManager.FriendRaterForm.StartPosition = FormStartPosition.Manual;
            m_AppManager.FriendRaterForm.Location = this.Location;
            m_AppManager.FriendRaterForm.Show();
        }
    }
}