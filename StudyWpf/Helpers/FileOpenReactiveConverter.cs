using Reactive.Bindings.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
//using Windows.Storage.Pickers;

namespace StudyWpf.Helpers
{
    public class FileOpenReactiveConverter : ReactiveConverter<RoutedEventArgs, string>
    {
        protected override IObservable<string> OnConvert(IObservable<RoutedEventArgs> source)
        {
            return null;
            // !! UWP でしか使えないのか・・
            //return source.SelectMany(async _ =>
            //{
            //    var picker = new FileOpenPicker();
            //    picker.FileTypeFilter.Add(".snippet");
            //    var f = await picker.PickSingleFileAsync();
            //    return f?.Path;
            //})
            //.Where(x => x != null);

        }
    }
}
