using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Interfaces;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Mapping;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Operations.Read;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;

namespace PruneUrl.Backend.Infrastructure.IoC.Modules.Infrastructure;

/// <summary>
/// The Firebase specific code IoC module
/// </summary>
internal sealed class FirebaseModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    RegisterFirestoreInterfaces(builder);
    RegisterFirestoreSDK(builder);
    RegisterFirestoreProfile(builder);
    RegisterOperationFactories(builder);
    RegisterRequests(builder);
  }

  private void RegisterFirestoreInterfaces(ContainerBuilder builder)
  {
    builder.RegisterType<FirestoreDbTransactionFactory>().As<IFirestoreDbTransactionFactory>();
  }

  private void RegisterFirestoreProfile(ContainerBuilder builder)
  {
    builder.RegisterAutoMapper(typeof(FirestoreDTOProfile).Assembly);
  }

  private void RegisterFirestoreSDK(ContainerBuilder builder)
  {
    builder
      .Register(componentContext =>
      {
        FirestoreDbOptions firestoreDbOptions = componentContext
          .Resolve<IConfiguration>()
          .GetFirestoreDbOptions();
        return new FirestoreDbBuilder
        {
          ProjectId = firestoreDbOptions.ProjectId,
          EmulatorDetection = firestoreDbOptions.EmulatorDetection
        }.Build();
      })
      .As<FirestoreDb>()
      .SingleInstance();
  }

  private void RegisterOperationFactories(ContainerBuilder builder)
  {
    builder.RegisterType<FirestoreDbGetByIdOperationFactory>().As<IDbGetByIdOperationFactory>();
    builder.Register(componentContext =>
      componentContext.Resolve<IDbGetByIdOperationFactory>().Create<ShortUrl>()
    );
    builder.Register(componentContext =>
      componentContext.Resolve<IDbGetByIdOperationFactory>().Create<SequenceId>()
    );
  }

  private void RegisterRequests(ContainerBuilder builder)
  {
    builder.RegisterType<FirestoreDbTransactionProvider>().As<IDbTransactionProvider>();
    builder.RegisterType<FirestoreDbWriteBatchFactory>().As<IDbWriteBatchFactory>();
    builder.Register(componentContext =>
      componentContext.Resolve<IDbWriteBatchFactory>().Create<ShortUrl>()
    );
    builder.Register(componentContext =>
      componentContext.Resolve<IDbWriteBatchFactory>().Create<SequenceId>()
    );
  }
}
