using AutoMapper;
using Either;
using System;

namespace Exebite.Common
{
    public interface IEitherMapper
    {
        //
        // Summary:
        //     Configuration provider for performing maps
        IConfigurationProvider Configuration { get; }        
        
        // Summary:
        //     Factory method for creating runtime instances of converters, resolvers etc.
        Func<Type, object> ServiceCtor { get; }

        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object. The source
        //     type is inferred from the source object.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        // Type parameters:
        //   TDestination:
        //     Destination type to create
        //
        // Returns:
        //     Mapped destination object
        Either<Error, TDestination> Map<TDestination>(object source);
        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object with supplied
        //     mapping options.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   opts:
        //     Mapping options
        //
        // Type parameters:
        //   TDestination:
        //     Destination type to create
        //
        // Returns:
        //     Mapped destination object
        Either<Error, TDestination> Map<TDestination>(object source, Action<IMappingOperationOptions> opts);
        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        // Type parameters:
        //   TSource:
        //     Source type to use, regardless of the runtime type
        //
        //   TDestination:
        //     Destination type to create
        //
        // Returns:
        //     Mapped destination object
        Either<Error, TDestination> Map<TSource, TDestination>(TSource source);
        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object with supplied
        //     mapping options.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   opts:
        //     Mapping options
        //
        // Type parameters:
        //   TSource:
        //     Source type to use
        //
        //   TDestination:
        //     Destination type to create
        //
        // Returns:
        //     Mapped destination object
        Either<Error, TDestination> Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts);
        //
        // Summary:
        //     Execute a mapping from the source object to the existing destination object.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   destination:
        //     Destination object to map into
        //
        // Type parameters:
        //   TSource:
        //     Source type to use
        //
        //   TDestination:
        //     Destination type
        //
        // Returns:
        //     The mapped destination object, same instance as the destination object
        Either<Error, TDestination> Map<TSource, TDestination>(TSource source, TDestination destination);
        //
        // Summary:
        //     Execute a mapping from the source object to the existing destination object with
        //     supplied mapping options.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   destination:
        //     Destination object to map into
        //
        //   opts:
        //     Mapping options
        //
        // Type parameters:
        //   TSource:
        //     Source type to use
        //
        //   TDestination:
        //     Destination type
        //
        // Returns:
        //     The mapped destination object, same instance as the destination object
        Either<Error, TDestination> Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts);
        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object with explicit
        //     System.Type objects
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   sourceType:
        //     Source type to use
        //
        //   destinationType:
        //     Destination type to create
        //
        // Returns:
        //     Mapped destination object
        Either<Error, object> Map(object source, Type sourceType, Type destinationType);
        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object with explicit
        //     System.Type objects and supplied mapping options.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   sourceType:
        //     Source type to use
        //
        //   destinationType:
        //     Destination type to create
        //
        //   opts:
        //     Mapping options
        //
        // Returns:
        //     Mapped destination object
        Either<Error, object> Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts);
        //
        // Summary:
        //     Execute a mapping from the source object to existing destination object with
        //     explicit System.Type objects
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   destination:
        //     Destination object to map into
        //
        //   sourceType:
        //     Source type to use
        //
        //   destinationType:
        //     Destination type to use
        //
        // Returns:
        //     Mapped destination object, same instance as the destination object
        Either<Error, object> Map(object source, object destination, Type sourceType, Type destinationType);
        //
        // Summary:
        //     Execute a mapping from the source object to existing destination object with
        //     supplied mapping options and explicit System.Type objects
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        //   destination:
        //     Destination object to map into
        //
        //   sourceType:
        //     Source type to use
        //
        //   destinationType:
        //     Destination type to use
        //
        //   opts:
        //     Mapping options
        //
        // Returns:
        //     Mapped destination object, same instance as the destination object
        Either<Error, object> Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts);
    }
}