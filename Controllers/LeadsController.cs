using CRMLeades.Data;
using CRMLeades.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRMLeades.Controllers
{
    public class LeadsController : Controller
    {
        public IActionResult Index()
        {
            List <LeadsEntity> leads = new List<LeadsEntity> ();
            LeadRepository leadRepository = new LeadRepository();

            leads = leadRepository.GetAllLeads();
            return View(leads);
        }

        public IActionResult AddLead()
        {
            return View();
        }

        public IActionResult AddNewLead(LeadsEntity leadDetails)
        {
            try {
                if (ModelState.IsValid)
                {
                    if (leadDetails.NextFollowUpDate <= leadDetails.LeadDate)
                    {
                        ViewBag.Message = "Follow up date cannot be less than Lead date.";
                        return View("AddLead");
                    }

                    LeadRepository _DbLead = new LeadRepository();
                    if (_DbLead.AddLead(leadDetails))
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public IActionResult EditLead (int Id)
        {
            LeadsEntity leads = new LeadsEntity();
            LeadRepository leadRepository = new LeadRepository();

            leads = leadRepository.GetLeadById22(Id);
            return View(leads);
        }

        public IActionResult EditLeadDetails (int Id, LeadsEntity leadDetails) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (leadDetails.NextFollowUpDate <= leadDetails.LeadDate)
                    {
                        ViewBag.Message = "Follow up date cannot be less than Lead date.";
                        return View("AddLead");
                    }

                    LeadRepository _DbLead = new LeadRepository();
                    if (_DbLead.EditLeadDetails(Id, leadDetails))
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult DeleteLead(int Id)
        {
            LeadsEntity leads = new LeadsEntity();
            LeadRepository leadRepository = new LeadRepository();

            leads = leadRepository.GetLeadById22(Id);
            return View(leads);

        }
        public IActionResult DeleteLeadDetails(int Id, LeadsEntity leadDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (leadDetails.NextFollowUpDate <= leadDetails.LeadDate)
                    {
                        ViewBag.Message = "Follow up date cannot be less than Lead date.";
                        return View("AddLead");
                    }

                    LeadRepository _DbLead = new LeadRepository();
                    if (_DbLead.DeleteLeadDetails(Id, leadDetails))
                    {
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        ViewBag.Message = "Error occurred while deleting the lead.";
                        return View("Error", leadDetails);
                    }

                }
                else
                {
                    return View("Error", leadDetails);
                }
               

            }
            catch (Exception ex)
            {
                ViewBag.Message = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }

        
    }
}
