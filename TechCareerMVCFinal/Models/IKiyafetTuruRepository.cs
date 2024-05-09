namespace TechCareerMVCFinal.Models
{
    public interface IKiyafetTuruRepository : IRepository<KiyafetTuru>
    {
        void Guncelle(KiyafetTuru kiyafetTuru);
        void Kaydet();
    }
}
