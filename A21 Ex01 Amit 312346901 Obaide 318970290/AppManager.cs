using System;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
    public sealed class AppManager
    {
        private const string AppId = "2695170000736846";
        private static readonly object sr_CreationalLockContect = new object();
        private static AppManager s_Instance = null;
        private LoginResult m_LoginResult; 
        private FormLogIn m_FormLogin = null;
        private FormMain m_FormMain = null; 
        private FormCheckInRecommendation m_FormCheckIn = null;
        private FormFaceLikers m_FormFriendRater = null;

        public static AppManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_CreationalLockContect)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new AppManager();
                        }
                    }
                }

                return s_Instance;
            }
        }

        public FormFaceLikers FriendRaterForm
        {
            get
            {
                if (m_FormFriendRater == null)
                {
                    m_FormFriendRater = new FormFaceLikers();
                }

                return m_FormFriendRater;
            }
        }

        public FormMain MainForm
        {
            get
            {
                if (m_FormMain == null)
                {
                    m_FormMain = new FormMain();
                }

                return m_FormMain;
            }
        }

        public FormCheckInRecommendation CheckInForm
        {
            get
            {
                if (m_FormCheckIn == null)
                {
                    m_FormCheckIn = new FormCheckInRecommendation();
                }

                return m_FormCheckIn;
            }
        }

        public FormLogIn LoginForm
        {
            get
            {
                if (m_FormLogin == null)
                {
                    m_FormLogin = new FormLogIn();
                }

                return m_FormLogin;
            }
        }

        private AppManager()
        {
        }

        public User LoggedInUser
        {
            get
            {
                if (m_LoginResult != null)
                {
                    return m_LoginResult.LoggedInUser;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Login()
        {
            try
            {
                m_LoginResult = FacebookWrapper.FacebookService.Login(
                                                            AppId,
                                                            "email",
                                                            "user_gender",
                                                            "user_birthday",
                                                            "user_friends",
                                                            "user_posts",
                                                            "user_events",
                                                            "user_likes",
                                                             "user_tagged_places",
                                                             "public_profile",
                                                             "publish_to_groups",
                                                             "user_age_range",
                                                             "user_link",
                                                             "user_videos",
                                                             "groups_access_member_info",
                                                             "user_location",
                                                             "user_photos",
                                                             "user_hometown",
                                                             "publish_to_groups",
                                                             "pages_read_engagement",
                                                             "pages_manage_posts");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
        }

        public void Logout()
        {
            try
            {
                FacebookService.Logout(null, AppId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
            finally
            {
                m_FormMain = null;
                m_FormCheckIn = null;
                m_FormFriendRater = null;
            }
        }
    }
}