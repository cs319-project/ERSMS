using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Utilities
{
    /// <summary>A singleton class to generate a collection of to-do items.</summary>
    public static class ToDoListSeeder
    {
        /// <summary>Returns a list of to-do items for seeding a student's to-do list with the pre-determined items.</summary>
        /// <returns>A collection of to-do items.</returns>
        public static ICollection<ToDoItem> studentToDoListChecklistSeeding()
        {
            ICollection<ToDoItem> listToReturn = new List<ToDoItem>();
            listToReturn.Add(new ToDoItem
            {
                Title = "Check Deadlines and Procedures",
                Description = "Visit the web page of the host university and learn the application deadline and procedures."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Contact Departmental Exchange Coordinator",
                Description = "Contact your Departmental Exchange Coordinator; make sure he/she nominated you to the host university."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Participate in OISEP Orientation Program",
                Description = "Participate to the Orientation Program the OISEP organizes."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Apply for Passport",
                Description = "Apply for a new passport or extend the existing one if necessary."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Apply to Host University",
                Description = "Apply to the host university before the application deadline."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Prepare Learning Agreements",
                Description = "Prepare 3 original copies of your Learning Agreement (Latest two months prior to your travel)."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Submit 3 Copies of LA",
                Description = "Submit one copy of your Learning Agreement to OISEP, send one copy to the host university for their signature, keep one copy with yourself."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Submit Signed LA to OISEP",
                Description = "After receiving the signed Learning Agreement from the host university submit a copy to OISEP before you leave for Erasmus."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Prepare Pre-Approval Forms",
                Description = "Prepare 3 original copies of your Pre-approval form. (Latest two months prior to your travel)"
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Submit 3 Copies of Pre-Approval Forms",
                Description = "Submit one copy of your Pre-approval form to OISEP, one to your Departmental Exchange Coordinator and keep one with yourself."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Submit Acceptance Letter to OISEP",
                Description = "As soon as you receive your Acceptance Letter from the host university submit a copy to OISEP. Latest two months prior to your travel)."
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Get an Insurance",
                Description = "Get an Extended Health and Travel Insurance, submit one copy to OISEP. (Latest two months prior to your travel)"
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Apply to Erasmus Student Visa",
                Description = "Apply for an Erasmus Student Visa. (As soon as you receive your acceptance letter)"
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Open a EURO Bank Account",
                Description = "Open a EURO bank account at any Yapı Kredi Bank Branch, email the IBAN number and Branch name to OISEP. (Latest two months prior to your travel)"
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Sign Erasmus Grant Agreement",
                Description = "Sign the Erasmus Grant Agreement with OISEP. (Latest 45 days prior to your travel)"
            });
            listToReturn.Add(new ToDoItem
            {
                Title = "Take the OLS Exam's 1st Step",
                Description = "Take the Online Linguistic Support (OLS) exam 1st step. (After you sign the Erasmus Grant Agreement the exam will be sent to your Bilkent email)"
            });

            return listToReturn;
        }
    }
}
