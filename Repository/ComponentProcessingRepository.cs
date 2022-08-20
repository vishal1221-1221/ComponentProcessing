using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ComponentProcessing.API.Data;
using ComponentProcessing.API.Models;
using ComponentProcessing.API.Repository.IRepository;

namespace ComponentProcessing.API.Repository
{
    public class ComponentProcessingRepository : IComponentProcessingRepository

    {
        private readonly ApplicationDbContext _db;

        public ComponentProcessingRepository(ApplicationDbContext db)
        {
            _db = db;
        }
 
        public bool ComponentProcessingExists(string name)
        {
            bool value = _db.componentProcessings.Any(a => a.name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ComponentProcessingExists(int id)
        {
            return _db.componentProcessings.Any(a => a.requestId == id);
        }

        public bool CreateComponentProcessing(ComponentProcessingModel componentProcessing)
        {
   
            var comType = componentProcessing.componentType;

            if (comType == "Integral")
            {
                componentProcessing.totalCharges =  500;
                componentProcessing.dateOfDelivery = DateTime.Now.AddDays(5);
            }
            else
            {
                componentProcessing.totalCharges =  300;
                componentProcessing.dateOfDelivery = DateTime.Now.AddDays(2);
            }
            componentProcessing.status = "Unfulfill";
            _db.componentProcessings.Add(componentProcessing);
            return Save();
        }

        public bool DeleteComponentProcessing(ComponentProcessingModel componentProcessing)
        {
            _db.componentProcessings.Remove(componentProcessing);
            return Save();
        }

        public ComponentProcessingModel GetComponentProcessing(int ComponentProcessingID)
        {
            return _db.componentProcessings.FirstOrDefault(e => e.requestId == ComponentProcessingID);
        }

        public ICollection<ComponentProcessingModel> GetComponentProcessings()
        {
            return _db.componentProcessings.OrderBy(a => a.name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateComponentProcessing(int cpId, ComponentProcessingModel componentProcessing)
        {

            var getData = _db.componentProcessings.Where(record => record.requestId == cpId).FirstOrDefault();
            getData.status = componentProcessing.status;
            _db.componentProcessings.Update(getData);
            return Save();
        }
    }
}
