using WorkBook.DTOs;
using WorkBook.repos;

namespace WorkBook.Services
{
    public class ProfessionServices
    {
        private DbServices _db;

        public ProfessionServices()
        {
            _db = new DbServices();
        }

        public List<ProfessionWorkers> GetWorkersForProfession()
        {
            var res = _db.GetWorkersForProfession();
            return res;

        }

        public List<TopProfessionWorkers> GetTopWorkersForProfession()
        {
            var topProfession = _db.GetTopProfessionWorkers();
            return topProfession;
        }
        


    }
}
