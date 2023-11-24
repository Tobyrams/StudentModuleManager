using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_TEST_ONE.Areas.Identity.Data;
using POE_TEST_ONE.Data;
using POE_TEST_ONE.Models;

namespace POE_TEST_ONE.Controllers
{
    [Authorize]
    public class ModuleController : Controller
    {
        //This is used to store an instance of the ApplicationDbContex class in _db which contains property Modules to store all the moduels
        private readonly ApplicationDbContext _db;

        //This will be used to manager the applicatio users eg.Registeer,Login,Logout
        private readonly UserManager<ApplicationUser> _userManager;


        // Constructor for CategoryController that takes an instance of ApplicationDbContext.
        public ModuleController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;// Assign the injected database context to the private field _db.
            this._userManager = userManager;// Assign the injected database context to the private field _userManager.
        }



        #region View Modules ========================================================
        public IActionResult Index()
        {
            //This gets the current users Id
            string currentUserId = _userManager.GetUserId(this.User);

            // returns only the modules for the Current user
            var userModules = _db.Modules.Where(m => m.UserId.Equals(currentUserId)).ToList();

            //Returns a list of modules of the currnet user only
            return View(userModules);

        }
        #endregion


        #region Create / Add Module ==============================================================

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Module obj)
        {
            //Validations Start👇

            //This makes sure that the Module code and name cannot be the same
            if (obj.Module_code == obj.Module_Name)
            {
                ModelState.AddModelError("Name", "The Module code and name cannot be the same");
            }
            //This checks if the calculation of the self study hours is less than zero
            if (PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(obj.Num_credits, obj.Num_weeks_in_semester, obj.Class_hours_per_week) < 0)
            {
                ModelState.AddModelError("Name", "Invalid Self Study Hours");
            }
            //This checks if the calculation of the remaining study hours is less than zero
            if (PARTONE_CLASSLIBRARY.Calculations.remainingStudyHours(PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(obj.Num_credits, obj.Num_weeks_in_semester, obj.Class_hours_per_week), obj.Hours_Studied_On_CertinDate) < 0)
            {
                ModelState.AddModelError("Name", "Invalid Remaining Study Hours");
            }
            //Validations End👆

            if (ModelState.IsValid)
            {
                // Assing the self study hours
                obj.Self_Study_hours = PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(obj.Num_credits, obj.Num_weeks_in_semester, obj.Class_hours_per_week);
                // Assing the remaining study hours
                obj.Remaining_study_hours = PARTONE_CLASSLIBRARY.Calculations.remainingStudyHours(PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(obj.Num_credits, obj.Num_weeks_in_semester, obj.Class_hours_per_week), obj.Hours_Studied_On_CertinDate);
                //Assign the userId
                obj.UserId = _userManager.GetUserId(this.User);

                _db.Modules.Add(obj);//Adding the modules to the Modules List of type Module
                _db.SaveChanges();//Adding the Modules to the database
                TempData["success"] = "Module Created successfully"; //Success popup message at top right of page
                return RedirectToAction("Index"); //Redirecting back to the Modules home page 
            }
            return View();
        }

        #endregion


        #region Delete ==============================================================
        public IActionResult Delete(int? id)
        {
            //If id is null or 0 return a NotFound error
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            //Finding the module by the id in the database
            Module moduleFromDb = _db.Modules.Find(id);

            //If module is not found we return a not found error
            if (moduleFromDb == null)
            {
                return NotFound();
            }

            return View(moduleFromDb);// Returns the module to the view
        }

        //Post
        /*(ActionName)👇
         * Here we use DeletePost cause we can't have the same view and
        Post name so we tell the program that this 'DeletePost' is
        still part of the Delete just has a different name*/
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            //Fist we have to find the Module from the database
            Module obj = _db.Modules.Find(id);

            if (obj == null) //If Module is null
            {
                return NotFound();
            }
            //if found we new remove it from the Module database
            _db.Modules.Remove(obj);
            //we save all changes to the database
            _db.SaveChanges();
            TempData["success"] = "Module Removed successfully"; //Success popup message at top right of page
            //We direct the user back to the index/Home page for category
            return RedirectToAction("Index");
        }

        #endregion


        #region Graph ===========================================================

        public IActionResult Graph()
        {
            return View();
        }

        [HttpPost]
        public List<object> GetSelfStudyHours()
        {

            //This sets the session user it into this variable to be used to insert into the Modules database
            string currentUserId = _userManager.GetUserId(this.User);

            List<object> data = new List<object>();// Data that will be displayed in the graph

            //The following will select only the modules of the current user and not of other users
            List<string> code = _db.Modules
                                .Where(c => c.UserId == currentUserId) // Filter based on UserId
                                .Select(c => c.Module_code)
                                .ToList();

            //Gets the selfStudyHours of the current user
            List<int> selfStudyHours = _db.Modules
                                        .Where(c => c.UserId == currentUserId)
                                        .Select(s => s.Hours_Studied_On_CertinDate)
                                        .ToList();

            data.Add(code); //Adding the Module code to the data
            data.Add(selfStudyHours); //Adds the selfStudyHours to the data
            return data;
        }

        #endregion


        #region Edit ==========================================================
        public IActionResult Edit(int? id) //Get the users id we pass it as a perameter **Url: https:localhost:7000/Category/Edit/1 **
        {
            //Validation for if id is null(empty) or 0
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //If valid we need to retireve that value fronm the database
            Module moduleFromDb = _db.Modules.FirstOrDefault(u => u.Id == id);

            if (moduleFromDb == null) //If Module if empty/null
            {
                return NotFound();
            }

            return View(moduleFromDb);// If found well pass it to  our display and view it 
        }



        /* Post explination
         Here I use ActionName to tell the program that this is the post for the edit from above
        with a different name as we cant have the same name for the actionresult when they both 
        recieve the same id paramater. So I'm giving it a different name even though it's the post
        for the ecit action result.
         */
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Retrieving the specific module from the database
            Module moduleFromDb = _db.Modules.FirstOrDefault(u => u.Id == id);

            //If module was not found we return a notfound error
            if (moduleFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (await TryUpdateModelAsync(moduleFromDb, "", m => m.Module_code, m => m.Module_Name, m => m.Num_credits, m => m.Class_hours_per_week, m => m.Num_weeks_in_semester, m => m.Semester_start_date, m => m.Studied_on_certin_date, m => m.Hours_Studied_On_CertinDate, m => m.Self_Study_hours, m => m.Remaining_study_hours))
                {

                    // Assing the self study hours
                    moduleFromDb.Self_Study_hours = PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(moduleFromDb.Num_credits, moduleFromDb.Num_weeks_in_semester, moduleFromDb.Class_hours_per_week);
                    // Assing the remaining study hours
                    moduleFromDb.Remaining_study_hours = PARTONE_CLASSLIBRARY.Calculations.remainingStudyHours(PARTONE_CLASSLIBRARY.Calculations.self_Study_Hours(moduleFromDb.Num_credits, moduleFromDb.Num_weeks_in_semester, moduleFromDb.Class_hours_per_week), moduleFromDb.Hours_Studied_On_CertinDate);

                    //Exception error to catch if the Hours studied on certin day exceeds the remaining study hours
                    if (moduleFromDb.Remaining_study_hours < 0)
                    {
                        ModelState.AddModelError("", "ERROR, Your input of 'Hours Studied on day' exceeds the 'Self Study Hours'. Ensure the Hours Studied on day doesn't exceed the Self Study Hours  ");
                        return View(moduleFromDb);
                    }
                    
                    //If the Module namd and code are the same show the user an error message
                    if (moduleFromDb.Module_code == moduleFromDb.Module_Name)
                    {
                        ModelState.AddModelError("", "ERROR, Module Code and Name cannot be the same ");
                        return View(moduleFromDb);
                    }

                    await _db.SaveChangesAsync(); //Saves the changes to the database
                    TempData["success"] = "Module Updated successfully"; //Success popup message at top right of page
                    return RedirectToAction("Index");
                }

            }

            //Here we return the newly updated value back to the edit view.
            return View(moduleFromDb);
        }

        #endregion




    }
}
