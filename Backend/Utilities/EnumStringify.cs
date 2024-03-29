using Backend.Utilities.Enum;

namespace Backend.Utilities
{
    /// <summary>A mission critical singleton class to convert between an enum and its string representation.</summary>
    public static class EnumStringify
    {
        /// <summary>Converts an actor string to the corresponding enum.</summary>
        /// <param name="actor">The actor's string representation.</param>
        /// <returns>The actor's enum representation.</returns>
        public static Actors ActorEnumarator(string actor)
        {
            switch (actor)
            {
                case "Student":
                    return Actors.Student;
                case "Course Coordinator Instructor":
                    return Actors.CourseCoordinatorInstructor;
                case "Exchange Coordinator":
                    return Actors.ExchangeCoordinator;
                case "Dean Department Chair":
                    return Actors.DeanDepartmentChair;
                case "Admin":
                    return Actors.Admin;
                case "Office of International Students and Exchange Programs":
                    return Actors.OISEP;
                default:
                    throw new Exception("Actor Exception");
            }
        }

        /// <summary>Converts a <see cref="Actors"/> to a string.</summary>
        /// <param name="actor">The actor to convert.</param>
        /// <returns>The string representation of the actor.</returns>
        public static string ActorStringify(Actors actor)
        {
            switch (actor)
            {
                case Actors.Student:
                    return "Student";
                case Actors.CourseCoordinatorInstructor:
                    return "Course Coordinator Instructor";
                case Actors.ExchangeCoordinator:
                    return "Exchange Coordinator";
                case Actors.DeanDepartmentChair:
                    return "Dean Department Chair";
                case Actors.Admin:
                    return "Admin";
                case Actors.OISEP:
                    return "Office of International Students and Exchange Programs";
                default:
                    throw new Exception("Actor Exception");
            }
        }

        /// <summary>Returns a list of roles that can be assigned to a user.</summary>
        /// <returns>A list of roles that can be assigned to a user.</returns>
        public static List<string> IdentityRoleList()
        {
            return new List<string>
            {
                "Student",
                "Course Coordinator Instructor",
                "Exchange Coordinator",
                "Dean Department Chair",
                "Admin",
                "Office of International Students and Exchange Programs"
            };
        }

        /// <summary>Converts a department string to the corresponding enum.</summary>
        /// <param name="department">The department's string representation.</param>
        /// <returns>The department's enum representation.</returns>
        public static Department DepartmentEnumarator(string department)
        {
            switch (department)
            {
                case "Department of Computer Engineering":
                    return Department.ComputerEngineering;
                case "Department of Electrical and Electronics Engineering":
                    return Department.ElectricalElectronicsEngineering;
                case "Department of Industrial Engineering":
                    return Department.IndustrialEngineering;
                case "Department of Mechanical Engineering":
                    return Department.MechanicalEngineering;
                case "Department of Architecture":
                    return Department.Architecture;
                case "Department of Communication and Design":
                    return Department.CommunicationDesign;
                case "Department of Fine Arts":
                    return Department.FineArts;
                case "Department of Graphic Design":
                    return Department.GraphicDesign;
                case "Department of Interior Architecture and Environmental Design":
                    return Department.InteriorArchitectureEnvironmentalDesign;
                case "Department of Urban Design and Landscape Architecture":
                    return Department.UrbanDesignLandscapeArchitecture;
                case "Department of Management":
                    return Department.Management;
                case "Department of Economics":
                    return Department.Economics;
                case "Department of History":
                    return Department.History;
                case "Department of International Relations":
                    return Department.InternationalRelations;
                case "Department of Political Science and Public Administration":
                    return Department.PoliticalSciencePublicAdministration;
                case "Department of Psychology":
                    return Department.Psychology;
                case "Department of American Culture and Literature":
                    return Department.AmericanCultureLiterature;
                case "Department of Archaeology":
                    return Department.Archaeology;
                case "Department of English Language and Literature":
                    return Department.EnglishLanguageLiterature;
                case "Department of Philosophy":
                    return Department.Philosophy;
                case "Department of Translation and Interpretation":
                    return Department.TranslationInterpretation;
                case "Department of Turkish Literature":
                    return Department.TurkishLiterature;
                case "Department of Law":
                    return Department.Law;
                case "Department of Mathematics":
                    return Department.Mathematics;
                case "Department of Physics":
                    return Department.Physics;
                case "Department of Chemistry":
                    return Department.Chemistry;
                case "Department of Molecular Biology and Genetics":
                    return Department.MolecularBiologyGenetics;
                case "Department of Music":
                    return Department.Music;
                case "Department of Performing Arts":
                    return Department.PerformingArts;
                case "Department of Information Systems and Technologies":
                    return Department.InformationSystemsTechnologies;
                case "Department of Tourism and Hotel Management":
                    return Department.TourismHotelManagement;
                default:
                    return Department.NotSpecified;
            }
        }

