using LabsAndManagement.Business;

namespace LabsAndManagement.API.Features.Labs
{
    public interface ILabRepository
    {
        void Add(Lab lab);
        void Delete(string id);
        IEnumerable<Lab> GetAll();
        void Update(Lab lab);
    }
}