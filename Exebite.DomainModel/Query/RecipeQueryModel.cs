namespace Exebite.DomainModel
{
    public class RecipeQueryModel : QueryBase
    {

        public RecipeQueryModel() : base()
        {

        }

        public RecipeQueryModel(int page, int size) : base(page, size)
        {

        }

        public int? Id { get; set; }


        public int? MainCourseId { get; set; }


    }
}
