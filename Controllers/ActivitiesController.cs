using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ProgramContext _context;

        public ActivitiesController(ProgramContext context)
        {
            _context = context;
        }

        // GET: Activities
        public async Task<IActionResult> Index()
        {
            var meetingList = new List<Meeting>();

            foreach (Meeting m in _context.Meetings) 
            {
                meetingList.Add(m);
            }

            ViewBag.Wards = meetingList;

            return _context.Activities != null ?
                        View(await _context.Activities.ToListAsync()) :
                        Problem("Entity set 'ProgramContext.Activities'  is null.");
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (activity == null)
            {
                return NotFound();
            }

            foreach (Meeting m in _context.Meetings)
            {
                if (m.Id == activity.MeetingID)
                {
                    ViewData["ViewWard"] = m.Ward;
                    ViewData["MeetingDate"] = m.Date.ToShortDateString();
                }
            }

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            PopulateMeetingsDropDownList();

            return View();
        }

        // POST: Activities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityID,MeetingID,EventName,EventInfo,EventFooter,Order")] Activity activity, int selectedMeeting)
        {
            bool testResult = ValidateActivity(activity, selectedMeeting);

            if (testResult == true)
            {
                PopulateMeetingsDropDownList();
                ViewData["ErrorMessage"] = "You already have this Activity in that meeting.";

                return View(activity);
            }

            testResult = ValidateActivityInfo(activity);

            if (testResult == false)
            {
                PopulateMeetingsDropDownList();
                ViewData["NullMessage"] = $"You must have this field blank while '{activity.EventName}' is selected.";

                return View(activity);
            }

            if (ModelState.IsValid)
            {
                activity.MeetingID = selectedMeeting;

                _context.Add(activity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateMeetingsDropDownList();

            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            PopulateMeetingsDropDownList();
            return View(activity);
        }

        // POST: Activities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityID,MeetingID,EventName,EventInfo,EventFooter,Order")] Activity activity, int selectedMeeting)
        {
            if (id != activity.ActivityID)
            {
                return NotFound();
            }
            bool testResult = ValidateActivity(activity, selectedMeeting, id);

            if (testResult == true)
            {
                PopulateMeetingsDropDownList();
                ViewData["ErrorMessage"] = "You already have this Activity in that meeting.";

                return View(activity);
            }
            testResult = ValidateActivityInfo(activity);

            if (testResult == false)
            {
                PopulateMeetingsDropDownList();

                ViewData["NullMessage"] = $"You must have this field blank while '{activity.EventName}' is selected.";

                return View(activity);
            }

            if (ModelState.IsValid)
            {

                activity.MeetingID = selectedMeeting;
                var activityToUpdate = await _context.Activities.FirstOrDefaultAsync(s => s.ActivityID == id);
                activityToUpdate.MeetingID = selectedMeeting;
                if (await TryUpdateModelAsync<Activity>(
                    activityToUpdate,
                    "",
                    s => s.EventName, s => s.EventInfo, s => s.EventFooter, s => s.MeetingID, s => s.Order))
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                }

            }

            PopulateMeetingsDropDownList();

            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (activity == null)
            {
                return NotFound();
            }

            foreach (Meeting m in _context.Meetings)
            {
                if (m.Id == activity.MeetingID)
                {
                    ViewData["ViewWard"] = m.Ward;
                    ViewData["MeetingDate"] = m.Date.ToShortDateString();
                }
                else
                {
                    ViewData["ViewWard"] = "None";
                    ViewData["MeetingDate"] = "None";
                }
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Activities == null)
            {
                return Problem("Entity set 'ProgramContext.Activities'  is null.");
            }
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return (_context.Activities?.Any(e => e.ActivityID == id)).GetValueOrDefault();
        }

        // LOAD: Department info for the drop-down list.
        private void PopulateMeetingsDropDownList(object? selectedDepartment = null)
        {
            var meetingsQuery = from m in _context.Meetings
                                orderby m.Ward
                                select m;
            ViewBag.MeetingsID = new SelectList(meetingsQuery.AsNoTracking(), "Id", "Ward", selectedDepartment);
        }

        // VALIDATE: Custom validation for the Create/Edit pages. This checks to see if we have duplicate activites in one
        // meeting.
        private bool ValidateActivity(Activity activity, int meetingId, int? id = null)
        {
            if (activity.EventName != "Speaker" && activity.EventName != "Youth Speaker" && activity.EventName != "Article of Faith")
            {
                if (id != null)
                {
                    int countActivity = 0;
                    int thatId = 0;
                    foreach (Activity a in _context.Activities)
                    {
                        if (a.EventName == activity.EventName && a.ActivityID == id)
                        {
                            countActivity += 1;
                            thatId = a.ActivityID;
                        }
                    }

                    if (countActivity == 1 && thatId == id)
                    {
                        return false;
                    }
                }
                foreach (Activity a in _context.Activities)
                {

                    if (a.MeetingID == meetingId && a.EventName == activity.EventName)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        private bool ValidateActivityInfo(Activity activity)
        {
            // Create a check to see if values in EventInfo that should be null are not null.
            // Second Test, see if this object's info needs to be null.
            if (activity.EventName == "Ward Buisness")
            {
                if (activity.EventInfo != null)
                {
                    // Test Failed return true.
                    return false;
                }
            }

            else if (activity.EventName == "Passing of the Sacrament")
            {
                if (activity.EventInfo != null)
                {
                    // Test Failed return true.
                    return false;
                }
            }

            else if (activity.EventName == "Testimonies")
            {
                if (activity.EventInfo != null)
                {
                    // Test Failed return true.
                    return false;
                }
            }

            // If we are ok send back a true because we passed!
            return true;
        }

        // PRINT: Code for the Print page.
        public async Task<IActionResult> Print(int? printMeeting = null)
        {
            var meeting = new Meeting();

            // Set a value to print meeting to trigger the class that will stop the div
            // from showing until printmeeting != null.

            if (printMeeting != null) 
            {
                ViewData["printMeetingShow"] = "printShow";
            }

            if (_context.Meetings == null)
            {
                return Problem("Entity set 'MvcMovieContext.Meetings' is null.");
            }

            // Get the meeting.
            foreach (Meeting m in _context.Meetings)
            {
                if (m.Id == printMeeting)
                {
                    meeting = m;
                }
            }

            // Get the Activites.

            var meetingActivities = new List<Activity>();

            foreach (Activity a in _context.Activities)
            {
                if (a.MeetingID == printMeeting)
                {
                    meetingActivities.Add(a);
                }
            }

            //Order the list.
            meetingActivities = meetingActivities.OrderBy(a => a.Order).ToList();

            // Add the title of the meeting.
            ViewData["MeetingWard"] = meeting.Ward;
            if (printMeeting == 0)
            {
                ViewData["MeetingDate"] = "";
            }
            else
            {
                ViewData["MeetingDate"] = meeting.Date.ToShortDateString();
            }
            ViewData["MeetingAddress"] = meeting.Address;

            // Save to printList so it can display them in the view.

            ViewBag.PrintList = meetingActivities;

            PopulateMeetingsDropDownList();
            return View();
        }
    }
}