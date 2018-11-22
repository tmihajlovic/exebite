using AutoMapper;
using AutoMapper.Configuration;
using Exebite.API;
using Exebite.Common;
using Exebite.DataAccess;
using Xunit;

namespace Mappings.Test
{
    public class EitherMappingTest
    {
        [Fact]
        public void EitherMappingProfile_VerifyDataAccessMappings()
        {
            var configExpresion = new MapperConfigurationExpression();
            configExpresion.AddProfile<DataAccessMappingProfile>();

            var config = new MapperConfiguration(configExpresion);

            var mapper = new EitherMapper(new Mapper(config));

            var provider = mapper.Configuration;
            provider.AssertConfigurationIsValid();
        }

        [Fact]
        public void EitherMappingProfile_VerifyUIMappings()
        {
            var configExpresion = new MapperConfigurationExpression();
            configExpresion.AddProfile<UIMappingProfile>();

            var config = new MapperConfiguration(configExpresion);

            var mapper = new EitherMapper(new Mapper(config));

            var provider = mapper.Configuration;
            provider.AssertConfigurationIsValid();
        }
    }
}
