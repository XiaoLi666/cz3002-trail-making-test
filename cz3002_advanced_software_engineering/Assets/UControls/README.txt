In version 1.0, the flexible ListView is implemented.

Listview supports custom header, drag reordering, selecting, child value path binding(max binding depth of 2) in MVVM(Model-ViewModel) style.

You don't even have to create prefabs to use the default listview from code(generated). Of course you can create your own styled ListItem.

ObservableList, ObservableDicionary, ObservableQueue, ObservableStack are included! UIControl totally supports those IObservale stuff and INotifypropertyChanged members.(That means when you modified the bound datasource, the listview will refresh itself automatically).

The four demo included in this package covered most features of the UControl ListView. Follow them as a guide-through.

Other controls are coming soon. Please leave your feedback.

Visit official website for suppport: http://www.ultralpha.com