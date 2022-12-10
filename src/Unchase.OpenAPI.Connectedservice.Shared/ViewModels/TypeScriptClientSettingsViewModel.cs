﻿using System;
using System.Linq;
using Microsoft.VisualStudio.ConnectedServices;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag.CodeGeneration.TypeScript;
using NSwag.Commands;
using NSwag.Commands.CodeGeneration;
using Unchase.OpenAPI.ConnectedService.Views;

namespace Unchase.OpenAPI.ConnectedService.ViewModels
{
    internal class TypeScriptClientSettingsViewModel :
        ConnectedServiceWizardPage
    {
        #region Properties

        public OpenApiToTypeScriptClientCommand Command { get; set; } = new OpenApiToTypeScriptClientCommand();

        /// <summary>Gets the supported TypeScript versions.</summary>
        public decimal[] TypeScriptVersions => new[] { 1.8m, 2.0m, 2.4m, 2.7m, 4.3m };

        /// <summary>Gets the supported RxJs versions.</summary>
        public decimal[] RxJsVersions => new[] { 5.0m, 6.0m };

        /// <summary>Gets the output templates.</summary>
        public TypeScriptTemplate[] Templates { get; } = Enum.GetNames(typeof(TypeScriptTemplate))
            .Select(t => (TypeScriptTemplate)Enum.Parse(typeof(TypeScriptTemplate), t))
            .ToArray();

        /// <summary>Gets the operation modes.</summary>
        public OperationGenerationMode[] OperationGenerationModes { get; } = Enum.GetNames(typeof(OperationGenerationMode))
            .Select(t => (OperationGenerationMode)Enum.Parse(typeof(OperationGenerationMode), t))
            .ToArray();

        /// <summary>Gets the promise types.</summary>
        public PromiseType[] PromiseTypes { get; } = Enum.GetNames(typeof(PromiseType))
            .Select(t => (PromiseType)Enum.Parse(typeof(PromiseType), t))
            .ToArray();

        /// <summary>Gets the promise types.</summary>
        public HttpClass[] HttpClasses { get; } = Enum.GetNames(typeof(HttpClass))
            .Select(t => (HttpClass)Enum.Parse(typeof(HttpClass), t))
            .ToArray();

        /// <summary>Gets the promise types.</summary>
        public InjectionTokenType[] InjectionTokenTypes { get; } = Enum.GetNames(typeof(InjectionTokenType))
            .Select(t => (InjectionTokenType)Enum.Parse(typeof(InjectionTokenType), t))
            .ToArray();

        /// <summary>Gets the list of type styles.</summary>
        public TypeScriptTypeStyle[] TypeStyles { get; } = Enum.GetNames(typeof(TypeScriptTypeStyle))
            .Select(t => (TypeScriptTypeStyle)Enum.Parse(typeof(TypeScriptTypeStyle), t))
            .ToArray();

        /// <summary>Gets the list of enum styles.</summary>
        public TypeScriptEnumStyle[] EnumStyles { get; } = Enum.GetNames(typeof(TypeScriptEnumStyle))
            .Select(t => (TypeScriptEnumStyle)Enum.Parse(typeof(TypeScriptEnumStyle), t))
            .ToArray();

        /// <summary>Gets the list of date time types.</summary>
        public TypeScriptDateTimeType[] DateTimeTypes { get; } = Enum.GetNames(typeof(TypeScriptDateTimeType))
            .Select(t => (TypeScriptDateTimeType)Enum.Parse(typeof(TypeScriptDateTimeType), t))
            .ToArray();

        /// <summary>Gets the list of null values.</summary>
        public TypeScriptNullValue[] NullValues { get; } = Enum.GetNames(typeof(TypeScriptNullValue))
            .Select(t => (TypeScriptNullValue)Enum.Parse(typeof(TypeScriptNullValue), t))
            .ToArray();

        /// <summary>Gets new line behaviors. </summary>
        public NewLineBehavior[] NewLineBehaviors { get; } = Enum.GetNames(typeof(NewLineBehavior))
            .Select(t => (NewLineBehavior)Enum.Parse(typeof(NewLineBehavior), t))
            .ToArray();

        #endregion

        #region Constructors

        public TypeScriptClientSettingsViewModel() : base()
        {
            Title = "TypeScript Client Settings";
            Description = "Settings for generating TypeScript client";
            Legend = "TypeScript Client Settings";
            View = new TypeScriptClientSettings {DataContext = this};
        }

        #endregion
    }
}