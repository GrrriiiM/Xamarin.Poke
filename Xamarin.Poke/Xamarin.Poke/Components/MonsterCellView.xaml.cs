using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Poke.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonsterCellView : ContentView
	{
		public MonsterCellView ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}