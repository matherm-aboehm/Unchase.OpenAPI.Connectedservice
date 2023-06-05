﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

using Microsoft.VisualStudio.ConnectedServices;
#if VS17
using Unchase.OpenAPI.Connectedservice.VS22.Properties;
#else
using Unchase.OpenAPI.ConnectedService.Properties;
#endif

namespace Unchase.OpenAPI.ConnectedService
{
    [ConnectedServiceProviderExport(Constants.ProviderId, SupportsUpdate = true)]
    internal class Provider :
        ConnectedServiceProvider
    {
        #region Constructors

        public Provider()
        {
            Category = Constants.ExtensionCategory;
            Name = Constants.ExtensionName;
            Description = Constants.ExtensionDescription;
            Icon = Imaging.CreateBitmapSourceFromHBitmap(
                Resources.preview_200x200.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(64, 64)
            );
            CreatedBy = Constants.Author;
            Version = typeof(Provider).Assembly.GetName().Version;
            MoreInfoUri = new Uri(Constants.Website);
        }

        #endregion

        #region Methods

        public override async Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderContext context)
        {
            return await Wizard.CreateAsync(context);
        }

        public override IEnumerable<Tuple<string, Uri>> GetSupportedTechnologyLinks()
        {
            yield return Tuple.Create("OpenAPI (Swagger)", new Uri("https://swagger.io/docs/specification/about/"));
            yield return Tuple.Create("NSwag", new Uri("https://github.com/RSuter/NSwag"));
        }

        #endregion
    }
}