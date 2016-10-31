using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SocialNetwork.UI
{
    public class DocumentBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty = 
            DependencyProperty.Register("Document", typeof(FlowDocument), typeof(DocumentBox));

        private bool changedIndicator;

        public new FlowDocument Document
        {
            get
            {
                if ((FlowDocument)GetValue(DocumentProperty) == null)
                {
                    return new FlowDocument();
                }

                return (FlowDocument)GetValue(DocumentProperty);
            }

            set
            {
                SetValue(DocumentProperty, value);
            }
        }
        
        public bool ResetChangedIndicator()
        {
            bool retValue = changedIndicator;
            changedIndicator = false;
            return retValue;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (e.Changes.Count > 0)
            {
                changedIndicator = true;
            }

            base.OnTextChanged(e);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == DocumentProperty)
            {
                base.Document = Document;
            }

            base.OnPropertyChanged(e);
        }
    }
}