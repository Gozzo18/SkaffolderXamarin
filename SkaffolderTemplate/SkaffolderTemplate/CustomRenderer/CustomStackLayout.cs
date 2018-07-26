using SkaffolderTemplate.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SkaffolderTemplate.CustomRenderer
{
    public class CustomStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items), typeof(ObservableCollection<View>), typeof(CustomStackLayout), null,
                propertyChanged: (b, o, n) =>
                {
                    (n as ObservableCollection<View>).CollectionChanged += (coll, arg) =>
                    {
                        switch (arg.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                foreach (var v in arg.NewItems)
                                    (b as CustomStackLayout).Children.Add((View)v);
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                foreach (var v in arg.NewItems)
                                    (b as CustomStackLayout).Children.Remove((View)v);
                                break;
                        }
                    };
                });


        public ObservableCollection<View> Items
        {
            get { return (ObservableCollection<View>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
    }
}
