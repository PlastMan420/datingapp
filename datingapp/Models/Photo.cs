namespace datingapp.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public System.DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicID { get; set; }
        public User user { get; set; }  // to have deletable photos automatically, 
                                        // we do the inverse of what we did in the user.cs class
                                        // we initiate user in the photos class.
        public int UserId { get; set; }
    }
}