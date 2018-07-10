using Livet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StudyWpf.Models
{
    public class NotificationObjectExtend : NotificationObject
    {
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value)) { return; }
            field = value;
            RaisePropertyChanged(propertyName);
        }
    }
}
