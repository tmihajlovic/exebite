using Exebite.Common.Reflecsion;
using Exebite.DataAccess.Repositories;
using Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class CustomerRepositoryTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(50, 2)]
        public void GetById_ValidId_ValidResult(int count, int id)
        {
            var customerRepository = CustomerDataForTesing(Methods.GetCurrentMethod() + count + id, count);

            var res = customerRepository.GetByID(id);

            Assert.NotNull(res);
            Assert.Equal(id, res.Id);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_InValidId_ValidResult(int count)
        {
            var customerRepository = CustomerDataForTesing(Methods.GetCurrentMethod(), count);

            var res = customerRepository.GetByID(count - 1);

            Assert.Null(res);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Query_MultipleElements(int count)
        {
            CustomerRepository customerRepository = CustomerDataForTesing(Methods.GetCurrentMethod() + count, count);

            var res = customerRepository.Query(new CustomerQueryModel());

            Assert.Equal(count, res.Count);
        }

        [Fact]
        public void Query_QueryByIDId_ValidId()
        {
            CustomerRepository customerRepository = CustomerDataForTesing(Methods.GetCurrentMethod(), 1);

            var res = customerRepository.Query(new CustomerQueryModel() { Id = 1 });

            Assert.Equal(1, res.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void Query_QueryByIDId_NonExistingID(int id)
        {
            CustomerRepository customerRepository = CustomerDataForTesing(Methods.GetCurrentMethod() + id, 1);

            var res = customerRepository.Query(new CustomerQueryModel() { Id = id });

            Assert.Equal(0, res.Count);
        }
    }
}
