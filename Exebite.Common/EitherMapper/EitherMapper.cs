using System;
using AutoMapper;
using Either;

namespace Exebite.Common
{
    public class EitherMapper : IEitherMapper
    {
        private readonly IMapper _mapper;

        public EitherMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IConfigurationProvider Configuration => _mapper.ConfigurationProvider;

        public Func<Type, object> ServiceCtor => _mapper.ServiceCtor;

        public Either<Error, TDestination> Map<TDestination>(object source)
        {
            try
            {
                return new Right<Error, TDestination>(_mapper.Map<TDestination>(source));
            }
            catch (Exception ex)
            {
                return new Left<Error, TDestination>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, TDestination> Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
        {
            try
            {
                return _mapper.Map<TDestination>(source, opts);
            }
            catch (Exception ex)
            {
                return new Left<Error, TDestination>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, TDestination> Map<TSource, TDestination>(TSource source)
        {
            try
            {
                return _mapper.Map<TSource, TDestination>(source);
            }
            catch (Exception ex)
            {
                return new Left<Error, TDestination>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, TDestination> Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            try
            {
                return _mapper.Map(source, opts);
            }
            catch (Exception ex)
            {
                return new Left<Error, TDestination>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, TDestination> Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            try
            {
                return _mapper.Map(source, destination);
            }
            catch (Exception ex)
            {
                return new Left<Error, TDestination>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, TDestination> Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            try
            {
                return _mapper.Map(source, destination, opts);
            }
            catch (Exception ex)
            {
                return new Left<Error, TDestination>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, object> Map(object source, Type sourceType, Type destinationType)
        {
            try
            {
                return _mapper.Map(source, sourceType, destinationType);
            }
            catch (Exception ex)
            {
                return new Left<MappingError, object>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, object> Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            try
            {
                return _mapper.Map(source, sourceType, destinationType, opts);
            }
            catch (Exception ex)
            {
                return new Left<MappingError, object>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, object> Map(object source, object destination, Type sourceType, Type destinationType)
        {
            try
            {
                return _mapper.Map(source, sourceType, sourceType, destinationType);
            }
            catch (Exception ex)
            {
                return new Left<MappingError, object>(new MappingError(ex.ToString()));
            }
        }

        public Either<Error, object> Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            try
            {
                return _mapper.Map(source, sourceType, sourceType, destinationType, opts);
            }            
            catch (Exception ex)
            {
                return new Left<MappingError, object>(new MappingError(ex.ToString()));
            }
        }
    }
}
