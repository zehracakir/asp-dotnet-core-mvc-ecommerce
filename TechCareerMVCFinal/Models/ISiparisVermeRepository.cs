namespace TechCareerMVCFinal.Models
{
    public interface ISiparisVermeRepository : IRepository<SiparisVerme>
    {
        void Guncelle(SiparisVerme siparisVerme);
        void Kaydet();
    }
}
