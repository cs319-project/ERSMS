using System.Runtime.Serialization;

namespace Backend.Utilities.Enum
{
    public enum Department
    {
        // Departments at the Bilkent University
        [EnumMember(Value = "Department of Computer Engineering")]
        ComputerEngineering,

        [EnumMember(Value = "Department of Electrical and Electronics Engineering")]
        ElectricalElectronicsEngineering,

        [EnumMember(Value = "Department of Industrial Engineering")]
        IndustrialEngineering,

        [EnumMember(Value = "Department of Mechanical Engineering")]
        MechanicalEngineering,

        [EnumMember(Value = "Department of Architecture")]
        Architecture,

        [EnumMember(Value = "Department of Communication and Design")]
        CommunicationDesign,

        [EnumMember(Value = "Department of Fine Arts")]
        FineArts,

        [EnumMember(Value = "Department of Graphic Design")]
        GraphicDesign,

        [EnumMember(Value = "Department of Interior Architecture and Environmental Design")]
        InteriorArchitectureEnvironmentalDesign,

        [EnumMember(Value = "Department of Urban Design and Landscape Architecture")]
        UrbanDesignLandscapeArchitecture,

        [EnumMember(Value = "Department of Management")]
        Management,

        [EnumMember(Value = "Department of Economics")]
        Economics,

        [EnumMember(Value = "Department of History")]
        History,

        [EnumMember(Value = "Department of International Relations")]
        InternationalRelations,

        [EnumMember(Value = "Department of Political Science and Public Administration")]
        PoliticalSciencePublicAdministration,

        [EnumMember(Value = "Department of Psychology")]
        Psychology,

        [EnumMember(Value = "Department of American Culture and Literature")]
        AmericanCultureLiterature,

        [EnumMember(Value = "Department of Archaeology")]
        Archaeology,

        [EnumMember(Value = "Department of English Language and Literature")]
        EnglishLanguageLiterature,

        [EnumMember(Value = "Department of Philosophy")]
        Philosophy,

        [EnumMember(Value = "Department of Translation and Interpretation")]
        TranslationInterpretation,

        [EnumMember(Value = "Department of Turkish Literature")]
        TurkishLiterature,

        [EnumMember(Value = "Department of Law")]
        Law,

        [EnumMember(Value = "Department of Chemistry")]
        Chemistry,

        [EnumMember(Value = "Department of Mathematics")]
        Mathematics,

        [EnumMember(Value = "Department of Physics")]
        Physics,

        [EnumMember(Value = "Department of Molecular Biology and Genetics")]
        MolecularBiologyGenetics,

        [EnumMember(Value = "Department of Music")]
        Music,

        [EnumMember(Value = "Department of Performing Arts")]
        PerformingArts,

        [EnumMember(Value = "Information Systems and Technologies")]
        InformationSystemsTechnologies,

        [EnumMember(Value = "Tourism and Hotel Management")]
        TourismHotelManagement,

        [EnumMember(Value = "Not Specified")]
        NotSpecified,
    }
}
