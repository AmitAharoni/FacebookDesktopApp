using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
    public partial class FormFaceLikers : Form
    {
        private AppManager m_AppManager;
        private User m_LoggedInUser;
        private Dictionary<string, LikerData> m_LikersDic;
        private List<LikerData> m_SortedListByTotalLikes;

        private class CompareByTotalLikes : Comparer<LikerData>
        {
            public override int Compare(LikerData x, LikerData y)
            {
                return x.TotalLikes.CompareTo(y.TotalLikes);
            }
        }

        public FormFaceLikers()
        {
            InitializeComponent();
            m_AppManager = AppManager.Instance;
            m_LoggedInUser = m_AppManager.LoggedInUser;
            fetchAllLikesData();
        }

        private void fetchAllLikesData()
        {
            m_LikersDic = new Dictionary<string, LikerData>();

            fetchLikesList();
            sortLikesData();
            fillListBoxWithSortedData();
            initFirstSelect();
        }

        private void initFirstSelect()
        {
            if (listBoxSortFriendsList.Items.Count == 0)
            {
                labelSortByLikes.Text = "No Friends to show";
            }
            else
            {
                listBoxSortFriendsList.SelectedIndex = 0;
            }
        }

        private void fillListBoxWithSortedData()
        {
            foreach (LikerData likerData in m_SortedListByTotalLikes)
            {
                if (likerData.UserName != null)
                {
                    listBoxSortFriendsList.Items.Add(likerData.UserName);
                }
            }
        }

        private void sortLikesData()
        {
            m_SortedListByTotalLikes = m_LikersDic.Values.ToList<LikerData>();
            m_SortedListByTotalLikes.Sort(new CompareByTotalLikes());
        }

        private void fetchLikesList()
        {
            try
            {
                foreach (Album album in m_LoggedInUser.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        foreach (User liker in photo.LikedBy)
                        {
                            addToDictionaryIfNec(liker);
                            countLike(liker);
                            firstLikeUpdate(liker, photo);
                            lastLikeUpdate(liker, photo);
                            leastLikedPhotoUpdate(liker, photo);
                            mostLikedPhotoUpdate(liker, photo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Failed: " + ex.Message);
            }
        }

        private void addToDictionaryIfNec(User i_Liker)
        {
            if (!m_LikersDic.ContainsKey(i_Liker.Name))
            {
                m_LikersDic.Add(i_Liker.Name, new LikerData(i_Liker));
            }
        }

        private void countLike(User i_Liker)
        {
            m_LikersDic[i_Liker.Name].TotalLikes++;
        }

        private void firstLikeUpdate(User i_Liker, Photo i_Photo)
        {
            if (m_LikersDic[i_Liker.Name].FirstLikePhoto == null || m_LikersDic[i_Liker.Name].FirstLikePhoto.CreatedTime > i_Photo.CreatedTime)
            {
                m_LikersDic[i_Liker.Name].FirstLikePhoto = i_Photo;
            }
        }

        private void lastLikeUpdate(User i_Liker, Photo i_Photo)
        {
            if (m_LikersDic[i_Liker.Name].LastLikePhoto == null || m_LikersDic[i_Liker.Name].LastLikePhoto.CreatedTime < i_Photo.CreatedTime)
            {
                m_LikersDic[i_Liker.Name].LastLikePhoto = i_Photo;
            }
        }

        private void leastLikedPhotoUpdate(User i_Liker, Photo i_Photo)
        {
            if (m_LikersDic[i_Liker.Name].LeastLikedPhoto == null || m_LikersDic[i_Liker.Name].LeastLikedPhoto.LikedBy.Count > i_Photo.LikedBy.Count)
            {
                m_LikersDic[i_Liker.Name].LeastLikedPhoto = i_Photo;
            }
        }

        private void mostLikedPhotoUpdate(User i_Liker, Photo i_Photo)
        {
            if (m_LikersDic[i_Liker.Name].MostLikedPhoto == null || m_LikersDic[i_Liker.Name].MostLikedPhoto.LikedBy.Count < i_Photo.LikedBy.Count)
            {
                m_LikersDic[i_Liker.Name].MostLikedPhoto = i_Photo;
            }
        }

        private void updateLabelUserName(string i_SelectedName)
        {
            labelUserName.Text = i_SelectedName;
        }

        private void updateLabelTotalLikes()
        {
            string numOfTotalLikes = m_SortedListByTotalLikes.First().TotalLikes.ToString();
            labelTotalLikes.Text = string.Format("total likes: {0}", numOfTotalLikes);
        }

        private void updatePictureBoxProfileImage(string i_PictureNormalURL)
        {
            pictureBoxProfileImage.Load(i_PictureNormalURL);
        }

        private void updatePictureBoxFirstLikedPhoto(Photo i_PhotoToUpdate)
        {
            pictureBoxFirstLikedPhoto.Load(i_PhotoToUpdate.PictureNormalURL);
        }

        private void updatePictureBoxLastLikedPhoto(Photo i_PhotoToUpdate)
        {
            pictureBoxLastLikedPhoto.Load(i_PhotoToUpdate.PictureNormalURL);
        }

        private void updatePictureBoxLeastLikedPhoto(Photo i_PhotoToUpdate)
        {
            pictureBoxLeastLikedPhoto.Load(i_PhotoToUpdate.PictureNormalURL);
        }

        private void updatePictureBoxMostLikedPhoto(Photo i_PhotoToUpdate)
        {
            pictureBoxMostLikedPhoto.Load(i_PhotoToUpdate.PictureNormalURL);
        }

        private void listBoxSortFriendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedName = listBoxSortFriendsList.SelectedItem.ToString();
            LikerData selectedData = m_LikersDic[selectedName];
            showFriendStatisticsAndData(selectedName, selectedData);
        }

        private void showFriendStatisticsAndData(string i_SelectedName, LikerData i_SelectedData)
        {
            updateLabelUserName(i_SelectedName);
            updateLabelTotalLikes();
            updatePictureBoxProfileImage(i_SelectedData.ProfilePhotoURL);
            updatePictureBoxFirstLikedPhoto(i_SelectedData.FirstLikePhoto);
            updatePictureBoxLastLikedPhoto(i_SelectedData.LastLikePhoto);
            updatePictureBoxLeastLikedPhoto(i_SelectedData.LeastLikedPhoto);
            updatePictureBoxMostLikedPhoto(i_SelectedData.MostLikedPhoto);
        }

        private void buttonUnfriend_Click(object sender, EventArgs e)
        {
            m_LoggedInUser.Friends.Remove(listBoxSortFriendsList.SelectedItem as User);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_AppManager.MainForm.StartPosition = FormStartPosition.Manual;
            m_AppManager.MainForm.Location = this.Location;
            m_AppManager.MainForm.Show();
        }
    }
}