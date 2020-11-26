using System;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
    public partial class FormCheckInRecommendation : Form
    {
        private readonly string r_ImagesTemplateUrl;
        private readonly string r_VideosTemplateUrl;
        private readonly string r_WeatherTemplateUrl;
        private readonly string r_RestaurantsTemplateUrl;
        private Uri m_URL;
        private string m_Location;
        private ePage m_CureentPage;
        private AppManager m_AppManager;

        public FormCheckInRecommendation()
        {
            InitializeComponent();
            m_AppManager = AppManager.Instance;
            webBrowser.ScriptErrorsSuppressed = true; 
            r_ImagesTemplateUrl = "https://www.google.com/search?q={0}&tbm=isch&q=findSomeImage";
            r_VideosTemplateUrl = "https://www.google.com/search?q={0}&tbm=vid";
            r_WeatherTemplateUrl = "https://www.google.com/search?q={0}+weather";
            r_RestaurantsTemplateUrl = "https://www.yelp.com/search?find_desc=Restaurants&find_loc={0}%2C+CA&ns=1";
            splitContainer1.Dock = DockStyle.Fill;
            fetchCheckIns();
        }

        private enum ePage
        {
            Images,
            Videos,
            Weather,
            Resturants
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_AppManager.MainForm.StartPosition = FormStartPosition.Manual;
            m_AppManager.MainForm.Location = this.Location;
            m_AppManager.MainForm.Show();
        }

        private void fetchCheckIns()
        {
            try
            {
                foreach (Checkin checkIn in m_AppManager.LoggedInUser.Checkins)
                {
                    if (!listBoxCheckIns.Items.Contains(checkIn.Place.Name))
                    {
                        listBoxCheckIns.Items.Add(checkIn.Place.Name);
                    }
                }

                if (listBoxCheckIns.Items.Count == 0)
                {
                    checkInLable.Text = "No checkIn to show";
                }
                else
                {
                    m_CureentPage = ePage.Images;
                    listBoxCheckIns.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
        }

        private bool isCheckInEmpty()
        {
            return listBoxCheckIns.Items.Count == 0;
        }

        private void imagesButton_Click(object sender, EventArgs e)
        {
            if (!isCheckInEmpty())
            {
                m_URL = new Uri(string.Format(r_ImagesTemplateUrl, m_Location));
                webBrowser.Navigate(m_URL);
                m_CureentPage = ePage.Images;
            }
        }

        private void videosButton_Click(object sender, EventArgs e)
        {
            if (!isCheckInEmpty())
            {
                m_URL = new Uri(string.Format(r_VideosTemplateUrl, m_Location));
                webBrowser.Navigate(m_URL);
                m_CureentPage = ePage.Videos;
            }
        }

        private void weatherButton_Click(object sender, EventArgs e)
        {
            if (!isCheckInEmpty())
            {
                m_URL = new Uri(string.Format(r_WeatherTemplateUrl, m_Location));
                webBrowser.Navigate(m_URL);
                m_CureentPage = ePage.Weather;
            }
        }

        private void restaurantsButton_Click(object sender, EventArgs e)
        {
            if (!isCheckInEmpty())
            {
                m_URL = new Uri(string.Format(r_RestaurantsTemplateUrl, m_Location));
                webBrowser.Navigate(m_URL);
                m_CureentPage = ePage.Resturants;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ListBox)
            {
                m_Location = (sender as ListBox).Text.Replace(' ', '+');
                refreshPage(sender, e);
            }
        }

        private void refreshPage(object i_Sender, EventArgs i_Arguments)
        {
            switch (m_CureentPage)
            {
                case ePage.Images:
                    imagesButton_Click(i_Sender, i_Arguments);
                    break;
                case ePage.Videos:
                    videosButton_Click(i_Sender, i_Arguments);
                    break;
                case ePage.Weather:
                    weatherButton_Click(i_Sender, i_Arguments);
                    break;
                case ePage.Resturants:
                    restaurantsButton_Click(i_Sender, i_Arguments);
                    break;
                default:
                    imagesButton_Click(i_Sender, i_Arguments);
                    break;
            }
        }
    }
}
