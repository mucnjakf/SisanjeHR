using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.SubscriptionViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class SubscriptionController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetSubscriptions")]
        public IEnumerable<Subscription> GetSubscriptions()
        {
            return unitOfWork.Subscriptions.GetAll();
        }
        
        [HttpPost]
        [Route("api/InsertSubscription")]
        public bool InsertSubscription(SubscriptionInsertVM subscriptionInsertVM)
        {
            Subscription subscription = new Subscription()
            {
                Type = subscriptionInsertVM.Type,
                Price = subscriptionInsertVM.Price
            };

            unitOfWork.Subscriptions.Add(subscription);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditSubscription")]
        public bool EditSubscription(SubscriptionEditVM subscriptionEditVM)
        {
            Subscription subscription = unitOfWork.Subscriptions.Get(subscriptionEditVM.Id);
            subscription.Type = subscriptionEditVM.Type;
            subscription.Price = subscriptionEditVM.Price;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteSubscription/{id}")]
        public bool DeleteSubscription(int id)
        {
            Subscription subscription = unitOfWork.Subscriptions.Get(id);

            unitOfWork.Subscriptions.Remove(subscription);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}