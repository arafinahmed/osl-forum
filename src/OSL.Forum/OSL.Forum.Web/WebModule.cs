﻿using Autofac;
using AutoMapper;
using OSL.Forum.Core.Profiles;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using OSL.Forum.Web.Areas.Dashboard.Models.Forum;
using OSL.Forum.Web.Models.Category;
using OSL.Forum.Web.Profiles;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Forum.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<WebProfile>();
                cfg.AddProfile<CoreProfiles>();
            }
            )).AsSelf().InstancePerRequest();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProfileService>().As<IProfileService>().InstancePerLifetimeScope();
            builder.RegisterType<MailTemplate>().As<IMailTemplate>().InstancePerLifetimeScope();
            builder.RegisterType<CreateCategoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CategoriesListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EditCategoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeleteCategoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CategoryDetailsModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CreateForumModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EditForumModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeleteForumModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AllCategoryModel>().AsSelf().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}