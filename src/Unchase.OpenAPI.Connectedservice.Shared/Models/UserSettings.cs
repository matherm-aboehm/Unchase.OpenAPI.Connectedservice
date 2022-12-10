﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

using Microsoft.OpenApi;
using Microsoft.OpenApi.OData;
using Microsoft.VisualStudio.ConnectedServices;
using NSwag.Commands;
using Unchase.OpenAPI.ConnectedService.Common;

namespace Unchase.OpenAPI.ConnectedService.Models
{
    [DataContract]
    internal class UserSettings
    {
        #region Private

        private const string Name = "Settings";

        private const int MaxMruEntries = 10;

        private ConnectedServiceLogger _logger;

        #endregion

        #region Public properties

        [DataMember]
        public ObservableCollection<string> MruEndpoints { get; private set; }

        [DataMember]
        public bool GenerateCSharpClient { get; set; } = false;

        [DataMember]
        public bool GenerateTypeScriptClient { get; set; } = false;

        [DataMember]
        public bool GenerateCSharpController { get; set; } = false;

        [DataMember]
        public string Variables { get; set; }

        [DataMember]
        public Runtime Runtime { get; set; }

        [DataMember]
        public bool CopySpecification { get; set; } = false;

        [DataMember]
        public string Endpoint { get; set; }

        [DataMember]
        public string ServiceName { get; set; }

        [DataMember]
        public string GeneratedFileName { get; set; }

        [DataMember]
        public bool OpenGeneratedFilesOnComplete { get; set; } = false;

        [DataMember]
        public bool UseRelativePath { get; set; } = false;

        [DataMember]
        public bool ConvertFromOdata { get; set; } = false;

        [DataMember]
        public OpenApiConvertSettings OpenApiConvertSettings { get; set; } = new OpenApiConvertSettings();

        [DataMember]
        public OpenApiSpecVersion OpenApiSpecVersion { get; set; }

        [DataMember]
        public bool AcceptAllUntrustedCertificates { get; set; } = true;

        public string ProjectPath { get; set; }

        #endregion

        #region Constructors

        private UserSettings()
        {
            MruEndpoints = new ObservableCollection<string>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Set properties from <see cref="UserSettings"/>.
        /// </summary>
        /// <param name="serviceConfiguration"><see cref="ServiceConfiguration"/>.</param>
        internal void SetFromServiceConfiguration(ServiceConfiguration serviceConfiguration)
        {
            CopySpecification = serviceConfiguration.CopySpecification;
            Endpoint = serviceConfiguration.Endpoint;
            GenerateCSharpClient = serviceConfiguration.GenerateCSharpClient;
            GenerateCSharpController = serviceConfiguration.GenerateCSharpController;
            GenerateTypeScriptClient = serviceConfiguration.GenerateTypeScriptClient;
            OpenGeneratedFilesOnComplete = serviceConfiguration.OpenGeneratedFilesOnComplete;
            Runtime = serviceConfiguration.Runtime;
            ServiceName = serviceConfiguration.ServiceName;
            AcceptAllUntrustedCertificates = serviceConfiguration.AcceptAllUntrustedCertificates;
            GeneratedFileName = serviceConfiguration.GeneratedFileName;
            UseRelativePath = serviceConfiguration.UseRelativePath;
            ConvertFromOdata = serviceConfiguration.ConvertFromOdata;
            OpenApiConvertSettings = serviceConfiguration.OpenApiConvertSettings;
            OpenApiSpecVersion = serviceConfiguration.OpenApiSpecVersion;
            Variables = serviceConfiguration.Variables;
        }

        public void Save()
        {
            UserSettingsPersistenceHelper.Save(this, Constants.ProviderId, Name, null, _logger);
        }

        public static UserSettings Load(ConnectedServiceLogger logger)
        {
            var userSettings = UserSettingsPersistenceHelper.Load<UserSettings>(
                Constants.ProviderId, Name, null, logger) ?? new UserSettings();
            userSettings._logger = logger;
            return userSettings;
        }

        public static void AddToTopOfMruList<T>(ObservableCollection<T> mruList, T item)
        {
            var index = mruList.IndexOf(item);
            if (index >= 0)
            {
                // Ensure there aren't any duplicates in the list.
                for (var i = mruList.Count - 1; i > index; i--)
                {
                    if (EqualityComparer<T>.Default.Equals(mruList[i], item))
                    {
                        mruList.RemoveAt(i);
                    }
                }

                if (index > 0)
                {
                    // The item is in the MRU list but it is not at the top.
                    mruList.Move(index, 0);
                }
            }
            else
            {
                // The item is not in the MRU list, make room for it by clearing out the oldest item.
                while (mruList.Count >= MaxMruEntries)
                {
                    mruList.RemoveAt(mruList.Count - 1);
                }

                mruList.Insert(0, item);
            }
        }

        #endregion
    }
}