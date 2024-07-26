using MVCIdentity.Areas.Identity.Data;

namespace MVCIdentity.Models
{
    public class OgrenciDers
    {
        public int Id { get; set; }
        public int DersId { get; set; }
        public Ders Ders { get; set; }
        public string MVCIdentityUserId { get; set; }
        public MVCIdentityUser MVCIdentityUser { get; set; }
    }
}
