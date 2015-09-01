using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Messenger;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace AddingMore
{
    public class MessagePage : ContentPage
    {
        ObservableCollection<Message> messageList = new ObservableCollection<Message>();
        ListView listView;

        public MessagePage()
        {
            this.ToolbarItems.Add(new ToolbarItem(){ Text = "Add", Command = new Command(o => AddAnotherItem()) });
            FillMessageList();
            //messageList = messageList.OrderBy(t => t.datestamp.Year).ThenBy(t => t.datestamp.Month).ThenBy(t => t.datestamp.Day).ThenBy(t => t.datestamp.Hour).ThenBy(t => t.datestamp.Minute).ThenBy(t => t.datestamp.Second);
            if (Device.OS == TargetPlatform.iOS)
                this.Padding = new Thickness(0, 20, 0, 0);
            if (Device.OS != TargetPlatform.iOS)
                this.BackgroundColor = Color.White;

            listView = new ListView()
            {
                ItemsSource = messageList,
                ItemTemplate = new DataTemplate(typeof(Messages)),
                HasUnevenRows = true,
            };

            listView.ScrollTo(messageList.Count - 1, ScrollToPosition.End, true);

            Content = listView;
        }

        void AddAnotherItem()
        {
            var moreLyrics = new List<string>
            {
                "Hey, hey we're the Monkees",
                "Power from the needle to the plastic, AM FM I feel so extatic,",
                "Peu dormi, vidé, et brimé - J'ai du dormir dans la gouttière - j'ai eu un flash - Hou! Hou! Hou! Hou! - En quatre couleurs",
                "What'sa matta you, hey - Gotta no respect, whatta you think you do - Why you looka so sad? - It's-a not so bad, it's-a nice-a place - Ah, shaddap you face",
                "Amadeus, Amadeus, Oh Oh Oh, Amadeus",
                "Don't dream it, Be it",
                "Taumatawhakatangihangakoayauo-Tamateaturipukakapikimaungahoro-Nukypokaiwhenuakitanatahu",
                "Always look on the brightside of life",
                "Tie my kangaroo down sport",
                "Waltzing Matilda, Waltzing Matilda, You'll come a Waltzing Matilda with me",
                "Who are you?",
                "You're the one that I want, one that I want",
                "Hello shoes, I'm sorry I'm going to have to stand on you again.... euw!",
                "Chalkdust!"
            };
            var rand = new Random();

            messageList.Add(new Message
                {
                    id = Utils.NewID,
                    parent_id = Utils.NewID,
                    message = moreLyrics[rand.Next(0, moreLyrics.Count)],
                    datestamp = DateTime.Now.AddDays((double)rand.Next(0, 10)),
                    is_reply = rand.Next(0, 10) >= 5 ? true : false,
                    has_attachments = rand.Next(0, 10) >= 5 ? true : false,
                    attachment_id = Utils.NewID // just fill it for now
                });

            listView.ScrollTo(messageList.Count - 1, ScrollToPosition.End, true);
        }

        void FillMessageList()
        {
            var lyrics = new List<string>
            {
    
                "Ding dong, the witch is dead",
                "When you walk through a storm",
                "I love rock and roll",
                "D'oh!",
                "People say on the day of victory, no fatigue is felt " +
                "Garbo, it's you that has the power that makes ev'ry man's heart melt " +
                "They say that, when the heart is a fire sparks fly out of the cage " +
                "But beauty is like a good wine, the taste is sweeter with age",
                "No man can guess in cold blood what he might do in passion " +
                "But the things that he deplores today are tomorrow's latest fashion " +
                "Serving one's own passion is the greatest slavery " +
                "But if in wanting you I become your slave, I intend no bravery",
                "Remember you're a womble"
            };
            var rand = new Random();
            foreach (var l in lyrics)
            {
                messageList.Add(new Message
                    {
                        id = Utils.NewID,
                        parent_id = Utils.NewID,
                        message = l,
                        datestamp = DateTime.Now.AddDays((double)rand.Next(0, 10)),
                        is_reply = rand.Next(0, 10) >= 5 ? true : false,
                        has_attachments = rand.Next(0, 10) >= 5 ? true : false,
                        attachment_id = Utils.NewID // just fill it for now
                    });
            }
        }
    }

    public class Messages : ViewCell
    {
        public Messages()
        {
            var label = new Label()
            {
                Text = "lyric",
                Font = Font.SystemFontOfSize(NamedSize.Default),
                TextColor = Color.Blue
            };
            label.SetBinding(Label.TextProperty, new Binding("message"));
            label.SetBinding(Label.TextColorProperty, new Binding("is_reply", converter: new BoolToColor()));
            label.SetBinding(Label.HorizontalOptionsProperty, new Binding("is_reply", converter: new BoolToHPos()));

            var dateLabel = new Label()
            {
                Text = "date",
                Font = Font.SystemFontOfSize(NamedSize.Small),
                TextColor = Color.Black
            };
            dateLabel.SetBinding(Label.TextProperty, new Binding("datestamp"));
            dateLabel.SetBinding(Label.HorizontalOptionsProperty, new Binding("is_reply", converter: new BoolToHPos()));

            label.BindingContextChanged += (object sender, EventArgs e) =>
            {
                var m = (Message)BindingContext;
                App.Self.IsReply = m.is_reply;
            };

            View = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(12, 8),
                Children = { label, dateLabel }
            };
        }
    }

    public static class Utils
    {
        public static string NewID
        {
            get
            {
                return new Guid().ToString();
            }
        }
    }

    public class BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? (Color)Color.Blue : (Color)Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var col = (Color)value;
            return col != Color.Red;
        }
    }

    public class BoolToHPos : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = (bool)value;
            return res ? (LayoutOptions)LayoutOptions.End : (LayoutOptions)LayoutOptions.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var col = (LayoutOptions)value;
            var res = col.Equals(LayoutOptions.Start);
            return (bool)res;
        }
    }
}


