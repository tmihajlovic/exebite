namespace Exebite.API.Models
{
    public class RecipeQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public int? MainCourseId { get; set; }
    }
}