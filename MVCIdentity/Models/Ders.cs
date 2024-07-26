namespace MVCIdentity.Models
{
    public class Ders
    {
        public int Id { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int Kredi { get; set; }
        public List<OgrenciDers> OgrenciDersler { get; set; }
    }
}