        /// <summary>Converts a <see cref="Department"/> to a string.</summary>
        /// <param name="department">The department to convert.</param>
        /// <returns>The string representation of the department.</returns>
        public static string DepartmentStringify(Department department)
        {
            switch (department)
            {
                case Department.ComputerEngineering:
                    return "Department of Computer Engineering";
                case Department.ElectricalElectronicsEngineering:
                    return "Department of Electrical and Electronics Engineering";
                case Department.IndustrialEngineering:
                    return "Department of Industrial Engineering";
                case Department.MechanicalEngineering:
                    return "Department of Mechanical Engineering";
                case Department.Architecture:
                    return "Department of Architecture";
                case Department.CommunicationDesign:
                    return "Department of Communication and Design";
                case Department.FineArts:
                    return "Department of Fine Arts";
                case Department.GraphicDesign:
                    return "Department of Graphic Design";
                case Department.InteriorArchitectureEnvironmentalDesign:
                    return "Department of Interior Architecture and Environmental Design";
                case Department.UrbanDesignLandscapeArchitecture:
                    return "Department of Urban Design and Landscape Architecture";
                case Department.Management:
                    return "Department of Management";
                case Department.Economics:
                    return "Department of Economics";
                case Department.History:
                    return "Department of History";
                case Department.InternationalRelations:
                    return "Department of International Relations";
                case Department.PoliticalSciencePublicAdministration:
                    return "Department of Political Science and Public Administration";
                case Department.Psychology:
                    return "Department of Psychology";
                case Department.AmericanCultureLiterature:
                    return "Department of American Culture and Literature";
                case Department.Archaeology:
                    return "Department of Archaeology";
                case Department.EnglishLanguageLiterature:
                    return "Department of English Language and Literature";
                case Department.Philosophy:
                    return "Department of Philosophy";
                case Department.TranslationInterpretation:
                    return "Department of Translation and Interpretation";
                case Department.TurkishLiterature:
                    return "Department of Turkish Literature";
                case Department.Law:
                    return "Department of Law";
                case Department.Mathematics:
                    return "Department of Mathematics";
                case Department.Physics:
                    return "Department of Physics";
                case Department.Chemistry:
                    return "Department of Chemistry";
                case Department.MolecularBiologyGenetics:
                    return "Department of Molecular Biology and Genetics";
                case Department.Music:
                    return "Department of Music";
                case Department.PerformingArts:
                    return "Department of Performing Arts";
                case Department.InformationSystemsTechnologies:
                    return "Department of Information Systems and Technologies";
                case Department.TourismHotelManagement:
                    return "Department of Tourism and Hotel Management";
                case Department.NotSpecified:
                    return "Not Specified";
                default:
                    return "Not Specified";
            }
        }

        /// <summary>Converts a faculty string to the corresponding enum.</summary>
        /// <param name="faculty">The faculty's string representation.</param>
        /// <returns>The faculty's enum representation.</returns>
        public static Faculty FacultyEnumarator(string faculty)
        {
            switch (faculty)
            {
                case "Faculty of Engineering":
                    return Faculty.Engineering;
                case "Faculty of Art, Design, and Architecture":
                    return Faculty.ArtsDesignArchitecture;
                case "Faculty of Economics, Administrative, and Social Sciences":
                    return Faculty.EconomicsAdministrativeSocialSciences;
                case "Faculty of Business Administration":
                    return Faculty.BusinessAdministration;
                case "Faculty of Law":
                    return Faculty.Law;
                case "Faculty of Science":
                    return Faculty.Science;
                case "Faculty of Education":
                    return Faculty.Education;
                case "Faculty of Humanities and Letters":
                    return Faculty.HumanitiesLetters;
                case "Faculty of Music and Performing Arts":
                    return Faculty.MusicPerformingArts;
                case "Faculty of Applied Sciences":
                    return Faculty.AppliedSciences;
                default:
                    return Faculty.NotSpecified;
            }
        }

        /// <summary>Converts a <see cref="Faculty"/> to a string.</summary>
        /// <param name="faculty">The faculty to convert.</param>
        /// <returns>The string representation of the faculty.</returns>
        public static string FacultyStringify(Faculty faculty)
        {
            switch (faculty)
            {
                case Faculty.Engineering:
                    return "Faculty of Engineering";
                case Faculty.ArtsDesignArchitecture:
                    return "Faculty of Art, Design, and Architecture";
                case Faculty.EconomicsAdministrativeSocialSciences:
                    return "Faculty of Economics, Administrative, and Social Sciences";
                case Faculty.BusinessAdministration:
                    return "Faculty of Business Administration";
                case Faculty.Law:
                    return "Faculty of Law";
                case Faculty.Science:
                    return "Faculty of Science";
                case Faculty.Education:
                    return "Faculty of Education";
                case Faculty.HumanitiesLetters:
                    return "Faculty of Humanities and Letters";
                case Faculty.MusicPerformingArts:
                    return "Faculty of Music and Performing Arts";
                case Faculty.AppliedSciences:
                    return "Faculty of Applied Sciences";
                case Faculty.NotSpecified:
                    return "";
                default:
                    return "";
            }
        }

