using AutoMapper;
using AutoMapper.Configuration;
using Exebite.API;
using Exebite.DataAccess;
using Xunit;

namespace Mappings.Test
{
    public class MappingTest
    {
        [Fact]
        public void MappingProfile_VerifyDataAccessMappings()
        {
            var configExpresion = new MapperConfigurationExpression();
            configExpresion.AddProfile<DataAccessMappingProfile>();

            var config = new MapperConfiguration(configExpresion);

            var mapper = new Mapper(config) as IMapper;

            var provider = mapper.ConfigurationProvider;
            provider.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingProfile_VerifyUIMappings()
        {
            var configExpresion = new MapperConfigurationExpression();
            configExpresion.AddProfile<Exebite.API.UIMappingProfile>();

            var config = new MapperConfiguration(configExpresion);

            var mapper = new Mapper(config) as IMapper;

            var provider = mapper.ConfigurationProvider;
            provider.AssertConfigurationIsValid();
        }
    }
}
