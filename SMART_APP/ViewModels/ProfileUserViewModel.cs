using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_APP.ViewModels
{
    public partial class ProfileUserViewModel : ObservableObject
    {
        [ObservableProperty]
        private string emailAddress;

        [ObservableProperty]
        private string fullName;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string phoneNumber;
    }
}
