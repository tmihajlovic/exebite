﻿namespace Exebite.DataAccess.Repositories
{
    public class CustomerQueryModel : QueryBase
    {
        public CustomerQueryModel()
        {
        }

        public CustomerQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public long? Id { get; set; }

        public string GoogleUserId { get; set; }

        public bool? IsActive { get; set; }
    }
}
