using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassroomManager.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : AppPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        public override void ShowLoader()
        {
            base.ShowLoader();
        }
    }
}