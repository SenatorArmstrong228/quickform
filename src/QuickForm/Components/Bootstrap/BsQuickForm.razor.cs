﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using QuickForm.Common;

// TODO make one for tailwind and place it at the root QuickForm namespace;

namespace QuickForm.Components.Bootstrap;

/// <summary>
/// A quick form with Bootstrap styling.
/// </summary>
public sealed class BsQuickForm<TEntity> : QuickForm<TEntity>
    where TEntity : class, new()
{
    /// <summary>
    /// Set the CSS class provider for the form fields.
    /// </summary>
    protected override void OnParametersSet()
    {
        CssClassProvider = new CustomQuickFormClassProvider
        {
            Editor = field => "text-start mb-3" + (field.PropertyInfo.PropertyType == typeof(bool) ? " form-check" : ""),
            Label = field => "text-info fw-bold mb-1" + (field.PropertyInfo.PropertyType == typeof(bool) ? "form-check-label" : ""),
            Input = field => field.PropertyInfo.PropertyType == typeof(bool) ? "form-check-input" : "form-control",
        };

        ValidationCssClassProvider = new CustomFieldCssClassProvider("is-valid", "is-invalid");

        ValidFeedbackTemplate ??= validFeedback =>
        {
            return builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "valid-feedback fw-bold");
                builder.AddContent(2, validFeedback);
                builder.CloseElement();
            };
        };

        InValidFeedbackTemplate ??= invalidFeedback =>
        {
            return builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "invalid-feedback fw-bold");
                builder.AddContent(2, invalidFeedback);
                builder.CloseElement();
            };
        };

        SubmitButtonTemplate ??= builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "type", "submit");
            builder.AddAttribute(2, "class", "btn btn-outline-success w-100");
            builder.AddContent(3, "submit");
            builder.CloseElement();
        };

        base.OnParametersSet();

        FormClass ??= "form w-50 mx-auto";

        EditContext.OnValidationRequested += (_, _) => (ValidationCssClassProvider as CustomFieldCssClassProvider)!.ValidationRequested = true;
    }
}