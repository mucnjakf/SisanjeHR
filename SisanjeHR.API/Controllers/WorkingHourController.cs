using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Utilities.ViewModels.WorkingHourViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace SisanjeHrApi.Controllers
{
    public class WorkingHourController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork(new SisanjeHrDbContext());

        [HttpGet]
        [Route("api/GetWorkingHours")]
        public IEnumerable<WorkingHour> GetWorkingHours()
        {
            return unitOfWork.WorkingHours.GetAll();
        }

        [HttpPost]
        [Route("api/InsertWorkingHour")]
        public bool InsertWorkingHour(WorkingHourInsertVM workingHourInsertVM)
        {
            WorkingHour workingHour = new WorkingHour()
            {
                DayInWeek = workingHourInsertVM.DayInWeek,
                TimeStart = workingHourInsertVM.TimeStart,
                TimeEnd = workingHourInsertVM.TimeEnd
            };

            unitOfWork.WorkingHours.Add(workingHour);

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpPut]
        [Route("api/EditWorkingHour")]
        public bool EditWorkingHour(WorkingHourEditVM workingHourEditVM)
        {
            WorkingHour workingHour = unitOfWork.WorkingHours.Get(workingHourEditVM.Id);
            workingHour.DayInWeek = workingHourEditVM.DayInWeek;
            workingHour.TimeStart = workingHourEditVM.TimeStart;
            workingHour.TimeEnd = workingHourEditVM.TimeEnd;

            int success = unitOfWork.Complete();
            return success > 0;
        }

        [HttpDelete]
        [Route("api/DeleteWorkingHour/{id}")]
        public bool DeleteWorkingHour(int id)
        {
            WorkingHour workingHour = unitOfWork.WorkingHours.Get(id);

            unitOfWork.WorkingHours.Remove(workingHour);

            int success = unitOfWork.Complete();
            return success > 0;
        }
    }
}
