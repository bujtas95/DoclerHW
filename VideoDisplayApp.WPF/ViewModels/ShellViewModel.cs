using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoDisplayApp.WPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private VideoDisplayViewModel _videoDisplayVM;

        public ShellViewModel(VideoDisplayViewModel videoDisplayVM)
        {
            _videoDisplayVM = videoDisplayVM;
            ActivateItem(_videoDisplayVM);
        }
    }
}
