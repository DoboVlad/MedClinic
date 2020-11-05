using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Helpers
{
    //to add images, add the image in the images folder and set the action, in properties, to embedded image

    [ContentProperty(nameof(Source))] //to make sure the content si a source type

    public class ImageResourceExtension : IMarkupExtension
    {   // IMarkupExtension is needed  
        public string Source
        {
            get; set;
        }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            var imageResource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            return imageResource;
        }
    }
}
