using System.Runtime.Serialization;

namespace Backend.Utilities.Enum
{
    public enum Faculty
    {
        // Faculties at the Bilkent University
        [EnumMember(Value = "Faculty of Engineering")]
        Engineering,

        [EnumMember(Value = "Faculty of Art, Design, and Architecture")]
        ArtsDesignArchitecture,

        [EnumMember(Value = "Faculty of Business Administration")]
        BusinessAdministration,

        [EnumMember(Value = "Faculty of Economics, Administrative, and Social Sciences")]
        EconomicsAdministrativeSocialSciences,

        [EnumMember(Value = "Faculty of Education")]
        Education,

        [EnumMember(Value = "Faculty of Humanities and Letters")]
        HumanitiesLetters,

        [EnumMember(Value = "Faculty of Law")]
        Law,

        [EnumMember(Value = "Faculty of Science")]
        Science,

        [EnumMember(Value = "Faculty of Music and Performing Arts")]
        MusicPerformingArts,

        [EnumMember(Value = "Faculty of Applied Sciences")]
        AppliedSciences,

        [EnumMember(Value = "Not Specified")]
        NotSpecified
    }
}
