namespace TechCareerMVCFinal.Models
{
    public interface IKiyafetRepository : IRepository<Kiyafet>
    {
        void Guncelle(Kiyafet kiyafet);
        void Kaydet();
    }
}
