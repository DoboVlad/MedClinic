using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mobile.Resources
{
    // this class will hold apps animations
    class Animations
    {
        // this method is going to give buttons a pop-up alike animation when clicked
       async public static void Button_Scale_Clicked(Button button)
        {
            await button.ScaleTo(1.03, 100, null);
            await button.ScaleTo(1, 100, null);
        }
    }
}
