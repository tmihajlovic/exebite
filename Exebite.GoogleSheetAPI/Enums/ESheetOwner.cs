using System.ComponentModel;

namespace Exebite.GoogleSheetAPI.Enums
{
    /// <summary>
    /// All available Sheet Owners.
    /// </summary>
    public enum ESheetOwner
    {
        [Description("Kuhinjica pod Lipom")]
        LIPA,

        [Description("Topli Obrok")]
        TOPLI_OBROK,

        [Description("Index House")]
        INDEX_HOUSE,

        [Description("Serpica 021")]
        SERPICA,

        [Description("Mima's")]
        MIMAS,

        [Description("Parrilla")]
        PARRILLA,

        [Description("Hey Day")]
        HEY_DAY,

        [Description("Kasa")]
        KASA,
    }
}
