using LabsAndManagement.API.Data;
using LabsAndManagement.Business;

namespace LabsAndManagement.API.Features.Labs
{
    public class LabRepository : ILabRepository
    {
        private readonly DatabaseContext context;

        public LabRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Lab lab)
        {
            this.context.Labs.Add(lab);
            this.context.SaveChanges();
        }

        public void Update(Lab lab)
        {
            this.context.Labs.Update(lab);
            this.context.SaveChanges();
        }

        public void Delete(string id)
        {
            var lab = this.context.Labs.FirstOrDefault(l => l.Id == id);
            if (lab == null)
            {
                return;
            }
            this.context.Labs.Remove(lab);
            this.context.SaveChanges();
        }

        public IEnumerable<Lab> GetAll()
        {
            return this.context.Labs.ToList();
        }
    }
}
