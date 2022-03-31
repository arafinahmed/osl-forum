﻿using Autofac;
using OSL.Forum.Core.Contexts;
using OSL.Forum.Core.Repositories;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.UnitOfWorks;
using OSL.Forum.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core
{
    public class CoreModule : Module
    {
        private readonly string _connectionString;

        public CoreModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CoreDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<CoreDbContext>().As<ICoreDbContext>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            //Repositories
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ForumRepository>().As<IForumRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<TopicRepository>().As<ITopicRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FavoriteForumRepository>().As<IFavoriteForumRepository>()
                .InstancePerLifetimeScope();

            //Unit Of Work
            builder.RegisterType<CoreUnitOfWork>().As<ICoreUnitOfWork>().InstancePerLifetimeScope();

            //Service Classes
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ForumService>().As<IForumService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();
            builder.RegisterType<FavoriteForumService>().As<IFavoriteForumService>().InstancePerLifetimeScope();

            //Utilities
            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