        /// <summary>Converts a semester string to the corresponding enum.</summary>
        /// <param name="semester">The semester's string representation.</param>
        /// <returns>The semester's enum representation.</returns>
        /// <exception cref="Exception">Thrown when the semester is not valid.</exception>
        public static Semester SemesterEnumarator(string semester)
        {
            switch (semester)
            {
                case "Fall Semester":
                    return Semester.Fall;
                case "Spring Semester":
                    return Semester.Spring;
                case "Full Year":
                    return Semester.FullYear;
                default:
                    throw new Exception("Semester Exception");
            }
        }

        /// <summary>Converts a <see cref="Semester"/> to a string.</summary>
        /// <param name="semester">The semester to convert.</param>
        /// <returns>The string representation of the semester.</returns>
        /// <exception cref="Exception">Thrown when the semester is not valid.</exception>
        public static string SemesterStringify(Semester semester)
        {
            switch (semester)
            {
                case Semester.Fall:
                    return "Fall Semester";
                case Semester.Spring:
                    return "Spring Semester";
                case Semester.FullYear:
                    return "Full Year";
                default:
                    throw new Exception("Semester Exception");
            }
        }

        /// <summary>Converts a <see cref="CourseTypes"/> to a string.</summary>
        /// <param name="courseType">The course type to convert.</param>
        /// <returns>The string representation of the course type.</returns>
        /// <exception cref="Exception">Thrown when the course type is not valid.</exception>
        public static string CourseTypeStringify(CourseTypes courseType)
        {
            switch (courseType)
            {
                case CourseTypes.MandatoryCourse:
                    return "Mandatory Course";
                case CourseTypes.GeneralElective:
                    return "General Elective";
                case CourseTypes.TechnicalElective:
                    return "Technical Elective";
                case CourseTypes.ProjectElective:
                    return "Project Elective";
                case CourseTypes.SocialScienceCoreElective:
                    return "Social Science Core Elective";
                case CourseTypes.AdditionalCourse:
                    return "Additional Course";
                case CourseTypes.ArtsCoreElective:
                    return "Arts Core Elective";
                case CourseTypes.BreadthElective:
                    return "Breadth Elective";
                case CourseTypes.EngineeringElective:
                    return "Engineering Elective";
                case CourseTypes.MathElective:
                    return "Math Elective";
                default:
                    throw new Exception("CourseType Exception");
            }
        }

        /// <summary>Converts a course type string to the corresponding enum.</summary>
        /// <param name="courseType">The course type's string representation.</param>
        /// <returns>The course type's enum representation.</returns>
        /// <exception cref="Exception">Thrown when the course type is not valid.</exception>
        public static CourseTypes CourseTypeEnumarator(string courseType)
        {
            switch (courseType)
            {
                case "Mandatory Course":
                    return CourseTypes.MandatoryCourse;
                case "General Elective":
                    return CourseTypes.GeneralElective;
                case "Technical Elective":
                    return CourseTypes.TechnicalElective;
                case "Project Elective":
                    return CourseTypes.ProjectElective;
                case "Social Science Core Elective":
                    return CourseTypes.SocialScienceCoreElective;
                case "Additional Course":
                    return CourseTypes.AdditionalCourse;
                case "Arts Core Elective":
                    return CourseTypes.ArtsCoreElective;
                case "Breadth Elective":
                    return CourseTypes.BreadthElective;
                case "Math Elective":
                    return CourseTypes.MathElective;
                case "Engineering Elective":
                    return CourseTypes.EngineeringElective;
                default:
                    throw new Exception("CourseType Exception");
            }
        }


        /// <summary>Converts a <see cref="FormType"/> to a string.</summary>
        /// <param name="formType">The form type to convert.</param>
        /// <returns>The string representation of the form type.</returns>
        /// <exception cref="Exception">Thrown when the form type is not valid.</exception>
        public static string FormTypeStringify(FormType formType)
        {
            switch (formType)
            {
                case FormType.CTEForm:
                    return "CTE Form";
                case FormType.PreApprovalForm:
                    return "Pre-Approval Form";
                case FormType.EquivalenceRequest:
                    return "Equivalence Request Form";
                default:
                    throw new Exception("FormType Exception");
            }
        }
    }
}
