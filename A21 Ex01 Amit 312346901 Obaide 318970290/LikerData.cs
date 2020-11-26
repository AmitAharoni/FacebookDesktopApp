using FacebookWrapper.ObjectModel;

namespace A21_Ex01_Amit_312346901_Obaide_318970290
{
    public class LikerData
    {
        public string UserName { get; private set; }

        public int TotalLikes { get; set; }

        public string ProfilePhotoURL { get; set; }

        public Photo FirstLikePhoto { get; set; }

        public Photo LastLikePhoto { get; set; }

        public Photo MostLikedPhoto { get; set; }

        public Photo LeastLikedPhoto { get; set; }

        public LikerData(User liker)
        {
            UserName = liker.Name;
            TotalLikes = 0;
            ProfilePhotoURL = liker.PictureNormalURL;
            FirstLikePhoto = null;
            LastLikePhoto = null; 
            MostLikedPhoto = null;
            LeastLikedPhoto = null;
        }
    }
}