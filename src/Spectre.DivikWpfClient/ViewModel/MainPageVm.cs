using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Mvvm.Base;

namespace Spectre.DivikWpfClient.ViewModel
{
    /// <summary>
    /// Serves as VM for main page.
    /// </summary>
    /// <seealso cref="Spectre.Mvvm.Base.PropertyChangedNotification" />
    public class MainPageVm: PropertyChangedNotification
    {
        public string InputPath
        {
            get { return GetValue(() => InputPath); }
            set { SetValue(() => InputPath, value); }
        }
    }
}
