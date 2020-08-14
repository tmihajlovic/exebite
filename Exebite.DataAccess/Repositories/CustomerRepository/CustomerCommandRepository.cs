﻿using System;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly IMealOrderingContextFactory _factory;

        public CustomerCommandRepository(IMealOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public Either<Error, long> Insert(CustomerInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var customerEntity = new CustomerEntity()
                    {
                        Name = entity.Name,
                        GoogleUserId = entity.GoogleUserId,
                        Balance = entity.Balance,
                        DefaultLocationId = entity.DefaultLocationId,
                        Role = entity.Role,
                        IsActive = entity.IsActive
                    };

                    var addedEntity = context.Customer.Add(customerEntity).Entity;
                    context.SaveChanges();
                    return new Right<Error, long>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, long>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(long id, CustomerUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Customer.Find(id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.Name = entity.Name;
                    currentEntity.GoogleUserId = entity.GoogleUserId;
                    currentEntity.Balance = entity.Balance;
                    currentEntity.DefaultLocationId = entity.DefaultLocationId;
                    currentEntity.Role = entity.Role;
                    currentEntity.IsActive = entity.IsActive;

                    context.SaveChanges();
                }

                return new Right<Error, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Delete(long id)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var itemSet = context.Set<CustomerEntity>();
                    var item = itemSet.Find(id);
                    if (item == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound($"Record with Id='{id}' is not found."));
                    }

                    itemSet.Remove(item);
                    context.SaveChanges();
                    return new Right<Error, bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> UpdateByGoogleId(CustomerUpdateModel customer)
        {
            try
            {
                if (customer == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(customer)));
                }

                if (string.IsNullOrWhiteSpace(customer.GoogleUserId))
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(customer.GoogleUserId)));
                }

                using (var context = _factory.Create())
                {
                    var dbCustomer = context.Customer.FirstOrDefault(c => customer.GoogleUserId.Equals(c.GoogleUserId));
                    if (dbCustomer == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(customer)));
                    }

                    dbCustomer.Name = customer.Name;
                    dbCustomer.GoogleUserId = customer.GoogleUserId;
                    dbCustomer.Balance = customer.Balance;
                    dbCustomer.DefaultLocationId = customer.DefaultLocationId;
                    dbCustomer.Role = customer.Role;
                    dbCustomer.IsActive = customer.IsActive;

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }
    }
}