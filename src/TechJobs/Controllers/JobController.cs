using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        private static JobData JobData { get => jobData; set => jobData = value; }

        // The detail display for a given Job at URLs like /Job?id=17
        // TODO #1 - get the Job with the given ID and pass it into the view
        public IActionResult Index(int id)
        {
            Job singlejob = JobData.Find(id);

            return View(singlejob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (ModelState.IsValid)
            {
                return View(newJobViewModel);
            }

            else
            {
                JobData data = JobData.GetInstance();
                Job newJob = new Job();
             
                newJob.Name = newJobViewModel.Name;
                newJob.Employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                newJob.Location = jobData.Locations.Find(newJobViewModel.LocationID);
                newJob.CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                newJob.PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);


                JobData.Jobs.Add(newJob);
                return Redirect(string.Format("/Job?id={0}",  + newJob.ID));
            }
        }
    }
}
